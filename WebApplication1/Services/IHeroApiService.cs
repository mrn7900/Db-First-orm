﻿using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IHeroApiService
    {
        public Task<Herobio> Get();
        public int userid { get; set; }

        public string username { get; set; }
    }
}
