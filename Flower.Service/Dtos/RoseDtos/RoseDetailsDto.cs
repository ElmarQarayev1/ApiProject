using System;
using Flower.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Flower.Service.Dtos.RoseDtos
{
	public class RoseDetailsDto
	{
        public string Name { get; set; }

        public double Value { get; set; }

        public string Desc { get; set; }

      public List<PictureResponseDto> Pictures { get; set; }

        public List<int> CategoryIds { get; set; }

    }
}

