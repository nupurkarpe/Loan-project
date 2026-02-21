using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Application.DTO.ScoreCard
{
    public class ScorecardResponseDto
    {
        public int ScoreId { get; set; }
        public int CustomerId { get; set; }

        public int CibilScore { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string EmploymentType { get; set; }
        public int Age { get; set; }

        public decimal ExistingObligation { get; set; }
        public decimal CalculationScore { get; set; }
        public decimal EligibleLoanAmount { get; set; }

        public string RiskCategory { get; set; }
        public DateTime GeneratedDate { get; set; }
    }
}
