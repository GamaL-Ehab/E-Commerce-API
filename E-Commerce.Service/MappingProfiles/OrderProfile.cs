using AutoMapper;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Shared.Dtos.Orders;

namespace E_Commerce.Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Product.PictureUrl));

            CreateMap<DeliveryMethod, DeliveryMethodResponse>();
        }
    }
}
