using System;
using System.Text.RegularExpressions;
using AutoMapper;
using Flower.Core.Entities;
using Flower.Data.Repositories.Interfaces;
using Flower.Service.Dtos;
using Flower.Service.Dtos.CategoryDtos;
using Flower.Service.Exceptions;
using Flower.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Flower.Service.Implementations
{
	public class CategoryService:ICategoryService
	{

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public int Create(CategoryCreateDto createDto)
        {
            if (_categoryRepository.Exists(x => x.Name == createDto.Name))
                throw new RestException(StatusCodes.Status400BadRequest, "Name", "Name already taken");

            var roseCategories = createDto.RoseCategories?
        .Where(rc => rc.RoseId.HasValue) 
        .Select(rc => new RoseCategory { RoseId = rc.RoseId.Value }) 
        .ToList();

            var entity = new Category
            {
                Name = createDto.Name,
                RoseCategories = roseCategories
            };

            _categoryRepository.Add(entity);
            _categoryRepository.Save();

            return entity.Id;
        }


        public void Delete(int id)
        {
            Category entity = _categoryRepository.Get(x => x.Id == id);

            if (entity == null)
                throw new RestException(StatusCodes.Status404NotFound, "Category not found");

            _categoryRepository.Delete(entity);

            _categoryRepository.Save();
        }

        public List<CategoryGetDto> GetAll(string? search = null)
        {
            var categories = _categoryRepository.GetAll(x => search == null || x.Name.Contains(search)).ToList();
            return _mapper.Map<List<CategoryGetDto>>(categories);
        }

        public PaginatedList<CategoryPaginatedGet> GetAllByPage(string? search = null, int page = 1, int size = 10)
        {
            var query = _categoryRepository.GetAll(x => x.Name.Contains(search) || search == null, "RoseCategories");


            var paginated = PaginatedList<Category>.Create(query, page, size);

            var categoryDtos = _mapper.Map<List<CategoryPaginatedGet>>(paginated.Items);

            return new PaginatedList<CategoryPaginatedGet>(categoryDtos, paginated.TotalPages, page, size);
        }

        public CategoryGetDto GetById(int id)
        {
            Category category = _categoryRepository.Get(x => x.Id == id , "RoseCategories");

            if (category == null)
                throw new RestException(StatusCodes.Status404NotFound, "Category not found");

            return _mapper.Map<CategoryGetDto>(category);
        }

        public void Update(int id, CategoryUpdateDto updateDto)
        {
            Category entity = _categoryRepository.Get(x => x.Id == id, "RoseCategories");

            if (entity == null)
                throw new RestException(StatusCodes.Status404NotFound, "Category not found");

            if (entity.Name != updateDto.Name && _categoryRepository.Exists(x => x.Name == updateDto.Name))
                throw new RestException(StatusCodes.Status400BadRequest, "Name", "Name already taken");

            entity.Name = updateDto.Name;

            if (updateDto.RoseCategories != null)
            {
                entity.RoseCategories = updateDto.RoseCategories
                    .Where(rc => rc.RoseId.HasValue) 
                    .Select(rc => new RoseCategory { RoseId = rc.RoseId.Value, CategoryId = id }) 
                    .ToList();
            }
            else
            {
                entity.RoseCategories = null; 
            }

            _categoryRepository.Save();
        }

    }

}

