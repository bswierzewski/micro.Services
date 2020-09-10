using Database.Entities.DeviceInfo;
using Device.Data.DeviceInfo;
using Device.Data.DeviceInfo.DeviceComponent;
using Device.Dtos.DeviceInfo.Category;
using Device.Dtos.DeviceInfo.Kind;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Device.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class Devices_InfoController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IDeviceInfoRepository _repo;

        public Devices_InfoController(IDeviceInfoRepository repo, ILogger<DevicesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        #region Kinds

        [HttpGet("kinds/{id}")]
        public async Task<IActionResult> GetKindDto(int id)
        {
            var kind = await _repo.GetKinds(id);

            return Ok(kind);
        }

        [HttpGet("kinds")]
        public async Task<IActionResult> GetKindsDto()
        {
            var kinds = await _repo.GetKinds();

            return Ok(kinds);
        }


        [HttpPost("kinds/add")]
        public async Task<IActionResult> AddKindsDto(AddKindDto addKindDto)
        {
            var kind = new Kind()
            {
                Created = DateTime.Now,
                Name = addKindDto.Name,
            };

            await _repo.Add(kind);

            return Ok(kind);
        }

        #endregion Kinds

        #region DeviceComponents

        [HttpGet("deviceComponents/{id}")]
        public async Task<IActionResult> GetDeviceComponentDto(int id)
        {
            var component = await _repo.GetDeviceComponents(id);

            return Ok(component);
        }


        [HttpGet("deviceComponents")]
        public async Task<IActionResult> GetDeviceComponentsDto()
        {
            var components = await _repo.GetDeviceComponents();

            return Ok(components);
        }

        [HttpPost("deviceComponents/add")]
        public async Task<IActionResult> AddDeviceComponentDto(AddDeviceComponentDto addComponentDto)
        {
            var component = new DeviceComponent()
            {
                Created = DateTime.Now,
                CategoryId = addComponentDto.CategoryId,
                Name = addComponentDto.Name,
            };

            await _repo.Add(component);

            return Ok(component);
        }

        #endregion DeviceComponents

        #region Categories

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategoryDto(int id)
        {
            var categories = await _repo.GetCategories(id);

            return Ok(categories);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategoriesDto()
        {
            var categories = await _repo.GetCategories();

            return Ok(categories);
        }

        [HttpPost("categories/add")]
        public async Task<IActionResult> AddCategoryDto(AddCategoryDto addCategoryDto)
        {
            var category = new Category()
            {
                Created = DateTime.Now,
                Name = addCategoryDto.Name
            };

            await _repo.Add(category);

            return Ok(category);
        }

        #endregion Categories
    }
}
