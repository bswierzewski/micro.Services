using AutoMapper;
using Database.Entities.DeviceInfo;
using Device.Data.DeviceInfo;
using Device.Dtos;
using Device.Params;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Device.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api")]
    public class Devices_InfoController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IMapper _mapper;
        private readonly IDeviceInfoRepository _repo;

        public Devices_InfoController(IDeviceInfoRepository repo, ILogger<DevicesController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
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


        [HttpPost("kinds")]
        public async Task<IActionResult> AddKinds(KindDto kindDto)
        {
            var kind = new Kind();

            if (kindDto.Id == 0)
            {
                kind = new Kind()
                {
                    Created = DateTime.Now,
                    Name = kindDto.Name,
                    Icon = kindDto.Icon,
                };

                await _repo.Add(kind);
            }
            else
            {
                kind = await _repo.Find<Kind>(kindDto.Id);

                kind.Icon = kindDto.Icon;

                await _repo.SaveAllChangesAsync();
            }

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

        [HttpGet("components/{id}")]
        public async Task<IActionResult> GetDeviceComponent(int id)
        {
            var component = await _repo.GetDeviceComponent(id);

            return Ok(component);
        }


        [HttpGet("components")]
        public async Task<IActionResult> GetDeviceComponents([FromQuery] DeviceComponentParams deviceComponentParams)
        {
            var components = await _repo.GetDeviceComponents(deviceComponentParams);

            return Ok(components);
        }

        [HttpPost("components")]
        public async Task<IActionResult> AddDeviceComponent(DeviceComponentDto deviceComponentDto)
        {
            DeviceComponent component = null;

            if (deviceComponentDto.Id == 0)
            {
                component = new DeviceComponent()
                {
                    Created = DateTime.Now,
                    CategoryId = deviceComponentDto.CategoryId,
                    Name = deviceComponentDto.Name,
                    Icon = deviceComponentDto.Icon,
                };

                await _repo.Add(component);
            }
            else
            {
                component = await _repo.Find<DeviceComponent>(deviceComponentDto.Id);

                component.Icon = deviceComponentDto.Icon;
                component.CategoryId = deviceComponentDto.CategoryId;

                await _repo.SaveAllChangesAsync();
            }

            return Ok(component);
        }

        [HttpDelete("components/{id}")]
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
            var category = await _repo.GetCategories(id);

            var categoryToReturn = _mapper.Map<CategoryDto>(category);

            return Ok(categoryToReturn);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repo.GetCategories();

            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(categoriesToReturn);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            ICollection<DeviceComponent> deviceComponents = null;
            Category category = null;

            if (categoryDto.Id == 0)
            {

                if (categoryDto.DeviceComponentIds.IsAny())
                    deviceComponents = await _repo.GetDeviceComponentsByIds(categoryDto.DeviceComponentIds);

                category = new Category()
                {
                    Created = DateTime.Now,
                    Name = categoryDto.Name,
                    Icon = categoryDto.Icon,
                    DeviceComponents = deviceComponents,
                };

                await _repo.Add(category);
            }
            else
            {
                category = await _repo.Find<Category>(categoryDto.Id);

                category.Icon = categoryDto.Icon;

                await _repo.UpdateDeviceComponents(categoryDto);

                await _repo.SaveAllChangesAsync();
            }

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
