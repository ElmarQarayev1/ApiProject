using System;
using Flower.Core.Entities;
using Flower.Data.Repositories.Interfaces;

namespace Flower.Data.Repositories.Implementations
{
	public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        public SliderRepository(AppDbContext context) : base(context)
        {


        }

    }
}

