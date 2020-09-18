using AutoMapper;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IMapper _mapper;

        public UpdateController(ILogger<UpdateController> logger, IUpdateRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
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
            var addressId = await _repo.GetAddressId(confirmDto.Address);

            if (!addressId.HasValueGreaterThan(0))
                return StatusCode((int)HttpStatusCode.NotFound, "Address not found!");

            var device = await _repo.GetDevice(addressId);

            if (device == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Device not found!");

            var version = await _repo.GetVersionById(confirmDto.VersionId.Value);

            if (version == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Version not found!");

            await _repo.ConfirmUpdateDevice(device, version);

            return Ok();
        }
    }
}