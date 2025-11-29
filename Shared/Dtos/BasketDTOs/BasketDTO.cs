namespace Shared.Dtos.BasketDTOs
{
    public record BasketDTO
    {
        public string Id { get; init; }

        public IEnumerable<BasketItemDTO> Items { get; init; } = [];

        public string? ClientSecret { get; set; }

        public string? PaymentIntentId { get; set; }

        public int? DeliveryMethodId { get; set; }

        public int? ShippingPrice { get; set; }
    }
}
