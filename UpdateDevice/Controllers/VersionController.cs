using Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UpdateDevice.Data;
using UpdateDevice.Dtos;

namespace UpdateDevice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _logger;
        private readonly IVersionRepository _repo;

        public VersionController(ILogger<VersionController> logger, IVersionRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> GetAllVersion()
        {
            try
            {
                var versions = await _repo.GetAllVersion();

                if (versions.Any())
                    return Ok(versions);
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVersionInfo(int id)
        {
            var version = await _repo.GetVersionById(id);

            if (version == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Version not exists");

            return Ok(version);

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("set")]
        public async Task<IActionResult> SetDeviceVersion(SetDeviceVersionDto setDeviceVersionDto)
        {
            var device = await _repo.GetDevice(setDeviceVersionDto.DeviceId);

            if (device == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device not exists!");

            var version = await _repo.GetVersionById(setDeviceVersionDto.VersionId);

            if (version == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Version not exists!");

            var deviceVersion = await _repo.GetDeviceVersion(setDeviceVersionDto.DeviceId);

            if (deviceVersion == null)
            {
                deviceVersion.VersionId = setDeviceVersionDto.VersionId;

                await _repo.SaveAllChanges();
            }
            else
            {
                var newDeviceVersion = new DeviceVersion
                {
                    DeviceId = device.Id,
                    VersionId = version.Id
                };

                if (await _repo.AddDeviceVersion(newDeviceVersion))
                    return StatusCode((int)HttpStatusCode.Created);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, "Error Upload");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadNewVersion([FromForm] UploadDto uploadDto)
        {
            if (await _repo.IsVersionExists(uploadDto.Major, uploadDto.Minor, uploadDto.Patch, uploadDto.DeviceTypeId ?? 0, uploadDto.DeviceKindId ?? 0))
                return BadRequest("Version already exists!");

            if (uploadDto.File.Length > 0)
            {

                var fileData = new FileData()
                {
                    Created = DateTime.Now,
                    Extension = Path.GetExtension(uploadDto.File.FileName),
                    Name = uploadDto.File.FileName.Replace(Path.GetExtension(uploadDto.File.FileName), ""),
                };

                using (var ms = new MemoryStream())
                {
                    uploadDto.File.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    fileData.Content = fileBytes;
                }

                await _repo.UploadFile(fileData);

                if (fileData.Id > 0)
                {
                    var newVersion = new Database.Entities.Version
                    {
                        Created = DateTime.Now,
                        Major = uploadDto.Major,
                        Minor = uploadDto.Minor,
                        Patch = uploadDto.Patch,
                        Name = uploadDto.Name ?? fileData.Name,
                        DeviceTypeId = uploadDto.DeviceTypeId,
                        DeviceKindId = uploadDto.DeviceKindId,
                        FileDataId = fileData.Id
                    };

                    await _repo.AddVersion(newVersion);

                    if (newVersion.Id > 0)
                        return StatusCode((int)HttpStatusCode.Created);
                }
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, "Error Upload");
        }
    }
}