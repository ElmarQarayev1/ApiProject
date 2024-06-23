using System;
using Flower.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flower.Data.Configurations
{
	public class RoseConfiguration: IEntityTypeConfiguration<Rose>
    {
		
        public void Configure(EntityTypeBuilder<Rose> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired(true);

            builder.Property(x => x.Desc).HasMaxLength(200).IsRequired(true);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value).IsRequired(true);

        }
    }
}

