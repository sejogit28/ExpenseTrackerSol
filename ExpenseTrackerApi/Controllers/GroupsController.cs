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

        [HttpGet("addmembertogroup/{newGroupId:int}/{potentialMemberName}")]
        public async Task<IActionResult> addGroupCreatorToNewGroup(int newGroupId, string potentialMemberName) 
        {
            var potentialMember = await _userManager.FindByNameAsync(potentialMemberName);
            if(potentialMember == null) 
            {
                return NotFound($"{potentialMemberName} could not be found");
            }

            var newGroup = await _datExpBase.Groups.FindAsync(newGroupId);
            if (newGroup == null)
            {
                return NotFound($"Group could not be found");
            }

            //The Identity Users Id had to be put first for some reason....
            var groupCheck = await _datExpBase.GroupUsers.FindAsync(potentialMember.Id, newGroupId);
            if (groupCheck != null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            else
            {
                var newGroupMember = new GroupUsers
                {
                    ExpenseTrackerUserId = potentialMember.Id,
                    GroupsGroupsId = newGroupId
                };
                await _datExpBase.GroupUsers.AddAsync(newGroupMember);
                await _datExpBase.SaveChangesAsync();
                return Ok();
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

        [HttpGet("sendgroupinvitation/{groupId:int}/{groupMemberEmail}/{invitedUserEmail}")]
        public async Task<IActionResult> sendGroupInvite(int groupId, string groupMemberEmail, string invitedUserEmail) 
        {
            var groupMember = await _userManager.FindByNameAsync(groupMemberEmail);
            var invitedUser =  await _userManager.FindByNameAsync(invitedUserEmail);
            var groupMemberId = groupMember.Id;
            if (invitedUser == null) 
            {
                return NotFound($"{invitedUserEmail} could not be found. If {invitedUserEmail} has already" +
                    $" created an account, please check spelling and try again. If they have not created " +
                    $"an account, have them register first");
            }
            else 
            {
                var message = new EmailMessage(new string[] {$"{invitedUserEmail}"}, 
                    "Invited to join a group for expenseTrack.com", $"Hello!! You've been invited to join" +
                    $" a group by {groupMember}. Please follow the link to be added in:" +
                    $" https://localhost:44382/inviteeconfirm/{groupId}/{groupMemberId}");

                   _emailSender.SendEmail(message);
                    return Ok();
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
                return Ok();
            }
        }

        [HttpPost("sendgroupinvitation/inviterconfirm")]
        public async Task<IActionResult> inviterConfirm([FromBody] PossibleMemberConfirm possible)
        {
            var invitingMember = await _userManager.FindByNameAsync(possible.InviterEmail);
            if (invitingMember == null)
                return NotFound($"This member: {possible.InviterEmail} could not be found");
            if (!await _userManager.CheckPasswordAsync(invitingMember, possible.Password))
                return Unauthorized(new LoginResponseDto { ErrMessage = "Invalid Authentication" });

            var invitedMember = await _userManager.FindByNameAsync(possible.InviteeEmail);
            if (invitedMember == null)
                return NotFound($"{invitedMember.Email} could not be found");

            var currentGroup = await _datExpBase.Groups.FindAsync(possible.GroupId);
            if (currentGroup == null)
                return NotFound("This group could not be found");
            else
            {
                var newMemberAddition = new GroupUsers
                {
                    ExpenseTrackerUserId = invitedMember.Id,
                    GroupsGroupsId = possible.GroupId

                };

                await _datExpBase.GroupUsers.AddAsync(newMemberAddition);
                await _datExpBase.SaveChangesAsync();
                return Ok($"New Member: {possible.InviteeEmail} has been added to group: {currentGroup.GroupName}");
            }
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

        [HttpGet("listofgroupmembernames/{groupId:int}")]
        public async Task<IActionResult> listOfGroupMemberNames(int groupId) 
        {
            IList<string> GroupUserNames = new List<string>();

            var listofGroupUsers = await _datExpBase.GroupUsers
                .Where(g => g.GroupsGroupsId == groupId).ToListAsync();
            foreach(var user in listofGroupUsers) 
            {
                var userObj = await _datExpBase.ExpenseTrackerUser.FindAsync(user.ExpenseTrackerUserId);
                GroupUserNames.Add(userObj.UserName);
            }
            /*It doesn't appear that you can return a linq query without getting a 500 error about
             * Json Serializer Cycles(would get this error if you returned the listofGroupUsers)*/
            return Ok(GroupUserNames);
        }
    }
}
