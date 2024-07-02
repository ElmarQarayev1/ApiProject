using System;
using Flower.Service.Dtos;
using Flower.Service.Dtos.RoseDtos;

namespace Flower.Service.Interfaces
{
	public interface IRoseService
	{
        int Create(RoseCreateDto createDto);
        PaginatedList<RosePaginatedGet> GetAllByPage(string? search = null, int page = 1, int size = 10);
        List<RoseGetDto> GetAll(string? search = null);
        RoseDetailsDto GetById(int id);
        void Update(int id, RoseUpdateDto updateDto);
        void Delete(int id);
       
    }
}

