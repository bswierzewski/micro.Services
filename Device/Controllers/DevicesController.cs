using Device.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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