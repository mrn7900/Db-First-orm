
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
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
        private readonly IRedisCacheService _redisCache;
        public HeroService(IHeroRepo heroRepo, IHeroApiService heroApiService , IMethodResult methodResult , IDistributedCache cache , IRedisCacheService Rcache )
        {
            _heroApiService = heroApiService;
            _heroRepo = heroRepo;
            _methodResult = methodResult;
            _cache = cache;
            _redisCache = Rcache;
        }
        //IMethodResult service will return a Object and a string(for error) 
        public async Task<IMethodResult> GetHero(int id)
        {
            //first the method will search redis for cache if the cache was null ,it will search db, if there was requested (by Id) data it will return it else it will use incoming Api to get and set data in database
            //start caching check (redis db)
            string cacheKey = "herolist";
            string cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            { // Cache hit, return cached data

                var showcache = JsonConvert.DeserializeObject<Herobio>(cachedData);
                if (showcache != null)
                {
                    if (showcache.id == id)
                    {
                        _methodResult.Result = showcache;
                        return _methodResult;
                    }
                }

            }
           
            
                var res = await _heroRepo.GetHeroTbl(id);
                await _redisCache.cachebyid(id);
                _methodResult.Result = res;
                if (res == null)
                {
                    _heroApiService.userid = id;
                    var ApiTbl = await _heroApiService.Get();
                    if (ApiTbl.id == 0)
                    {
                        var show = await _heroRepo.GetHeroTbl(id);
                        _methodResult.Result = show;
                        return _methodResult;

                    }
                    else
                    {
                        //control try catch
                        _heroRepo.CreateHero(ApiTbl);
                        await _redisCache.cachebyid(id);
                        var ex = _heroRepo.exeption;
                        if (ex == null)
                        {
                            var show1 = await _heroRepo.GetHeroTbl(id);
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
          

        public async Task<IMethodResult> Update(Herobio hero)
        {
            //This method will check DB at first .if there is a data it will update it.

            var res = await _heroRepo.GetHeroTbl(hero.id);
            if (res == null)
            {
                return null;
            }
            var data = await _heroRepo.Update(hero);

          
            await _redisCache.cachebyid(hero.id);

            _methodResult.Result = data;
            return _methodResult;
           
        }

        public async Task<IMethodResult> Get()
        {
           
            var data = await _heroRepo.GetHeros();

            _methodResult.Result = data;

            return _methodResult;

        }


        public async Task<IMethodResult> GetByName(string name)
        {
            var res = await _heroRepo.GetHeroName(name);
            if(res.Count != 0)
            {
                await _redisCache.cachebyname(name);
                _methodResult.Result = res;
            }

            else
            if (res.Count == 0)
            {
                //returns error => not found in db
               
                var ex = _heroRepo.exeption;
                _methodResult.Errors = ex;
                return _methodResult;

            }
            return _methodResult;
        }
       
        public async Task<Herobio> GetHeroDB(int id)
        {
            //get by id ,just for checking db not online api
            var show = _heroRepo.GetHeroTbl(id);
            return await show;
        }

        public async Task Create(Herobio hero)
        {
            _heroRepo.CreateHero(hero);
            await _redisCache.cachebyid(hero.id);
        }

        public async Task Delete(int id)
        {
            //This method will check DB at first .if there is a data it will wipe it.
            
             _heroRepo.DeleteHero(id);
            string cacheKey = "herolist";
           await _cache.RemoveAsync(cacheKey);
        }
    }
}
