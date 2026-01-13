using E_Commerce.Shared.Dtos.Baskets;

namespace E_Commerce.Services.Abstraction
{
    public interface IPaymentService
    {
        Task<BasketDto> CreatePaymentIntentAsync(string basketId);
    }
}
