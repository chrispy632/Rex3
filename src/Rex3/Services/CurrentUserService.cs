using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rex3.Services
{
    public class CurrentUserService : ICurrentUserService
    {

        private IHttpContextAccessor _contextAccessor;
        private HttpContext _context { get { return _contextAccessor.HttpContext; } }

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserName
        {
            get
            {
                var userName = "SystemGenerated";
                // Figure out the user's identity
                if (_context != null)
                {
                    if (_context.User != null)
                    {
                        var identity = _context.User.Identity;

                        if (identity != null && identity.IsAuthenticated)
                        {
                            userName = identity.Name;
                        }
                    }
                }

                return userName;
            }
        }

        public string GetUserName()
        {
            string shortName = UserName.Split('\\')[1];

            return shortName;
        }
    }
}
