using Countries.Models;
using Countries.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Countries.Controllers
{
    public class CountryController : ApiController
    {

        private readonly CountryService countryService;

        public CountryController()
        {
            countryService = new CountryService();
        }

        public async Task<IEnumerable<Country>> Get()
        {
            return await countryService.GetAllCountriesAsync();
        }

        public async Task<Country> Get(string name)
        {
            return await countryService.GetCountryAsync(name);
        }

        [Route("api/country/code/{code}")]
        public async Task<Country> GetByCode(string code)
        {
            return await countryService.GetCountryByCodeAsync(code);
        }        
    }
}
