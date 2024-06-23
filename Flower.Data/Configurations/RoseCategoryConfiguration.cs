using System;
using Flower.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flower.Data.Configurations
{
	public class RoseCategoryConfiguration : IEntityTypeConfiguration<RoseCategory>
    {
		

        public void Configure(EntityTypeBuilder<RoseCategory> builder)
        {
            builder.HasKey(rc => new { rc.FlowerId, rc.CategoryId });

            
            builder.HasOne(rc => rc.Rose)
                   .WithMany(r => r.RoseCategories)
                   .HasForeignKey(rc => rc.FlowerId);

            builder.HasOne(rc => rc.Category)
                   .WithMany(c => c.RoseCategories)
                   .HasForeignKey(rc => rc.CategoryId);
        }
    }
}

