using System;
namespace Flower.UI.Models
{
	public class RoseCreateRequest
	{
        public List<int> CategoryIds { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public  string Desc { get; set; }

        public double DiscountPercent { get; set; }

        public DateTime DiscountExpireDate { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}

