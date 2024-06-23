using System;
using Flower.Core.Entities;
using Flower.Data.Repositories.Interfaces;

namespace Flower.Data.Repositories.Implementations
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }


    }
}

