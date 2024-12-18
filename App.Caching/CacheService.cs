using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public Task AddAsync<T>(string cacheKey, T value, TimeSpan exprTimeSpan)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = exprTimeSpan
        };

        _memoryCache.Set(cacheKey, value, cacheOptions);
        
        return Task.CompletedTask;
    }

    public Task<T?> GetAsync<T>(string cacheKey)
    {
        return Task.FromResult(_memoryCache.TryGetValue(cacheKey, out T cacheItem) ? cacheItem : default(T));
    }

    public Task RemoveAsync<T>(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);

        return Task.CompletedTask;
    }
}