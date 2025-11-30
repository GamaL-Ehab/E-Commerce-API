using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string> GetAsync(string key)
        {
            var redisValue = await _database.StringGetAsync(key);

            return redisValue;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            await _database.StringSetAsync(key, JsonSerializer.Serialize(value), duration);
        }
    }
}
