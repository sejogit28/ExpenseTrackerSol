using ExpenseTrackerModels;
using ExpenseTrackerModels.AuthModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public interface IUserProfileRepository
    {
        Task DeleteSingleUser(string deletedUserId);
        Task<IList<ExpenseTrackerUser>> GetAllUsers();
        Task<ExpenseTrackerUser> GetSingleUser(string singleUserId);
        Task UpdateUserPassword(ChangePassword changePassword, string updatedUserId);
    }
}