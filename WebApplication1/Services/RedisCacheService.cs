﻿using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Repos;

namespace WebApplication1.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IHeroRepo _heroRepo;
        public RedisCacheService(IHeroRepo heroRepo, IDistributedCache cache)
        {
            _heroRepo = heroRepo;
            _cache = cache;
        }
         public async Task cachebyid(int id)
        {
            //start caching check (redis db)
            string cacheKey = "herolist";
            string cachedData = await _cache.GetStringAsync(cacheKey);

            // fetch data from database or any other source
            var data1 = await _heroRepo.GetHeroTbl(id);

            // Store data in cache for future use
            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(data1), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) // Set cache expiration time
            });
        }
        public async Task cachebyname(string name)
        {
            //start caching check (redis db)
            string cacheKey = "herolist";
            string cachedData = await _cache.GetStringAsync(cacheKey);

            // fetch data from database or any other source
            var data1 = await _heroRepo.GetHeroName(name);

            // Store data in cache for future use
            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(data1), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) // Set cache expiration time
            });
        }
       
    }
}
