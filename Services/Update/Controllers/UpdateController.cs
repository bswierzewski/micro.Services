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
                return StatusCode(304);

            if (device.IsAutoUpdate.HasValue && device.IsAutoUpdate.Value == true)
            {
                if (!device.KindId.HasValue || !device.ComponentId.HasValue)
                    return StatusCode(304);

                var versionId = await _repo.GetLatestVersionId(device.KindId, device.ComponentId) ?? 0;

                if (versionId > 0 && versionId != device.VersionId)
                {
                    device.VersionId = versionId;
                    device.IsUpdated = false;
                    device.Updated = null;

                    await _repo.SaveAllChangesAsync();
                }
            }

            if (device.IsUpdated.HasValue && device.IsUpdated.Value == true)
                return StatusCode(304);

            Database.Entities.Version version = null;

            if (device.VersionId.HasValue)
                version = await _repo.GetVersionById(device.VersionId.Value);

            if (version == null)
                return StatusCode(304);

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