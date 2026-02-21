using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.DTO
{
    public class CreatePaymentRequest
    {
        [Required]
        public int LoanId { get; set; }
        public int? ScheduleId { get; set; }
        [Required]
        public decimal PaymentAmount { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
        public string? TransactionReference { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? Remarks { get; set; }
    }
    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public int LoanId { get; set; }
        public int? ScheduleId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal PrincipalPaid { get; set; }
        public decimal InterestPaid { get; set; }
        public decimal PenaltyPaid { get; set; }
        public decimal AdvancePayment { get; set; }
        public string PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
        public string? TransactionReference { get; set; }
        public string PaymentStatus { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? ReceiptNumber { get; set; }
        public string? Remarks { get; set; }
    }
}
