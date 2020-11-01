using Authentication.Data;
using Authentication.Dtos;
using AutoMapper;
using Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    u.Email,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList(),
                    u.IsActive,
                    u.Created,
                    u.LastActive,
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userManager.Users
                .Where(x => x.Id == id)
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    u.Email,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList(),
                    u.IsActive,
                    u.Created,
                    u.LastActive,
                })
                .FirstOrDefaultAsync();

            return Ok(user);
        }

        [HttpPut("users")]
        public async Task<ActionResult> UpdateUser(UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());

            if (user == null)
                return NotFound("Could not find user");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, userDto.Roles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(userDto.Roles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove from roles");

            user.IsActive = userDto.IsActive;

            await _userManager.UpdateAsync(user);

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [HttpDelete("users/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return BadRequest();

            return Ok(await _userManager.DeleteAsync(user));
        }
    }
}