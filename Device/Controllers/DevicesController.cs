using DeviceService.Data;
using DeviceService.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DeviceService.Controllers
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
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _repo.GetDevices();

            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            var device = await _repo.GetDevice(id);

            return Ok(device);
        }

        [HttpGet("scanners")]
        public async Task<IActionResult> GetScanners()
        {
            var devices = await _repo.GetDevices(DeviceRoleEnum.Scanner);

            return Ok(devices);
        }

        [HttpGet("scanners/{id}")]
        public async Task<IActionResult> GetScanner(int id)
        {
            var device = await _repo.GetDevice(id, DeviceRoleEnum.Scanner);

            return Ok(device);
        }

        [HttpGet("locators")]
        public async Task<IActionResult> GetLocators()
        {
            var devices = await _repo.GetDevices(DeviceRoleEnum.Locator);

            return Ok(devices);
        }

        [HttpGet("locators/{id}")]
        public async Task<IActionResult> GetLocator(int id)
        {
            var device = await _repo.GetDevice(id, DeviceRoleEnum.Locator);

            return Ok(device);
        }


        [HttpPost("add")]
        public IActionResult PostDevice(string macaddress)
        {
            return Ok($"Mock callback ok...");
        }

        [HttpPut("{macaddress}")]
        public IActionResult PutDevice(string macaddress)
        {
            return Ok($"Mock callback ok...");
        }

        [HttpDelete("{macaddress}")]
        public IActionResult DeleteDevice(string macaddress)
        {
            return Ok($"Mock callback ok...");
        }
    }
}