using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClosureService.Domain.Models
{
    public class Foreclosure : BaseEntity
    {
        [Key]
        public int ForeclosureId { get; set; }
        public int LoanId { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime? ForeclosureDate { get; set; }
        public int? RequestedBy { get; set; }
        public ForeclosureStatus ApprovalStatus { get; set; } = ForeclosureStatus.Requested;
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrincipalOutstanding { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestOutstanding { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PenaltyOutstanding { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ForeclosureCharges { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal RebateAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPayable { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; } = 0;
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMode { get; set; }
        public string? TransactionId { get; set; }
        public string? ForeclosureType { get; set; }
        public int RemainingTenure { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SavingsAmount { get; set; }
        public bool CertificateGenerated { get; set; } = false;
        public string? CertificatePath { get; set; }
        public string? Remarks { get; set; }
        public string? RejectionReason { get; set; }
    }
}
