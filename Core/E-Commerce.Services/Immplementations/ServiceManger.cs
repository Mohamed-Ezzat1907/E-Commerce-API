using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Services.Abstractions.Contracts;

namespace E_Commerce.Services.Immplementations
{
    public class ServiceManger(IUnitOfWork _unitOfWork , IMapper _mapper) : IServiceManger
    {
        private readonly Lazy<IProductService> _productService
            = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _productService.Value;
    }
}
