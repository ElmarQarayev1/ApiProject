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

        public List<string > Files { get; set; }

        public List<CategoryRoseDto> CategoryIds { get; set; }
    }
}

