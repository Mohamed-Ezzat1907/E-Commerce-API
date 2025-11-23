using E_Commerce.Api.Middlewares;
using E_Commerce.Domain.Contracts;

namespace E_Commerce.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task <WebApplication> SeedDbAsync (this WebApplication app)
        {
            // Create Object From Type That Implement IDbInititlaeizer
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInititlaeizer>();
            await dbInitializer.InitializeAsync();

            return app;
        }

        public static WebApplication UseCustomExceptionMiddleware(this WebApplication app) 
        {
            app.UseMiddleware<GlobalErrorHandelingMiddleware>();

            return app;
        }

        public static WebApplication UseSwaggerMiddleware (this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
