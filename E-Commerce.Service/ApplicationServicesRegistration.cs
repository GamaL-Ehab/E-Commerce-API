using E_Commerce.Service.MappingProfile;
using E_Commerce.Service.Services;
using E_Commerce.Services.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(x => x.AddProfile(new ProductProfile(configuration)));

            return services;
        }

    }
}
