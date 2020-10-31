using Authentication.Data;
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
                    Email = u.Email,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList(),
                    u.IsActive
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
                    Email = u.Email,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList(),
                    u.IsActive
                })
                .FirstOrDefaultAsync();

            return Ok(user);
        }

        [HttpPost("users/edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",");

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return NotFound("Could not find user");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove from roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [HttpPost("users/edit-activate/{username}")]
        public async Task<ActionResult> EditActivated(string username, [FromQuery] string activated)
        {
            if (!bool.TryParse(activated, out bool activate))
                return BadRequest("Bad activated argument");

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return NotFound("Could not find user");

            user.IsActive = activate;

            await _userManager.UpdateAsync(user);

            return Ok();
        }
    }
}