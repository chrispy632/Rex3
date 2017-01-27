using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rex3.Models;
using Microsoft.EntityFrameworkCore;
using Rex3.ViewModels;

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

        public List<UserListVM> GetUserListVM()
        {
            var userListVM = _context.Set<UserListVM>().FromSql("GetUsers")
                .AsNoTracking()
                .ToList();

            return userListVM;
        }


        public async Task<User> GetUserDetails(string UserId)
        {
            var result = await _context.Users
                .Include(ur => ur.UserRoles)
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

        public async Task<UserViewModel> GetUserDetailsVM(string UserId, string currentUserId)
        {
            UserViewModel userVM = new UserViewModel();
            var result = await _context.Users
                .Include(ur => ur.UserRoles)
                .Where(a => a.UserId == UserId)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                userVM.UserId = "Not Known";
                userVM.Name = "Not Known";
                userVM.IsActive = false;
                return userVM;
            }

            userVM = MapUserToViewModel(result, currentUserId);

            return userVM;
        }

        public async Task<User> AddUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task UpdateAsync(User userUpdates)
        {
                _context.Users.Attach(userUpdates);
                _context.Entry(userUpdates).State = EntityState.Modified;
                await _context.SaveChangesAsync();

        }

        public async Task UpdateAsyncVM(UserViewModel userVMUpdates, string currentUserId)
        {
            User userUpdates = MapViewModelToUser(userVMUpdates, currentUserId);

            var existingUser = _context.Users
                .Where(p => p.UserId == userUpdates.UserId)
                .Include(p => p.UserRoles)
                .SingleOrDefault();

            if (existingUser != null)
            {
                // Update parent
                _context.Entry(existingUser).CurrentValues.SetValues(userUpdates);
                // Delete children
                foreach (var existingRoles in existingUser.UserRoles.ToList())
                {
                    //if (userVMUpdates.RoleId != existingRoles.RoleId)
                        _context.UserRoles.Remove(existingRoles);
                }

                // Update and Insert children
                var currentUserRoles = existingUser.UserRoles
                    .Where(c => c.RoleId == userVMUpdates.RoleId)
                    .SingleOrDefault();

                if (currentUserRoles != null)
                    // Update child
                    _context.Entry(currentUserRoles).CurrentValues.SetValues(new UserRole
                        { Id = currentUserRoles.RoleId,
                        RoleId = userVMUpdates.RoleId,
                        UserId = userVMUpdates.UserId,
                        CreatedDt = DateTime.Now,
                        CreatedId = currentUserId }
                        );
                else
                {
                    // Insert child
                    var roleToAdd = new UserRole { RoleId = userVMUpdates.RoleId, UserId = userVMUpdates.UserId, CreatedDt = DateTime.Now, CreatedId = currentUserId };
                    existingUser.UserRoles.Add(roleToAdd);
                }

            }

            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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

        private UserViewModel MapUserToViewModel(User user, string currentUserId)
        {
            UserViewModel userVM = new UserViewModel();
            userVM.UserId = user.UserId;
            userVM.Name = user.Name;
            userVM.email = user.email;
            userVM.IsActive = user.IsActive;
            userVM.CountryCode = user.CountryCode;
            userVM.CreatedDt = user.CreatedDt;
            userVM.CreatedId = user.CreatedId;
            userVM.UpdatedDt = DateTime.Now;
            userVM.UpdatedId = currentUserId;

            if (userVM.CountryCode == null || userVM.CountryCode == "")
            {
                userVM.CountryCode = "";
            }
            else
            {
                userVM.CountryCode = userVM.CountryCode.TrimEnd();
            }

            if (user.UserRoles == null || user.UserRoles.FirstOrDefault() == null)
            {
                userVM.RoleId = 10;
            }
            else
            {
                userVM.RoleId = user.UserRoles.FirstOrDefault().RoleId;
            }

            return userVM;
        }
        private User MapViewModelToUser(UserViewModel userVM, string currentUserId)
        {
            User user = new User();
            user.UserId = userVM.UserId;
            user.Name = userVM.Name;
            user.email = userVM.email;
            user.IsActive = userVM.IsActive;
            user.CountryCode = userVM.CountryCode;
            user.CreatedDt = userVM.CreatedDt;
            user.CreatedId = userVM.CreatedId;
            user.UpdatedDt = DateTime.Now;
            user.UpdatedId = currentUserId;
            return user;
        }
    }
}
