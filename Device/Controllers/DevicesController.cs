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

            return Ok(devices.ToList());
        }

        [HttpGet("type/{deviceTypeId}")]
        public async Task<IActionResult> GetDevicesByType(short deviceTypeId)
        {
            var devices = await _repo.GetDevices(deviceTypeId);

            return Ok(devices);
        }


        [HttpGet("kind/{deviceKindId}")]
        public async Task<IActionResult> GetDevicesByKind(short deviceKindId)
        {
            var devices = await _repo.GetDevices(deviceKindId: deviceKindId);

            return Ok(devices);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDevice(PostDeviceDto postDeviceDto)
        {
            var device = await _repo.GetDeviceByMacAddress(postDeviceDto.MacAddress);

            if (device != null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device exists!");

            if (!await _repo.ExistsDeviceType(postDeviceDto.DeviceTypeId.Value))
                return StatusCode((int)HttpStatusCode.NotFound, "Device type not exists!");

            if (!await _repo.ExistsDeviceKind(postDeviceDto.DeviceKindId.Value))
                return StatusCode((int)HttpStatusCode.NotFound, "Device kind not exists!");

            var newDevice = new Database.Entities.Device()
                {
                    Created = DateTime.Now,
                    DeviceTypeId = postDeviceDto.DeviceTypeId,
                    MacAddress = postDeviceDto.MacAddress,
                    DeviceKindId = postDeviceDto.DeviceKindId,
                    Name = postDeviceDto.Name ?? postDeviceDto.MacAddress,
                    PhotoUrl = postDeviceDto.PhotoUrl,
                    VersionId = postDeviceDto.VersionId
                };

                await _repo.Add(newDevice);

                return Ok(newDevice);
            
        }
    }
}