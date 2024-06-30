namespace UserIdentityService.Services
{
    public class CacheService : ICacheService
    {

        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            await _cache.SetStringAsync(key, value);
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task RemoveCacheValueAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }

}
