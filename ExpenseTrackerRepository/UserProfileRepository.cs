using ExpenseTrackerModels;
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

        public async Task<IList<ExpenseTrackerUser>> GetAllUsers()
        {
            var usersList = await webApiExecuter.InvokeGet<IList<ExpenseTrackerUser>>("api/accounts/allUsers");
            return usersList;
        }

        public async Task<ExpenseTrackerUser> GetSingleUser(string singleUserName)
        {
            return await webApiExecuter.InvokeGet<ExpenseTrackerUser>($"api/accounts/getSingleUser/{singleUserName}");
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
