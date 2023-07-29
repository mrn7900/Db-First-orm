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
      /*  public int userid(int id)
        {
            int userid = id;
            return userid;
        }*/
        public int userid { get; set; }

        public async Task<Herobio> Get()
        {
            int incomingid = this.userid;
           
            // Make an HTTP GET request to the API
            var response = await _httpClient.GetAsync("https://superheroapi.com/api/956111282311222/"+incomingid+"/biography");

            var content = await response.Content.ReadAsStringAsync();

            var myheroObj = JsonConvert.DeserializeObject<Herobio>(content);
            
            //var alies = myheroObj.Aliases.


         /*   if (response.IsSuccessStatusCode)
            {


                return myjson;

            }*/
            return myheroObj;

        }


    }
}
