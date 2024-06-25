using System;
using System.Collections.Generic;
using Flower.Core.Entities;
using FluentValidation;

namespace Flower.Service.Dtos.CategoryDtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public List<int>? RoseIds { get; set; }
    }

    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(35).MinimumLength(2);
            RuleForEach(x => x.RoseIds).NotEmpty().GreaterThan(0);
        }
    }
}
