using Shared.Dtos;

namespace E_Commerce.Services.Abstractions.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();

        Task<ProductResultDto> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
    }
}
