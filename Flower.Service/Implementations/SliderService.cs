using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using AutoMapper;
using Flower.Core.Entities;
using Flower.Data.Repositories.Implementations;
using Flower.Data.Repositories.Interfaces;
using Flower.Service.Dtos;
using Flower.Service.Dtos.CategoryDtos;
using Flower.Service.Dtos.RoseDtos;
using Flower.Service.Dtos.SliderDtos;
using Flower.Service.Exceptions;
using Flower.Service.Helpers;
using Flower.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Flower.Service.Implementations
{
	public class SliderService:ISliderService
	{
        private readonly ISliderRepository _sliderRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SliderService(ISliderRepository sliderRepository,IMapper mapper,IWebHostEnvironment env)
		{
			_sliderRepository = sliderRepository;
			_mapper = mapper;
			_env = env;

		}
        public int Create(SliderCreateDto createDto)
        {
            var validator = new SliderCreateDtoValidator();
            var validationResult = validator.Validate(createDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            Slider slider = new Slider
            {
                Title = createDto.Title,
                Desc = createDto.Desc,
                Order = createDto.Order,
                ImageName = FileManager.Save(createDto.File, _env.WebRootPath, "uploads/sliders")
            };
            _sliderRepository.Add(slider);
            _sliderRepository.Save();

            return slider.Id;
        }


        public void Delete(int id)
        {

            Slider entity = _sliderRepository.Get(x => x.Id == id);

            if (entity == null)
                throw new RestException(StatusCodes.Status404NotFound, "Slider not found");

            _sliderRepository.Delete(entity);

            _sliderRepository.Save();
            FileManager.Delete(_env.WebRootPath, "uploads/sliders", entity.ImageName);

        }

        public List<SliderGetDto> GetAll(string? search = null)
        {
            var sliders = _sliderRepository.GetAll(x => search == null || x.Title.Contains(search)).ToList();
            return _mapper.Map<List<SliderGetDto>>(sliders);

        }


        public PaginatedList<SliderPaginatedGet> GetAllByPage(string? search = null, int page = 1, int size = 10)
        {
            var query = _sliderRepository.GetAll(x => x.Title.Contains(search) || search == null);


            var paginated = PaginatedList<Slider>.Create(query, page, size);

            var sliderDtos = _mapper.Map<List<SliderPaginatedGet>>(paginated.Items);

            return new PaginatedList<SliderPaginatedGet>(sliderDtos, paginated.TotalPages, page, size);
        }


        public SliderGetDto GetById(int id)
        {
            Slider slider = _sliderRepository.Get(x => x.Id == id);

            if (slider == null)
                throw new RestException(StatusCodes.Status404NotFound, "Slider not found");

            return _mapper.Map<SliderGetDto>(slider);

        }


        public void Update(int id, SliderUpdateDto updateDto)
        {
            var slider = _sliderRepository.Get(x => x.Id == id);
            if (slider == null)
            {
                throw new RestException(StatusCodes.Status404NotFound, "Id", "Slider not found by given Id");
            }

            var validator = new SliderUpdateDtoValidator();
            var validationResult = validator.Validate(updateDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


      
            string deletedFile = slider.ImageName;
            
            if (!string.IsNullOrEmpty(updateDto.Title))
            {
                slider.Title = updateDto.Title;
            }

            if (!string.IsNullOrEmpty(updateDto.Desc))
            {
                slider.Desc = updateDto.Desc;
            }

            if (updateDto.Order > 0)
            {
                slider.Order = updateDto.Order;
            }

            
            if (updateDto.File != null)
            {
                slider.ImageName = FileManager.Save(updateDto.File, _env.WebRootPath, "uploads/sliders");
            }

            
            _sliderRepository.Save();

           
            if (deletedFile != null && deletedFile != slider.ImageName)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/sliders", deletedFile);
            }
        }



    }
}
