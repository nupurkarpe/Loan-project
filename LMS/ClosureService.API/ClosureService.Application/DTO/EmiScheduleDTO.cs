namespace ClosureService.Application.DTO
{
    public class EmiScheduleDTO
    {
        public int ScheduleId { get; set; }
        public int LoanId { get; set; }
        public int InstallmentNumber { get; set; }
        public decimal EmiAmount { get; set; }
        public string PaymentStatus { get; set; }
    }
}
