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
     
        public int userid { get; set; }

        public string username { get; set; }

        public async Task<Herobio> Get()
        {
            int incomingid = this.userid;
            string incomingname = this.username;
          
                // Make an HTTP GET request to the API
                var response = await _httpClient.GetAsync("https://superheroapi.com/api/956111282311222/" + incomingid + "/biography");
                var content = await response.Content.ReadAsStringAsync();

                var myheroObj = JsonConvert.DeserializeObject<Herobio>(content);

                return myheroObj;
            
           /* else
            {
                var response = await _httpClient.GetAsync("https://superheroapi.com/api/956111282311222/search/" + incomingname + "");
                *//* var response = await _httpClient.GetAsync("https://www.superheroapi.com/api.php/956111282311222/search/A-bomb");*//*
                var content = await response.Content.ReadAsStringAsync();

                var myheroObj = JsonConvert.DeserializeObject<Herobio>(content);
                return myheroObj;
            }
           */




        }


    }
}
