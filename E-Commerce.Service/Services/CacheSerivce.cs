using E_Commerce.Domain.Contracts;
using E_Commerce.Services.Abstraction;

namespace E_Commerce.Service.Services
{
    internal class CacheSerivce(ICacheRepository _cacheRepository) : ICacheSerivce
    {
        public async Task<string> GetAsync(string key)
        {
            var result = await _cacheRepository.GetAsync(key); 

            return result;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            await _cacheRepository.SetAsync(key, value, duration);
        }
    }
}
