using System;
using Flower.Service.Dtos;
using Flower.Service.Dtos.CategoryDtos;
using Flower.Service.Dtos.RoseDtos;
using Flower.Service.Implementations;
using Flower.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Flower.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RosesController:ControllerBase
	{
		private readonly IRoseService _roseService;
		public RosesController(IRoseService roseService )
		{
			_roseService = roseService;

		}
     
        [HttpPost("")]
        public ActionResult Create([FromForm] RoseCreateDto createDto)
        {

            return StatusCode(201, new { id = _roseService.Create(createDto) });

        }
        [HttpGet("all")]
        public ActionResult<List<RoseGetDto>> GetAll()
        {
            return StatusCode(200, _roseService.GetAll());
        }

        [HttpGet("")]
        public ActionResult<PaginatedList<RoseGetDto>> GetAll(string? search = null, int page = 1, int size = 10)
        {
            return StatusCode(200, _roseService.GetAllByPage(search, page, size));
        }
        [HttpGet("{id}")]
        public ActionResult<RoseDetailsDto> GetById(int id)
        {
            return StatusCode(200, _roseService.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] RoseUpdateDto updateDto)
        {
            _roseService.Update(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _roseService.Delete(id);
            return NoContent();
        }



    }
}

