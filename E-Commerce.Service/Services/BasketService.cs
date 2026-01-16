using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using E_Commerce.Domain.Exceptions.BadRequest;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Services.Abstraction;
using E_Commerce.Shared.Dtos.Baskets;

namespace E_Commerce.Service.Services
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            if (basket is null) throw new BasketNotFoundException(id);

            var result = _mapper.Map<BasketDto>(basket);

            return result;
        }
        public async Task<BasketDto?> CreateBasketAsync(BasketDto basketDto, TimeSpan duration)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);
            var result = await _basketRepository.CreateBasketAsync(basket, duration);
            if (result is null) throw new CreateOrUpdateBasketBadRequestException();

            return _mapper.Map<BasketDto>(result);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            var isDeleted = await _basketRepository.DeleteBasketAsync(id);
            if (!isDeleted) throw new DeleteBasketBadRequestException();

            return isDeleted;
        }

    }
}
