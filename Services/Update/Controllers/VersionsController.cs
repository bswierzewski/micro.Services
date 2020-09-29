using AutoMapper;
using Database.Entities;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Update.Data;
using Update.Dtos;

namespace Update.Controllers
{
    [ApiController]
    [Route("api")]
    public class VersionsController : ControllerBase
    {
        private readonly ILogger<VersionsController> _logger;
        private readonly IVersionRepository _repo;
        private readonly IMapper _mapper;

        public VersionsController(ILogger<VersionsController> logger, IVersionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("versions/{id}")]
        public async Task<IActionResult> GetVersion(int id)
        {
            var version = await _repo.GetVersion(id);

            if (version == null)
                return BadRequest();

            return Ok(version);
        }

        [HttpGet("versions")]
        public async Task<IActionResult> GetVersions()
        {
            var versions = await _repo.GetVersions();

            var versionsToReturn = _mapper.Map<IEnumerable<VersionForListDto>>(versions);

            return Ok(versionsToReturn);
        }

        [HttpPost("versions")]
        public async Task<IActionResult> UploadNewVersion([FromForm] UploadDto uploadDto)
        {
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
                        Major = uploadDto.Major.Value,
                        Minor = uploadDto.Minor.Value,
                        Patch = uploadDto.Patch.Value,
                        Name = uploadDto.Name ?? fileData.Name,
                        FileDataId = fileData.Id,
                        ComponentId = uploadDto.ComponentId,
                        KindId = uploadDto.KindId
                    };

                    await _repo.AddVersion(newVersion);

                    if (newVersion.Id > 0)
                        return StatusCode((int)HttpStatusCode.Created);
                }
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, "Error Upload");
        }

        [HttpPut("versions/{id}")]
        public async Task<IActionResult> UpdateVersion(int id, UploadDto uploadDto)
        {
            var version = await _repo.Find<Database.Entities.Version>(id);

            if (version == null)
                return BadRequest();

            if (!string.IsNullOrEmpty(uploadDto.Name))
                version.Name = uploadDto.Name;

            if (uploadDto.Major.HasValue)
                version.Major = uploadDto.Major.Value;

            if (uploadDto.Minor.HasValue)
                version.Minor = uploadDto.Minor.Value;

            if (uploadDto.Patch.HasValue)
                version.Patch = uploadDto.Patch.Value;

            if (uploadDto.ComponentId.HasValueGreaterThan(0))
                version.ComponentId = uploadDto.ComponentId.Value;

            if (uploadDto.KindId.HasValueGreaterThan(0))
                version.KindId = uploadDto.KindId.Value;

            await _repo.SaveAllChangesAsync();

            return Ok();
        }
    }
}