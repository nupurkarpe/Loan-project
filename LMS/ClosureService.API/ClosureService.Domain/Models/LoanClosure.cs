using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClosureService.Domain.Models
{
    public class LoanClosure : BaseEntity
    {
        [Key]
        public int ClosureId { get; set; }
        public int LoanId { get; set; }
        public ClosureType ClosureType { get; set; }
        public DateTime ClosureDate { get; set; } = DateTime.UtcNow;
        public string? ClosureReason { get; set; }
        public DateTime? OriginalMaturityDate { get; set; }
        public DateTime? ActualMaturityDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDisbursed { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrincipalPaid { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalInterestPaid { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPenaltyPaid { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmountPaid { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OutstandingAtClosure { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ClosureCharges { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal RebateAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal WriteOffAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SettlementAmount { get; set; }
        public string? NocIssued { get; set; }
        public DateTime? NocIssueDate { get; set; }
        public string? NocNumber { get; set; }
        public string? NocFilePath { get; set; }
        public string? ClosureCertificatePath { get; set; }
        public string? ClosureApprovedBy { get; set; }
        public ClosureStatus ClosureStatus { get; set; } = ClosureStatus.Pending;
        public string? Remarks { get; set; }
    }
}
