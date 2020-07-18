using Device.Data;
using Device.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var devices = await _repo.GetDevices().ToListAsync();

            return Ok(devices);
        }

        /// <summary>
        /// Pobiera urz¹dzenie po ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceDto(int id)
        {
            var device = await _repo.GetDevices(expressionDevice: n => n.Id == id).FirstOrDefaultAsync();

            return Ok(device);
        }

        /// <summary>
        /// Dodaje nowe urz¹dzenie
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

        [HttpGet("types/{typeId}")]
        public async Task<IActionResult> GetDevicesDtoByType(short? typeId)
        {
            if (typeId == 0)
                typeId = null;

            var device = await _repo.GetDevices(expressionDevice: n => n.DeviceTypeId == typeId).ToListAsync();

            return Ok(device);
        }

        [HttpGet("kinds/{kindId}")]
        public async Task<IActionResult> GetDevicesDtoByKind(short? kindId)
        {
            if (kindId == 0)
                kindId = null;

            var device = await _repo.GetDevices(expressionDevice: n => n.DeviceKindId == kindId).ToListAsync();

            return Ok(device);
        }

        [HttpPost("{id}/update")]
        public async Task<IActionResult> UpdateDeviceKind(int id, UpdateDeviceDto updateDeviceDto)
        {
            var device = await _repo.GetDevice(id);

            if (device == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device not exists!");

            device.Modified = DateTime.Now;

            if (updateDeviceDto.TypeId != null && await _repo.IsDeviceType(updateDeviceDto.TypeId.Value))
                device.DeviceTypeId = updateDeviceDto.TypeId;

            if (updateDeviceDto.KindId != null && await _repo.IsDeviceKind(updateDeviceDto.KindId.Value))
                device.DeviceKindId = updateDeviceDto.KindId;

            if (!string.IsNullOrEmpty(updateDeviceDto.Name))
                device.Name = updateDeviceDto.Name;

            if (updateDeviceDto.VersionId != null)
                device.VersionId = updateDeviceDto.VersionId;

            if (await _repo.SaveAllChanges())
                return Ok(device);
            else
                return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}