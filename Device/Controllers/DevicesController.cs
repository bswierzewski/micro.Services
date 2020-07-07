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
        public async Task<IActionResult> GetDeviceDto(int id)
        {
            var device = await _repo.GetDeviceDtoById(id);

            return Ok(device);
        }

        [HttpGet]
        public async Task<IActionResult> GetDevicesDto()
        {
            var devices = await _repo.GetDevicesDto();

            return Ok(devices);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDevice(AddDeviceDto addDeviceDto)
        {
            if (await _repo.IsDevice(addDeviceDto.MacAddress))
                return StatusCode((int)HttpStatusCode.NotFound, "Device exists!");

            var newDevice = new Database.Entities.Device()
            {
                Created = DateTime.Now,
                DeviceTypeId = addDeviceDto.DeviceTypeId,
                MacAddress = addDeviceDto.MacAddress,
                DeviceKindId = addDeviceDto.DeviceKindId,
                Name = addDeviceDto.Name,
                PhotoUrl = addDeviceDto.PhotoUrl,
                VersionId = addDeviceDto.VersionId
            };

            await _repo.Add(newDevice);

            return Ok(newDevice);

        }
    }
}