using System;
using Flower.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flower.Data.Configurations
{
	public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
		

        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasOne(x => x.Rose).WithMany(s => s.Pictures).HasForeignKey(x => x.RoseId);
        }
    }
}

