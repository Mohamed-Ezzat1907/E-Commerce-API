using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Exceptions;
using E_Commerce.Services.Abstractions.Contracts;
using E_Commerce.Services.Specifications;
using Shared;
using Shared.Dtos.ProductDTOs;

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
        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecParams parameters)
        {
            // 1. Retrive All Products ==> UnitOfWork
            var products = await _unitOfWork.GetReository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeApecifications(parameters));

            // 2. Map Entities To Dtos
            var productsResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);

            var pageSize = productsResult.Count();
            var totalCount = await _unitOfWork.GetReository<Product,int>().CountAsync(new ProductCountSpecifications(parameters));

            // 3. Return The Result
            return new PaginatedResult<ProductResultDto>(parameters.PageIndex, pageSize , totalCount, productsResult);
        }

        // Get Product By Id
        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            // 1. Retrive The Product By Id ==> UnitOfWork
            var product = await _unitOfWork.GetReository<Product , int>().GetByIdAsync(new ProductWithBrandAndTypeApecifications(id));

            // 3. Return The Result
            return product is null ? throw new ProductNotFoundException(id) : _mapper.Map<ProductResultDto>(product);
        }

        #endregion
    }
}
