using WebApplication1.Models;
using WebApplication1.Properties;

namespace WebApplication1.Services
{
    public interface IHeroService
    {
        public Task Create(Herobio hero);
        /*        public Task<Herobio> GetHero(int id);*/
        public Task<IMethodResult> GetHero(int id);
        public Task<IMethodResult> GetByName(string name);
        public Task<Herobio> GetHeroDB(int id);
        /*public Task<List<Herobio>> Get();*/
        public Task<IMethodResult> Get();
        public Task<IMethodResult> Delete(int id);
        public Task<IMethodResult> Update(Herobio hero);
    }
}
