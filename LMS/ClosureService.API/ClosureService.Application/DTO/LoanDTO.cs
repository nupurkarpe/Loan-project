namespace ClosureService.Application.DTO
{
    public class LoanDTO
    {
        public int LoanId { get; set; }
        public decimal SanctionedAmount { get; set; }
        public decimal DisbursedAmount { get; set; }
        public decimal OutstandingPrincipal { get; set; }
        public decimal OutstandingInterest { get; set; }
        public decimal TotalOutstanding { get; set; }
        public int RemainingTenure { get; set; }
        public string AccountStatus { get; set; }
        public DateTime MaturityDate { get; set; }
    }
}
