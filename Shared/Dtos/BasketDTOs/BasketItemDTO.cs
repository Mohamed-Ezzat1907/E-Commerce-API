using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.BasketDTOs
{
    public record BasketItemDTO
    {
        public int Id { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;

        [Range(1,double.MaxValue,ErrorMessage ="Price Must Be Greater Than Zero.")]
        public decimal Price { get; init; }

        [Range(1,99,ErrorMessage ="Quantity Must Be At Least One Item.")]
        public int Quantity { get; init; }
    }
}