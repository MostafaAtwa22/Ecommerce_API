using Ecommerce.Core.Models.Identity;

namespace Ecommerce.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
        RefreshToken CreateRefreshToken();
        void SetRefreshTokenInCookie(string refreshToken, DateTime expires);
    }
}
