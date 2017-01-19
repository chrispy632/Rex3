using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rex3.Models;
using Rex3.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rex3.Controllers
{
    public class ClientController : Controller
    {
        private IAccountService _accountService;

        public ClientController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: /Client/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Client/NewClient
        public IActionResult NewClient()
        {
            return View();
        }

        // GET: /Client/List
        public IActionResult List()
        {      
            return View();
        }

        // GET: /Client/GetClients
        public IActionResult GetClients()
        {
            return new ObjectResult(_accountService.GetAccounts());

        }
    }
}
