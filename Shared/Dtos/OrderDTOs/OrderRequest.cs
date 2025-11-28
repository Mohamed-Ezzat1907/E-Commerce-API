namespace Shared.Dtos.OrderDTOs
{
    public record OrderRequest
    {
        public string BasketId { get; init; } = string.Empty;
        public AddressDTO ShippingAddress { get; init; }
        public int DeliveryMethodId { get; init; }
    }
}
