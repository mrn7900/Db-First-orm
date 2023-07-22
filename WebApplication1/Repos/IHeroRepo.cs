using WebApplication1.Models;

namespace WebApplication1.Repos
{
    public interface IHeroRepo
    {
        public void CreateHero(Herobio hero);

        public void DeleteHero(int id);

        public IAsyncEnumerable<Herobio> GetHeroTbl(int id);

        public Task<List<Herobio>> GetHeros();

        public Task<List<Herobio>> Update(Herobio Req);

    }
}
