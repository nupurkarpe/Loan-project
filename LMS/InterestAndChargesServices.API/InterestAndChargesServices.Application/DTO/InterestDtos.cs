namespace InterestAndChargesServices.Application.DTO
{
    public class InterestAccrualResponse
    {
        public int AccrualId { get; set; }
        public int LoanId { get; set; }
        public DateTime AccrualDate { get; set; }
        public decimal PrincipalBalance { get; set; }
        public decimal InterestRate { get; set; }
        public decimal DailyInterestRate { get; set; }
        public decimal AccruedInterest { get; set; }
        public decimal CumulativeInterest { get; set; }
        public string AccrualType { get; set; }
        public string CalculationMethod { get; set; }
        public string AccrualStatus { get; set; }
        public string? Remarks { get; set; }
    }
}
