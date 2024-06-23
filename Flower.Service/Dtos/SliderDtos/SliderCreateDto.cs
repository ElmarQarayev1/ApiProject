using System;
using Flower.Service.Dtos.RoseDtos;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Flower.Service.Dtos.SliderDtos
{
	public class SliderCreateDto
	{
        public string Title { get; set; }

        public string Desc { get; set; }

        public IFormFile File { get; set; }

        public int Order { get; set; }
    }
    public class SliderCreateDtoValidator : AbstractValidator<SliderCreateDto>
    {
        public SliderCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(35).MinimumLength(2);
            RuleFor(x => x.Desc).NotEmpty().MaximumLength(200).MinimumLength(3);
            RuleFor(x => x.Order).NotEmpty().GreaterThan(0);


            RuleFor(x => x.File)
                .Must(file => file == null || file.Length <= 2 * 1024 * 1024)
                .WithMessage("File must be less than or equal to 2MB.")
                .Must(file => file == null || new[] { "image/png", "image/jpeg" }.Contains(file.ContentType))
                .WithMessage("File type must be png, jpeg, or jpg.");
        }
    }

}

