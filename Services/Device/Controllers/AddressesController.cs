using AutoMapper;
using Device.Data.Address;
using Device.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Device.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api")]
    public class AddressesController : ControllerBase
    {
        private readonly ILogger<AddressesController> _logger;
        private readonly IAddressRepository _repo;

        public AddressesController(IAddressRepository repo, ILogger<AddressesController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("addresses/{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            try
            {
                var address = await _repo.Find<Database.Entities.Address>(id);

                return Ok(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpGet("addresses")]
        public async Task<IActionResult> GetAddresses([FromQuery] AddressParams addressParams)
        {
            try
            {
                var addresses = await _repo.GetAddresses(addressParams);

                return Ok(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                var address = await _repo.Find<Database.Entities.Address>(id);

                if (address == null)
                    return StatusCode((int)HttpStatusCode.BadRequest, "Address not exists!");

                await _repo.Delete(address);

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
