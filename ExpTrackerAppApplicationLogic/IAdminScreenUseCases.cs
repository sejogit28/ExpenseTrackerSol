using ExpenseTrackerModels;
using ExpenseTrackerModels.AuthModels;
using ExpenseTrackerModels.UserViewModel;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpTrackerAppApplicationLogic
{
    public interface IAdminScreenUseCases
    {
        Task DeleteSingleUser(string deletedUserId);
        Task UpdateUserPassword(ChangePassword changePassword, string updatedUserId);
        Task<IList<UserProfile>> ViewAllUsers();
        Task<UserProfile> ViewSingleUser(string singleUserName);
    }
}