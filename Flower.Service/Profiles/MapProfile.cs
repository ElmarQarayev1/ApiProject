﻿using System;
using System.Text.RegularExpressions;
using AutoMapper;
using Flower.Core.Entities;
using Flower.Service.Dtos.CategoryDtos;
using Flower.Service.Dtos.RoseDtos;
using Flower.Service.Dtos.SliderDtos;
using Microsoft.AspNetCore.Http;

namespace Flower.Service.Profiles
{
    public class MapProfile : Profile
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
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.RoseCategories))
                .ForMember(dest => dest.Files, opt => opt.Ignore());

            CreateMap<RoseCreateDto, Rose>();
            CreateMap<Rose, RoseDetailsDto>();

            CreateMap<Rose, RoseGetDto>().ReverseMap();

            CreateMap<Rose,RosePaginatedGet>().
                ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.RoseCategories.Select(rc => rc.CategoryId).ToList()));



            CreateMap<Category, CategoryGetDto>().ReverseMap();
            CreateMap<Category, CategoryPaginatedGet>();



            CreateMap<Slider, SliderCreateDto>().ReverseMap();
            CreateMap<Slider, SliderPaginatedGet>()
                 .ForMember(dest => dest.File, opt => opt.MapFrom(src => baseUrl + "/uploads/sliders/" + src.ImageName));



            CreateMap<Slider, SliderGetDto>()
                .ForMember(dest => dest.File, opt => opt.MapFrom(src => baseUrl + "/uploads/sliders/" + src.ImageName));


            
        }
    }

}

