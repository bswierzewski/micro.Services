using AutoMapper;
using Database.Entities;
using Device.Data.DeviceInfo;
using Device.Dtos;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Controllers
{
    //[Authorize]
    [Route("api")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ILogger<CategoriesController> _logger;
        private readonly IMapper _mapper;
        private readonly IDeviceInfoRepository _repo;

        public CategoriesController(IDeviceInfoRepository repo, ILogger<CategoriesController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _repo.GetCategory(id);

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
            ICollection<Component> components = null;

            if (categoryDto.DeviceComponentIds.IsAny())
                components = await _repo.GetComponentsByIds(categoryDto.DeviceComponentIds);

            var category = new Category()
            {
                Created = DateTime.Now,
                Name = categoryDto.Name,
                Icon = categoryDto.Icon,
                Components = components,
            };

            await _repo.Add(category);

            return Ok(category);
        }

        [HttpPut("categories/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            Category category = await _repo.Find<Category>(id);

            if (category == null)
                return BadRequest("Category not found");

            category.Icon = categoryDto.Icon;

            await _repo.UpdateComponents(id, categoryDto);

            await _repo.SaveAllChangesAsync();

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
    }
}
