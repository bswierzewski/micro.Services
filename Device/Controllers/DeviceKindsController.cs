using Database.Entities;
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
    [Route("api/kinds")]
    public class DeviceKindsController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IDeviceKindRepository _repo;
        public DeviceKindsController(IDeviceKindRepository repo, ILogger<DevicesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeviceKinds()
        {
            var kinds = await _repo.GetDeviceKinds();

            return Ok(kinds);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDeviceKind(AddDeviceKindDto deviceKindDto)
        {
            if (await _repo.IsDeviceKind(deviceKindDto.Kind))
                return StatusCode((int)HttpStatusCode.NotFound, "Device kind already exists!");

            var deviceKind = new DeviceKind()
            {
                Kind = deviceKindDto.Kind,
                Created = DateTime.Now,
            };

            await _repo.Add(deviceKind);

            return Ok(deviceKind);
        }
    }
}
