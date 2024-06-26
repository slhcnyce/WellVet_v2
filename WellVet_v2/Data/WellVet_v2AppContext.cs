﻿using Microsoft.EntityFrameworkCore;
using WellVet_v2.Models;

namespace WellVet_v2.Data
{
    public class WellVet_v2AppContext : DbContext
    {
        public WellVet_v2AppContext(DbContextOptions<WellVet_v2AppContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-BM64HT3\\SQLEXPRESS;Initial Catalog=WellVet_v2;Integrated Security=true;TrustServerCertificate=true");
            optionsBuilder.UseNpgsql(@"User ID=WellVet;Password=Ab12345!;Server=91.241.49.155;Port=5432;Database=WellVet_v2;Integrated Security = true; Pooling=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
