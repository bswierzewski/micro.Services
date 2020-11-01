using Authentication.Data;
using Authentication.Dtos;
using Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _userManager.Users.AnyAsync(x => x.UserName == userForRegisterDto.Username.ToLower()))
                return BadRequest();

            var user = new User
            {
                UserName = userForRegisterDto.Username,
                Email = userForRegisterDto.Email,
                IsActive = false,
                Created = DateTime.Now,
                LastActive = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == userForLoginDto.Username.ToLower());

            if (user == null)
                user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == userForLoginDto.Username.ToLower());

            if (user == null)
                return Unauthorized("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized();

            if (!user.IsActive)
                return Unauthorized("Account nonactivated");

            user.LastActive = DateTime.Now;

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
            });
        }
    }
}