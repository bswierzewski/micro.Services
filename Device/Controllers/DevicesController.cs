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
        public async Task<IActionResult> GetDevice(int id)
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
        public async Task<IActionResult> GetDevices()
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
        public async Task<IActionResult> GetDeviceByCategory(int id)
        {
            try
            {
                var device = await _repo.GetDevices(expressionDevice: n => n.DeviceComponent.CategoryId == id).ToListAsync();

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpGet("components/{id}")]
        public async Task<IActionResult> GetDeviceByComponent(int id)
        {
            try
            {
                var device = await _repo.GetDevices(expressionDevice: n => n.DeviceComponentId == id).ToListAsync();

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpGet("kinds/{id}")]
        public async Task<IActionResult> GetDeviceByKind(int id)
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
        public async Task<IActionResult> AddDevice(DeviceDto deviceDto)
        {
            try
            {
                if (await _repo.ExistsDevice(deviceDto.MacAddress))
                    return StatusCode((int)HttpStatusCode.BadRequest, "Device exists!");

                var newDevice = new Database.Entities.Device()
                {
                    MacAddress = deviceDto.MacAddress,
                    Name = deviceDto.Name,
                    Created = DateTime.Now,
                    KindId = deviceDto.KindId,
                    DeviceComponentId = deviceDto.DeviceComponentId,
                    PhotoUrl = deviceDto.PhotoUrl,
                    SpecificVersionId = deviceDto.SpecificVersionId,
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
        public async Task<IActionResult> UpdateDeviceKind(int id, DeviceDto deviceDto)
        {
            try
            {
                var device = await _repo.GetDevice(id);

                if (device == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "Device not exists!");

                if (!string.IsNullOrEmpty(deviceDto.Name))
                    device.Name = deviceDto.Name;

                if (deviceDto.SpecificVersionId.HasValue)
                    device.SpecificVersionId = deviceDto.SpecificVersionId;

                if (deviceDto.KindId.HasValue)
                    device.KindId = deviceDto.KindId;

                if (deviceDto.DeviceComponentId.HasValue)
                    device.DeviceComponentId = deviceDto.DeviceComponentId;

                if (!string.IsNullOrEmpty(deviceDto.PhotoUrl))
                    device.PhotoUrl = deviceDto.PhotoUrl;

                if (deviceDto.IsAutoUpdate.HasValue)
                    device.IsAutoUpdate = deviceDto.IsAutoUpdate;

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