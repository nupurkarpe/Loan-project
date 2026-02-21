using EligibilityandScorecard.Domain.Models.CreditAndLOS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Eligibility> Eligibilities { get; set; }

        public DbSet<Scorecard> Scorecards { get; set; }


    }
}
