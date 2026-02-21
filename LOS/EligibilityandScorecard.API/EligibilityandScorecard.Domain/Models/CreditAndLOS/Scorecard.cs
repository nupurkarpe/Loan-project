using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Domain.Models.CreditAndLOS
{
    public class Scorecard
    {
        [Key]
        public int ScoreId { get; set; }

        public int CustomerId { get; set; }


        public int CibilScore { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string EmploymentType { get; set; } = string.Empty;
        public int Age { get; set; }

        public decimal ExistingObligation { get; set; }
        public decimal CalculationScore { get; set; }
        public decimal EligibleLoanAmount { get; set; }

        public string RiskCategory { get; set; } = string.Empty;
        public DateTime GeneratedDate { get; set; }
    }
}
