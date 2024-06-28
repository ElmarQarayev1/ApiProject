using System;
using Flower.Core.Entities;

namespace Flower.Service.Dtos.RoseDtos
{
	public class RosePaginatedGet
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        public string Desc { get; set; }

        public double DiscountPercent { get; set; }

        public DateTime DiscountExpireDate { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}

