using Countries.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Countries.Services
{
    internal interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryAsync(string name);
        Task<Country> GetCountryByCodeAsync(string code);
    }
}
