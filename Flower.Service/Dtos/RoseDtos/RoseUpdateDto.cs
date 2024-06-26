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

        public List<int>? CategoryIds { get; set; }

        public List<int> ExistPictureIds { get; set; } = new List<int>();
    }

    public class RoseUpdateDtoValidator : AbstractValidator<RoseUpdateDto>
    {
        public RoseUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(35).MinimumLength(2);

            RuleFor(x => x.Value).NotNull();

            RuleFor(x => x.Desc).NotEmpty().MaximumLength(200);


            RuleForEach(x => x.Files)
                           .Must(file => file.Length <= 2 * 1024 * 1024)
                           .WithMessage("Each file must be less than or equal to 2MB.")
                           .Must(file => new[] { "image/png", "image/jpeg" }.Contains(file.ContentType))
                           .WithMessage("Each file type must be png, jpeg, or jpg.");

        }

    }
}

