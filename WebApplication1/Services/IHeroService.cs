using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IHeroService
    {
        public Task Create(Herobio hero);
        public Task<Herobio> GetHero(int id);
        public Task<Herobio> GetHeroDB(int id);
        public Task<List<Herobio>> Get();
        public Task Delete(int id);
        public Task<List<Herobio>> Update(Herobio hero);
    }
}
