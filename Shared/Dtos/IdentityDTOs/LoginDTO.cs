using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.IdentityDTOs
{
    public record LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        [Required]
        public string Password { get; init; } = string.Empty;
    }
}
