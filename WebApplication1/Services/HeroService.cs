
using WebApplication1.Models;
using WebApplication1.Properties;
using WebApplication1.Repos;

namespace WebApplication1.Services
{
    public class HeroService : IHeroService
    {
        private readonly IHeroApiService _heroApiService;
        private readonly IHeroRepo _heroRepo;
        private readonly IMethodResult _methodResult;
        public HeroService(IHeroRepo heroRepo, IHeroApiService heroApiService , IMethodResult methodResult)
        {
            _heroApiService = heroApiService;
            _heroRepo = heroRepo;
            _methodResult = methodResult;
        }

        /*public async Task<Herobio> GetHero(int id)*/
        public async Task<IMethodResult> GetHero(int id)
        {
            //The method will search db, if there was requested data it will return it else it will use incoming Api to get and set data in database
            var res = _heroRepo.GetHeroTbl(id);
            _methodResult.Result = res;
            if (res.Result == null)
            {
                _heroApiService.userid = id;
                var ApiTbl = await _heroApiService.Get();
                if (ApiTbl.id == 0)
                {
                    var show = _heroRepo.GetHeroTbl(id);
                    _methodResult.Result = show;
                    return _methodResult;
                    
                }
                else
                {
              /*      _heroRepo.CreateHero(ApiTbl);
                    var show = _heroRepo.GetHeroTbl(id);
                    _methodResult.Result = show;
                    return _methodResult;
*/
                    //control try catch
                    _heroRepo.CreateHero(ApiTbl);
                    var ex = _heroRepo.exeption;
                    if (ex == null)
                    {
                        var show1 = _heroRepo.GetHeroTbl(id);
                        _methodResult.Result = show1;
                        return _methodResult;
                    }
                    else

                        _methodResult.Errors = ex;
                    return _methodResult;

                }

            }
            return _methodResult;

            //---------------------------------------------------------------------------
            /*
                        var res = _heroRepo.GetHeroTbl(id);
                        if (res.Result == null)
                        {
                            _heroApiService.userid = id;
                            var ApiTbl = await _heroApiService.Get();
                            if (ApiTbl.id == 0)
                            {
                                var show = _heroRepo.GetHeroTbl(id);
                                return await show;
                            }
                            else
                            {
                                _heroRepo.CreateHero(ApiTbl);
                                var show = _heroRepo.GetHeroTbl(id);
                                return await show;


                            }

                        }
                        return await res;*/
        }
       
        public async Task<Herobio> GetHeroDB(int id)
        {
            //just for checking db
            var show = _heroRepo.GetHeroTbl(id);
            return await show;
        }
        
        public async Task<List<Herobio>> Get()
        {
            return await _heroRepo.GetHeros();
        }
        
        public async Task Create(Herobio hero)
        {
            _heroRepo.CreateHero(hero);
        }
        
        public  Task Delete(int id)
        {
            //This method will check DB at first .if there is a data it will wipe it.
            var res = _heroRepo.GetHeroTbl(id);
            if (res.Result == null)
            {
                return null;
            }
            else
            {
                _heroRepo.DeleteHero(id);
                return _heroRepo.GetHeroTbl(id);
            }
            

        }
        
        public async Task GetHeros()
        {
            _heroRepo.GetHeros();
        }
        
        public async Task<List<Herobio>> Update(Herobio hero)
        {
            //This method will check DB at first .if there is a data it will update it.
            var res = _heroRepo.GetHeroTbl(hero.id);
            if (res.Result == null)
            {
                return null;
            }
            return await _heroRepo.Update(hero);
        }
    }
}
