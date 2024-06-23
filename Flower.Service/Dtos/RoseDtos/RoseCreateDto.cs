using System;
using System.Collections.Generic;
using Flower.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Flower.Service.Dtos.RoseDtos
{
    public class RoseCreateDto
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Desc { get; set; }
        public IFormFile File { get; set; }

        public List<CategoryRoseDto>? RoseCategories { get; set; }
    }

    public class CategoryRoseDto
    {
        public int? CategoryId { get; set; }
    }

    public class RoseCreateDtoValidator : AbstractValidator<RoseCreateDto>
    {
        public RoseCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(35).MinimumLength(2);
            RuleFor(x => x.Value).NotNull();
            RuleFor(x => x.Desc).NotEmpty().MaximumLength(200);


            RuleFor(x => x.RoseCategories)
                .Must(categories => categories == null || categories.Count == 0 || categories.Any(rc => rc.CategoryId.HasValue))
                .WithMessage("If RoseCategories is provided, it must contain at least one valid CategoryId.")
                .When(x => x.RoseCategories != null);



            RuleFor(x => x.File)
                .Must(file => file == null || file.Length <= 2 * 1024 * 1024)
                .WithMessage("File must be less than or equal to 2MB.")
                .Must(file => file == null || new[] { "image/png", "image/jpeg" }.Contains(file.ContentType))
                .WithMessage("File type must be png, jpeg, or jpg.");
        }
    }
}
