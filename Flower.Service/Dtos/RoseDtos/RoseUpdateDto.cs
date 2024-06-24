using System;
using Flower.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Flower.Service.Dtos.RoseDtos
{
	public class RoseUpdateDto
	{
        public string Name { get; set; }

        public double Value { get; set; }

        public string Desc { get; set; }

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();

        public List<CategoryRoseDto>? RoseCategories { get; set; }
    }

    public class RoseUpdateDtoValidator : AbstractValidator<RoseUpdateDto>
    {
        public RoseUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(35).MinimumLength(2);

            RuleFor(x => x.Value).NotNull();

            RuleFor(x => x.Desc).NotEmpty().MaximumLength(200);


            RuleFor(x => x.RoseCategories)
                .Must(categories => categories == null || categories.Count == 0 || categories.Any(rc => rc.CategoryId.HasValue))
                .WithMessage("If RoseCategories is provided, it must contain at least one valid CategoryId.")
                .When(x => x.RoseCategories != null);


            RuleForEach(x => x.Files)
                           .Must(file => file.Length <= 2 * 1024 * 1024)
                           .WithMessage("Each file must be less than or equal to 2MB.")
                           .Must(file => new[] { "image/png", "image/jpeg" }.Contains(file.ContentType))
                           .WithMessage("Each file type must be png, jpeg, or jpg.");

        }

    }
}

