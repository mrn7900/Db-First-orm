using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IHeroService
    {
        public Task<bool> GetHero(int id);
        public Task Get();
        public Task Delete(int id);
        public Task Update(Herobio hero);
    }
}
