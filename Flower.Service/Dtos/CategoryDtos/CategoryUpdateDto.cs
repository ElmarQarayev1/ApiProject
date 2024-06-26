using System;
using Flower.Core.Entities;
using FluentValidation;

namespace Flower.Service.Dtos.CategoryDtos
{
	public class CategoryUpdateDto
	{
        public string Name { get; set; }
    }

    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(35).MinimumLength(2);
        }

    }
}

