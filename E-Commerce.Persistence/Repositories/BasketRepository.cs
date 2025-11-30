using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var redisValue = await _database.StringGetAsync(id);
            if(redisValue.IsNullOrEmpty) return null;

            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);
            if(basket is null) return null;

            return basket;
        }
        public async Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket, TimeSpan duration)
        {
            var redisValue = JsonSerializer.Serialize(basket);

            var isCreated = await _database.StringSetAsync(basket.Id, redisValue, duration);
            if (!isCreated) return null;

            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);        
        }
    }
}
