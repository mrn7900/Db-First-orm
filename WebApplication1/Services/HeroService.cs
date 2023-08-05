
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache _cache;
        public HeroService(IHeroRepo heroRepo, IHeroApiService heroApiService , IMethodResult methodResult , IDistributedCache cache)
        {
            _heroApiService = heroApiService;
            _heroRepo = heroRepo;
            _methodResult = methodResult;
            _cache = cache;
        }
        //IMethodResult service will return a Object and a string(for error) 
        public async Task<IMethodResult> GetHero(int id)
        {
            //The method will search db, if there was requested (by Id) data it will return it else it will use incoming Api to get and set data in database
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
        }

        public async Task<IMethodResult> GetByName(string name)
        {
            var res = _heroRepo.GetHeroName(name);
            if(res.Result.Count != 0)
            _methodResult.Result = res;
            else
            if (res.Result.Count == 0)
            {
                //returns error => not found in db
                var show = _heroRepo.GetHeroName(name);
                var ex = _heroRepo.exeption;
                _methodResult.Errors = ex;
                return _methodResult;
                /*  _heroApiService.username = name;
                  var ApiTbl = await _heroApiService.Get();
                  if (ApiTbl.id == 0)
                  {
                      var show = _heroRepo.GetHeroName(name);
                      var ex = _heroRepo.exeption;
                      _methodResult.Errors = ex;
                      return _methodResult;

                  }*/
                /*  else
                  {
                      //control try catch
                      _heroRepo.CreateHero(ApiTbl);
                      var ex = _heroRepo.exeption;
                      if (ex == null)
                      {
                          var show1 = _heroRepo.GetHeroName(name);
                          _methodResult.Result = show1;
                          return _methodResult;
                      }
                      else

                          _methodResult.Errors = ex;
                      return _methodResult;
                  }*/
            }
            return _methodResult;
        }
       
        public async Task<Herobio> GetHeroDB(int id)
        {
            //just for checking db
            var show = _heroRepo.GetHeroTbl(id);
            return await show;
        }
        
/*        public async Task<List<Herobio>> Get()*/
        public async Task<IMethodResult> Get()
        {
            //first checking if cached data exist 
            string cacheKey = "HeroTable";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                _methodResult.Result = cachedData;
                return _methodResult;
            }
            var data = await _heroRepo.GetHeros();
            string stres = String.Join(",", data);
            // Store data in cache for future use
            //check for bug
            await _cache.SetStringAsync(cacheKey, stres);

            _methodResult.Result = stres;
            return _methodResult;

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
