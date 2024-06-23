using System;
using System.Text.RegularExpressions;
using AutoMapper;
using Flower.Core.Entities;
using Flower.Data.Repositories.Interfaces;
using Flower.Service.Dtos;
using Flower.Service.Dtos.RoseDtos;
using Flower.Service.Exceptions;
using Flower.Service.Helpers;
using Flower.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Flower.Service.Implementations
{
	public class RoseService:IRoseService
	{
        private readonly IRoseRepository _roseRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryRepository _categoryRepository;

        public RoseService(IRoseRepository roseRepository, IMapper mapper,ICategoryRepository categoryRepository,IWebHostEnvironment env)
        {
            _roseRepository = roseRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _env = env;
        }


        public int Create(RoseCreateDto createDto)
        {
           
            var categoryIds = createDto.RoseCategories?.Select(rc => rc.CategoryId).ToList();

            List<Category> categories = new List<Category>();

            if (categoryIds != null && categoryIds.Any())
            {
                categories = _categoryRepository.GetAll(x => categoryIds.Contains(x.Id)).ToList();
            }


           
            if (categoryIds != null && categoryIds.Any() && categories.Count != categoryIds.Count)
            {
                throw new RestException(StatusCodes.Status404NotFound, "CategoryId", "One or more categories not found by given Ids");
            }


            
            if (_roseRepository.Exists(x => x.Name.ToUpper() == createDto.Name.ToUpper() && !x.IsDeleted))
            {
                throw new RestException(StatusCodes.Status400BadRequest, "Name", "Rose already exists by given Name");
            }

           
            var roseCategories = new List<RoseCategory>();

            if (createDto.RoseCategories != null && createDto.RoseCategories.Any())
            {
                foreach (var rc in createDto.RoseCategories)
                {
                    if (rc.CategoryId.HasValue)
                    {
                        var category = categories.FirstOrDefault(c => c.Id == rc.CategoryId.Value);
                        if (category != null)
                        {
                            roseCategories.Add(new RoseCategory
                            {
                                Category = category  
                            });
                        }
                    }
                }
            }

            Rose rose = new Rose
            {
                Name = createDto.Name,
                Desc = createDto.Desc,
                Value = createDto.Value,
                ImageName = FileManager.Save(createDto.File, _env.WebRootPath, "uploads/roses"),
                RoseCategories = roseCategories  
            };

            _roseRepository.Add(rose);
            _roseRepository.Save();

            return rose.Id;
        }


        public void Delete(int id)
        {
            Rose entity = _roseRepository.Get(x => x.Id == id);

            if (entity == null) throw new RestException(StatusCodes.Status404NotFound, "Rose not found");

            _roseRepository.Delete(entity);

            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.Now;
            _roseRepository.Save();
        }


        public List<RoseGetDto> GetAll(string? search = null)
        {
            var roses = _roseRepository.GetAll(x => !x.IsDeleted && (search == null || x.Name.Contains(search))).ToList();
            return _mapper.Map<List<RoseGetDto>>(roses);


        }
        public PaginatedList<RoseGetDto> GetAllByPage(string? search = null, int page = 1, int size = 10)
        {
            var query = _roseRepository.GetAll(x => !x.IsDeleted);

            PaginatedList<Rose> roses = PaginatedList<Rose>.Create(query, page, size);

            return new PaginatedList<RoseGetDto>(_mapper.Map<List<RoseGetDto>>(roses.Items), roses.TotalPages, roses.PageIndex, roses.PageSize);
        }


        public RoseDetailsDto GetById(int id)
        {
            Rose rose = _roseRepository.Get(x => x.Id == id && !x.IsDeleted,"RoseCategories");

            if (rose == null) throw new RestException(StatusCodes.Status404NotFound, "Rose not found");

            return _mapper.Map<RoseDetailsDto>(rose);
        }

        public void Update(int id, RoseUpdateDto updateDto)
        {
            Rose rose = _roseRepository.Get(x => x.Id == id, "RoseCategories");

            if (rose == null)
            {
                throw new RestException(StatusCodes.Status404NotFound, "RoseId", "Rose not found by given Id");
            }

            if (!string.Equals(rose.Name, updateDto.Name, StringComparison.OrdinalIgnoreCase) &&
                _roseRepository.Exists(x => x.Name.ToUpper() == updateDto.Name.ToUpper() && x.Id != id && !x.IsDeleted))
            {
                throw new RestException(StatusCodes.Status400BadRequest, "Name", "Rose already exists by given Name");
            }

            rose.Name = updateDto.Name;
            rose.Desc = updateDto.Desc;
            rose.Value = updateDto.Value;

           
            if (updateDto.RoseCategories != null)
            {
                var categoryIds = updateDto.RoseCategories.Select(rc => rc.CategoryId).ToList();

               
                List<Category> categories = new List<Category>();
                if (categoryIds.Any())
                {
                    categories = _categoryRepository.GetAll(x => categoryIds.Contains(x.Id)).ToList();
                }

                
                if (categoryIds.Any() && categories.Count != categoryIds.Count)
                {
                    throw new RestException(StatusCodes.Status404NotFound, "CategoryId", "One or more categories not found by given Ids");
                }
      
                rose.RoseCategories.Clear();
      
                foreach (var rc in updateDto.RoseCategories)
                {
                    if (rc.CategoryId.HasValue)
                    {
                        rose.RoseCategories.Add(new RoseCategory
                        {
                            CategoryId = rc.CategoryId.Value
                        });
                    }
                }
            }

            string deletedFile = null;

           
            if (updateDto.File != null)
            {
                deletedFile = rose.ImageName;
                rose.ImageName = FileManager.Save(updateDto.File, _env.WebRootPath, "uploads/roses");
            }

            rose.ModifiedAt = DateTime.Now;
            _roseRepository.Save();

            if (deletedFile != null)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/roses", deletedFile);
            }
        }


    }
}

