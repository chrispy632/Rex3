using Microsoft.AspNetCore.Mvc.Rendering;
using Rex3.Models;
using System.Collections.Generic;

namespace Rex3.Services
{
    public interface IFormDataService
    {
        List<Country> GetCountries();
    }
}