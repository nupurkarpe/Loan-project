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
    public class ScorecardRepo : IScorecardRepo
    {
        private readonly ApplicationDbContext _context;

        public ScorecardRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Scorecard> AddAsync(Scorecard scorecard)
        {
            _context.Scorecards.Add(scorecard);
            await _context.SaveChangesAsync();
            return scorecard;
        }

        public async Task<List<Scorecard>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Scorecards
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Scorecard> GetLatestAsync(int customerId)
        {
            return await _context.Scorecards
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.GeneratedDate)
                .FirstOrDefaultAsync();
        }
    }
}

