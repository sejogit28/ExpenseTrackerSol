using DataStoreEF;
using EmailService;
using ExpenseTrackerModels;
using ExpenseTrackerModels.AuthModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/accounts")]//This is called "attribute routing" and can be done on the Controller or the Endpoint Name

    /*If the full route(api/accounts) isn't placed here(at the top of the controller) than the full route must 
     be placed on every single endpoint individually, instead of just putting the last name of the endpoint right
     next to the action verb
     Ex: [Route("api/accounts/Registration")] 
     If there is a full route on the controller and you put one on an endpoint than the endpoint route takes priority
     only IF(!!!!!) you start the endpoint route with a slash. Not starting it with a slash just adds whatever you typed
     to the Route specified in the Controller....which would be an error*/

    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfigurationSection _jwtSettings;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly ExpenseTrackerDbContext _datExpBase;
        public AccountsController(UserManager<IdentityUser> userManager, IConfiguration configuration, IEmailSender emailSender, ExpenseTrackerDbContext etDB)
        {
            _datExpBase = etDB;
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _emailSender = emailSender;
        }

        [HttpGet("allUsers")]
        public IActionResult GetAllIdentityUsers() 
        {
            var usersQueryList =  _userManager.Users;
            var usersList = usersQueryList.ToList();
            return Ok(usersList);
        }

        [HttpGet("getSingleUser/{singleUserId}")]
        public async Task<IActionResult> GetIdentityUser(string singleUserId)
        {
            var singleUser = await _userManager.FindByIdAsync(singleUserId);
            if (singleUser == null) 
            {
                return NotFound();
            }
            else 
            {
               return Ok(singleUser);
            }
         
        }

        [HttpPut("updateUserPassword/{userName}")]
        public async Task<IActionResult> EditIdentityUserPassword([FromBody] ChangePassword changePassword, string userName)
        {
            var updatedUser = await _userManager.FindByNameAsync(userName);
            var passwordCheck = await _userManager.CheckPasswordAsync(updatedUser, changePassword.CurrentPassword);

            if (updatedUser == null)
            {
                return NotFound($"User with Identiy Username: \"{userName}\" was not found");
            }
            else if (passwordCheck != true) 
            {
                return BadRequest($"Value entered for \"current Password\" for user: {updatedUser.Email} did not match stored password ");
            }
            else 
            {
                //if(changePassword != updatedUser.)
                var result = await _userManager.ChangePasswordAsync(updatedUser, changePassword.CurrentPassword, changePassword.NewPassword);
                if (result.Succeeded) 
                {
                    return Ok($"The user {updatedUser.Email} has had their password updated");
                }
                else
                {
                    return BadRequest($"Something went wrong while trying to update the password for the user: {updatedUser.Email}");
                }
            }
        }

        [HttpDelete("deleteSingleUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId) 
        {
            var deletedUser = await _userManager.FindByIdAsync(userId);
            if(deletedUser == null) 
            {
                return NotFound($"User with Identiy Id: \"{userId}\" was not found");
            }
            else 
            {
                var deletedExpenses = _datExpBase.Expenses.Where(p => p.UserId == deletedUser.UserName);
                var deletedIncomes = _datExpBase.Incomes.Where(p => p.UserId == deletedUser.UserName);
                _datExpBase.Incomes.RemoveRange(deletedIncomes);
                _datExpBase.Expenses.RemoveRange(deletedExpenses);

                var result = await _userManager.DeleteAsync(deletedUser);
                if (result.Succeeded) 
                {
                    await _datExpBase.SaveChangesAsync();
                    return Ok(deletedUser);
                }
                else 
                {
                    return BadRequest($"Something went wrong while trying to delete the user: {deletedUser.Email}");
                }

            }
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterExpUser([FromBody] UserRegistrationDto userRegistration)
        {
            if (userRegistration == null || !ModelState.IsValid)
                return BadRequest();
            var user = new IdentityUser { UserName = userRegistration.Email, Email = userRegistration.Email };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            await _userManager.AddToRoleAsync(user, "User");

            return Ok($"{user.UserName} has been registered");//String interpolation is similar to Javascript
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)

            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim>claims)
        {
            var tokOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);

            return tokOptions;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthenticationDto loginAuthenticationDto)
        {
            

            var user = await _userManager.FindByNameAsync(loginAuthenticationDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginAuthenticationDto.Password))
                return Unauthorized(new LoginResponseDto { ErrMessage = "Invalid Authentication" });

            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


            return Ok(new LoginResponseDto { IsLoginSuccessful = true, Token = token });
        }
        [HttpGet("EmailTest")]
        public void EmailTest() 
        {
            var message = new EmailMessage(new string[] { "sejoTestEmail1828@mailinator.com" }, "Test Email", "This is the content for a test email!");
            _emailSender.SendEmail(message);
        }

        [HttpGet("ResetPassword")]
        public async Task ResetPassword([FromBody]ForgotPassword forgotPassword)
        {
            var user = await _userManager.FindByNameAsync(forgotPassword.Email);
            var userId = user.Id;

            if (user == null) 
            {
                Unauthorized(new LoginResponseDto { ErrMessage = "Invalid Authentication" });
            }
            else 
            {
                var message = new EmailMessage(new string[] { $"{forgotPassword.Email}" }, 
                    "Reset Password",
                    $"Hi!! If you're seeing this it's cause someone requested a password" +
                    $" change using your email. This user has the id of {userId}");
                _emailSender.SendEmail(message);

            }
        }


    }
}
