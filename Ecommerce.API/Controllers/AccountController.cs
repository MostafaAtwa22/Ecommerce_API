using AutoMapper;
using Ecommerce.API.Dtos;
using Ecommerce.API.Errors;
using Ecommerce.API.Extensions;
using Ecommerce.Core.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Ecommerce.API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmail(HttpContext.User);

            if (user is null)
                return NotFound(new ApiResponse(404));

            return new UserDto
            {
                Email = user?.Email!,
                Token = _tokenService.CreateToken(user!),
                DisplayName = user?.DisplayName!
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
            var user = new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                Email = user.Email!,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
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

            return new UserDto
            {
                Email = user.Email!,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }
    }
}
