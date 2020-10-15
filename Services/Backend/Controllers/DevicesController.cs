using AutoMapper;
using Device.Data;
using Device.Dtos;
using Device.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Device.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api")]
    public class DevicesController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IMapper _mapper;
        private readonly IDeviceRepository _repo;

        public DevicesController(IDeviceRepository repo, ILogger<DevicesController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("devices/{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            try
            {
                var device = await _repo.GetDevice(id);

                var deviceToReturn = _mapper.Map<DeviceDto>(device);

                return Ok(deviceToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpGet("devices")]
        public async Task<IActionResult> GetDevices([FromQuery] DeviceParams deviceParams)
        {
            try
            {
                var devices = await _repo.GetDevices(deviceParams);

                var devicesToReturn = _mapper.Map<IEnumerable<DeviceDto>>(devices);

                return Ok(devicesToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpPost("devices")]
        public async Task<IActionResult> AddDevice(DeviceDto deviceDto)
        {
            try
            {
                if (string.IsNullOrEmpty(deviceDto.AddressLabel))
                    return StatusCode((int)HttpStatusCode.BadRequest, "The Address field is required.");

                var address = await _repo.GetAddress(deviceDto.AddressLabel);

                if (address == null)
                    address = await _repo.AddAddress(deviceDto.AddressLabel);
                else if (address.IsConfirmed == false)
                    address.IsConfirmed = true;
                else
                    return StatusCode((int)HttpStatusCode.BadRequest, "Address exists!");

                var newDevice = new Database.Entities.Device()
                {
                    Name = deviceDto.Name,
                    AddressId = address.Id,
                    Created = DateTime.Now,
                    KindId = deviceDto.KindId,
                    ComponentId = deviceDto.ComponentId,
                    CategoryId = deviceDto.CategoryId,
                    Icon = deviceDto.Icon,
                    IsAutoUpdate = deviceDto.IsAutoUpdate,
                    VersionId = deviceDto.VersionId,
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

        [HttpPut("devices/{id}")]
        public async Task<IActionResult> UpdateDevice(int id, DeviceDto deviceDto)
        {
            try
            {
                var device = await _repo.Find<Database.Entities.Device>(id);

                if (device == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "Device not exists!");

                if (!string.IsNullOrEmpty(deviceDto.Name))
                    device.Name = deviceDto.Name;

                if (!string.IsNullOrEmpty(deviceDto.Icon))
                    device.Icon = deviceDto.Icon;

                if (deviceDto.KindId.HasValue)
                    device.KindId = deviceDto.KindId;

                if (deviceDto.ComponentId.HasValue)
                    device.ComponentId = deviceDto.ComponentId;

                if (deviceDto.CategoryId.HasValue)
                    device.CategoryId = deviceDto.CategoryId;

                if (deviceDto.IsAutoUpdate.HasValue)
                    device.IsAutoUpdate = deviceDto.IsAutoUpdate;

                if (deviceDto.VersionId.HasValue)
                {
                    device.VersionId = deviceDto.VersionId;
                    device.IsUpdated = false;
                    device.Updated = null;
                }

                device.Modified = DateTime.Now;

                await _repo.SaveAllChangesAsync();

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpDelete("devices/{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            try
            {
                var device = await _repo.Find<Database.Entities.Device>(id);

                if (device == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "Device not exists!");

                await _repo.Delete(device);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }
    }
}