using System.Text.Json;
using Application.Contracts;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace InfraStructure.Repositories;

public class CacheRepository : ICacheRepository
{
    private readonly IDatabase _database;
    private readonly ILogger<CacheRepository> _logger;

    public CacheRepository(IConnectionMultiplexer connection, ILogger<CacheRepository> logger)
    {
        _database = connection.GetDatabase();
        _logger = logger;
    }

    public async Task<string?> GetDataAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            var cacheValue = await _database.StringGetAsync(key);
            return cacheValue.IsNullOrEmpty ? null : cacheValue.ToString();
        }
        catch (RedisConnectionException ex)
        {
            _logger.LogError(ex, "Redis connection failed while getting key {Key}", key);
            return null; 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting key {Key}", key);
            return null;
        }
    }

    public async Task SetDataAsync(string key, string data, TimeSpan ttl, CancellationToken cancellationToken = default)
    {
        try
        {
            await _database.StringSetAsync(key, data, ttl);
        }
        catch (RedisConnectionException ex)
        {
            _logger.LogError(ex, "Redis connection failed while setting key {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while setting key {Key}", key);
        }
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            await _database.KeyDeleteAsync(key);
        }
        catch (RedisConnectionException ex)
        {
            _logger.LogError(ex, "Redis connection failed while removing key {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while removing key {Key}", key);
        }
    }
}