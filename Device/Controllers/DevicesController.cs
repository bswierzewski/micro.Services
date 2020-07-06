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
    public class DevicesController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IDeviceRepository _repo;

        public DevicesController(IDeviceRepository repo, ILogger<DevicesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            var device = await _repo.GetDevice(id);

            return Ok(device);
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _repo.GetDevices();

            return Ok(devices);
        }

        [HttpGet("type/{deviceTypeId}")]
        public async Task<IActionResult> GetDevicesByType(int deviceTypeId)
        {
            var devices = await _repo.GetDevices(deviceTypeId);

            return Ok(devices);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDevice(PostDeviceDto postDeviceDto)
        {
            if (!await _repo.ExistsDeviceType(postDeviceDto.DeviceTypeId))
                return StatusCode((int)HttpStatusCode.NotFound, "Device type not exists!");

            if (await _repo.ExistsDevice(postDeviceDto.MacAddress))
                return StatusCode((int)HttpStatusCode.NotFound, "Device already exists!");

            var device = new Database.Entities.Device()
            {
                Created = DateTime.Now,
                DeviceTypeId = postDeviceDto.DeviceTypeId,
                MacAddress = postDeviceDto.MacAddress,
                Name = postDeviceDto.Name ?? postDeviceDto.MacAddress,
            };

            await _repo.Add(device);

            return Ok(device);
        }
    }
}