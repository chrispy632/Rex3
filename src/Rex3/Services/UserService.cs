using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rex3.Models;
using Microsoft.EntityFrameworkCore;

namespace Rex3.Services
{
    public class UserService : IUserService
    {
        private RexContext _context;
        public UserService(RexContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public User GetUserDetails(string UserId)
        {
            var result = _context.Users
                .Where(a => a.UserId == UserId)
                .Include(i => i.UserRoles)
                .Include(i => i.UserRoles.FirstOrDefault().Role)
                .FirstOrDefault();

            if (result == null)
            {
                User user = new User();
                user.Name = "Not Known";
                return user;
            }
           return result;
        }
    }
}
