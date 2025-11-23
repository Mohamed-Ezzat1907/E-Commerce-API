using E_Commerce.Api.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Extensions
{
    public static class WebApisServicesExtensions
    {
        public static IServiceCollection AddWebApisServices(this IServiceCollection services) 
        {
            services.AddControllers();
            //ده بيحتوى على ال status code
            //IActionResult اللي مسئول عن انه يرجع 
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
