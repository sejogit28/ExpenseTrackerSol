using ExpenseTrackerModels;
using ExpenseTrackerModels.GroupModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpTrackerAppApplicationLogic
{
    public interface IGroupsScreenUseCases
    {
        Task AddUserToGroup(AddNewMemberToGroup addNew);
        Task CreateNewGroup(Groups newgroup, string groupCreatorName);
        Task DeleteSingleGroup(int deletedGroupId);
        Task GroupInviteeConfirm(PossibleMemberConfirm possibleMember);
        Task GroupInviterConfirm(PossibleMemberConfirm currentMember);
        Task RemoveMemberFromGroup(int shrinkingGroupId, string removedMember);
        Task SendInitialEmail(PossibleMemberInvite memberInvite);
        Task UpdateGroupName(Groups updatedGroup);
        Task<IList<string>> ViewAllGroupMemberNames(int currentGroupId);
        Task<IList<Groups>> ViewAllGroups();
        Task<Groups> ViewSingleGroup(int singleGroupId);
    }
}