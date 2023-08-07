
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Properties;
using WebApplication1.Repos;

namespace WebApplication1.Services
{
    //داکیومنت روی سواگر ، داکیومنت عادی ، سرویس ردیس
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
           
                var res = await _heroRepo.GetHeroTbl(id);
                _methodResult.Result = res;
                if (res == null)
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

            //start caching check (redis db)
            string cacheKey = "herolist";
            string cachedData = await _cache.GetStringAsync(cacheKey);
           
                // fetch data from database or any other source
                var data1 = await _heroRepo.GetHeros();

                // Store data in cache for future use
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(data1), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) // Set cache expiration time
                });
     
                _methodResult.Result = data;
            return _methodResult;
           
        }

        public async Task<IMethodResult> Get()
        {
            //start caching check (redis db)
            string cacheKey = "herolist";
            string cachedData = await _cache.GetStringAsync(cacheKey);

            if (cachedData != null)
            {
                // Cache hit, return cached data
                _methodResult.Result = cachedData;
                var show = JsonConvert.DeserializeObject<List<Herobio>>(cachedData);
                _methodResult.Result = show;
                return _methodResult;
            }

            // Cache miss, fetch data from database or any other source
            var data = await _heroRepo.GetHeros();


            // Store data in cache for future use
            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(data), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) // Set cache expiration time
            });

            _methodResult.Result = data;

            return _methodResult;

        }


        public async Task<IMethodResult> GetByName(string name)
        {
            var res =  _heroRepo.GetHeroName(name);
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
        
        public async Task Create(Herobio hero)
        {
            _heroRepo.CreateHero(hero);
        }
        
        public async Task<IMethodResult> Delete(int id)
        {
            //This method will check DB at first .if there is a data it will wipe it.
            var res = await _heroRepo.GetHeroTbl(id);
            if (res == null)
            {
                return null;
            }
            else
            {
                _heroRepo.DeleteHero(id);

                //start caching check (redis db)
                string cacheKey = "herolist";
                string cachedData = await _cache.GetStringAsync(cacheKey);

                // fetch data from database or any other source
                var data1 = await _heroRepo.GetHeros();

                // Store data in cache for future use
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(data1), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) // Set cache expiration time
                });
                _methodResult.Result = await _heroRepo.GetHeroTbl(id);
                return _methodResult;
            }
        }
    }
}
