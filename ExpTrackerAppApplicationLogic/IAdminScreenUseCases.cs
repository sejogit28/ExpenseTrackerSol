using ExpenseTrackerModels.AuthModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpTrackerAppApplicationLogic
{
    public interface IAdminScreenUseCases
    {
        Task DeleteSingleUser(string deletedUserId);
        Task UpdateUserPassword(ChangePassword changePassword, string updatedUserId);
        Task<IList<IdentityUser>> ViewAllUsers();
        Task<IdentityUser> ViewSingleUser(string singleUserId);
    }
}