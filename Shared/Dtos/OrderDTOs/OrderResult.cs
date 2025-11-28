namespace Shared.Dtos.OrderDTOs
{
    public record OrderResult
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; } = string.Empty;
        public AddressDTO ShippingAdress { get; init; }
        public ICollection<OrderItemDTO> OrderItems { get; init; } = new List<OrderItemDTO>();

        public string PaymentStatus { get; init; } = string.Empty;
        public string DeliveryMethod { get; init; } = string.Empty;

        public int? DeliveryMethodID { get; init; }

        // Subtotal = Sum(OrderItem.price * OrderItem.quantity)
        public decimal Subtotal { get; init; }
        public string PaymentIntentId { get; init; } = string.Empty;

        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.UtcNow;

        // Total = Subtotal + DeliveryMethod.Price (Derived Attribute) 
        public decimal Total { get; init; }
    }
}
