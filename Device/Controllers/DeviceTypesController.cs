using Database.Entities;
using Device.Data;
using Device.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly IDeviceTypeRepository _repo;
        public DeviceTypesController(IDeviceTypeRepository repo, ILogger<DevicesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeviceTypes()
        {
            var types = await _repo.GetDeviceTypes();

            return Ok(types);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDeviceType(AddDeviceTypeDto deviceTypeDto)
        {
            if (await _repo.IsDeviceType(deviceTypeDto.Type))
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
