using E_Commerce.Services;
using E_Commerce.Services.Abstractions.Contracts;
using E_Commerce.Services.Immplementations;

namespace E_Commerce.Api.Extensions
{
    public static class CoreServicersExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // Core Services
            services.AddScoped<IServiceManger, ServiceManger>();
            services.AddAutoMapper(o => { }, typeof(AssemblyReference).Assembly);
            return services;
        }
    }
}
