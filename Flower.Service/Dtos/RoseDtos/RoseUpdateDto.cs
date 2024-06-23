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

        public IFormFile File { get; set; }

        public List<RoseCategory>? RoseCategories { get; set; }
    }

    public class RoseUpdateDtoValidator : AbstractValidator<RoseUpdateDto>
    {
        public RoseUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(35).MinimumLength(6);

            RuleFor(x => x.Value).NotNull();

            RuleFor(x => x.Desc).NotEmpty().MaximumLength(200);

            RuleFor(x => x.RoseCategories)
                .NotNull()
                .Must(categories => categories != null && categories.Count > 0)
                .WithMessage("There must be at least one category.");

            RuleFor(x => x.File)
              .Must(file => file == null || file.Length <= 2 * 1024 * 1024)
              .WithMessage("File must be less or equal than 2MB")
              .Must(file => file == null || new[] { "image/png", "image/jpeg" }.Contains(file.ContentType))
              .WithMessage("File type must be png, jpeg, or jpg");

        }

    }
}

