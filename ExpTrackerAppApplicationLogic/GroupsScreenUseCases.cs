using ExpenseTrackerModels;
using ExpenseTrackerModels.GroupModels;
using ExpenseTrackerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTrackerAppApplicationLogic
{
    public class GroupsScreenUseCases : IGroupsScreenUseCases
    {
        private readonly IGroupsRepository _groupRepository;
        public GroupsScreenUseCases(IGroupsRepository groupsRepository)
        {
            _groupRepository = groupsRepository;
        }



        public async Task<IList<Groups>> ViewAllGroups()
        {
            var groupEnumerable = await _groupRepository.listOfGroups();
            IList<Groups> groupList = (IList<Groups>)groupEnumerable;
            return groupList;
        }

        public async Task<Groups> ViewSingleGroup(int singleGroupId)
        {
            return await _groupRepository.getSingleGroup(singleGroupId);
        }

        public async Task CreateNewGroup(Groups newgroup, string groupCreatorName)
        {
            await _groupRepository.createGroup(newgroup, groupCreatorName);
        }

        public async Task DeleteSingleGroup(int deletedGroupId)
        {
            await _groupRepository.deleteGroup(deletedGroupId);
        }

        public async Task UpdateGroupName(Groups updatedGroup)
        {
            await _groupRepository.updateGroup(updatedGroup);
        }

        public async Task<IList<string>> ViewAllGroupMemberNames(int currentGroupId)
        {
            return await _groupRepository.listofGroupMemberNames(currentGroupId);
        }

        public async Task AddUserToGroup(AddNewMemberToGroup addNew)
        {
            await _groupRepository.addGroupMember(addNew);
        }

        public async Task SendInitialEmail(PossibleMemberInvite memberInvite)
        {
            await _groupRepository.sendGroupInvite(memberInvite);
        }

        public async Task GroupInviteeConfirm(PossibleMemberConfirm possibleMember)
        {
            await _groupRepository.inviteeConfirm(possibleMember);
        }

        public async Task GroupInviterConfirm(PossibleMemberConfirm currentMember)
        {
            await _groupRepository.inviterConfirm(currentMember);
        }

        public async Task RemoveMemberFromGroup(int shrinkingGroupId, string removedMember)
        {
            await _groupRepository.removeMemberFromGroup(shrinkingGroupId, removedMember);
        }
    }
}
