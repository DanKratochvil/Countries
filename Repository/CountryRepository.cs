using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Countries.Models;
using Newtonsoft.Json;

namespace Countries.Repository
{
    public class CountryRepository : ICountryRepository
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            HttpResponseMessage result = await client.GetAsync("https://restcountries.com/v3.1/all?fields=name");
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Country>>(response).OrderBy(c => c.Name.Common).ToList();
            }
            throw new HttpRequestException($"{result.StatusCode} {result.RequestMessage}");
        }

        public async Task<Country> GetCountryAsync(string name)
        {
            HttpResponseMessage result = await client.GetAsync($"https://restcountries.com/v3.1/name/{name}?fields=name,capital,borders,translations");
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Country>>(response).FirstOrDefault();
            }
            throw new HttpRequestException($"{result.StatusCode} {result.RequestMessage}");
        }

        public async Task<Country> GetCountryByCodeAsync(string code)
        {
            HttpResponseMessage result = await client.GetAsync($"https://restcountries.com/v3.1/alpha/{code}?fields=name,translations");
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Country>(response);
            }
            throw new HttpRequestException($"{result.StatusCode} {result.RequestMessage}");
        }
    }
}
