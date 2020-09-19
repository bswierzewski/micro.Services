using AutoMapper;
using Device.Data;
using Device.Dtos;
using Device.Params;
using Extensions;
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

                var deviceToReturn = _mapper.Map<DeviceForDetailDto>(device);

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

                var devicesToReturn = _mapper.Map<IEnumerable<DeviceForListDto>>(devices);

                return Ok(devicesToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpPost("devices/add")]
        public async Task<IActionResult> AddDevice(DeviceDto deviceDto)
        {
            try
            {
                if (string.IsNullOrEmpty(deviceDto.Address))
                    return StatusCode((int)HttpStatusCode.BadRequest, "The Address field is required.");

                var addressId = await _repo.GetAddressId(deviceDto.Address);

                if (addressId.HasValueGreaterThan(0))
                    return StatusCode((int)HttpStatusCode.BadRequest, "Device exists!");
                else
                    addressId = await _repo.AddAddress(deviceDto.Address);

                var newDevice = new Database.Entities.Device()
                {
                    Name = deviceDto.Name,
                    AddressId = addressId.Value,
                    Created = DateTime.Now,
                    KindId = deviceDto.KindId,
                    DeviceComponentId = deviceDto.DeviceComponentId,
                    CategoryId = deviceDto.CategoryId,
                    Icon = deviceDto.Icon,
                    IsAutoUpdate = deviceDto.IsAutoUpdate,
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

        [HttpPost("devices/update")]
        public async Task<IActionResult> UpdateDeviceKind(DeviceDto deviceDto)
        {
            try
            {
                var device = await _repo.Find<Database.Entities.Device>(deviceDto.Id);

                if (device == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "Device not exists!");

                if (!string.IsNullOrEmpty(deviceDto.Name))
                    device.Name = deviceDto.Name;

                if (!string.IsNullOrEmpty(deviceDto.Icon))
                    device.Icon = deviceDto.Icon;

                if (deviceDto.KindId.HasValue)
                    device.KindId = deviceDto.KindId;

                if (deviceDto.DeviceComponentId.HasValue)
                    device.DeviceComponentId = deviceDto.DeviceComponentId;

                if (deviceDto.CategoryId.HasValue)
                    device.DeviceComponentId = deviceDto.CategoryId;

                if (deviceDto.IsAutoUpdate.HasValue)
                    device.IsAutoUpdate = deviceDto.IsAutoUpdate;

                device.Modified = DateTime.Now;

                await _repo.SaveAllChanges();

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