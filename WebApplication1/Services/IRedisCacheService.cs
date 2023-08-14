namespace WebApplication1.Services
{
    public interface IRedisCacheService
    {
        public Task cachebyid(int id);
        public Task cachebyname(string name);
    }
}
