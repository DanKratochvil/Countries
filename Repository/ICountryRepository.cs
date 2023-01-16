using Countries.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Countries.Repository
{
    internal interface ICountryRepository
    {
        Task<List<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryAsync(string name);
        Task<Country> GetCountryByCodeAsync(string code);
    }
}
