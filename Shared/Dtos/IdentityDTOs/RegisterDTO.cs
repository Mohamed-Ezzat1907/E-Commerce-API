using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.IdentityDTOs
{
    public record RegisterDTO
    {
        [Required(ErrorMessage = "DisplayName Is Required")]
        public string DisplayName { get; init; } = string.Empty;
        [Required(ErrorMessage = "DisplayName Is Required")]
        public string UserName { get; init; } = string.Empty;
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; init; } = string.Empty;
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; init; } = string.Empty;
        public string? PhoneNumber { get; init; }
    }
}
