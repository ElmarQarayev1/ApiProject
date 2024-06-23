using System;
using System.Text.RegularExpressions;
using AutoMapper;
using Flower.Core.Entities;
using Flower.Service.Dtos.CategoryDtos;
using Flower.Service.Dtos.RoseDtos;
using Microsoft.AspNetCore.Http;

namespace Flower.Service.Profiles
{
	public class MapProfile:Profile
	{
        private readonly IHttpContextAccessor _context;

        public MapProfile(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;

            var uriBuilder = new UriBuilder(_context.HttpContext.Request.Scheme, _context.HttpContext.Request.Host.Host, _context.HttpContext.Request.Host.Port ?? -1);

            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }
            string baseUrl = uriBuilder.Uri.AbsoluteUri;

            CreateMap<Rose, RoseCreateDto>()
            .ForMember(dest => dest.RoseCategories, s => s.MapFrom(s => s.RoseCategories));
            CreateMap<RoseCreateDto, Rose>();
            CreateMap<Rose, RoseDetailsDto>();
            CreateMap<Rose, RoseGetDto>();



            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryGetDto, Category>();


            //CreateMap<Student, StudentDetailsDto>()
            //    .ForMember(dest => dest.GroupName, s => s.MapFrom(s => s.Group.No));
            //CreateMap<Student, StudentGetDto>()
            //  .ForMember(dest => dest.Age, s => s.MapFrom(s => DateTime.Now.Year - s.BirthDate.Year))
            //  .ForMember(dest => dest.ImageUrl, s => s.MapFrom(s => baseUrl + "uploads/students/" + s.FileName));

        }
    }
}

