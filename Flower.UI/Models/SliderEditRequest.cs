using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Flower.UI.Models
{
	public class SliderEditRequest
	{
        [Required]
        public string Title { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public int Order { get; set; }

        [JsonIgnore]
        public string? FileUrl { get; set; }

        public IFormFile? File { get; set; }
    }
}

