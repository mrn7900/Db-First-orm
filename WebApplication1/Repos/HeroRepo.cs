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
            /*var hero = await _context.FindAsync(id);
            if (hero > -1)
            {
                _hero.RemoveAt(hero);
            }*/
            var dbHero = await _context.Herobios.FindAsync(id);
            if (dbHero != null)
            _context.Herobios.Remove(dbHero);
            await _context.SaveChangesAsync();
            
        }

        public async IAsyncEnumerable<Herobio> GetHeroTbl(int id)
        {
            /*var hero = _hero.Where(x => x.Id == id).FirstOrDefault();
            return hero;*/
            var hero = await _context.Herobios.FindAsync(id);
            var notfound = "notfound";
            if (hero == null)
            {
                //yield return notfound;
            }
            yield return await Task.FromResult(hero);
        }

        public async Task<List<Herobio>> GetHeros()
        {
            return await _context.Herobios.ToListAsync();
        }

        public async Task<List<Herobio>> Update(Herobio Req)
        {
            var dbHero = await _context.Herobios.FindAsync(Req.Id);

            dbHero.Name = Req.Name;
            dbHero.Aliases = Req.Aliases;
            dbHero.FirstAppearance = Req.FirstAppearance;
            dbHero.PlaceOfBirth = Req.PlaceOfBirth;
            dbHero.Fullname = Req.Fullname;
            dbHero.Publisher = Req.Publisher;
            dbHero.Alignment= Req.Publisher;

            await _context.SaveChangesAsync();

            return await _context.Herobios.ToListAsync();

            /*var Hero = _hero.FindIndex(x => x.Id == id);
            if (Hero > -1)
            {
                _hero[Hero] = hero;
            }*/
        }
    }
}
