using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.DTO.Scorecard
{
    public class ScorecardDataDto
    {
        public int ScoreId { get; set; }

        public int CustomerId { get; set; }

        public int CibilScore { get; set; }

        public decimal EligibleLoanAmount { get; set; }

        public string RiskCategory { get; set; } = string.Empty;
    }
}
