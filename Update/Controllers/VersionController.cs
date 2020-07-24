using Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Update.Data;
using Update.Dtos;

namespace Update.Controllers
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


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("upload")]
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
                        Major = uploadDto.Major,
                        Minor = uploadDto.Minor,
                        Patch = uploadDto.Patch,
                        Name = uploadDto.Name ?? fileData.Name,
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