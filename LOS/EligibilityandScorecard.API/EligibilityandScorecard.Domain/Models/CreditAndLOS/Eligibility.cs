using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Domain.Models.CreditAndLOS
{
    public class Eligibility
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
    

        public int CibilId { get; set; }
       

        public bool IsEligible { get; set; }
        public string RejectionReason { get; set; }
        public DateTime EvaluatedDate { get; set; }
    }
}
