using System;
using AutoMapper;
using Flower.Data.Repositories.Interfaces;
using Flower.Service.Dtos;
using Flower.Service.Dtos.RoseDtos;
using Flower.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;

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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<RoseGetDto> GetAll(string? search = null)
        {
            throw new NotImplementedException();
        }

        public PaginatedList<RoseGetDto> GetAllByPage(string? search = null, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public RoseDetailsDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, RoseUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}

