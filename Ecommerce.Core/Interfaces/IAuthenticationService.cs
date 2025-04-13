using Ecommerce.Core.Models.Identity;

namespace Ecommerce.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> CreateToken(ApplicationUser user);
        RefreshToken GenerateRefreshToken();
        Task<RefreshToken> CreateRefreshToken(string token);
        Task<bool> RevokeToken(string token);
    }
}
