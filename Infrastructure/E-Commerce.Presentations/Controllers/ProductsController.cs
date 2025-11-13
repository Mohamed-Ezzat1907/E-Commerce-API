using E_Commerce.Services.Abstractions.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace E_Commerce.Presentations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpGet] // GET: BaseUrl/api/Products
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProducts()
            => Ok(await serviceManger.ProductService.GetAllProductsAsync());

        [HttpGet("Brands")] // GET: BaseUrl/api/Products/Brands
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
            => Ok(await serviceManger.ProductService.GetAllBrandsAsync());

        [HttpGet("Types")] // GET: BaseUrl/api/Products/Types
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
            => Ok(await serviceManger.ProductService.GetAllTypesAsync());

        [HttpGet("{id : int}")] // GET: BaseUrl/api/Products/{id}
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
            => Ok(await serviceManger.ProductService.GetProductByIdAsync(id));
    }
}
