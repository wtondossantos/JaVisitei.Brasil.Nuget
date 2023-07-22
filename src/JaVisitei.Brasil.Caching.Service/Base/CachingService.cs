using Microsoft.Extensions.Caching.Distributed;

namespace JaVisitei.Brasil.Caching.Service.Base
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _distributedCache;

        public CachingService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<string> GetAsync(string key)
        {
            return await _distributedCache.GetStringAsync(key) ?? string.Empty;
        }

        public async Task SetAsync(string key, string value, double expiration)
        {
            await _distributedCache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiration)
            });
        }
    }
}
