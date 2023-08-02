using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MoonFood.Common.RedisCache
{
    public class CacheService<T> : ICacheService<T>
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetCachedData(string key, Func<Task<T>> getDataFunc, TimeSpan cacheDuration)
        {
            string cachedData = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedData))
            {
                var data = await getDataFunc();
                var serializedData = JsonSerializer.Serialize(data);

                await _cache.SetStringAsync(key, serializedData, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = cacheDuration
                });

                return data;
            }
            else
            {
                var data = JsonSerializer.Deserialize<T>(cachedData);
                return data;
            }
        }
    }

}
