using System;
using Flower.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Flower.Service.Dtos.RoseDtos
{
	public class RoseGetDto
	{
        public string Name { get; set; }

        public double Value { get; set; }

        public string Desc { get; set; }

        public IFormFile File { get; set; }

        public List<RoseCategory>? RoseCategories { get; set; }
    }
}

