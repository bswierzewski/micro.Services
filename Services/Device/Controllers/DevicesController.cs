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
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            try
            {
                var device = await _repo.GetDevice(id);

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpGet]
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

        [HttpPost("add")]
        public async Task<IActionResult> AddDevice(DeviceToAddDto deviceToAddDto)
        {
            try
            {
                var addressId = await _repo.GetAddressId(deviceToAddDto.Address);

                if (!addressId.HasValue)
                    addressId = await _repo.AddAddress(deviceToAddDto.Address);

                var newDevice = new Database.Entities.Device()
                {
                    Name = deviceToAddDto.Name,
                    AddressId = addressId.Value,
                    Created = DateTime.Now,
                    KindId = deviceToAddDto.KindId,
                    DeviceComponentId = deviceToAddDto.DeviceComponentId,
                    CategoryId = deviceToAddDto.CategoryId,
                    Icon = deviceToAddDto.Icon,
                    IsAutoUpdate = deviceToAddDto.IsAutoUpdate,
                    SpecificVersionId = deviceToAddDto.SpecificVersionId,
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

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDeviceKind(DeviceToAddDto deviceToAddDto)
        {
            try
            {
                var device = await _repo.GetDevice(deviceToAddDto.Id);

                if (device == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "Device not exists!");

                if (!string.IsNullOrEmpty(deviceToAddDto.Name))
                    device.Name = deviceToAddDto.Name;

                if (!string.IsNullOrEmpty(deviceToAddDto.Icon))
                    device.Icon = deviceToAddDto.Icon;

                if (deviceToAddDto.SpecificVersionId.HasValue)
                    device.SpecificVersionId = deviceToAddDto.SpecificVersionId;

                if (deviceToAddDto.KindId.HasValue)
                    device.KindId = deviceToAddDto.KindId;

                if (deviceToAddDto.DeviceComponentId.HasValue)
                    device.DeviceComponentId = deviceToAddDto.DeviceComponentId;

                if (deviceToAddDto.CategoryId.HasValue)
                    device.DeviceComponentId = deviceToAddDto.CategoryId;

                if (deviceToAddDto.IsAutoUpdate.HasValue)
                    device.IsAutoUpdate = deviceToAddDto.IsAutoUpdate;

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
    }
}