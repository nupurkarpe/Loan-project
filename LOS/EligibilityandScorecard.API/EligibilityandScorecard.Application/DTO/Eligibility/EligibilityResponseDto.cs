using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Application.DTO.Eligibility
{
    public class EligibilityResponseDto
    {
        public int CustomerId { get; set; }
        public bool IsEligible { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime EvaluatedDate { get; set; }

    }
}
