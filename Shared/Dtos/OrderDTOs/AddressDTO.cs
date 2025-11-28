namespace Shared.Dtos.OrderDTOs
{
    public record AddressDTO
    {
        public string FristName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Street { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
    }
}
