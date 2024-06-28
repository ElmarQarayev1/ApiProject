using System;
namespace Flower.Core.Entities
{
	public class Rose:AuditEntity
	{
		public string Name { get; set; }

		public double Value { get; set; }

		public string Desc { get; set; }

		public double DiscountPercent { get; set; }

		public DateTime DiscountExpireDate { get; set; }

	    public List<Picture>? Pictures { get; set; }

        public List<RoseCategory>? RoseCategories { get; set; }

	}
}

