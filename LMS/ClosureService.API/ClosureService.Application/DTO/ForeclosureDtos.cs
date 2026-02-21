using System.ComponentModel.DataAnnotations;

namespace ClosureService.Application.DTO
{
    public class ForeclosureRequestDto
    {
        [Required]
        public int LoanId { get; set; }
        public int? RequestedBy { get; set; }
        public string? ForeclosureType { get; set; }
    }

    public class ForeclosureCalculationResponse
    {
        public int LoanId { get; set; }
        public decimal PrincipalOutstanding { get; set; }
        public decimal InterestOutstanding { get; set; }
        public decimal PenaltyOutstanding { get; set; }
        public decimal ForeclosureCharges { get; set; }
        public decimal RebateAmount { get; set; }
        public decimal TotalPayable { get; set; }
        public int RemainingTenure { get; set; }
        public decimal SavingsAmount { get; set; }
    }

    public class ForeclosureApprovalRequest
    {
        [Required]
        public string ApprovedBy { get; set; }
    }

    public class ForeclosureResponse
    {
        public int ForeclosureId { get; set; }
        public int LoanId { get; set; }
        public int? RequestedBy { get; set; }
        public string? ForeclosureType { get; set; }
        public decimal PrincipalOutstanding { get; set; }
        public decimal InterestOutstanding { get; set; }
        public decimal PenaltyOutstanding { get; set; }
        public decimal ForeclosureCharges { get; set; }
        public decimal RebateAmount { get; set; }
        public decimal TotalPayable { get; set; }
        public int RemainingTenure { get; set; }
        public decimal SavingsAmount { get; set; }
        public string ApprovalStatus { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? ForeclosureDate { get; set; }
        public bool CertificateGenerated { get; set; }
        public string? CertificatePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
