using E_Commerce.Domain.Entities.ProductModule;
using Shared;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithBrandAndTypeApecifications : BaseSpecifications<Product, int>
    {
        // Get All Products With Brands And Types
        // _dbContext.Set<Product>.Include(p => p.ProductBrand).Include(p => p.ProductType)
        public ProductWithBrandAndTypeApecifications(ProductSpecParams parameters) : 
            base(product =>
            (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value) &&
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value)&&
            (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            // Sorting
            switch (parameters.Sort) 
            {
                case ProductSortingOptions.NameAsc:
                    SetOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    SetOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    SetOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    SetOrderByDescending(p => p.Price);
                    break;
                default:
                    SetOrderBy(p => p.Name);
                    break;
            }

            // Pagination
            ApplyPagination(parameters.PageIndex, parameters.PageSize);
        }

        // CTOR for Get Product By Id With Brand And Type
        public ProductWithBrandAndTypeApecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
