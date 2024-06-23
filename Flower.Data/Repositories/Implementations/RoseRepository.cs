using System;
using System.Text.RegularExpressions;
using Flower.Core.Entities;
using Flower.Data.Repositories.Interfaces;

namespace Flower.Data.Repositories.Implementations
{
	public class RoseRepository : Repository<Rose>, IRoseRepository
    {
        public RoseRepository(AppDbContext context) : base(context)
        {

        }

    }
}

