using ClosureService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClosureService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Foreclosure> Foreclosures { get; set; }
        public DbSet<LoanClosure> LoanClosures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Foreclosure>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<LoanClosure>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Foreclosure>().Property(e => e.ApprovalStatus).HasConversion<string>();
            modelBuilder.Entity<LoanClosure>().Property(e => e.ClosureType).HasConversion<string>();
            modelBuilder.Entity<LoanClosure>().Property(e => e.ClosureStatus).HasConversion<string>();
        }
    }
}
