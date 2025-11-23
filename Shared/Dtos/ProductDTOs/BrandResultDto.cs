namespace Shared.Dtos.ProductDTOs
{
    public record BrandResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
