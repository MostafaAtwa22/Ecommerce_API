using Ecommerce.Core.Models.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IConfiguration config,
            IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]!));
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.DisplayName!),
            };

            SigningCredentials signingCredentials = new SigningCredentials(
                _key,
                SecurityAlgorithms.HmacSha512Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signingCredentials,
                Issuer = _config["JWT:ValidIssuer"],
                Audience = _config["JWT:ValidAudiance"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            RandomNumberGenerator.Fill(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(2),
                CreatedOn = DateTime.UtcNow,
            };
        }

        public void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
