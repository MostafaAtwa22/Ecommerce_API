using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Dtos
{
    public class UserAddressDto
    {
        [MinLength(3), MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;

        [MinLength(3), MaxLength(255)]
        public string LastName { get; set; } = string.Empty;

        [MinLength(4), MaxLength(255)]
        public string Street { get; set; } = string.Empty;

        [MinLength(4), MaxLength(255)]
        public string City { get; set; } = string.Empty;

        [MinLength(2), MaxLength(255)]
        public string State { get; set; } = string.Empty;

        [MinLength(3), MaxLength(255)]
        public string Zipcode { get; set; } = string.Empty;
    }
}
