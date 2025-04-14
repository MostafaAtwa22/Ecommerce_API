using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Dtos
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3), MaxLength(255)]
        public string Username { get; set; } = string.Empty;

        [MinLength(3), MaxLength(255)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
            ErrorMessage = "Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? Role { get; set; } = default!;
    }
}
