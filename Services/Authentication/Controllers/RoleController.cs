using Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [Route("api")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles
                .Select(r => new
                {
                    r.Id,
                    r.Name,
                })
                .ToListAsync();

            return Ok(roles);
        }

        [HttpPost("roles")]
        public async Task<IActionResult> AddRoles([FromQuery] string roles)
        {
            var tempRoles = roles.Split(",").Select(n => new Role { Name = n }).ToList();

            foreach (var role in tempRoles)
            {
                if (await _roleManager.RoleExistsAsync(role.Name))
                    continue;

                await _roleManager.CreateAsync(role);
            }

            return Ok();
        }

        [HttpDelete("roles")]
        public async Task<IActionResult> DeleteRoles([FromQuery] string roles)
        {
            var rolesFromQuery = roles.Split(",");

            foreach (var roleName in rolesFromQuery)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                    continue;

                var role = await _roleManager.FindByNameAsync(roleName);

                await _roleManager.DeleteAsync(role);
            }

            return Ok();
        }

    }
}