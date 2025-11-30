using AutoMapper;
using E_Commerce.Domain.Entities.Baskets;
using E_Commerce.Shared.Dtos.Baskets;

namespace E_Commerce.Service.MappingProfile
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
