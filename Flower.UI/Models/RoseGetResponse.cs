using System;
using System.Text.Json.Serialization;

namespace Flower.UI.Models
{
	public class RoseGetResponse
	{
        public string Name { get; set; }

        public List<int> CategoryIds { get; set; }

        public double Value { get; set; }


        public string  Desc { get; set; }

        public double DiscountPercent { get; set; }

        public DateTime DiscountExpireDate { get; set; }

        public List<PictureResponse>? Pictures { get; set; }


      
    }
}

