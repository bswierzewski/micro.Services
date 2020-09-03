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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceDto(int id)
        {
            try
            {
                var device = await _repo.GetDevices(expressionDevice: n => n.Id == id).FirstOrDefaultAsync();

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDevicesDto()
        {
            try
            {
                var devices = await _repo.GetDevices().ToListAsync();

                return Ok(devices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetDeviceDtoByCategory(int id)
        {
            try
            {
                var device = await _repo.GetDevices(expressionDevice: n => n.Component.CategoryId == id).ToListAsync();

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpGet("components/{id}")]
        public async Task<IActionResult> GetDeviceDtoByComponent(int id)
        {
            try
            {
                var device = await _repo.GetDevices(expressionDevice: n => n.ComponentId == id).ToListAsync();

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpGet("kinds/{id}")]
        public async Task<IActionResult> GetDeviceDtoByKind(int id)
        {
            try
            {
                var device = await _repo.GetDevices(expressionDevice: n => n.KindId == id).ToListAsync();

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDevice(AddDeviceDto addDeviceDto)
        {
            try
            {
                if (await _repo.ExistsDevice(addDeviceDto.MacAddress))
                    return StatusCode((int)HttpStatusCode.BadRequest, "Device exists!");

                var newDevice = new Database.Entities.Device()
                {
                    MacAddress = addDeviceDto.MacAddress,
                    Name = addDeviceDto.Name,
                    Created = DateTime.Now,
                    KindId = addDeviceDto.KindId,
                    ComponentId = addDeviceDto.ComponentId,
                    PhotoUrl = addDeviceDto.PhotoUrl,
                    SpecificVersionId = addDeviceDto.SpecificVersionId,
                };

                await _repo.Add(newDevice);

                return Ok(newDevice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpPost("{id}/update")]
        public async Task<IActionResult> UpdateDeviceKind(int id, UpdateDeviceDto updateDeviceDto)
        {
            try
            {
                var device = await _repo.GetDevice(id);

                if (device == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "Device not exists!");

                if (!string.IsNullOrEmpty(updateDeviceDto.Name))
                    device.Name = updateDeviceDto.Name;

                if (updateDeviceDto.SpecificVersionId.HasValue)
                    device.SpecificVersionId = updateDeviceDto.SpecificVersionId;

                if (updateDeviceDto.KindId.HasValue)
                    device.KindId = updateDeviceDto.KindId;

                if (updateDeviceDto.ComponentId.HasValue)
                    device.ComponentId = updateDeviceDto.ComponentId;

                if (!string.IsNullOrEmpty(updateDeviceDto.PhotoUrl))
                    device.PhotoUrl = updateDeviceDto.PhotoUrl;

                if (updateDeviceDto.IsAutoUpdate.HasValue)
                    device.IsAutoUpdate = updateDeviceDto.IsAutoUpdate;

                device.Modified = DateTime.Now;

                await _repo.SaveAllChanges();

                var result = await _repo.GetDevices(x => x.Id == id).FirstOrDefaultAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }
    }
}