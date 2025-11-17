using E_Commerce.Domain.Entities.ProductModule;
using Shared;

namespace E_Commerce.Services.Specifications
{
    internal class ProductCountSpecifications : BaseSpecifications<Product,int>
    {
        public ProductCountSpecifications(ProductSpecParams parameters) : 
            base(product =>
            (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value) &&
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
            (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))
        {
            
        }
    }
}
