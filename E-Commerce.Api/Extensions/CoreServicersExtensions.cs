using E_Commerce.Services;
using E_Commerce.Services.Abstractions.Contracts;
using E_Commerce.Services.Immplementations;
using Shared;

namespace E_Commerce.Api.Extensions
{
    public static class CoreServicersExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services , IConfiguration configuration)
        {
            // Core Services
            services.AddScoped<IServiceManger, ServiceManger>();
            services.AddAutoMapper(o => { }, typeof(AssemblyReference).Assembly);
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return services;
        }
    }
}
