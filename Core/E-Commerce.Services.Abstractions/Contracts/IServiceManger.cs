namespace E_Commerce.Services.Abstractions.Contracts
{
    public interface IServiceManger
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IAuthenticationService AuthenticationService { get; }

        public IOrderService OrderService { get; }
    }
}
