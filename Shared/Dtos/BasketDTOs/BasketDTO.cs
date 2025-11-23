namespace Shared.Dtos.BasketDTOs
{
    public record BasketDTO
    {
        public string Id { get; init; }
        public IEnumerable<BasketItemDTO> Items { get; init; } = [];
    }
}
