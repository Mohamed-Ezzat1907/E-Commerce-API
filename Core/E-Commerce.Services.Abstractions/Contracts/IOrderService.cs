using Shared.Dtos.OrderDTOs;

namespace E_Commerce.Services.Abstractions.Contracts
{
    public interface IOrderService
    {
        // Get order by ID (Guid id) ==> return OrderResult
        public Task<OrderResult> GetOrderByIdAsync(Guid id);

        // Get all orders for a user (string userEmail) ==> return ICollection<OrderResult>
        public Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail);

        // Create order (OrderRequest(BasketId , DeliveryMethodId, ShippingAddress) , string userEmail) ==> return OrderResult
        public Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail);

        // Get all delivery methods ==> return ICollection<DeliveryMethodDTO>
        public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
