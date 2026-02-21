using InterestAndChargesServices.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InterestAndChargesServices.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<InterestAccrual> InterestAccruals { get; set; }
        public DbSet<PenaltyCharge> PenaltyCharges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InterestAccrual>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<PenaltyCharge>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<InterestAccrual>().Property(e => e.AccrualType).HasConversion<string>();
            modelBuilder.Entity<InterestAccrual>().Property(e => e.CalculationMethod).HasConversion<string>();
            modelBuilder.Entity<InterestAccrual>().Property(e => e.AccrualStatus).HasConversion<string>();
            modelBuilder.Entity<PenaltyCharge>().Property(e => e.ChargeType).HasConversion<string>();
            modelBuilder.Entity<PenaltyCharge>().Property(e => e.PenaltyStatus).HasConversion<string>();
        }
    }
}
