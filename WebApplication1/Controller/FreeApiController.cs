using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using WebApplication1.Models;

//Api docs
//https://superheroapi.com/?ref=apilist.fun
//Api acess token
//956111282311222
namespace WebApplication1.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreeApiController : ControllerBase
    {/*
        //add NLog and context
        private readonly ILogger<AdminsController> _logger;
        private readonly HttpClient _httpClient;
        private readonly testContext _Context;

        public FreeApiController(HttpClient httpClient, testContext context, ILogger<AdminsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into AdminController");
            _httpClient = httpClient;
            _Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Make an HTTP GET request to the API
            var response = await _httpClient.GetAsync("https://superheroapi.com/api/956111282311222/1/biography");



            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return Ok(content);

            }

            return BadRequest();
        }
*/

    }
}
