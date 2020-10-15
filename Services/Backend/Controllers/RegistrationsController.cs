using AutoMapper;
using Device.Data.Registrations;
using Device.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Controllers
{
    [ApiController]
    [Route("api")]
    public class RegistrationsController : ControllerBase
    {
        private readonly ILogger<RegistrationsController> _logger;
        private readonly IRegistrationRepository _repo;
        private readonly IMapper _mapper;

        public RegistrationsController(ILogger<RegistrationsController> logger, IRegistrationRepository repo, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("registrations/{id}")]
        public async Task<IActionResult> GetRegistrations(int id)
        {
            var registrations = await _repo.GetRegistrations(id);

            var registrationsToReturn = _mapper.Map<IEnumerable<RegistrationForListDto>>(registrations);

            return Ok(registrationsToReturn);
        }
    }
}