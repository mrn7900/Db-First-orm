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
            await _context.SaveChangesAsync();
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
            var dbHero = await _context.Herobios.FindAsync(Req.Id);
            if (dbHero != null)
            {
                dbHero.Id = Req.Id;
                dbHero.Name = Req.Name;
                dbHero.Aliases = Req.Aliases;
                dbHero.FirstAppearance = Req.FirstAppearance;
                dbHero.PlaceOfBirth = Req.PlaceOfBirth;
                dbHero.Fullname = Req.Fullname;
                dbHero.Publisher = Req.Publisher;
                dbHero.Alignment = Req.Alignment;
                dbHero.AlterEgos = Req.AlterEgos;

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
