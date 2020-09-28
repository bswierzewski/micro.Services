using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Update.Data;

namespace Update.Controllers
{
    [ApiController]
    [Route("api")]
    public class UpdateController : ControllerBase
    {
        private readonly ILogger<UpdateController> _logger;
        private readonly IUpdateRepository _repo;
        private readonly IMapper _mapper;

        public UpdateController(ILogger<UpdateController> logger, IUpdateRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("updates/{address}")]
        public async Task<IActionResult> UpdateDevice(string address)
        {
            var addressId = await GetAddressId(address);

            Database.Entities.Device device = await _repo.GetDeviceByAddresId(addressId);

            if (device == null)
                return BadRequest();

            Database.Entities.Version version = null;

            if (device.IsAutoUpdate.HasValue && device.IsAutoUpdate.Value == true)
            {
                if (!device.KindId.HasValue)
                    return BadRequest();

                if (!device.ComponentId.HasValue)
                    return BadRequest();

                version = await _repo.GetLatestVersion(device.KindId, device.ComponentId);
            }
            else if (!device.IsUpdated.HasValue || device.IsUpdated.Value == false)
            {
                if (!device.VersionId.HasValue)
                    return BadRequest();

                version = await _repo.Find<Database.Entities.Version>(device.VersionId.Value);
            }
            else
            {
                return BadRequest();
            }

            if (version == null)
                return BadRequest();

            var file = version.FileData;

            var content = new MemoryStream(file.Content);
            var contentType = "APPLICATION/octet-stream";
            var fileName = $"{file.Name}{file.Extension}";

            return File(content, contentType, fileName);
        }

        [HttpPost("updates/{address}")]
        public async Task<IActionResult> ConfirmUpdate(string address)
        {
            var addressId = await GetAddressId(address);

            Database.Entities.Device device = await _repo.GetDeviceByAddresId(addressId);

            if (device == null)
                return BadRequest();

            device.IsUpdated = true;
            device.Updated = DateTime.Now;

            await _repo.SaveAllChangesAsync();

            return Ok();
        }

        private async Task<int> GetAddressId(string address)
        {
            int addressId = await _repo.GetAddressId(address) ?? 0;

            if (addressId == 0)
            {
                var newAddress = new Database.Entities.Address()
                {
                    Created = DateTime.Now,
                    IsConfirmed = false,
                    Label = address,
                };

                await _repo.Add(newAddress);
            }

            return addressId;
        }
    }
}