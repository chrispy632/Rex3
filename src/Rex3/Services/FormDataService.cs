using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rex3.Services
{
    public class FormDataService : IFormDataService
    {
        private RexContext context;

        public FormDataService(RexContext context)
        {
            this.context = context;
        }

        public List<Country> GetCountries()
        {
            var items = context.Countries
                .OrderBy(o => o.Name)
                .ToList();

            return items;
        }
    }

}
