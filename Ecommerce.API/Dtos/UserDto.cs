using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.API.Dtos
{
    public class UserDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
