using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.DTO.LoanDeals
{
    public class CreateLoanDealRequestDto
    {
        public int custId { get; set; }

        public int loanTypeId { get; set; }

        public double approvedAmount { get; set; }
    }
}
