using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterestAndChargesServices.Domain.Models
{
    public class InterestAccrual : BaseEntity
    {
        [Key]
        public int AccrualId { get; set; }
        public int LoanId { get; set; }
        public DateTime AccrualDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrincipalBalance { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }
        [Column(TypeName = "decimal(8,6)")]
        public decimal DailyInterestRate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AccruedInterest { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CumulativeInterest { get; set; }
        public AccrualType AccrualType { get; set; } = AccrualType.Regular;
        public CalculationMethod CalculationMethod { get; set; } = CalculationMethod.Reducing;
        public int DaysInMonth { get; set; }
        public AccrualStatus AccrualStatus { get; set; } = AccrualStatus.Pending;
        public string? Remarks { get; set; }
    }
}
