using Rex3.Models;
using Rex3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rex3.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Get a list of users
        /// </summary>
        /// <returns>
        /// List of users
        /// </returns>
        Task<List<User>> GetUsers();
        List<UserListVM> GetUserListVM();
        Task<User> GetUserDetails(string UserId);
        Task<UserViewModel> GetUserDetailsVM(string UserId, string currentUserId);
        Task<User> AddUser(User user);

        Task UpdateAsync(User userUpdates);
        Task UpdateAsyncVM(UserViewModel userVMUpdates, string currentUserId);
        Task<int> DeleteUser(User user);
    }
}
