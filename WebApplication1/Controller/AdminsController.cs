using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//Read IConfiguratio
//https://www.youtube.com/watch?v=UiqTDvIFJ3g
namespace WebApplication1.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
       /* //add NLog and context
        private readonly ILogger<AdminsController> _logger;
        private readonly testContext _Context;
        public AdminsController(testContext context, ILogger<AdminsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into AdminController");
            _Context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Admin>>> Get()
        {
            return Ok(await _Context.Admins.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> Get(int id)
        {
            var hero = await _Context.Admins.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<Admin>>> Post(Admin Hero)
        {
            _Context.Admins.Add(Hero);
            await _Context.SaveChangesAsync();
            return Ok(await _Context.Admins.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Admin>>> Update(Admin Req)
        {
            var dbHero = await _Context.Admins.FindAsync(Req.Id);
            if (dbHero == null)
                return NotFound();
            dbHero.Username = Req.Username;
            dbHero.City = Req.City;



            await _Context.SaveChangesAsync();

            return Ok(await _Context.Admins.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Admin>>> Delete(int id)
        {
            var dbHero = await _Context.Admins.FindAsync(id);
            if (dbHero == null)
                return NotFound();
            _Context.Admins.Remove(dbHero);
            await _Context.SaveChangesAsync();
            return Ok(await _Context.Admins.ToListAsync());
        }*/

    }
}

