using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Flower.UI.Models
{
	public class RoseEditRequest
	{

        [Required]
        public string Name { get; set; }

        public List<int> CategoryIds { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public double Value { get; set; }

        [JsonIgnore]
        public List<string>? FileUrls { get; set; }

        public List<IFormFile>? Files { get; set; }
    }
}

