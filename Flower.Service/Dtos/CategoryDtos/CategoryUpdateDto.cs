using System;
using Flower.Core.Entities;
using FluentValidation;

namespace Flower.Service.Dtos.CategoryDtos
{
	public class CategoryUpdateDto
	{
        public string Name { get; set; }

        public List<RoseCategoryDto>? RoseCategories { get; set; }
    }

    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(35).MinimumLength(2);

            RuleForEach(x => x.RoseCategories)
               .Must((dto, roseCategory) =>
               {

                   if (!roseCategory.RoseId.HasValue && roseCategory.GetType().GetProperties().All(
                       p => p.Name == "RoseId" || p.GetValue(roseCategory) == null))
                       return true;


                   if (roseCategory.RoseId.HasValue)
                       return true;

                   return false;
               })
               .WithMessage("Each RoseCategory must have a valid RoseId if provided.");
        }

    }
}

