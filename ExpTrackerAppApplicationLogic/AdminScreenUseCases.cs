using ExpenseTrackerModels;
using ExpenseTrackerModels.AuthModels;
using ExpenseTrackerModels.UserViewModel;
using ExpenseTrackerRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTrackerAppApplicationLogic
{

    public class AdminScreenUseCases : IAdminScreenUseCases
    {
        private readonly IUserProfileRepository userProfileRepository;

        public AdminScreenUseCases(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        public async Task<IList<UserProfile>> ViewAllUsers()
        {
            return await userProfileRepository.GetAllUsers();
        }

        public async Task<UserProfile> ViewSingleUser(string singleUserName)
        {
            return await userProfileRepository.GetSingleUser(singleUserName);
        }

        public async Task DeleteSingleUser(string deletedUserId)
        {
            await userProfileRepository.DeleteSingleUser(deletedUserId);
        }

        public async Task UpdateUserPassword(ChangePassword changePassword, string updatedUserName)
        {
            await userProfileRepository.UpdateUserPassword(changePassword, updatedUserName);
        }
    }
}
