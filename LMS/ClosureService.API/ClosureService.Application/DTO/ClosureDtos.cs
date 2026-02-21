using System.ComponentModel.DataAnnotations;

namespace ClosureService.Application.DTO
{
    public class InitiateClosureRequest
    {
        [Required]
        public int LoanId { get; set; }
        [Required]
        public string ClosureType { get; set; }
        public string? ClosureReason { get; set; }
    }

    public class ClosureResponse
    {
        public int ClosureId { get; set; }
        public int LoanId { get; set; }
        public string ClosureType { get; set; }
        public DateTime ClosureDate { get; set; }
        public string? ClosureReason { get; set; }
        public decimal TotalDisbursed { get; set; }
        public decimal TotalPrincipalPaid { get; set; }
        public decimal TotalInterestPaid { get; set; }
        public decimal TotalPenaltyPaid { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public decimal OutstandingAtClosure { get; set; }
        public decimal ClosureCharges { get; set; }
        public string? NocIssued { get; set; }
        public string? NocNumber { get; set; }
        public string? NocFilePath { get; set; }
        public string? ClosureCertificatePath { get; set; }
        public string ClosureStatus { get; set; }
        public string? Remarks { get; set; }
    }
}
