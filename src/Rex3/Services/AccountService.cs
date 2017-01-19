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
            //var AccountList = new List<Account>()
            //{
            //    new Account() { AccountId=1, AccountName="Test 1", AccountNameAka="Test", HooversDescription="description of this account"},
            //    new Account() { AccountId=2, AccountName="Test 2", AccountNameAka="Test", HooversDescription="description of this account"},
            //    new Account() { AccountId=3, AccountName="Test 3", AccountNameAka="Test", HooversDescription="description of this account"}
            //};

            //SqlParameter category = new SqlParameter("@CategoryName", "Test");
            var AccountList = context.Set<Account>().FromSql("GetAccounts").AsNoTracking().ToList();


            return AccountList;
        }
    }
}
