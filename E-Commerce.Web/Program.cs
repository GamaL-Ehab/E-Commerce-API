using E_Commerce.Domain.Contracts;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Middlewares;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAllServices(builder.Configuration);

            var app = builder.Build();

            // Configure all application middlewares
            await app.ConfigureMiddlewaresAsync();

            app.Run();
        }
    }
}
