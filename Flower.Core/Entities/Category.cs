using System;
namespace Flower.Core.Entities
{
	public class Category: BaseEntity
	{
        public string Name { get; set; }

        public List<RoseCategory>? RoseCategories { get; set; }
    }
}

