using Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UpdateDevice.Data;
using UpdateDevice.Dtos;

namespace UpdateDevice.Controllers
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
        [HttpPost("get")]
        public async Task<IActionResult> GetUpdate(GetUpdateDto getUpdateDto)
        {
            var device = await _repo.GetDevice(getUpdateDto.MacAddress);

            var deviceType = await _repo.GetDeviceType(getUpdateDto.DeviceType);

            if (device == null)
            {
                if (deviceType == null)
                    return StatusCode((int)HttpStatusCode.NotFound, "Device Type not found!");

                var newDevice = new Device()
                {
                    MacAddress = getUpdateDto.MacAddress,
                    Created = DateTime.Now,
                    DeviceTypeId = deviceType.Id,
                    Name = getUpdateDto.MacAddress,
                };

                await _repo.AddDevice(newDevice);
            }
            else
            {
                var deviceVersion = await _repo.GetDeviceVersionByDeviceId(device.Id);

                if (deviceVersion != null)
                {
                    if (deviceVersion.VersionId == device.VersionId)
                        return StatusCode((int)HttpStatusCode.NotFound, "Up to date!");
                    else
                        return Ok(deviceVersion.VersionId);
                }
            }

            var latestVersion = await _repo.GetLatestDeviceTypeVersion(deviceType.Id);

            if (latestVersion == null)
                return StatusCode((int)HttpStatusCode.NotFound);

            if (device.VersionId == latestVersion.Id)
                return StatusCode((int)HttpStatusCode.NotFound, "Up to date!");

            var versionId = await _repo.GetLatestDeviceTypeVersion(deviceType.Id);

            return Ok(versionId);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("download/{versionId}")]
        public async Task<IActionResult> DownloadVersion(int versionId)
        {
            var version = await _repo.GetVersionById(versionId);

            if (version == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Version not found!");

            var file = await _repo.DownloadFile(version.FileDataId ?? 0);

            if (file == null)
                return StatusCode((int)HttpStatusCode.NotFound, "File not found!");

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