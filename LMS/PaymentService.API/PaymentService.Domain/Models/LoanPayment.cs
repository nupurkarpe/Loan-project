using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.Models
{
    public class LoanPayment
    {
        [Key]
        public int PaymentId { get; set; }

        public int? LoanId { get; set; }

        public int? ScheduleId { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public decimal PaymentAmount { get; set; }
        public decimal PrincipalPaid { get; set; }
        public decimal InterestPaid { get; set; }
        public decimal PenaltyPaid { get; set; }
        public decimal AdvancePayment { get; set; } = 0;
        public PaymentMethod PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
        public string? TransactionReference { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? ReceiptNumber { get; set; }
        public string? Remarks { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
