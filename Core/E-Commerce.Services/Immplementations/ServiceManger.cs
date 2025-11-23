using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Services.Abstractions.Contracts;

namespace E_Commerce.Services.Immplementations
{
    public class ServiceManger(IUnitOfWork _unitOfWork , IBasketRepository basketRepository , IMapper _mapper) : IServiceManger
    {
        #region Fields

        private readonly Lazy<IProductService> _productService
           = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));

        private readonly Lazy<IBasketService> _basketService
            = new Lazy<IBasketService>(() => new BasketService(basketRepository, _mapper));

        #endregion

        #region Properties

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;  

        #endregion
    }
}
