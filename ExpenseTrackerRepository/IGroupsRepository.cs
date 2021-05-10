using ExpenseTrackerModels;
using ExpenseTrackerModels.GroupModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public interface IGroupsRepository
    {
        Task addGroupMember(AddNewMemberToGroup addNew);
        Task createGroup(Groups newGroup, string groupCreatorUserName);
        Task deleteGroup(int deletedGroupId);
        Task<IEnumerable<Groups>> listOfGroups();
        Task<Groups> getSingleGroup(int singleGroupId);
        Task inviteeConfirm(PossibleMemberConfirm possibleInvitee);
        Task inviterConfirm(PossibleMemberConfirm possibleInviter);
        Task<List<string>> listofGroupMemberNames(int currentGroupId);
        Task removeMemberFromGroup(int shrinkingGroupId, string userName);
        Task sendGroupInvite(PossibleMemberInvite initialInvite);
        Task updateGroup(Groups updatedGroup);
    }
}