using Ecommerce.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Ecommerce.API.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<ApplicationUser?> FindByEmailWithAddress(this UserManager<ApplicationUser> input,
            ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value;

            return await input.Users.Include(u => u.Address)
                .SingleOrDefaultAsync(e => e.Email == email);
        }

        public static async Task<ApplicationUser?> FindByEmail(this UserManager<ApplicationUser> input,
            ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value;

            return await input.Users
                .SingleOrDefaultAsync(e => e.Email == email);
        }
    }
}
