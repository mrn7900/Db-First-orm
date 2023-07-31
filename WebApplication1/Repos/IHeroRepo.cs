using WebApplication1.Models;

namespace WebApplication1.Repos
{
    public interface IHeroRepo
    {
        public void CreateHero(Herobio hero);
        public string exeption { get; set; }

        public void DeleteHero(int id);

        public Task<Herobio> GetHeroTbl(int id);

        public  Task<List<Herobio>> GetHeroName(string name);
        
        public Task<List<Herobio>> GetHeros();

        public Task<List<Herobio>> Update(Herobio Req);

    }
}
