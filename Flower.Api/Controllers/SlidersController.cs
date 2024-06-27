using System;
using Flower.Service.Dtos;
using Flower.Service.Dtos.CategoryDtos;
using Flower.Service.Dtos.SliderDtos;
using Flower.Service.Implementations;
using Flower.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Flower.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        public SlidersController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }


        [HttpPost("")]
        public ActionResult Create([FromForm] SliderCreateDto createDto)
        {
            return StatusCode(201, new { Id = _sliderService.Create(createDto) });
        }


        [HttpGet("")]
        public ActionResult<PaginatedList<SliderGetDto>> GetAll(string? search = null, int page = 1, int size = 10)
        {
            return StatusCode(200, _sliderService.GetAllByPage(search, page, size));
        }


        [HttpGet("all")]
        public ActionResult<List<SliderGetDto>> GetAll()
        {
            return StatusCode(200, _sliderService.GetAll());
        }


        [HttpGet("{id}")]
        public ActionResult<SliderGetDto> GetById(int id)
        {
            return StatusCode(200, _sliderService.GetById(id));
        }

        [HttpPut("{id}")]
        public void Update(int id, [FromForm] SliderUpdateDto updateDto)
        {
            _sliderService.Update(id, updateDto);
           
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _sliderService.Delete(id);
            return NoContent();
        }


    }
}

