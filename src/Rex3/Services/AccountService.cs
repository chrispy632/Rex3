using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rex3.Models;
using Microsoft.EntityFrameworkCore;

namespace Rex3.Services
{
    public class AccountService : IAccountService
    {
        private RexContext context;
        public AccountService(RexContext context)
        {
            this.context = context;
        }


        public List<Account> GetAccounts()
        {
            var AccountList = context.Set<Account>().FromSql("GetAccounts")
                .AsNoTracking()
                .ToList();

            return AccountList;
        }
    }
}
