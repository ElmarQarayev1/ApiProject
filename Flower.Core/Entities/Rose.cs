using System;
namespace Flower.Core.Entities
{
	public class Rose:AuditEntity
	{
		public string Name { get; set; }

		public double Value { get; set; }

		public string Desc { get; set; }

		public string ImageName { get; set; }

	   public List<RoseCategory>? RoseCategories { get; set; }

	}
}

