using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModule;
using E_Commerce.Domain.Entities.OrderAggregate;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Domain.Exceptions;
using E_Commerce.Services.Abstractions.Contracts;
using E_Commerce.Services.Specifications;
using Shared.Dtos.OrderDTOs;

namespace E_Commerce.Services.Immplementations
{
    internal class OrderService(IMapper mapper,
        IBasketRepository basketRepository,
        IUnitOfWork unitOfWork) : IOrderService
    {
        #region Methods

        // Create Order
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
        {
            // 1. Shipping Address
            var address = mapper.Map<ShippingAddress>(orderRequest.ShippingAddress);

            // 2. Order Items ==> From BasketId ==> BasketItemts ==> Product Details
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);
            var orderItems = new List<OrderItem>();
            foreach(var item in basket.Items) 
            {
                var product = await unitOfWork.GetReository<Product, int>().GetByIdAsync(item.Id) ??
                    throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(product, item));
            }

            // 3. Delivery Method ==> DeliveryMethodId
            var deliveryMethod = await unitOfWork.GetReository<DeliveryMethod, int>()
                .GetByIdAsync(orderRequest.DeliveryMethodId) ?? throw new DeliveryMethodException(orderRequest.DeliveryMethodId);

            // 4. Subtotal ==> Sum of OrderItems Price * Quantity
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 5. create Order and save to DB
            var order = new Order(userEmail, address, orderItems, deliveryMethod, subTotal);
            await unitOfWork.GetReository<Order , Guid>().AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            // 6. return OrderResult
            return mapper.Map<OrderResult>(order);
        }

        // Get Delivery Methods
        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await unitOfWork.GetReository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }

        // Get Order By Id
        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = await unitOfWork.GetReository<Order , Guid>()
                .GetByIdAsync(new OrderWithIncludeSpecifications(id)) ??
                throw new OrderNotFoundException(id);

            return mapper.Map<OrderResult>(order);
        }

        // Get Orders By User Email
        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail)
        {
            var orders = await unitOfWork.GetReository<Order, Guid>()
                 .GetAllAsync(new OrderWithIncludeSpecifications(userEmail));

            return mapper.Map<IEnumerable<OrderResult>>(orders);
        }

        #endregion

        #region HelperMethods

        private OrderItem CreateOrderItem(Product product, BasketItem item)
            => new OrderItem
            (
                new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), item.Quantity, product.Price
            );

        #endregion
    }
}
