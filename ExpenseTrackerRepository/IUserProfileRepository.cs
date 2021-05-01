using ExpenseTrackerModels.AuthModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public interface IUserProfileRepository
    {
        Task DeleteSingleUser(string deletedUserId);
        Task<IList<IdentityUser>> GetAllUsers();
        Task<IdentityUser> GetSingleUser(string singleUserId);
        Task UpdateUserPassword(ChangePassword changePassword, string updatedUserId);
    }
}