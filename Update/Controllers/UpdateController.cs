using Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Update.Data;
using Update.Dtos;

namespace Update.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly ILogger<UpdateController> _logger;
        private readonly IUpdateRepository _repo;

        public UpdateController(ILogger<UpdateController> logger, IUpdateRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{macAddress}")]
        public async Task<IActionResult> GetUpdate(string macAddress)
        {
            var device = await _repo.GetDevice(macAddress);

            if (device == null)
            {
                var newDevice = new Device()
                {
                    MacAddress = macAddress,
                    Created = DateTime.Now,
                };

                await _repo.AddDevice(newDevice);

                return StatusCode((int)HttpStatusCode.NotFound, "Version not found!");
            }

            var deviceVersion = await _repo.GetDeviceVersionByDeviceId(device.Id);

            if (deviceVersion != null)
            {
                if (deviceVersion.VersionId == device.VersionId)
                    return StatusCode((int)HttpStatusCode.NotFound, "Up to date!");
                else
                    return Ok(deviceVersion.VersionId);
            }

            return StatusCode((int)HttpStatusCode.NotFound, "Version not found!");

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("download/{versionId}")]
        public async Task<IActionResult> DownloadVersion(int versionId)
        {
            var version = await _repo.GetVersionById(versionId);

            if (version == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Version not found!");

            var file = version.FileData;

            var content = new MemoryStream(file.Content);
            var contentType = "APPLICATION/octet-stream";
            var fileName = $"{file.Name}{file.Extension}";

            return File(content, contentType, fileName);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmUpdate(ConfirmDto confirmDto)
        {
            var device = await _repo.GetDevice(confirmDto.MacAddress);

            if (device == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device not found!");

            var version = await _repo.GetVersionById(confirmDto.VersionId);

            if (version == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Version not found!");

            await _repo.ConfirmUpdateDevice(device, version);

            return Ok();
        }
    }
}