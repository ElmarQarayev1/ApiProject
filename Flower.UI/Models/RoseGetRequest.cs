using System;
using System.Text.Json.Serialization;

namespace Flower.UI.Models
{
	public class RoseGetRequest
	{
        public string Name { get; set; }

        public List<int> CategoryIds { get; set; }

        public double Value { get; set; }


        public string  Desc { get; set; }

        [JsonPropertyName("pictures")]
        public List<string>? Files { get; set; }
    }
}

