using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rex3.Models;
using Rex3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rex3.Components
{
    public class UserNameWidget : ViewComponent
    {
        private ICurrentUserService _currentUserService;
        private IUserService _userService;

        public UserNameWidget(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _currentUserService.GetUserName();
            var user = _userService.GetUserDetails(userId);
            return View(user);
        }
    }


}



//public class UserNameWidget : ViewComponent
//{
//    private IHttpContextAccessor httpContextAccessor;

//    public UserNameWidget(IHttpContextAccessor httpContextAccessor)
//    {
//        this.httpContextAccessor = httpContextAccessor;
//    }

//    public async Task<IViewComponentResult> InvokeAsync()
//    {
//        //var test = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;


//        User user = new User()
//        {
//            UserId = test,
//            Name = "Test",
//            IsActive = true,
//            CreatedDt = DateTime.Now,
//            CreatedId = "System"
//        };
//        return View(user);
//    }
//}