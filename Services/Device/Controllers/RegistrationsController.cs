using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Device.Data.Registrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Device.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationsController : ControllerBase
    {
        private readonly ILogger<RegistrationsController> _logger;
        private readonly IRegistrationRepository _repo;

        public RegistrationsController(ILogger<RegistrationsController> logger, IRegistrationRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistrations(int id)
        {
            var registrations = await _repo.GetRegistrations(id);

            return Ok(registrations);
        }
    }
}