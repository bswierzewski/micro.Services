using Authentication.Data;
using Authentication.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var usersToReturn = _mapper.Map<IList<UserForListDto>>(users);

            return Ok(usersToReturn);
        }


        [HttpPost("users/activate")]
        public async Task<IActionResult> ChangeActivateUser(UserDto userDto)
        {
            var user = await _repo.GetUser(userDto.Id);

            if (user == null)
                return BadRequest("User not exists!");

            await _repo.ChangeActivateUser(user);

            return Ok();
        }
    }
}