using E_Commerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Persistence.Context.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                   .HasColumnType("varchar")
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(p => p.Description)
                   .HasColumnType("varchar")
                   .HasMaxLength(1024);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(p => p.PictureUrl)
                   .HasColumnType("varchar")
                   .HasMaxLength(256);

            builder.HasOne(p => p.ProductBrand)
                   .WithMany()
                   .HasForeignKey(p => p.BrandId)
                   .IsRequired();

            builder.HasOne(p => p.ProductType)
                   .WithMany()
                   .HasForeignKey(p => p.TypeId)
                   .IsRequired();
        }
    }
}
