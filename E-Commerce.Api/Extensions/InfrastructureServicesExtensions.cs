using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Persistence.Data;
using E_Commerce.Persistence.Data.DBContexts;
using E_Commerce.Persistence.Identity;
using E_Commerce.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared;
using StackExchange.Redis;
using System.Text;

namespace E_Commerce.Api.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastrcutureServices(this IServiceCollection services , IConfiguration configuration) 
        {
            services.AddScoped<IDbInititlaeizer, DbInititlaeizer>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddSingleton<IConnectionMultiplexer>(_ => 
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.ConfigureIdentityServices();
            services.ConfigureJwt(configuration);

            return services;
        }

        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(opteions =>
            {
                opteions.Password.RequireDigit = true;
                opteions.Password.RequireLowercase = false;
                opteions.Password.RequireUppercase = false;
                opteions.Password.RequireNonAlphanumeric = false;
                opteions.Password.RequiredLength = 8;
                opteions.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }

        public static IServiceCollection ConfigureJwt(this IServiceCollection services , IConfiguration configuration) 
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                // Log Or Not
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}
