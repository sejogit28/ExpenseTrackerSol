using ExpenseTrackerModels;
using ExpenseTrackerModels.GroupModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public interface IGroupsRepository
    {
        Task addGroupMember(AddNewMemberToGroup addNew);
        Task<Groups> createGroup(Groups newGroup, string groupCreatorUserName);
        Task deleteGroup(int deletedGroupId);
        Task<IEnumerable<Groups>> listOfGroups();
        Task<Groups> getSingleGroup(int singleGroupId);
        Task<OperationResponse> inviteeConfirm(PossibleMemberConfirm possibleInvitee);
        Task<OperationResponse> inviterConfirm(PossibleMemberConfirm possibleInviter);
        Task<List<string>> listofGroupMemberNames(int currentGroupId);
        Task removeMemberFromGroup(int shrinkingGroupId, string userName);
        Task<OperationResponse> sendGroupInvite(PossibleMemberInvite initialInvite);
        Task updateGroup(Groups updatedGroup);
        Task<List<GroupUsers>> listOfGroupsByUser(string userName);
    }
}