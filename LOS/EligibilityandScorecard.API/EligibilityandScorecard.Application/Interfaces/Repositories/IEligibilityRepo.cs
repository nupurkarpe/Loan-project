using EligibilityandScorecard.Domain.Models.CreditAndLOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Application.Interfaces.Repositories
{
    public interface IEligibilityRepo
    {
        Task<Eligibility> AddEligibilityAsync(Eligibility eligibility);
        Task<List<Eligibility>> GetEligibilityByCustomerIdAsync(int customerId);
    }
}
