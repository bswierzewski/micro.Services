using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeviceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetRegistrations()
        {
            return Ok("");
        }

        [HttpGet("{macaddress}")]
        public IActionResult GetDeviceRegistration(string macaddress)
        {
            return Ok($"{macaddress}");
        }
    }
}