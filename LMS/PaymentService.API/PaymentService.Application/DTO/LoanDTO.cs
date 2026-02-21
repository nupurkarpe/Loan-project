namespace PaymentService.Application.DTO
{
    public class LoanDTO
    {
        public int LoanId { get; set; }
        public int CustomerId { get; set; }
        public decimal SanctionedAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int TenureMonths { get; set; }
        public decimal EmiAmount { get; set; }
        public decimal DisbursedAmount { get; set; }
        public decimal OutstandingPrincipal { get; set; }
        public decimal OutstandingInterest { get; set; }
        public decimal TotalOutstanding { get; set; }
        public string AccountStatus { get; set; }
        public DateTime? NextEmiDate { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public int Dpd { get; set; }
    }
}
