using AutoMapper;
using Ecommerce.API.Dtos;
using Ecommerce.API.Errors;
using Ecommerce.API.Extensions;
using Ecommerce.Core.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IAuthenticationService authService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmail(HttpContext.User);

            if (user is null)
                return NotFound(new ApiResponse(404));

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Email = user?.Email!,
                Token = await _authService.CreateToken(user!),
                DisplayName = user?.DisplayName!,
                Roles = roles.ToList()
            };
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists([FromBody] string email)
             => await _userManager.FindByEmailAsync(email) is not null;

        [HttpGet("GetAddress")]
        [Authorize]
        public async Task<ActionResult<UserAddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindByEmailWithAddress(HttpContext.User);

            if (user is null)
                return NotFound(new ApiResponse(404));

            return _mapper.Map<Address, UserAddressDto>(user?.Address!);
        }

        [HttpPut("UpdateAddress")]
        [Authorize]
        public async Task<ActionResult<UserAddressDto>> UpdateUserAddress(UserAddressDto dto)
        {
            var user = await _userManager.FindByEmailWithAddress(HttpContext.User);

            if (user is null)
                return NotFound(new ApiResponse(404));

            user.Address = _mapper.Map<UserAddressDto, Address>(dto);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok(_mapper.Map<Address, UserAddressDto>(user.Address));

            return BadRequest("Problem with updating the user address !!");
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExists(registerDto.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = new[]
                    {
                        "Email address already exists"
                    }
                });

            var user = new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400, "Registration failed"));

            await _userManager.AddToRoleAsync(user, registerDto.Role ?? "User");

            var refreshToken = _authService.GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);

            await _userManager.UpdateAsync(user);

            SetRefreshTokenInCookie(refreshToken.Token, refreshToken.ExpiresOn);

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new UserDto
            {
                Email = user.Email!,
                Token = await _authService.CreateToken(user),
                DisplayName = user.DisplayName!,
                Roles = roles.ToList(),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn
            });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            var roles = await _userManager.GetRolesAsync(user);

            var userModel = new UserDto
            {
                Email = user.Email!,
                Token = await _authService.CreateToken(user),
                DisplayName = user.DisplayName!,
                Roles = roles.ToList()
            };

            RefreshToken refreshToken;

            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                refreshToken = user.RefreshTokens.First(t => t.IsActive);
            }
            else
            {
                refreshToken = _authService.GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }

            userModel.RefreshToken = refreshToken.Token;
            userModel.RefreshTokenExpiration = refreshToken.ExpiresOn;

            SetRefreshTokenInCookie(refreshToken.Token, refreshToken.ExpiresOn);

            return Ok(userModel);
        }

        [HttpGet("RefreshToken")]
        public async Task<ActionResult<UserDto>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized(new ApiResponse(401, "Refresh token not found"));

            var newRefreshToken = await _authService.CreateRefreshToken(refreshToken);

            if (newRefreshToken is null)
                return Unauthorized(new ApiResponse(401, "Invalid or expired refresh token"));

            var user = await _userManager.Users
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == newRefreshToken.Token));

            if (user is null)
                return Unauthorized(new ApiResponse(401, "User not found"));

            var roles = await _userManager.GetRolesAsync(user);

            var userModel = new UserDto
            {
                Email = user.Email!,
                DisplayName = user.DisplayName!,
                Token = await _authService.CreateToken(user),
                Roles = roles.ToList(),
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiresOn
            };

            SetRefreshTokenInCookie(newRefreshToken.Token, newRefreshToken.ExpiresOn);

            return Ok(userModel);
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken(RevokeTokenDto dto)
        {
            var token = dto.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new ApiResponse(404));

            var result = await _authService.RevokeToken(token);

            if (!result)
                return BadRequest(new ApiResponse(404));

            return Ok();
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
