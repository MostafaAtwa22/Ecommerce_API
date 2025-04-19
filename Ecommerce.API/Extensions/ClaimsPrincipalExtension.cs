using System.Security.Claims;

namespace Ecommerce.API.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
            => user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value!;
    }
}
