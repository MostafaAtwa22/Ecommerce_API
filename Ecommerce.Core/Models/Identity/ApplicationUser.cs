using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Core.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
        public Address Address { get; set; } = new Address();
    }
}
