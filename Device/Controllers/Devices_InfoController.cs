using Database.Entities.DeviceInfo;
using Device.Data.DeviceInfo;
using Device.Data.DeviceInfo.Component;
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

        #region Components

        [HttpGet("components/{id}")]
        public async Task<IActionResult> GetComponentDto(int id)
        {
            var component = await _repo.GetComponents(id);

            return Ok(component);
        }


        [HttpGet("components")]
        public async Task<IActionResult> GetComponentsDto()
        {
            var components = await _repo.GetComponents();

            return Ok(components);
        }

        [HttpPost("components/add")]
        public async Task<IActionResult> AddComponentDto(AddComponentDto addComponentDto)
        {
            var component = new Component()
            {
                Created = DateTime.Now,
                CategoryId = addComponentDto.CategoryId,
                Name = addComponentDto.Name,
            };

            await _repo.Add(component);

            return Ok(component);
        }

        #endregion Components

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
