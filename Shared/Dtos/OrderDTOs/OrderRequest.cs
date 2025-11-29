namespace Shared.Dtos.OrderDTOs
{
    public record OrderRequest
    {
        public string BasketId { get; init; } = string.Empty;
        public AddressDTO ShipToAddress { get; init; }
        public int DeliveryMethodId { get; init; }
    }
}
