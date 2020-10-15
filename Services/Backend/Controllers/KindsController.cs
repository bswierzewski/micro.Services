using AutoMapper;
using Database.Entities;
using Device.Data.DeviceInfo;
using Device.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Device.Controllers
{
    //[Authorize]
    [Route("api")]
    [ApiController]
    public class KindsController : ControllerBase
    {

        private readonly ILogger<KindsController> _logger;
        private readonly IMapper _mapper;
        private readonly IDeviceInfoRepository _repo;

        public KindsController(IDeviceInfoRepository repo, ILogger<KindsController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("kinds/{id}")]
        public async Task<IActionResult> GetKind(int id)
        {
            var kind = await _repo.Find<Kind>(id);

            return Ok(kind);
        }

        [HttpGet("kinds")]
        public async Task<IActionResult> GetKinds()
        {
            var kinds = await _repo.GetKinds();

            return Ok(kinds);
        }

        [HttpPost("kinds")]
        public async Task<IActionResult> AddKind(KindDto kindDto)
        {
            var kind = new Kind()
            {
                Created = DateTime.Now,
                Name = kindDto.Name,
                Icon = kindDto.Icon,
            };

            await _repo.Add(kind);

            return Ok(kind);
        }

        [HttpPut("kinds/{id}")]
        public async Task<IActionResult> UpdateKind(int id, KindDto kindDto)
        {
            var kind = await _repo.Find<Kind>(id);

            if (kind == null)
                return BadRequest("Kind not found!");

            kind.Icon = kindDto.Icon;

            await _repo.SaveAllChangesAsync();

            return Ok(kind);
        }


        [HttpDelete("kinds/{id}")]
        public async Task<IActionResult> DeleteKinds(int id)
        {
            var kind = await _repo.Find<Kind>(id);

            if (kind == null)
                return BadRequest("Kind not found");

            await _repo.Delete(kind);

            return Ok();
        }
    }
}
