using WebApplication1.Models;
using WebApplication1.Repos;

namespace WebApplication1.Services
{
    public class HeroService: IHeroService
    {
        private readonly IHeroApiService _heroApiService;
        private readonly IHeroRepo _heroRepo;
        public HeroService(HeroRepo heroRepo , HeroApiService heroApiService)
        {
            _heroApiService = heroApiService;
            _heroRepo = heroRepo;

        }
        public async Task<bool> GetHero(int id)
        {
            var res = _heroRepo.GetHeroTbl(id);
            if (res == null)
            {
                var ApiTbl = await _heroApiService.Get();
                _heroRepo.CreateHero(ApiTbl);
                return true;
            }
            return false;
            
        }
        public async Task Get()
        {
            _heroRepo.GetHeros();
        }
        /*    public async Task Create(Herobio hero)
            { 
                 _heroRepo.CreateHero(hero);
            }*/

        public async Task Delete(int id)
        {
            _heroRepo.DeleteHero(id);
        }
        
 /*       public async Task GetHeros()
        {
            _heroRepo.GetHeros();
        }*/
        public async Task Update(Herobio hero)
        {
            _heroRepo.Update(hero);
        }
    }
}
