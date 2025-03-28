using Ecommerce.API.Dtos;
using Ecommerce.API.Errors;
using Ecommerce.Core.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : BaseAPIController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<RoleDetailsDto>>> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var roleDetails = await Task.WhenAll(roles.Select(async role => new RoleDetailsDto
            {
                Id = role.Id,
                Name = role.Name!,
                TotalUsers = (await _userManager.GetUsersInRoleAsync(role.Name!)).Count
            }));

            return Ok(roleDetails);
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult<RoleDetailsDto>> Details(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound(new ApiResponse(404));

            var dto = new RoleDetailsDto
            {
                Id = role.Id,
                Name = role.Name!,
                TotalUsers = (await _userManager.GetUsersInRoleAsync(role.Name!)).Count
            };

            return Ok(dto);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<RoleDetailsDto>> Create(CreateRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid input"));

            var roleExists = await _roleManager.RoleExistsAsync(dto.RoleName);
            if (roleExists)
                return BadRequest(new ApiResponse(400, "Role already exists"));

            var role = new IdentityRole(dto.RoleName);
            var roleResult = await _roleManager.CreateAsync(role);

            if (roleResult.Succeeded)
                return Ok(new RoleDetailsDto { Id = role.Id, Name = role.Name!, TotalUsers = 0 });

            return BadRequest(new ApiResponse(400, "Failed to create role"));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound(new ApiResponse(404));

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
            if (usersInRole.Any())
                return BadRequest(new ApiResponse(400, "Cannot delete role assigned to users"));

            var res = await _roleManager.DeleteAsync(role);
            if (res.Succeeded)
                return Ok(new { Message = "Role deleted successfully" });

            return BadRequest(new ApiResponse(400, "Failed to delete role"));
        }

        [HttpPost("Assign")]
        public async Task<ActionResult<RoleAssignDto>> Assign(RoleAssignDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user is null)
                return NotFound(new ApiResponse(404));

            var role = await _roleManager.FindByIdAsync(dto.RoleId);
            if (role is null)
                return NotFound(new ApiResponse(404));

            if (await _userManager.IsInRoleAsync(user, role.Name!))
                return BadRequest(new ApiResponse(400, "User is already in this role"));

            var result = await _userManager.AddToRoleAsync(user, role.Name!);
            if (result.Succeeded)
                return Ok(dto);

            return BadRequest(new ApiResponse(400, "Failed to assign role"));
        }
    }
}