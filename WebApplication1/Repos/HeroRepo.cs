using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Repos
{
    public class HeroRepo : IHeroRepo
    {
        private readonly testContext _context;


        public HeroRepo(testContext context)
        {
            _context = context;
        }

        public async void CreateHero(Herobio hero)
        {
            _context.Herobios.Add(hero);
            await _context.SaveChangesAsync();
           // return Ok(await _context.Herobios.ToListAsync());
            
        }

        public async void DeleteHero(int id)
        {
          /*  var hero = await _context.FindAsync(id);
            if (hero > -1)
            {
                _hero.RemoveAt(hero);
            }*/
            var dbHero = await _context.Herobios.FindAsync(id);
            if (dbHero != null)
                _context.Herobios.Remove(dbHero);
            await _context.SaveChangesAsync();

        }

        public async Task<Herobio> GetHeroTbl(int id)
        {
            var hero = await _context.Herobios.FindAsync(id);
            return hero;

        }

        public async Task<List<Herobio>> GetHeros()
        {
            return await _context.Herobios.ToListAsync();
        }

        public async Task<List<Herobio>> Update(Herobio Req)
        {
            var dbHero = await _context.Herobios.FindAsync(Req.id);
            if (dbHero != null)
            {
                dbHero.id = Req.id;
                dbHero.name = Req.name;
                //dbHero.Aliases = Req.Aliases;
                dbHero.firstappearance = Req.firstappearance;
                dbHero.placeofbirth = Req.placeofbirth;
                dbHero.fullname = Req.fullname;
                dbHero.publisher = Req.publisher;
                dbHero.alignment = Req.alignment;
                dbHero.alteregos = Req.alteregos;

                await _context.SaveChangesAsync();
                return await _context.Herobios.ToListAsync();
            }
            else
            {
                return null;
            }




          /*  var Hero = _hero.FindIndex(x => x.Id == id);
            if (Hero > -1)
            {
                _hero[Hero] = hero;
            }*/
        }

    }
}
