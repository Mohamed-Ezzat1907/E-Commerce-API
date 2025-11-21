
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data;
using E_Commerce.Persistence.Data.DBContexts;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Services;
using E_Commerce.Services.Abstractions.Contracts;
using E_Commerce.Services.Immplementations;
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

            builder.Services.AddControllers();
            builder.Services.AddScoped<IDbInititlaeizer, DbInititlaeizer>();
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            builder.Services.AddAutoMapper(o => { } , typeof(AssemblyReference).Assembly);
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #endregion

            var app = builder.Build();

            await InitializeDbAsync(app);

            #region Middle Wares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers(); 

            #endregion

            app.Run();

            #region Methods

            async Task InitializeDbAsync(WebApplication app) 
            {
                // Create Object From Type That Implement IDbInititlaeizer
                using var scope = app.Services.CreateScope();
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInititlaeizer>();
                await dbInitializer.InitializeAsync();
            }

            #endregion
        }
    }
}
