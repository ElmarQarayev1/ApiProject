using System;
using Flower.Service.Dtos;
using Flower.Service.Dtos.CategoryDtos;
using Flower.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Flower.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService groupService)
        {
            _categoryService = groupService;
        }

        [HttpPost("")]
        public ActionResult Create(CategoryCreateDto createDto)
        {
            return StatusCode(201, new { Id = _categoryService.Create(createDto) });

        }
        [HttpGet("")]
        public ActionResult<PaginatedList<CategoryGetDto>> GetAll(string? search = null, int page = 1, int size = 10)
        {
            return StatusCode(200, _categoryService.GetAllByPage(search, page, size));
        }
        [HttpGet("all")]
        public ActionResult<List<CategoryGetDto>> GetAll()
        {
            return StatusCode(200, _categoryService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryGetDto> GetById(int id)
        {
            return StatusCode(200, _categoryService.GetById(id));
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryUpdateDto updateDto)
        {
            _categoryService.Update(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return NoContent();
        }


    }
}

