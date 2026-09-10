using System.Text.Json;
using Application.Contracts;

namespace InfraStructure.Caching;

public class CacheService(ICacheRepository cacheRepository) : ICacheService
{
    
    public Task<string?> GetDataAsync(string cacheKey)
    {
        return cacheRepository.GetDataAsync(cacheKey);
    }

    public Task SetDataAsync(string cacheKey, object cacheValue, TimeSpan timeToLive)
    {
        var cacheData = JsonSerializer.Serialize(cacheValue);
        return cacheRepository.SetDataAsync(cacheKey, cacheData, timeToLive);
    }
}