using System;

namespace LoanTable.Application.DTO
{
    public class UpdateLoanAfterPaymentRequest
    {
        public decimal PrincipalPaid { get; set; }
        public decimal InterestPaid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
