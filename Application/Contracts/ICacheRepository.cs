namespace Application.Contracts;

public interface ICacheRepository
{
    // get data from cache
    public Task<string?> GetDataAsync(string key , CancellationToken cancellationToken = default);
    
    // set data in cache
    public Task SetDataAsync(string key, string data , TimeSpan ttl , CancellationToken cancellationToken = default);
    
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

}