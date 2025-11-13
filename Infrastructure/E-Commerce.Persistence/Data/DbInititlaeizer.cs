using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Persistence.Data.DBContexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace E_Commerce.Persistence.Data
{
    public class DbInititlaeizer : IDbInititlaeizer
    {
        #region Field

        private readonly StoreDbContext _dbContext;

        #endregion

        #region Constructor

        public DbInititlaeizer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        public async Task InitializeAsync()
        {
            try
            {
                // Apply any pending migrations
                // Create Database if it does not exist & Apply Any Pending Migrations
                if (_dbContext.Database.GetPendingMigrations().Any())
                    await _dbContext.Database.MigrateAsync();

                // Seed Initial Data
                // 1. Seed Product Brands
                if (!_dbContext.ProductBrands.Any())
                {
                    // Read Data from Json File and Deserialize
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\E-Commerce.Persistence\Data\Seedings\brands.json");

                    // Transform Json Data to C# Object
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    // Add Data to Database & Save Changes
                    if (brands != null && brands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(brands);
                }

                // 2. Seed Product Types
                if (!_dbContext.ProductTypes.Any())
                {
                    // Read Data from Json File and Deserialize
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\E-Commerce.Persistence\Data\Seedings\types.json");

                    // Transform Json Data to C# Object
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // Add Data to Database & Save Changes
                    if (types != null && types.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(types);
                }

                // 3. Seed Products
                if (!_dbContext.Products.Any())
                {
                    // Read Data from Json File and Deserialize
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\E-Commerce.Persistence\Data\Seedings\products.json");

                    // Transform Json Data to C# Object
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // Add Data to Database & Save Changes
                    if (products != null && products.Any())
                        await _dbContext.Products.AddRangeAsync(products);
                }
                // Save All Changes to Database
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        } 

        #endregion
    }
}
