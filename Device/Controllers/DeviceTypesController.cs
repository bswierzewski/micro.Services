using Database.Entities;
using Device.Data;
using Device.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Device.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceTypesController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IDeviceRepository _repo;
        public DeviceTypesController(IDeviceRepository repo, ILogger<DevicesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeviceTypes()
        {
            var types = await _repo.GetDeviceTypes();

            if (types == null || types.Count() == 0)
                return StatusCode((int)HttpStatusCode.NotFound, "Not found any types!");

            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceType(short typeId)
        {
            var type = await _repo.GetDeviceTypeById(typeId);

            if (type == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Not found any types!");

            return Ok(type);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDeviceType(PostDeviceTypeDto deviceTypeDto)
        {
            var type = await _repo.GetDeviceType(deviceTypeDto.Type);

            if (type != null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device type already exists!");

            var deviceType = new DeviceType()
            {
                Type = deviceTypeDto.Type,
                Created = DateTime.Now,
            };

            await _repo.Add(deviceType);

            return Ok(deviceType);
        }
    }
}
