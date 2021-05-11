using ExpenseTrackerModels;
using ExpenseTrackerModels.GroupModels;
using ExpenseTrackerRepository.ApiRouteFetcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        public GroupsRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Groups>> listOfGroups()
        {
            //TESTED
            return await webApiExecuter.InvokeGet<IEnumerable<Groups>>("api/Groups/groupslist");
        }

        public async Task<List<GroupUsers>> listOfGroupsByUser(string userName) 
        {
            //TESTED
            return await webApiExecuter.InvokeGet<List<GroupUsers>>($"api/Groups/groupslistbyuser/{userName}");
        }

        public async Task<Groups> getSingleGroup(int singleGroupId)
        {
            //TESTED
            return await webApiExecuter.InvokeGet<Groups>($"api/Groups/{singleGroupId}");
        }

        public async Task createGroup(Groups newGroup, string groupCreatorUserName)
        {
            //TESTED
            await webApiExecuter.InvokePost($"api/Groups/creategroup/{groupCreatorUserName}", newGroup);
        }

        public async Task deleteGroup(int deletedGroupId)
        {
            //TESTED
            await webApiExecuter.InvokeDelete<Groups>($"api/Groups/deletesinglegroup/{deletedGroupId}");
        }

        public async Task updateGroup(Groups updatedGroup)
        {
            //TESTED
            await webApiExecuter.InvokePut($"api/Groups/updategroup/{updatedGroup.GroupsId}", updatedGroup);
        }

        public async Task<List<string>> listofGroupMemberNames(int currentGroupId)
        {
            //TESTED

            return await webApiExecuter.InvokeGet<List<string>>($"api/Groups/listofgroupmembernames/{currentGroupId}");
        }

        public async Task addGroupMember(AddNewMemberToGroup addNew)
        {
            //TESTED
            await webApiExecuter.InvokePost("api/Groups/addnewmembertogroup", addNew);
        }

        public async Task sendGroupInvite(PossibleMemberInvite initialInvite)
        {
            await webApiExecuter.InvokePost($"api/Groups/sendgroupinvitation/initialInviteEmail", initialInvite);
        }

        public async Task inviteeConfirm(PossibleMemberConfirm possibleInvitee)
        {
            await webApiExecuter.InvokePost("api/Groups/sendgroupinvitation/inviteeconfirm", possibleInvitee);
        }

        public async Task inviterConfirm(PossibleMemberConfirm possibleInviter)
        {
            await webApiExecuter.InvokePost("api/Groups/sendgroupinvitation/inviterconfirm", possibleInviter);
        }

        public async Task removeMemberFromGroup(int shrinkingGroupId, string userName)
        {
            //TESTED
            await webApiExecuter.InvokeDelete<GroupUsers>($"api/Groups/deletememberfromgroup/{shrinkingGroupId}/{userName}");
        }
    }
}
