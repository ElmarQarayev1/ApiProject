using System;
using Flower.Core.Entities;
using Flower.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flower.Data
{
	public class AppDbContext:IdentityDbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Rose> Roses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<RoseCategory> RoseCategories { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RoseConfiguration());
            modelBuilder.ApplyConfiguration(new RoseCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PictureConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

