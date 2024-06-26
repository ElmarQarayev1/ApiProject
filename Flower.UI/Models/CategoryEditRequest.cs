using System;
using System.ComponentModel.DataAnnotations;

namespace Flower.UI.Models
{
	public class CategoryEditRequest
	{
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

    }
}

