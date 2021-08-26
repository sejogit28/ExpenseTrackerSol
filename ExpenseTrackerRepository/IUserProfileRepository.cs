using ExpenseTrackerModels;
using ExpenseTrackerModels.AuthModels;
using ExpenseTrackerModels.UserViewModel;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public interface IUserProfileRepository
    {
        Task DeleteSingleUser(string deletedUserId);
        Task<IList<UserProfile>> GetAllUsers();
        Task<UserProfile> GetSingleUser(string singleUserName);
        Task UpdateUserPassword(ChangePassword changePassword, string updatedUserId);
    }
}