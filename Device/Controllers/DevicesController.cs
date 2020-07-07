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
        public async Task<IActionResult> GetDeviceDto(int id)
        {
            var device = await _repo.GetDeviceDtoById(id);

            return Ok(device);
        }

        [HttpGet]
        public async Task<IActionResult> GetDevicesDto()
        {
            var devices = await _repo.GetDevicesDto();

            return Ok(devices.ToList());
        }

        [HttpGet("type/{deviceTypeId}")]
        public async Task<IActionResult> GetDevicesByType(short deviceTypeId)
        {
            var devices = await _repo.GetDevicesDtoByType(deviceTypeId);

            return Ok(devices);
        }


        [HttpGet("kind/{deviceKindId}")]
        public async Task<IActionResult> GetDevicesByKind(short deviceKindId)
        {
            var devices = await _repo.GetDevicesDtoByKind(deviceKindId);

            return Ok(devices);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDevice(PostDeviceDto postDeviceDto)
        {
            var device = await _repo.GetDevice(postDeviceDto.MacAddress);

            if (device != null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device exists!");

            if (!await _repo.IsDeviceType(postDeviceDto.DeviceTypeId.Value))
                return StatusCode((int)HttpStatusCode.NotFound, "Device type not exists!");

            if (!await _repo.IsDeviceKind(postDeviceDto.DeviceKindId.Value))
                return StatusCode((int)HttpStatusCode.NotFound, "Device kind not exists!");

            var newDevice = new Database.Entities.Device()
            {
                Created = DateTime.Now,
                DeviceTypeId = postDeviceDto.DeviceTypeId,
                MacAddress = postDeviceDto.MacAddress,
                DeviceKindId = postDeviceDto.DeviceKindId,
                Name = string.IsNullOrEmpty(postDeviceDto.Name) ? postDeviceDto.MacAddress : postDeviceDto.Name,
                PhotoUrl = postDeviceDto.PhotoUrl,
                VersionId = postDeviceDto.VersionId
            };

            await _repo.Add(newDevice);

            return Ok(newDevice);

        }
    }
}