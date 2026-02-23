namespace LoanTable.Application.DTO
{
    public class GetDealDTO
    {
        public int dealId { get; set; }

        public int custId { get; set; }

        public int scorecardId { get; set; }

        public double eligibleAmount { get; set; }

        public double approvedAmount { get; set; }

        public string currentStatus { get; set; }
    }
}
