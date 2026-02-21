using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Application.DTO.ScoreCard
{
    public class GenerateScorecardRequestDto
    {
        public int CustomerId { get; set; }
        public decimal MonthlyIncome { get; set; }

        public string EmploymentType { get; set; } = string.Empty;

        public int Age { get; set; }

        public decimal ExistingObligation { get; set; }
    }
}
