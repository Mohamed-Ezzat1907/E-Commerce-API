using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_Commerce.Persistence.Data.DBContexts
{
    public class StoreDbContext : DbContext
    {
        #region Constructor

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        #endregion

        #region OverRide Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #endregion

        #region DbSets

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        #endregion
    }
}
