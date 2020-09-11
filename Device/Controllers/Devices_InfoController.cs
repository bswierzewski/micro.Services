using Database.Entities.DeviceInfo;
using Device.Data.DeviceInfo;
using Device.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
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
        public async Task<IActionResult> GetKind(int id)
        {
            var kind = await _repo.GetKinds(id);

            return Ok(kind);
        }

        [HttpGet("kinds")]
        public async Task<IActionResult> GetKinds()
        {
            var kinds = await _repo.GetKinds();

            return Ok(kinds);
        }


        [HttpPost("kinds/add")]
        public async Task<IActionResult> AddKinds(KindDto kindDto)
        {
            var kind = new Kind()
            {
                Created = DateTime.Now,
                Name = kindDto.Name,
            };

            await _repo.Add(kind);

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

        #endregion Kinds

        #region DeviceComponents

        [HttpGet("deviceComponents/{id}")]
        public async Task<IActionResult> GetDeviceComponent(int id)
        {
            var component = await _repo.GetDeviceComponents(id);

            return Ok(component);
        }


        [HttpGet("deviceComponents")]
        public async Task<IActionResult> GetDeviceComponents()
        {
            var components = await _repo.GetDeviceComponents();

            return Ok(components);
        }

        [HttpPost("deviceComponents/add")]
        public async Task<IActionResult> AddDeviceComponent(DeviceComponentDto deviceComponentDto)
        {
            var component = new DeviceComponent()
            {
                Created = DateTime.Now,
                CategoryId = deviceComponentDto.CategoryId,
                Name = deviceComponentDto.Name,
            };

            await _repo.Add(component);

            return Ok(component);
        }

        [HttpDelete("deviceComponents/{id}")]
        public async Task<IActionResult> DeleteDeviceComponents(int id)
        {
            var deviceComponent = await _repo.Find<DeviceComponent>(id);

            if (deviceComponent == null)
                return BadRequest("DeviceComponent not found");

            await _repo.Delete(deviceComponent);

            return Ok();
        }

        #endregion DeviceComponents

        #region Categories

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var categories = await _repo.GetCategories(id);

            return Ok(categories);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repo.GetCategories();

            return Ok(categories);
        }

        [HttpPost("categories/add")]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            var category = new Category()
            {
                Created = DateTime.Now,
                Name = categoryDto.Name
            };

            await _repo.Add(category);

            return Ok(category);
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            var category = await _repo.Find<Category>(id);

            if (category == null)
                return BadRequest("Category not found");

            await _repo.Delete(category);

            return Ok();
        }

        #endregion Categories
    }
}
