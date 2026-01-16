using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Persistence;
using E_Commerce.Persistence.Identity.Contexts;
using E_Commerce.Service;
using E_Commerce.Shared;
using E_Commerce.Shared.ErrorModels;
using E_Commerce.Web.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.Web.Extensions
{
    public static class Extension
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddInfrastructureServices(configuration);

            services.AddApplicationServices(configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Any())
                                                         .Select(M => new ValidationError()
                                                         {
                                                             Field = M.Key,
                                                             Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                                                         }).ToList();

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //Add Identity Service
            services.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<IdentityStoreDbContext>();

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateActor = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
                };
            });

            services.AddCors(opt => 
            {
                opt.AddPolicy("AllowAll", policy => 
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            return services;
        }


        public static async Task<WebApplication> ConfigureMiddlewaresAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializeAsync();
            await initializer.InitializeIdentityAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
