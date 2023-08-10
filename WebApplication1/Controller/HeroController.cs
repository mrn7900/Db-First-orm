using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {

        private readonly IHeroService _heroService;
        public HeroController(IHeroService heroService)
        {
            _heroService = heroService;
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<Herobio>> GetHero(int id)
        {
            //The method will search db by id, if there was requested data it will return it else it will use incoming Api to get and set data in database.
            var res = await _heroService.GetHero(id);
                if(res.Result == null)
                return NotFound();
                 else
                return Ok(res);

        }
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Herobio>> GetHeroByName(string name)
        {
            //The method will search db by name, if there was requested data it will return it .
            var res = await _heroService.GetByName(name);
            if (res == null)
                return NotFound();
            else
                return Ok(res);

        }

        [HttpGet]
        public async Task<ActionResult<List<Herobio>>> Get()
        {   
            return Ok(await _heroService.Get());
        }

        [HttpPost]
        public async Task<ActionResult<List<Herobio>>> Post(Herobio Hero)
        {
            //This method will add new data in database if there wont be any same data(by checking the id)
            var userid = await _heroService.GetHeroDB(Hero.id);
            if (userid == null)
            {
                await _heroService.Create(Hero);
                return Ok();
            }
            else
                return BadRequest("Doublicate Enterance(check id)");

        }

        [HttpPut]
        public async Task<ActionResult<List<Herobio>>> Update(Herobio Req)
        {
            var res = await _heroService.Update(Req);
            if (res == null)
                return NotFound();
            else
                return Ok(res);
        }
      
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Herobio>>> Delete(int id)
        {
            var res = await _heroService.Delete(id);
            if (res == null)
                return NotFound();
            else
            return Ok(await _heroService.Get());
        }
    }
}
