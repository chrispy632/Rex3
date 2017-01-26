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
                    .ThenInclude( ur => ur.Role)
                    .OrderByDescending(o => o.Name)
                .ToListAsync();

            return result;
        }
        public async Task<User> GetUserDetails(string UserId)
        {
            var result = await _context.Users
                .Where(a => a.UserId == UserId)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                User user = new User();
                user.Name = "Not Known";
                return user;
            }
           return result;
        }

        public async Task<User> AddUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<int> DeleteUser(User user)
        {
            int error_code = 0;
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                error_code = 1;
            }

            return error_code;
        }
    }
}
