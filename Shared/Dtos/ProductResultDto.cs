namespace Shared.Dtos
{
    // Record Special Type Was Introduced in C# 9 to represent Immutable Data Transfer Objects (DTOs) with built-in value-based equality.
    public record ProductResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = default!;
        public string BrandName { get; set; } = null!;
        public string TypeName { get; set; } = null!;
    }
}
