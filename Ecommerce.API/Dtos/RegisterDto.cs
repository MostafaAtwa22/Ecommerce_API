using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? Role { get; set; } = default!;
    }
}
