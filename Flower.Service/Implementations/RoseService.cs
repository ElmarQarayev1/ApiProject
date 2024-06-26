using System;
using System.Text.RegularExpressions;
using AutoMapper;
using Flower.Core.Entities;
using Flower.Data.Repositories.Implementations;
using Flower.Data.Repositories.Interfaces;
using Flower.Service.Dtos;
using Flower.Service.Dtos.RoseDtos;
using Flower.Service.Exceptions;
using Flower.Service.Helpers;
using Flower.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

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
            List<Category> categories = new List<Category>();

            var categoryIds = createDto.CategoryIds?.ToList();

            if (categoryIds != null)
            {
                categories = _categoryRepository.GetAll(x => categoryIds.Contains(x.Id)).ToList();
            }

            if (categoryIds == null || categories.Count == 0)
            {
                throw new RestException(StatusCodes.Status404NotFound, "CategoryId", "One or more categories not found by given Ids");
            }

            if (_roseRepository.Exists(x => x.Name.ToUpper() == createDto.Name.ToUpper() && !x.IsDeleted))
            {
                throw new RestException(StatusCodes.Status400BadRequest, "Name", "Rose already exists by given Name");
            }

            var roseCategories = createDto.CategoryIds.Select(x => new RoseCategory { CategoryId = x }).ToList();

            Rose rose = new Rose
            {
                Name = createDto.Name,
                Desc = createDto.Desc,
                Value = createDto.Value,
                RoseCategories = roseCategories,
                Pictures = new List<Picture>() 
            };

            foreach (var file in createDto.Files)
            {
                var filePath = FileManager.Save(file, _env.WebRootPath, "uploads/roses");
                rose.Pictures.Add(new Picture { ImageName = filePath });
            }

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

        public PaginatedList<RosePaginatedGet> GetAllByPage(string? search = null, int page = 1, int size = 10)
        {
            var query = _roseRepository.GetAll(x => !x.IsDeleted,"RoseCategories");

            PaginatedList<Rose> roses = PaginatedList<Rose>.Create(query, page, size);

            return new PaginatedList<RosePaginatedGet>(_mapper.Map<List<RosePaginatedGet>>(roses.Items), roses.TotalPages, roses.PageIndex, roses.PageSize);
        }

        public RoseDetailsDto GetById(int id)
        {
            Rose rose = _roseRepository.Get(x => x.Id == id && !x.IsDeleted,"RoseCategories","Pictures");

            if (rose == null) throw new RestException(StatusCodes.Status404NotFound, "Rose not found");

            return _mapper.Map<RoseDetailsDto>(rose);
        }

        public void Update(int id, RoseUpdateDto updateDto)
        {
            List<Category> categories = new List<Category>();

            var categoryIds = updateDto.CategoryIds?.ToList();

            if (categoryIds != null)
            {
                categories = _categoryRepository.GetAll(x => categoryIds.Contains(x.Id)).ToList();
            }


            if (categoryIds == null || categories.Count == 0)
            {
                throw new RestException(StatusCodes.Status404NotFound, "CategoryId", "One or more categories not found by given Ids");
            }


            Rose rose = _roseRepository.Get(x => x.Id == id, "RoseCategories", "Pictures");

            if (rose == null)
            {
                throw new RestException(StatusCodes.Status404NotFound, "RoseId", "Rose not found by given Id");
            }

            if (!string.Equals(rose.Name, updateDto.Name, StringComparison.OrdinalIgnoreCase) &&
                _roseRepository.Exists(x => x.Name.ToUpper() == updateDto.Name.ToUpper() && x.Id != id && !x.IsDeleted))
            {
                throw new RestException(StatusCodes.Status400BadRequest, "Name", "Rose already exists by given Name");
            }
            var roseCategories = updateDto.CategoryIds.Select(x => new RoseCategory { CategoryId = x }).ToList();

            rose.Name = updateDto.Name;
            rose.Desc = updateDto.Desc;
            rose.Value = updateDto.Value;
            rose.RoseCategories = roseCategories;


            List<Picture> pictures = rose.Pictures.Where(x => !updateDto.ExistPictureIds.Contains(x.Id)).ToList();
            List<Picture> removedPictures = rose.Pictures.Where(x => updateDto.ExistPictureIds.Contains(x.Id)).ToList();

            rose.Pictures = pictures;

            foreach (var imgFile in updateDto.Files)
            {
                Picture Img = new Picture
                {
                    ImageName = FileManager.Save(imgFile, _env.WebRootPath, "uploads/roses"),
                };
                rose.Pictures.Add(Img);
            }
         
            rose.ModifiedAt = DateTime.Now;

         

            _roseRepository.Save();


            foreach (var item in removedPictures)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/roses", item.ImageName);
            }

        }

    }
} 
