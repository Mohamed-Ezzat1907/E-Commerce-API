using E_Commerce.Domain.Entities.ProductModule;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithBrandAndTypeApecifications : BaseSpecifications<Product, int>
    {
        // Get All Products With Brands And Types
        // _dbContext.Set<Product>.Include(p => p.ProductBrand).Include(p => p.ProductType)
        public ProductWithBrandAndTypeApecifications() : base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        // CTOR for Get Product By Id With Brand And Type
        public ProductWithBrandAndTypeApecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
