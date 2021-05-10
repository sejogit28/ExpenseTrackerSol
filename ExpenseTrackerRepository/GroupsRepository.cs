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

        public async Task<IEnumerable<Groups>> getGroups()
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Groups>>("api/Groups/groupslist");
        }

        public async Task<Groups> getSingleGroup(int singleGroupId)
        {
            return await webApiExecuter.InvokeGet<Groups>($"api/Groups/{singleGroupId}");
        }

        public async Task createGroup(Groups newGroup, string groupCreatorUserName)
        {
            await webApiExecuter.InvokePost($"api/Groups/creategroup/{groupCreatorUserName}", newGroup);
        }

        public async Task deleteGroup(int deletedGroupId)
        {
            await webApiExecuter.InvokeDelete<Groups>($"api/Groups/deletesinglegroup/{deletedGroupId}");
        }

        public async Task updateGroup(Groups updatedGroup)
        {
            await webApiExecuter.InvokePut($"api/Groups/updategroup/{updatedGroup.GroupsId}", updatedGroup);
        }

        public async Task<List<string>> listofGroupMemberNames(int currentGroupId)
        {
            return await webApiExecuter.InvokeGet<List<string>>($"api/Groups/listofgroupmembernames/{currentGroupId}");
        }

        public async Task addGroupMember(AddNewMemberToGroup addNew)
        {
            await webApiExecuter.InvokePost("api/Groups/addnewmembertogroup", addNew);
        }

        public async Task sendGroupInvite(int groupId, string userName, string inviteeEmail)
        {
            await webApiExecuter.InvokeGet<GroupUsers>($"api/Groups/sendgroupinvitation/{groupId}/{userName}/{inviteeEmail}");
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
            await webApiExecuter.InvokeDelete<GroupUsers>($"api/Groups/deletememberfromgroup/{shrinkingGroupId}/{userName}");
        }
    }
}
