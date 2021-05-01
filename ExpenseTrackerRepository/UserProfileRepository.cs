using ExpenseTrackerModels.AuthModels;
using ExpenseTrackerRepository.ApiRouteFetcher;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        public UserProfileRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IList<IdentityUser>> GetAllUsers()
        {
            var usersList = await webApiExecuter.InvokeGet<IList<IdentityUser>>("api/accounts/allUsers");
            return usersList;
        }

        public async Task<IdentityUser> GetSingleUser(string singleUserId)
        {
            return await webApiExecuter.InvokeGet<IdentityUser>($"api/accounts/getSingleUser/{singleUserId}");
        }

        public async Task DeleteSingleUser(string deletedUserId)
        {
            await webApiExecuter.InvokeDelete<IdentityUser>($"api/accounts/deleteSingleUser/{deletedUserId}");
        }

        public async Task UpdateUserPassword(ChangePassword changePassword, string updatedUserName)
        {
            await webApiExecuter.InvokePut($"api/accounts/updateUserPassword/{updatedUserName}", changePassword);
        }

    }
}
