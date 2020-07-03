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
        [HttpGet]
        public async Task<IActionResult> GetLatestVersion()
        {
            var latestVersion = await _repo.GetLatestVersion();

            if (latestVersion != null)
                return Ok(latestVersion.Id);
            else
                return StatusCode((int)HttpStatusCode.NotFound);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetVersionInfo(int id)
        {
            var version = await _repo.GetVersionById(id);

            if (version == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Version not exists");

            var fileDatas = await _repo.GetFileDataById(version.FileDataId ?? 0);

            if (fileDatas == null)
                return StatusCode((int)HttpStatusCode.NotFound, "File not exists");

            return Ok($"Version info:{Environment.NewLine}" +
                          $" - Id: {version.Id}{Environment.NewLine}" +
                          $" - Version Created: {version.Created}{Environment.NewLine}" +
                          $" - Major: {version.Major}{Environment.NewLine}" +
                          $" - Patch: {version.Patch}{Environment.NewLine}" +
                          $" - File name: {fileDatas.FileName}{Environment.NewLine}" +
                          $" - File extension: {fileDatas.FileExtension}{Environment.NewLine}" +
                          $" - File created: {fileDatas.Created}{Environment.NewLine}");

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("get/{macAddress}")]
        public async Task<IActionResult> GetUpdate(string macAddress)
        {
            var device = await _repo.GetDevice(macAddress);

            if (device == null)
            {
                var newDevice = new Device()
                {
                    MacAddress = macAddress,
                    Created = DateTime.Now,
                    Name = macAddress,
                };

                await _repo.AddDevice(newDevice);

                return await GetLatestVersion();
            }

            var deviceVersion = await _repo.GetDeviceVersionByDeviceId(device.Id);

            if (deviceVersion != null)
            {
                if (deviceVersion.VersionId == device.VersionId)
                    return StatusCode((int)HttpStatusCode.NotFound, "Up to date");
                else
                    return Ok(deviceVersion.VersionId);
            }

            var latestVersion = await _repo.GetLatestVersion();

            if (latestVersion == null)
                return StatusCode((int)HttpStatusCode.NotFound);

            if (device.VersionId == latestVersion.Id)
                return StatusCode((int)HttpStatusCode.NotFound, "Up to date");

            return await GetLatestVersion();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("download/{versionId}")]
        public async Task<IActionResult> DownloadVersion(int versionId)
        {
            var version = await _repo.GetVersionById(versionId);

            if (version == null)
                return StatusCode((int)HttpStatusCode.NotFound);

            var file = await _repo.DownloadFile(version.FileDataId ?? 0);

            if (file == null)
                return StatusCode((int)HttpStatusCode.NotFound);

            var content = new MemoryStream(file.Content);
            var contentType = "APPLICATION/octet-stream";
            var fileName = $"{file.FileName}{file.FileExtension}";

            return File(content, contentType, fileName);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmUpdate(ConfirmDto confirmDto)
        {
            var device = await _repo.GetDevice(confirmDto.MacAddress);

            if (device == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device not exists!");

            var version = await _repo.GetVersionById(confirmDto.VersionId);

            if (version == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Version not exists!");

            await _repo.ConfirmUpdateDevice(device, version);

            return Ok();
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadNewVersion([FromForm] UploadDto uploadDto)
        {
            if (await _repo.IsVersionExists(uploadDto.Major, uploadDto.Minor, uploadDto.Patch))
                return BadRequest("Version already exists!");

            if (uploadDto.File.Length > 0)
            {

                var fileData = new FileData()
                {
                    Created = DateTime.Now,
                    FileExtension = Path.GetExtension(uploadDto.File.FileName),
                    FileName = uploadDto.File.FileName.Replace(Path.GetExtension(uploadDto.File.FileName), ""),
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
                        FileDataId = fileData.Id
                    };

                    await _repo.AddVersion(newVersion);

                    if (newVersion.Id > 0)
                        return StatusCode((int)HttpStatusCode.Created);
                }
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, "Error Upload");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("set")]
        public async Task<IActionResult> SetDeviceVersion(SetDeviceVersionDto uploadDto)
        {
            var device = await _repo.GetDevice(uploadDto.MacAddress);

            if (device == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device not exists!");

            var version = await _repo.GetVersionById(uploadDto.VersionId);

            if (version == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Version not exists!");


            var deviceVersion = new DeviceVersion
            {
                DeviceId = device.Id,
                VersionId = version.Id
            };

            if (await _repo.AddDeviceVersion(deviceVersion))
                return StatusCode((int)HttpStatusCode.Created);

            return StatusCode((int)HttpStatusCode.InternalServerError, "Error Upload");
        }
    }
}