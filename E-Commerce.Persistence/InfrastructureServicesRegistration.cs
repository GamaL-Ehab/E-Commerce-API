using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Context;
using E_Commerce.Persistence.DbInitializers;
using E_Commerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace E_Commerce.Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SQLConnection"));
            });

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"))
            );

            return services;
        }
    }
}
