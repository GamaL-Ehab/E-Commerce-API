using E_Commerce.Shared.Dtos.Baskets;

namespace E_Commerce.Services.Abstraction
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string id);
        Task<BasketDto?> CreateBasketAsync(BasketDto basket, TimeSpan duration);
        Task<bool> DeleteBasketAsync(string id);
    }
}
