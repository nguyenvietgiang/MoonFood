namespace MoonFood.Common.RedisCache
{
    public interface ICacheService<T>
    {
        Task<T> GetCachedData(string key, Func<Task<T>> getDataFunc, TimeSpan cacheDuration);
    }

}
