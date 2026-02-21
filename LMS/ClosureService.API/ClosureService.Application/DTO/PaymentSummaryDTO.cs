namespace ClosureService.Application.DTO
{
    public class PaymentSummaryDTO
    {
        public int PaymentId { get; set; }
        public int LoanId { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal PrincipalPaid { get; set; }
        public decimal InterestPaid { get; set; }
        public decimal PenaltyPaid { get; set; }
        public string PaymentStatus { get; set; }
    }
}
