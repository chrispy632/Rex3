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
            var result = await _context.Users
                .Include(ur => ur.UserRoles)
                    .ThenInclude( r => r.Role)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }
        public User GetUserDetails(string UserId)
        {
            var result = _context.Users
                .Where(a => a.UserId == UserId)
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
