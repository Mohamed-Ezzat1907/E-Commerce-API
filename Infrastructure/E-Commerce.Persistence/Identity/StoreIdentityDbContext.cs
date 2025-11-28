using E_Commerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Identity
{
    public class StoreIdentityDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>()
                   .ToTable("Addresses");
        }
    }
}
