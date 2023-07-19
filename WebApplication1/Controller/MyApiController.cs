using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

//Api docs
//https://superheroapi.com/?ref=apilist.fun
//Api acess token
//956111282311222
namespace WebApplication1.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public MyApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Make an HTTP GET request to the API
            var response = await _httpClient.GetAsync("https://superheroapi.com/api/956111282311222/1/biography");
           


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Process the response content
                return Ok(content);
            }

            // Handle unsuccessful response
            return BadRequest();
        }
    }
}
