using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using Microsoft.Extensions.Caching.Memory;

namespace E_Commerce.Persistence.Repositories
{
    public class BasketRepository(IMemoryCache _cache) : IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string id)
        {
            _cache.TryGetValue(id, out CustomerBasket? basket);
            return Task.FromResult(basket);
        }

        public Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket, TimeSpan duration)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = duration
            };

            _cache.Set(basket.Id, basket, options);

            return Task.FromResult(basket)!;
        }

        public Task<bool> DeleteBasketAsync(string id)
        {
            _cache.Remove(id);
            return Task.FromResult(true);
        }
    }
}
