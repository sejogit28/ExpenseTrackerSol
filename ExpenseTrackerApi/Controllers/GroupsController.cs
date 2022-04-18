using DataStoreEF;
using EmailService;
using ExpenseTrackerModels;
using ExpenseTrackerModels.GroupModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly ExpenseTrackerDbContext _datExpBase;
        private readonly UserManager<ExpenseTrackerUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly string localClient = "https://localhost:44382";
        private readonly string remoteClient = "https://loving-turing-d3cc22.netlify.app";

        public GroupsController(ExpenseTrackerDbContext etDB, UserManager<ExpenseTrackerUser> userManager, IEmailSender emailSender)
        {
            _datExpBase = etDB;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet("groupslist")]
        public async Task<IActionResult> GetGroups()
        {
            var groupList = await _datExpBase.Groups.ToListAsync();

            return Ok(groupList);
        }

        [HttpGet("groupslistbyuser/{userName}")]
        public async Task<IActionResult> GetGroupsByUser(string userName)
        {
            var currentUser = await _userManager.FindByNameAsync(userName);

            if (currentUser == null)
            {
                return NotFound($"The user with the name {userName} could not be found");
            }
            else
            {
                var listOfUserGroups = await _datExpBase.GroupUsers
                    .Include(e => e.ExpenseTrackerUser).ThenInclude(gu => gu.GroupUsers).ThenInclude(g => g.Groups)
                    .Where(u => u.ExpenseTrackerUserId == currentUser.Id).ToListAsync();

                return Ok(listOfUserGroups);
            }
        }

        [HttpGet("{singlegroupid:int}")]
        public async Task<IActionResult> GetSingleGroup(int singleGroupId)
        {
            var singleGroup = await _datExpBase.Groups.FindAsync(singleGroupId);

            if (singleGroup == null)
            {
                return NotFound();
            }

            return Ok(singleGroup);
        }

        [HttpPost("creategroup/{userName}")]
        public async Task<IActionResult> CreateGroup([FromBody] Groups newgroup, string userName)
        {
            var groupCreator = await _userManager.FindByNameAsync(userName);

            if (groupCreator == null)
            {
                return NotFound($"{userName} could not be found");
            }
            newgroup.ExpenseTrackerUserId = groupCreator.Id;
            await _datExpBase.Groups.AddAsync(newgroup);
            await _datExpBase.SaveChangesAsync();

            return Ok(newgroup);
        }

        [HttpPost("addnewmembertogroup")]
        public async Task<IActionResult> AddGroupCreatorToNewGroup([FromBody] AddNewMemberToGroup addNew)
        {
            var potentialMember = await _userManager.FindByNameAsync(addNew.NewMemberUserName);

            if (potentialMember == null)
            {
                return NotFound($"{addNew.NewMemberUserName} could not be found");
            }
            var newGroup = await _datExpBase.Groups.FindAsync(addNew.GroupId);
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
                return BadRequest($"Something went wrong.....");
            }
        }

        [HttpPut("updategroup/{updatedgroupId:int}")]
        public async Task<IActionResult> UpdateGroup(int updatedGroupId, [FromBody] Groups updatedGroup)
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
        public async Task<IActionResult> DeleteSingleGroup(int groupId)
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
        public async Task<IActionResult> SendGroupInvite([FromBody] PossibleMemberInvite invite)
        {
            var groupMember = await _userManager.FindByNameAsync(invite.InviterEmail);
            var invitedUser = await _userManager.FindByNameAsync(invite.InviteeEmail);

            if (invitedUser == null)
            {

                return NotFound(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"A user with the email: {invite.InviteeEmail} could not be found. If this user" +
                    $" has already created an account, please check spelling and try again. If they have not created " +
                    $"an account, have them register first"
                });
            }
            else
            {
                var message = new EmailMessage(new string[] { $"{invite.InviteeEmail}" },
                    "Invited to join a group for expenseTrack.com", $"Hello!! You've been invited to join" +
                    $" a group by {groupMember}. Please follow the link to be added in:" +
                    $" {localClient}/{invite.InviteeEmail}/{invite.GroupId}");

                _emailSender.SendEmail(message);

                return Ok(new OperationResponse
                {
                    OperationSuccessful = true,
                    OperationMessage = $"Success!! An email has been sent to {invite.InviteeEmail}."
                });
            }

        }

        [HttpPost("sendgroupinvitation/inviteeconfirm")]
        public async Task<IActionResult> InviteeConfirm([FromBody] PossibleMemberConfirm possible)
        {
            var currentGroup = await _datExpBase.Groups.FindAsync(possible.GroupId);
            var invitedMember = await _userManager.FindByNameAsync(possible.InviteeEmail);
            var invitingMember = await _userManager.FindByNameAsync(possible.InviterEmail);
            var emailSenderGroupCheck = await _datExpBase.GroupUsers.FindAsync(invitingMember.Id, possible.GroupId);

            if (currentGroup == null)
            {

                return NotFound(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"The group with the Id: {possible.GroupId} could not be found"
                });
            }
            else if (invitedMember == null)
            {

                return NotFound(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"The user with the name: {possible.InviteeEmail} could not be found"
                });
            }
            else if (!await _userManager.CheckPasswordAsync(invitedMember, possible.Password))
            {

                return Unauthorized(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"The user with the name: {possible.InviteeEmail} did not enter " +
                    $"the right password"
                });
            }
            else if (emailSenderGroupCheck == null)
            {

                return Unauthorized(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"{possible.InviterEmail} could not be found in the group" +
                    $" {currentGroup.GroupName}, and therefore, could not have sent this email"
                });
            }
            else
            {
                var message = new EmailMessage(new string[] { $"{possible.InviterEmail}" },
                    "Invitation Accepted!", $"Hello!!, you're receiving this email because the invitation" +
                    $" you sent to {possible.InviteeEmail} was accepted! In order to confirm their " +
                    $"addition to the group, click the following link: {localClient}/{possible.InviteeEmail}/{possible.GroupId}/ " +
                    $"If you didn't send an invitation to {possible.InviteeEmail}, you can disregard " +
                    $"this email.");

                _emailSender.SendEmail(message);

                return Ok(new OperationResponse
                {
                    OperationSuccessful = true,
                    OperationMessage = $"Success!! Your invite to the group: {currentGroup.GroupName} has been" +
                    $" confirmed, once {possible.InviterEmail} confirms on their end, you will join the group!"
                });
            }
        }

        [HttpPost("sendgroupinvitation/inviterconfirm")]
        public async Task<IActionResult> InviterConfirm([FromBody] PossibleMemberConfirm possible)
        {
            var invitingMember = await _userManager.FindByNameAsync(possible.InviterEmail);
            var invitedMember = await _userManager.FindByNameAsync(possible.InviteeEmail);
            var currentGroup = await _datExpBase.Groups.FindAsync(possible.GroupId);
            var currentGroupCheck = await _datExpBase.GroupUsers.FindAsync(invitedMember.Id, possible.GroupId);

            if (invitingMember == null)
            {

                return NotFound(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"This member: {possible.InviterEmail} could not be found"
                });
            }

            else if (!await _userManager.CheckPasswordAsync(invitingMember, possible.Password))
            {

                return Unauthorized(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = "Wrong password entered"
                });
            }

            else if (invitedMember == null)
            {

                return NotFound(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"{invitedMember.Email} could not be found"
                });
            }

            else if (currentGroup == null)
            {

                return NotFound(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = "The specified group could not be found"
                });
            }

            else if (currentGroupCheck != null)
            {

                return BadRequest(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"The user {possible.InviterEmail} is already apart of the " +
                    $"group {currentGroup.GroupName} so this operation was cancelled"
                });
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

                return Ok(new OperationResponse
                {
                    OperationSuccessful = true,
                    OperationMessage = $"Success!! {possible.InviteeEmail} has been added to the " +
                    $"group {currentGroup.GroupName}"
                });
            }
        }

        [HttpGet("listofgroupmembernames/{groupId:int}")]
        public async Task<IActionResult> ListOfGroupMemberNames(int groupId)
        {
            var currentGroup = await _datExpBase.Groups.FindAsync(groupId);

            if (currentGroup == null)
            {

                return NotFound(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"The specified group with the Id: {groupId} could not be found"
                });
            }
            else
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
                 * Json Serializer Cycles?(would get this error if you returned the listofGroupUsers)*/

                return Ok(GroupUserNames);
            }

        }

        [HttpDelete("deletememberfromgroup/{groupId:int}/{deletedMemberName}")]
        public async Task<IActionResult> DeleteGroupMember(int groupId, string deletedMemberName)
        {
            var deletedMember = await _userManager.FindByNameAsync(deletedMemberName);

            if (deletedMember == null)
            {

                return NotFound(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"{deletedMemberName} could not be found."
                });

            }

            var currentGroup = await _datExpBase.Groups.FindAsync(groupId);
            var deletedMembership = await _datExpBase.GroupUsers.FindAsync(deletedMember.Id, groupId);

            if (deletedMembership == null)
            {

                return NotFound(new OperationResponse
                {
                    OperationSuccessful = false,
                    OperationMessage = $"{deletedMemberName} is not currently a member of {currentGroup.GroupName}."
                });
            }
            else
            {
                _datExpBase.GroupUsers.Remove(deletedMembership);
                await _datExpBase.SaveChangesAsync();
                var groupMemberCheck = await _datExpBase.GroupUsers
                .Where(g => g.GroupsGroupsId == groupId).ToListAsync();

                if (groupMemberCheck.Count == 0)
                {
                    _datExpBase.Groups.Remove(currentGroup);
                    await _datExpBase.SaveChangesAsync();

                    return Ok(new OperationResponse
                    {
                        OperationSuccessful = true,
                        OperationMessage = $"{deletedMemberName} has been removed from {currentGroup.GroupName}. " +
                        $"This meant that {currentGroup.GroupName} had no members and was deleted"
                    });
                }
                else
                {
                    await _datExpBase.SaveChangesAsync();

                    return Ok(new OperationResponse
                    {
                        OperationSuccessful = true,
                        OperationMessage = $"{deletedMemberName} has been removed from {currentGroup.GroupName}"
                    });
                }
            }
        }
    }
}
