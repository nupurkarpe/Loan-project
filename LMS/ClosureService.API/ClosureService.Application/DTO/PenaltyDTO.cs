namespace ClosureService.Application.DTO
{
    public class PenaltyDTO
    {
        public int PenaltyId { get; set; }
        public int LoanId { get; set; }
        public decimal OutstandingAmount { get; set; }
        public string PenaltyStatus { get; set; }
    }
}
