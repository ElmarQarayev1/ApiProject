using System;
namespace Flower.Core.Entities
{
	public class Picture:BaseEntity
	{
        public int RoseId { get; set; }

        public Rose Rose { get; set; }
        
        public string ImageName { get; set; } 
    }
}

