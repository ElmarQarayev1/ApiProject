using System;
using System.Text.Json.Serialization;

namespace Flower.UI.Models
{
	public class SliderGetResponse
	{
        public string Title { get; set; }


        public string Desc { get; set; }


        public int Order { get; set; }

        public string? File { get; set; }
    }
}

