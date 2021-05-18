using ExpenseTrackerModels;
using ExpenseTrackerModels.GroupModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpTrackerAppApplicationLogic
{
    public interface IGroupsScreenUseCases
    {
        Task AddUserToGroup(AddNewMemberToGroup addNew);
        Task<Groups> CreateNewGroup(Groups newgroup, string groupCreatorName);
        Task DeleteSingleGroup(int deletedGroupId);
        Task<OperationResponse> GroupInviteeConfirm(PossibleMemberConfirm possibleMember);
        Task<OperationResponse> GroupInviterConfirm(PossibleMemberConfirm currentMember);
        Task RemoveMemberFromGroup(int shrinkingGroupId, string removedMember);
        Task<OperationResponse> SendInitialInviteEmail(PossibleMemberInvite memberInvite);
        Task UpdateGroupName(Groups updatedGroup);
        Task<IList<string>> ViewAllGroupMemberNames(int currentGroupId);
        Task<IList<Groups>> ViewAllGroups();
        Task<List<GroupUsers>> ViewAllGroupsByUser(string userName);
        Task<Groups> ViewSingleGroup(int singleGroupId);
    }
}