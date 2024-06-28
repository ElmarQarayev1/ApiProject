using System;
namespace Flower.UI.Models
{
	public class RoseListItemGetResponse
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public double Value { get; set; }

		public string Desc { get; set; }

        public double DiscountPercent { get; set; }

        public DateTime DiscountExpireDate { get; set; }
    }
}

