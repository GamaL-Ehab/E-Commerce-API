//using E_Commerce.Domain.Contracts;
//using StackExchange.Redis;
//using System.Text.Json;

//namespace E_Commerce.Persistence.Repositories
//{
//    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
//    {
//        private readonly IDatabase _database = connection.GetDatabase();
//        public async Task<string> GetAsync(string key)
//        {
//            var redisValue = await _database.StringGetAsync(key);

//            return redisValue;
//        }

//        public async Task SetAsync(string key, object value, TimeSpan duration)
//        {
//            await _database.StringSetAsync(key, JsonSerializer.Serialize(value), duration);
//        }
//    }
//}

using E_Commerce.Domain.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

public class CacheRepository : ICacheRepository
{
    private readonly IMemoryCache _cache;

    public CacheRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<string?> GetAsync(string key)
    {
        if (_cache.TryGetValue(key, out string? value))
            return Task.FromResult(value);

        return Task.FromResult<string?>(null);
    }

    public Task SetAsync(string key, object value, TimeSpan duration)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = duration
        };

        var serialized = JsonSerializer.Serialize(value);
        _cache.Set(key, serialized, options);

        return Task.CompletedTask;
    }
}
