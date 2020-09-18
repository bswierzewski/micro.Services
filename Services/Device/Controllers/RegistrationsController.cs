using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Device.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationsController : ControllerBase
    {
        private readonly ILogger<RegistrationsController> _logger;

        public RegistrationsController(ILogger<RegistrationsController> logger)
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