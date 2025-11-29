
using E_Commerce.Api.Extensions;
using E_Commerce.Api.Factories;
using E_Commerce.Api.Middlewares;
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data;
using E_Commerce.Persistence.Data.DBContexts;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Services;
using E_Commerce.Services.Abstractions.Contracts;
using E_Commerce.Services.Immplementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services Configurations - Dependency injection

            // Add services to the container.

            // WebApis Services 
            builder.Services.AddWebApisServices();

            // Infrastructure Services
            builder.Services.AddInfrastrcutureServices(builder.Configuration);

            // Core Services
            builder.Services.AddCoreServices(builder.Configuration);

            #endregion

            var app = builder.Build();

            #region Middle Wares

            // Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddleware();

            await app.SeedDbAsync();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CORSPoleicy");

            app.UseAuthorization();

            app.UseAuthorization();

            app.MapControllers(); 

            #endregion

            app.Run();
        }
    }
}
