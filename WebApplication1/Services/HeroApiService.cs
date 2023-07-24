using Newtonsoft.Json;
using System.Net.Http;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class HeroApiService : IHeroApiService
    {
        private readonly HttpClient _httpClient;
        public HeroApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<Herobio> Get()
        {
            // Make an HTTP GET request to the API
            var response = await _httpClient.GetAsync("https://superheroapi.com/api/956111282311222/1/biography");

            var content = await response.Content.ReadAsStringAsync();

            var myheroObj = JsonConvert.DeserializeObject<Herobio>(content);


         /*   if (response.IsSuccessStatusCode)
            {


                return myjson;

            }*/
            return myheroObj;

        }


    }
}
