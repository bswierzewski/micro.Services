using Database.Entities;
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
    public class DeviceKindsController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IDeviceRepository _repo;
        public DeviceKindsController(IDeviceRepository repo, ILogger<DevicesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeviceKinds()
        {
            var kinds = await _repo.GetDeviceKinds();

            if (kinds == null || kinds.Count() == 0)
                return StatusCode((int)HttpStatusCode.NotFound, "Not found any kinds!");

            return Ok(kinds);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDeviceKind(PostDeviceKindDto deviceKindDto)
        {
            var kind = await _repo.GetDeviceKind(deviceKindDto.Kind);

            if (kind != null)
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
