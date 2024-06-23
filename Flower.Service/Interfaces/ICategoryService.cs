using System;
using Flower.Service.Dtos;
using Flower.Service.Dtos.CategoryDtos;

namespace Flower.Service.Interfaces
{
	public interface ICategoryService
	{
        int Create(CategoryCreateDto createDto);
        PaginatedList<CategoryGetDto> GetAllByPage(string? search = null, int page = 1, int size = 10);
        List<CategoryGetDto> GetAll(string? search = null);
        CategoryGetDto GetById(int id);
        void Update(int id, CategoryUpdateDto updateDto);
        void Delete(int id);
    }
}

