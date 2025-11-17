using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Services.Abstractions.Contracts;
using E_Commerce.Services.Specifications;
using Shared.Dtos;

namespace E_Commerce.Services.Immplementations
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        #region Methods

        // Get All Brands
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            // 1. Retrive All Brands ==> UnitOfWork
            var brands = await _unitOfWork.GetReository<ProductBrand, int>().GetAllAsync();

            // 2. Map Entities To Dtos
            var brandsResult = _mapper.Map<IEnumerable<BrandResultDto>>(brands);

            // 3. Return The Result
            return brandsResult;
        }

        // Get All Types
        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            // 1. Retrive All Types ==> UnitOfWork
            var types = await _unitOfWork.GetReository<ProductType , int>().GetAllAsync();

            // 2. Map Entities To Dtos
            var typesResult = _mapper.Map<IEnumerable<TypeResultDto>>(types);

            // 3. Return The Result
            return typesResult;
        }

        // Get All Products
        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            // 1. Retrive All Products ==> UnitOfWork
            var products = await _unitOfWork.GetReository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeApecifications());

            // 2. Map Entities To Dtos
            var productsResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);

            // 3. Return The Result
            return productsResult;
        }

        // Get Product By Id
        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            // 1. Retrive The Product By Id ==> UnitOfWork
            var product = await _unitOfWork.GetReository<Product , int>().GetByIdAsync(new ProductWithBrandAndTypeApecifications(id));

            // 2. Map Entity To Dto
            var productResult = _mapper.Map<ProductResultDto>(product);

            // 3. Return The Result
            return productResult;
        }

        #endregion
    }
}
