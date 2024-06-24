using System;
namespace Flower.Service.Dtos.SliderDtos
{
	public class SliderPaginatedGet
	{
        public int Id { get; set; }

        public string Title { get; set; }

        public string Desc { get; set; }

        public string File { get; set; }

        public int Order { get; set; }
    }
}

