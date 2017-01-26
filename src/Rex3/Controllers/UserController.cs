using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rex3.Models;
using Rex3.Services;

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

            var model = await _userService.GetUserDetails(userId); 
            if (model == null)
            {
                return NotFound();
            }

            if (model.CountryCode == null || model.CountryCode == "")
            {
                model.CountryCode = "";
            }
            else {
                model.CountryCode = model.CountryCode.TrimEnd();
            }            

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost([Bind("UserId,Name,IsActive,email,CountryCode,CreatedDt,CreatedId")] User user)
        {
            if (userId == null || userId == "")
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Students.SingleOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Student>(
                studentToUpdate,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        public IActionResult Create()
        {
            var user = new User();
            user.CreatedDt = System.DateTime.Now;
            user.CreatedId = _currentUser.GetUserName();
            user.IsActive = true;
            return View(user);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,IsActive,email,CountryCode,CreatedDt,CreatedId")] User user)
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
