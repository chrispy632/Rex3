using Rex3.Models;
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
        Task<User> GetUserDetails(string UserId);
        Task<User> AddUser(User user);
        Task<int> DeleteUser(User user);
    }
}
