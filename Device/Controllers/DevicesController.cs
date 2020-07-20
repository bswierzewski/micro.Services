using Device.Data;
using Device.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly IDeviceRepository _repo;

        public DevicesController(IDeviceRepository repo, ILogger<DevicesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }


        /// <summary>
        /// Pobiera wszystkie urz퉐zenia
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDevicesDto()
        {
            var devices = await _repo.GetDevices().ToListAsync();

            return Ok(devices);
        }

        /// <summary>
        /// Pobiera urz퉐zenie po ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceDto(int id)
        {
            var device = await _repo.GetDevices(expressionDevice: n => n.Id == id).FirstOrDefaultAsync();

            return Ok(device);
        }

        /// <summary>
        /// Pobiera urz퉐zenia po kategorii
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetDeviceDtoByCategory(int id)
        {
            var device = await _repo.GetDevices(expressionDevice: n => n.Component.CategoryId == id).ToListAsync();

            return Ok(device);
        }


        /// <summary>
        /// Pobiera urz퉐zenia po komponentach
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("components/{id}")]
        public async Task<IActionResult> GetDeviceDtoByComponent(int id)
        {
            var device = await _repo.GetDevices(expressionDevice: n => n.ComponentId == id).ToListAsync();

            return Ok(device);
        }


        /// <summary>
        /// Pobiera urz퉐zenia po rodzaju
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("kinds/{id}")]
        public async Task<IActionResult> GetDeviceDtoByKind(int id)
        {
            var device = await _repo.GetDevices(expressionDevice: n => n.KindId == id).ToListAsync();

            return Ok(device);
        }

        /// <summary>
        /// Dodaje nowe urz퉐zenie
        /// </summary>
        /// <param name="addDeviceDto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddDevice(AddDeviceDto addDeviceDto)
        {
            if (await _repo.ExistsDevice(addDeviceDto.MacAddress))
                return StatusCode((int)HttpStatusCode.NotFound, "Device exists!");

            var newDevice = new Database.Entities.Device()
            {
            };

            await _repo.Add(newDevice);

            return Ok(newDevice);
        }

        /// <summary>
        /// Aktualizuje urz퉐zenie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDeviceDto"></param>
        /// <returns></returns>
        [HttpPost("{id}/update")]
        public async Task<IActionResult> UpdateDeviceKind(int id, UpdateDeviceDto updateDeviceDto)
        {
            var device = await _repo.GetDevice(id);

            if (device == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device not exists!");

            device.Modified = DateTime.Now;

            if (await _repo.SaveAllChanges())
                return Ok(device);
            else
                return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}