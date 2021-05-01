using ExpenseTrackerModels.AuthModels;
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

        public async Task<IList<IdentityUser>> ViewAllUsers()
        {
            var usersList = await userProfileRepository.GetAllUsers();
            return usersList;
        }

        public async Task<IdentityUser> ViewSingleUser(string singleUserId)
        {
            return await userProfileRepository.GetSingleUser(singleUserId);
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
