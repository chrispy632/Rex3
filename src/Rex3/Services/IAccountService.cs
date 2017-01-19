using Rex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rex3.Services
{
    public interface IAccountService
    {
        List<Account> GetAccounts();
    }
}
