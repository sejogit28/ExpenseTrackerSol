using ExpenseTrackerModels;
using ExpenseTrackerModels.AuthModels;
using ExpenseTrackerModels.UserViewModel;
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

        public async Task<IList<UserProfile>> GetAllUsers()
        {
            var usersList = await webApiExecuter.InvokeGet<IList<UserProfile>>("api/accounts/allUsers");
            return usersList;
        }

        public async Task<UserProfile> GetSingleUser(string singleUserName)
        {
            return await webApiExecuter.InvokeGet<UserProfile>($"api/accounts/getSingleUser/{singleUserName}");
        }

        public async Task DeleteSingleUser(string deletedUserId)
        {
            await webApiExecuter.InvokeDelete<ExpenseTrackerUser>($"api/accounts/deleteSingleUser/{deletedUserId}");
        }

        public async Task UpdateUserPassword(ChangePassword changePassword, string updatedUserName)
        {
            await webApiExecuter.InvokePut($"api/accounts/updateUserPassword/{updatedUserName}", changePassword);
        }

    }
}
