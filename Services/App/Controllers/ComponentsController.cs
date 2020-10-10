using AutoMapper;
using Database.Entities;
using Device.Data.DeviceInfo;
using Device.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Device.Controllers
{
    [Route("api")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {

        private readonly ILogger<ComponentsController> _logger;
        private readonly IMapper _mapper;
        private readonly IDeviceInfoRepository _repo;

        public ComponentsController(IDeviceInfoRepository repo, ILogger<ComponentsController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("components/{id}")]
        public async Task<IActionResult> GetComponent(int id)
        {
            var component = await _repo.Find<Component>(id);

            return Ok(component);
        }

        [HttpGet("components")]
        public async Task<IActionResult> GetComponents()
        {
            var components = await _repo.GetComponents();

            return Ok(components);
        }

        [HttpPost("components")]
        public async Task<IActionResult> AddComponent(ComponentDto componentDto)
        {
            Component component = new Component()
            {
                Created = DateTime.Now,
                CategoryId = componentDto.CategoryId,
                Name = componentDto.Name,
                Icon = componentDto.Icon,
            };

            await _repo.Add(component);

            return Ok(component);
        }

        [HttpPut("components/{id}")]
        public async Task<IActionResult> UpdateComponent(int id, ComponentDto componentDto)
        {

            Component component = await _repo.Find<Component>(id);

            if (component == null)
                return BadRequest("Component not found");

            component.Icon = componentDto.Icon;
            component.CategoryId = componentDto.CategoryId;

            await _repo.SaveAllChangesAsync();

            return Ok(component);
        }


        [HttpDelete("components/{id}")]
        public async Task<IActionResult> DeleteKinds(int id)
        {
            var component = await _repo.Find<Component>(id);

            if (component == null)
                return BadRequest("Component not found");

            await _repo.Delete(component);

            return Ok();
        }
    }
}
