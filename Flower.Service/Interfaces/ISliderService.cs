using System;
using Flower.Service.Dtos;

using Flower.Service.Dtos.SliderDtos;

namespace Flower.Service.Interfaces
{
	public interface ISliderService
	{
        int Create(SliderCreateDto createDto);
        PaginatedList<SliderGetDto> GetAllByPage(string? search = null, int page = 1, int size = 10);
        List<SliderGetDto> GetAll(string? search = null);
        SliderGetDto GetById(int id);
        void Update(int id, SliderUpdateDto updateDto);
        void Delete(int id);
    }
}

