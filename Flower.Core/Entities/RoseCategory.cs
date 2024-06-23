﻿using System;
namespace Flower.Core.Entities
{
	public class RoseCategory
	{
		public int RoseId { get; set; }

		public int CategoryId { get; set; }

		public Category Category { get; set; }

		public Rose Rose { get; set; }
	}
}

