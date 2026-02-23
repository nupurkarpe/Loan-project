using System;

namespace InterestService.Application.DTO
{
    public class UpdateEmiAfterPaymentRequest
    {
        public decimal PaidAmount { get; set; }
        public decimal PaidInterest { get; set; }
        public DateTime PaidDate { get; set; }
    }
}
