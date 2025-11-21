using Shared;
using Shared.Dtos;

namespace E_Commerce.Services.Abstractions.Contracts
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecParams parameters);

        Task<ProductResultDto> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
    }
}
