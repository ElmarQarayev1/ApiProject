﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Flower.UI.Models
{
	public class SliderCreateRequest
	{
        [Required]
        public string Title { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
