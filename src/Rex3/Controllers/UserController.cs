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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetUsers());
        }

        public IActionResult Details(string userId)
        {
            var model = _userService.GetUserDetails(userId);
            return View(model);
        }

    }
}
