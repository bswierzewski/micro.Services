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
        public async Task<IActionResult> AddRoles(Role role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name))
                return BadRequest();

            await _roleManager.CreateAsync(role);

            return Ok();
        }

        [HttpDelete("roles/{id}")]
        public async Task<IActionResult> DeleteRoles(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return BadRequest("Role not exists!");

            await _roleManager.DeleteAsync(role);

            return Ok();
        }
    }

}
