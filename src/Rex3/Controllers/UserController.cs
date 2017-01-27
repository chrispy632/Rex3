using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rex3.Models;
using Rex3.Services;
using Rex3.ViewModels;

namespace Rex3.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private ICurrentUserService _currentUser;

        public UserController(IUserService userService, ICurrentUserService currentUser)
        {
            _userService = userService;
            _currentUser = currentUser;
        } 

        public async Task<IActionResult> Index()
        {
            var model = await _userService.GetUsers();
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        public IActionResult List()
        {
            return View();
        }

        // GET: /Client/GetClients
        public IActionResult GetUsers()
        {
            var model = _userService.GetUserListVM();
            if (model == null)
            {
                return NotFound();
            }
            return new ObjectResult(model);

        }

        public async Task<IActionResult> Details(string userId)
        {
            if (userId == null || userId =="")
            {
                return NotFound();
            }

            var model = await _userService.GetUserDetails(userId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string userId)
        {
            if (userId == null || userId == "")
            {
                return NotFound();
            }
            var currentUserId = _currentUser.GetUserName();
            var model = await _userService.GetUserDetailsVM(userId, currentUserId); 
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost([Bind("UserId,Name,IsActive,email,CountryCode,CreatedDt,CreatedId, RoleId")] UserViewModel userVM)
        {
            if (userVM.UserId == null || userVM.UserId == "")
            {
                return NotFound();
            }
            var currentUserId = _currentUser.GetUserName();         
            await _userService.UpdateAsyncVM(userVM, currentUserId);
            
            return RedirectToAction("Index"); 
        }

        public IActionResult Create()
        {
            var currentUserId = _currentUser.GetUserName();
            UserViewModel modelVM = new UserViewModel
            {
                UserId = "",
                Name = "",
                email = "@xerox.com",
                CountryCode = "",
                RoleId = 10,
                IsActive = true,
                CreatedDt = DateTime.Now,
                CreatedId = currentUserId,
                UpdatedDt = DateTime.Now,
                UpdatedId = currentUserId
            };

            return View(modelVM);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,IsActive,email,CountryCode,CreatedDt,CreatedId,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedDt = System.DateTime.Now;
                user.CreatedId = _currentUser.GetUserName();
                var model = await _userService.AddUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string userId, bool? saveChangesError = false)
        {
            if (userId == null || userId == "")
            {
                return NotFound();
            }

            var model = await _userService.GetUserDetails(userId);
            if (model == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return View(model);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(string userId)
        {
            var user = await _userService.GetUserDetails(userId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            int error = await _userService.DeleteUser(user);

            if (error != 0)
            {
                return RedirectToAction("Delete", new { userId = userId, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }
    }
}
