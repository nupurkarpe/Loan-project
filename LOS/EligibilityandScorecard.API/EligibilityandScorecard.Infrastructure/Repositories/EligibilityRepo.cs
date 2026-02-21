using EligibilityandScorecard.Application.Interfaces.Repositories;
using EligibilityandScorecard.Domain.Models.CreditAndLOS;
using EligibilityandScorecard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Infrastructure.Repositories
{
    public class EligibilityRepo : IEligibilityRepo
    {
        private readonly ApplicationDbContext _context;

        public EligibilityRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Eligibility> AddEligibilityAsync(Eligibility eligibility)
        {
            _context.Eligibilities.Add(eligibility);
            await _context.SaveChangesAsync();
            return eligibility;
        }

        public async Task<List<Eligibility>> GetEligibilityByCustomerIdAsync(int customerId)
        {
            return await _context.Eligibilities
                                 .Where(e => e.CustomerId == customerId)
                                 .ToListAsync();
        }
    }
}

