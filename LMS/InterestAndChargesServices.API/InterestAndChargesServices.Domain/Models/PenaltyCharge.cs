using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterestAndChargesServices.Domain.Models
{
    public class PenaltyCharge : BaseEntity
    {
        [Key]
        public int PenaltyId { get; set; }
        public int LoanId { get; set; }
        public int? ScheduleId { get; set; }
        public PenaltyType ChargeType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ChargeAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal OutstandingAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal WaiverAmount { get; set; } = 0;
        public DateTime ChargeDate { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal PenaltyRate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CalculationBase { get; set; }
        public int DaysOverdue { get; set; }
        public PenaltyStatus PenaltyStatus { get; set; } = PenaltyStatus.Pending;
        public string? WaiverReason { get; set; }
        public string? WaiverApprovedBy { get; set; }
        public DateTime? WaiverDate { get; set; }
        public string? Remarks { get; set; }
    }
}
