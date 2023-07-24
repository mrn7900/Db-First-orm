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
            return Ok(await _heroService.GetHero(id));
        }
        [HttpGet]
        public async Task<ActionResult<List<Herobio>>> Get()
        {
            return Ok(_heroService.Get());
        }
     /*   [HttpPost]
        public async Task<ActionResult<List<Herobio>>> Post(Herobio Hero)
        {
            _heroService.(Hero);
            await _Context.SaveChangesAsync();
            return Ok(await _Context.Admins.ToListAsync());
        }*/
        [HttpPut]
        public async Task<ActionResult<List<Herobio>>> Update(Herobio Req)
        {
            if (Req != null)
                return Ok(_heroService.Update(Req));
            else
                return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Herobio>>> Delete(int id)
        {

            return Ok(_heroService.Delete(id));
        }
    }
}
