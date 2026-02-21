using EligibilityandScorecard.Domain.Models.CreditAndLOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Application.Interfaces.Repositories
{
    public interface IScorecardRepo
    {
        Task<Scorecard> AddAsync(Scorecard scorecard);

        Task<List<Scorecard>> GetByCustomerIdAsync(int customerId);

        Task<Scorecard> GetLatestAsync(int customerId);
    }
}
