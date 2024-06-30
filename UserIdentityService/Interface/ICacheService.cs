namespace UserIdentityService.Interface
{
    public interface ICacheService
    {
        Task SetCacheValueAsync(string key, string value);
        Task<string> GetCacheValueAsync(string key);
        Task RemoveCacheValueAsync(string key);
    }
}
