using WebApplication1.Models;
using WebApplication1.Repos;

namespace WebApplication1.Services
{
    public class HeroService : IHeroService
    {
        private readonly IHeroApiService _heroApiService;
        private readonly IHeroRepo _heroRepo;
        public HeroService(IHeroRepo heroRepo, IHeroApiService heroApiService)
        {
            _heroApiService = heroApiService;
            _heroRepo = heroRepo;

        }
        public async Task<Herobio> GetHero(int id)
        {
            var res = _heroRepo.GetHeroTbl(id);
            if (res.Result == null)
            {
                _heroApiService.userid = id;
                var ApiTbl = await _heroApiService.Get();
                _heroRepo.CreateHero(ApiTbl);
                var show = _heroRepo.GetHeroTbl(id);
                return await show;
            }
            return await res;

        }
        public async Task<List<Herobio>> Get()
        {
            return await _heroRepo.GetHeros();
        }
        public async Task Create(Herobio hero)
        {
            _heroRepo.CreateHero(hero);
        }

        public async Task Delete(int id)
        {
            _heroRepo.DeleteHero(id);
        }

        public async Task GetHeros()
        {
            _heroRepo.GetHeros();
        }
        public async Task<List<Herobio>> Update(Herobio hero)
        {
            return await _heroRepo.Update(hero);
        }
    }
}
