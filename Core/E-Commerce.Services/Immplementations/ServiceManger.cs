using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Services.Abstractions.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared;

namespace E_Commerce.Services.Immplementations
{
    public class ServiceManger(IUnitOfWork _unitOfWork ,
        IBasketRepository basketRepository , 
        IMapper _mapper , 
        UserManager<User> userManager,
        IOptions<JwtOptions> options) : IServiceManger
    {
        #region Fields

        private readonly Lazy<IProductService> _productService
           = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));

        private readonly Lazy<IBasketService> _basketService
            = new Lazy<IBasketService>(() => new BasketService(basketRepository, _mapper));

        private readonly Lazy<IAuthenticationService> _authenticationService
            = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager , options , _mapper));

        private readonly Lazy<IOrderService> _orderService
            = new Lazy<IOrderService>(() => new OrderService(_mapper, basketRepository, _unitOfWork));

        #endregion

        #region Properties

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IOrderService OrderService => _orderService.Value;

        #endregion
    }
}
