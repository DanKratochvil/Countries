using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Timers;
using Countries.Models;
using Countries.Repository;

namespace Countries.Services
{
    public class CountryService : ICountryService
    {
        static readonly CountryRepository repo = new CountryRepository();
        static readonly ObjectCache cache = MemoryCache.Default;
        static double cacheExpiresSec = 60;
        static Dictionary<string, int> displayedCountries = new Dictionary<string, int>();
        static Timer timer = new Timer();
        static StreamWriter log = new StreamWriter(ConfigurationManager.AppSettings["LogFileName"]);

        static CountryService()
        {
            if (Double.TryParse(ConfigurationManager.AppSettings["CacheExpiresSec"], out double res))
                cacheExpiresSec = res;
            timer.Interval = 1000 * 60;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
            log.AutoFlush = true;
        }

        private static async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.TimeOfDay <= new TimeSpan(0, 1, 0))
                await GenerateStatisticsAsync();
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            var countries = cache["all"] as List<Country>;
            if (countries != null) return countries;

            try
            {
                countries = await repo.GetAllCountriesAsync();
                cacheSet("all", countries);
                return countries;
            }
            catch (Exception ex)
            {
                log.WriteLine($"{DateTime.Now} {ex.Message}");
            }
            return null;
        }

        public async Task<Country> GetCountryAsync(string name)
        {
            var country = cache[name] as Country;
            if (country != null)
            {
                displayedCountries[name] = displayedCountries.ContainsKey(name) ? ++displayedCountries[name] : 1;
                log.WriteLine($"{DateTime.Now} country data for {name} were taken from cache");
                return country;
            }

            try
            {
                DateTime restCallStart = DateTime.Now;
                country = await repo.GetCountryAsync(name);
                log.WriteLine($"{restCallStart} GetCountryAsync called for {name} duration {DateTime.Now - restCallStart}");

                displayedCountries[name] = displayedCountries.ContainsKey(name) ? ++displayedCountries[name] : 1;              
                cacheSet(name, country);
                return country;
            }
            catch (Exception ex)
            {
                log.WriteLine($"{DateTime.Now} {ex.Message}");
            }
            return null;
        }

        public async Task<Country> GetCountryByCodeAsync(string code)
        {
            var country = cache[code] as Country;
            if (country != null)
                return country;

            try
            {
                country = await repo.GetCountryByCodeAsync(code);
                cacheSet(code, country);
                return country;
            }
            catch (Exception ex)
            {
                log.WriteLine($"{DateTime.Now} {ex.Message}");
            }
            return null;
        }

        private void cacheSet(string key, object item)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(cacheExpiresSec);
            cache.Set(key, item, policy);
        }

        static async Task GenerateStatisticsAsync()
        {
            using (var sw = new StreamWriter(ConfigurationManager.AppSettings["StatFileName"]))
            {
                foreach (var key in displayedCountries.Keys)
                    await sw.WriteLineAsync($"{key} displayed {displayedCountries[key]} times");
            }
        }
    }

}
