using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data;
using E_Commerce.Persistence.Data.DBContexts;
using E_Commerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

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

            return services;
        }
    }
}
