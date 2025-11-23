using E_Commerce.Services.Abstractions.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dtos.ProductDTOs;

namespace E_Commerce.Presentations.Controllers
{
   
    public class ProductsController(IServiceManger serviceManger) : ApiControllerBase
    {
        [HttpGet] // GET: BaseUrl/api/Products
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecParams parameters)
            => Ok(await serviceManger.ProductService.GetAllProductsAsync(parameters));

        [HttpGet("Brands")] // GET: BaseUrl/api/Products/Brands
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
            => Ok(await serviceManger.ProductService.GetAllBrandsAsync());

        [HttpGet("Types")] // GET: BaseUrl/api/Products/Types
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
            => Ok(await serviceManger.ProductService.GetAllTypesAsync());

        [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status200OK)]
        [HttpGet("{id:int}")] // GET: BaseUrl/api/Products/{id}
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
            => Ok(await serviceManger.ProductService.GetProductByIdAsync(id));
    }
}
