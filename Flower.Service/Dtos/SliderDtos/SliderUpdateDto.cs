using System;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Flower.Service.Dtos.SliderDtos
{
    public class SliderUpdateDto
    {
        public string Title { get; set; }

        public string Desc { get; set; }

        public IFormFile File { get; set; }

        public int Order { get; set; }
    }
    public class SliderUpdateDtoValidator : AbstractValidator<SliderUpdateDto>
    {
        public SliderUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
               .MaximumLength(35).WithMessage("Title cannot be longer than 35 characters.")
               .MinimumLength(2).WithMessage("Title must be at least 2 characters long.")
               .When(x => !string.IsNullOrEmpty(x.Title)); 

            RuleFor(x => x.Desc)
                .MaximumLength(200).WithMessage("Description cannot be longer than 200 characters.")
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
                .When(x => !string.IsNullOrEmpty(x.Desc)); 

            RuleFor(x => x.Order)
                .GreaterThan(0).WithMessage("'Order' must be greater than 0.")
                .When(x => x.Order != 0);

            RuleFor(x => x.File)
                .Must(file => file == null || file.Length <= 2 * 1024 * 1024)
                .WithMessage("File must be less than or equal to 2MB.")
                .Must(file => file == null || new[] { "image/png", "image/jpeg" }.Contains(file.ContentType))
                .When(x => x.File != null);
        }
    }
}


