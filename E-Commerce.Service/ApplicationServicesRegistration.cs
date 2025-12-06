using E_Commerce.Service.MappingProfile;
using E_Commerce.Service.Services;
using E_Commerce.Services.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ICacheSerivce, CacheSerivce>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddAutoMapper(x => x.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(x => x.AddProfile(new BasketProfile()));

            return services;
        }

    }
}
