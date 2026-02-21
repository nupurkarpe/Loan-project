using System.ComponentModel.DataAnnotations;

namespace InterestAndChargesServices.Application.DTO
{
    public class PenaltyResponse
    {
        public int PenaltyId { get; set; }
        public int LoanId { get; set; }
        public int? ScheduleId { get; set; }
        public string ChargeType { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal OutstandingAmount { get; set; }
        public decimal WaiverAmount { get; set; }
        public DateTime ChargeDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int DaysOverdue { get; set; }
        public string PenaltyStatus { get; set; }
        public string? WaiverReason { get; set; }
        public string? Remarks { get; set; }
    }

    public class CalculatePenaltyRequest
    {
        [Required]
        public int LoanId { get; set; }
        public int? ScheduleId { get; set; }
        [Required]
        public string ChargeType { get; set; }
        [Required]
        public decimal ChargeAmount { get; set; }
        public decimal PenaltyRate { get; set; }
    }

    public class WaivePenaltyRequest
    {
        [Required]
        public int PenaltyId { get; set; }
        public decimal WaiverAmount { get; set; }
        [Required]
        public string WaiverReason { get; set; }
        [Required]
        public string WaiverApprovedBy { get; set; }
    }
}
