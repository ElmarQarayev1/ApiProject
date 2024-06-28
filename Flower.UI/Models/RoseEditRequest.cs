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

        public List<int> ExistPictureIds { get; set; } = new List<int>();

        [JsonIgnore]
        public List<PictureResponse>? FileUrls { get; set; }

        public List<IFormFile>? Files { get; set; }

        public List<int> RemovedPictureIds { get; set; } = new List<int>();
    }
}

