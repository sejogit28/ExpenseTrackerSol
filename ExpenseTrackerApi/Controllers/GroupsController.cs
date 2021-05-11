using DataStoreEF;
using EmailService;
using ExpenseTrackerModels;
using ExpenseTrackerModels.GroupModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly ExpenseTrackerDbContext _datExpBase;
        private readonly UserManager<ExpenseTrackerUser> _userManager;
        private readonly IEmailSender _emailSender;

        public GroupsController(ExpenseTrackerDbContext etDB, UserManager<ExpenseTrackerUser> userManager, IEmailSender emailSender)
        {
            _datExpBase = etDB;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet("groupslist")]
        public async Task<IActionResult> getGroups() 
        {
            var groupList = await _datExpBase.Groups.ToListAsync();
            return Ok(groupList);
        }

        [HttpGet("groupslistbyuser/{userName}")]
        public async Task<IActionResult> getGroupsByUser(string userName) 
        {
            var currentUser = await _userManager.FindByNameAsync(userName);
            if(currentUser == null) 
            {
                return NotFound($"The user with the name {userName} could not be found");
            }
            else 
            {
                var listOfUsersGroupsHopefully = await _datExpBase.GroupUsers
                    .Include(g => g.Groups).ThenInclude(gu => gu.GroupUsers)
                    .Where(u => u.ExpenseTrackerUserId == currentUser.Id).ToListAsync();
                return Ok(listOfUsersGroupsHopefully);
            }
        }

        [HttpGet("{singlegroupid:int}")]
        public async Task<IActionResult> getSingleGroup(int singlegroupid)
        {
            var singleGroup = await _datExpBase.Groups.FindAsync(singlegroupid);
            if (singleGroup == null)
            {
                return NotFound();
            }
            return Ok(singleGroup);
        }

        [HttpPost("creategroup/{userName}")]
        public async Task<IActionResult> createGroup([FromBody] Groups newgroup, string userName)
        {

            var groupCreator = await _userManager.FindByNameAsync(userName);
            if(groupCreator == null) 
            {
                return NotFound($"{userName} could not be found");
            }
            newgroup.ExpenseTrackerUserId = groupCreator.Id;
            await _datExpBase.Groups.AddAsync(newgroup);
             
            await _datExpBase.SaveChangesAsync();

            return Ok(newgroup);
        }

        [HttpPost("addnewmembertogroup")]
        public async Task<IActionResult> addGroupCreatorToNewGroup([FromBody] AddNewMemberToGroup addNew) 
        {
            var potentialMember = await _userManager.FindByNameAsync(addNew.NewMemberUserName);
            if (potentialMember == null)
            {
                return NotFound($"{addNew.NewMemberUserName} could not be found");
            }
            var newGroup = await _datExpBase.Groups.FindAsync(addNew.GroupId);
            //The Identity Users Id had to be put first for some reason....

            var groupCheck = await _datExpBase.GroupUsers.FindAsync(potentialMember.Id, addNew.GroupId);

            

            if (newGroup == null)
            {
                return NotFound($"Group could not be found");
            }

            
            else if (groupCheck == null)
            {
                var newGroupMember = new GroupUsers
                {
                    ExpenseTrackerUserId = potentialMember.Id,
                    GroupsGroupsId = addNew.GroupId
                };
                await _datExpBase.GroupUsers.AddAsync(newGroupMember);
                await _datExpBase.SaveChangesAsync();
                return Ok(newGroupMember);
            }
                
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
           
        }

        [HttpPut("updategroup/{updatedgroupId:int}")]
        public async Task<IActionResult> updateGroup(int updatedGroupId, [FromBody] Groups updatedGroup)
        {

            if (updatedGroupId != updatedGroup.GroupsId) return BadRequest();
            _datExpBase.Entry(updatedGroup).State = EntityState.Modified;
            try
            {
                await _datExpBase.SaveChangesAsync();
                return Ok(updatedGroup);
            }

            catch
            {
                if (await _datExpBase.Groups.FindAsync(updatedGroupId) == null)
                {
                    return NotFound();
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("deletesinglegroup/{groupId:int}")]
        public async Task<IActionResult> deleteSingleGroup(int groupId) 
        {
            var deletableGroup = await _datExpBase.Groups.FindAsync(groupId);
            if (deletableGroup == null)
            {
                return NotFound();
            }

            _datExpBase.Groups.Remove(deletableGroup);
            await _datExpBase.SaveChangesAsync();
            return NoContent();
         }

        [HttpPost("sendgroupinvitation/initialInviteEmail")]
        public async Task<IActionResult> sendGroupInvite([FromBody] PossibleMemberInvite invite) 
        {
            var groupMember = await _userManager.FindByNameAsync(invite.InviterEmail);
            var invitedUser =  await _userManager.FindByNameAsync(invite.InviteeEmail);
            var groupMemberId = groupMember.Id;
            if (invitedUser == null) 
            {
                return NotFound($"{invite.InviteeEmail} could not be found. If {invite.InviteeEmail} has already" +
                    $" created an account, please check spelling and try again. If they have not created " +
                    $"an account, have them register first");
            }
            else 
            {
                var message = new EmailMessage(new string[] {$"{invite.InviteeEmail}"}, 
                    "Invited to join a group for expenseTrack.com", $"Hello!! You've been invited to join" +
                    $" a group by {groupMember}. Please follow the link to be added in:" +
                    $" https://localhost:44382/inviteeconfirm/{invite.GroupId}/{groupMemberId}");

                   _emailSender.SendEmail(message);
                    return Ok(message);
            }
            
        }
        
        [HttpPost("sendgroupinvitation/inviteeconfirm")]
        public async Task<IActionResult> inviteeConfirm([FromBody] PossibleMemberConfirm possible) 
        {
            var currentGroup = await _datExpBase.Groups.FindAsync(possible.GroupId);
            if (currentGroup == null)
                return NotFound("This group could not be found");

            var invitedMember = await _userManager.FindByNameAsync(possible.InviteeEmail);
            if (invitedMember == null)
                return NotFound("This user could not be found");

            if (!await _userManager.CheckPasswordAsync(invitedMember, possible.Password))
                return Unauthorized(new LoginResponseDto { ErrMessage = "Invalid Authentication" });
            else 
            {
                var message = new EmailMessage(new string[] { $"{possible.InviterEmail}" },
                    "Invitation Accepted!", $"Hello!!, you're receiving this email because the invitation" +
                    $" you sent to {possible.InviteeEmail} was accepted! In order to confirm their " +
                    $"addition to the group, click the following link: https://localhost:44382/inviterconfirm/{possible.GroupId}/{possible.InviteeEmail} " +
                    $"If you didn't send an invitation to {possible.InviteeEmail}, you can disregard " +
                    $"this email.");

                _emailSender.SendEmail(message);
                return Ok(possible);
            }
        }

        [HttpPost("sendgroupinvitation/inviterconfirm")]
        public async Task<IActionResult> inviterConfirm([FromBody] PossibleMemberConfirm possible)
        {
            var invitingMember = await _userManager.FindByNameAsync(possible.InviterEmail);
            var invitedMember = await _userManager.FindByNameAsync(possible.InviteeEmail);
            var currentGroup = await _datExpBase.Groups.FindAsync(possible.GroupId);
            var currentGroupCheck = await _datExpBase.GroupUsers.FindAsync(invitedMember.Id, possible.GroupId);

            if (invitingMember == null)
            {
                return NotFound($"This member: {possible.InviterEmail} could not be found");
            }
                
            else if (!await _userManager.CheckPasswordAsync(invitingMember, possible.Password))
            {
                return Unauthorized(new LoginResponseDto { ErrMessage = "Invalid Authentication" });
            }
                        
            else if (invitedMember == null)
            {
                return NotFound($"{invitedMember.Email} could not be found");
            }
                          
            else if (currentGroup == null)
            {
                return NotFound("This group could not be found");
            }

            else if(currentGroupCheck != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
                
            else
            {
                var newMemberAddition = new GroupUsers
                {
                    ExpenseTrackerUserId = invitedMember.Id,
                    GroupsGroupsId = possible.GroupId

                };

                await _datExpBase.GroupUsers.AddAsync(newMemberAddition);
                await _datExpBase.SaveChangesAsync();
                return Ok(newMemberAddition);
            }
        }

        [HttpGet("listofgroupmembernames/{groupId:int}")]
        public async Task<IActionResult> listOfGroupMemberNames(int groupId)
        {
            IList<string> GroupUserNames = new List<string>();

            var listofGroupUsers = await _datExpBase.GroupUsers
                .Where(g => g.GroupsGroupsId == groupId).ToListAsync();
            foreach (var user in listofGroupUsers)
            {
                var userObj = await _datExpBase.ExpenseTrackerUser.FindAsync(user.ExpenseTrackerUserId);
                GroupUserNames.Add(userObj.UserName);
            }
            /*It doesn't appear that you can return a linq query without getting a 500 error about
             * Json Serializer Cycles(would get this error if you returned the listofGroupUsers)*/
            return Ok(GroupUserNames);
        }

        [HttpDelete("deletememberfromgroup/{groupId:int}/{deletedMemberName}")]
        public async Task<IActionResult> deleteGroupMember(int groupId, string deletedMemberName) 
        {
            var deletedMember = await _userManager.FindByNameAsync(deletedMemberName);
            if (deletedMember == null)
                return NotFound($"{deletedMemberName} could not be found.");

            var currentGroup = await _datExpBase.Groups.FindAsync(groupId);
            var deletedMembership = await _datExpBase.GroupUsers.FindAsync(deletedMember.Id, groupId);
            if (deletedMembership == null)
                return NotFound($"{deletedMemberName} is not currently a member of {currentGroup.GroupName}.");
            else 
            {
                _datExpBase.GroupUsers.Remove(deletedMembership);
                await _datExpBase.SaveChangesAsync();
                return Ok();
            }
            
        }

      
    }
}
