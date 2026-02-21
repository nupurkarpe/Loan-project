namespace InterestAndChargesServices.Application.DTO
{
    public class LoanDTO
    {
        public int LoanId { get; set; }
        public decimal InterestRate { get; set; }
        public decimal OutstandingPrincipal { get; set; }
        public decimal OutstandingInterest { get; set; }
        public string AccountStatus { get; set; }
        public int Dpd { get; set; }
        public DateTime? NextEmiDate { get; set; }
    }
}
