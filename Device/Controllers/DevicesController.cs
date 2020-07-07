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

        [HttpGet]
        public async Task<IActionResult> GetDevicesDto()
        {
            var devices = await _repo.GetDevicesDto();

            return Ok(devices);
        }

        /// <summary>
        /// Pobiera urządzenie po ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceDto(int id)
        {
            var device = await _repo.GetDeviceDtoById(id);

            return Ok(device);
        }

        /// <summary>
        /// Dodaje nowe urządzenie
        /// </summary>
        /// <param name="addDeviceDto"></param>
        /// <returns></returns>
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

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetDevicesDtoByType(short type)
        {
            var device = await _repo.GetDevicesDtoByType(type);

            return Ok(device);
        }

        [HttpGet("kind/{kind}")]
        public async Task<IActionResult> GetDevicesDtoByKind(short kind)
        {
            var device = await _repo.GetDevicesDtoByKind(kind);

            return Ok(device);
        }

        [HttpPost("update/type/{type}")]
        public async Task<IActionResult> UpdateDeviceKind(UpdateDeviceDto updateDeviceDto)
        {
            var device = await _repo.GetDevice(updateDeviceDto.DeviceId);

            if (device == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device not exists!");

            device.DeviceTypeId = updateDeviceDto.Payload;

            await _repo.SaveAllChanges();

            return Ok();
        }

        [HttpPost("update/kind/{kind}")]
        public async Task<IActionResult> UpdateDeviceType(UpdateDeviceDto updateDeviceDto)
        {
            var device = await _repo.GetDevice(updateDeviceDto.DeviceId);

            if (device == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device not exists!");

            device.DeviceKindId = updateDeviceDto.Payload;

            await _repo.SaveAllChanges();

            return Ok();
        }
    }
}