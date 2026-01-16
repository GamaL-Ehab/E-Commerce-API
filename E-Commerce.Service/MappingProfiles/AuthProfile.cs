using AutoMapper;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Shared.Dtos.Auth;

namespace E_Commerce.Service.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
