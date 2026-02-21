using GeographicService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GeographicService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }      
        public DbSet<Branch> branch { get; set; }
        public DbSet<Country> country { get; set; }
        public DbSet<State> state { get; set; }
        public DbSet<City> city { get; set; }
        public DbSet<Area> area { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.city)
                .WithMany()
                .HasForeignKey(b => b.cityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.area)
                .WithMany()
                .HasForeignKey(b => b.areaId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
