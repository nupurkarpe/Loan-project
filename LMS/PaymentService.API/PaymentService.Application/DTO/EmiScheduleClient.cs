using System.Net.Http.Json;

namespace PaymentService.Application.DTO
{
    public class EmiScheduleClient
    {
        private readonly HttpClient client;

        public EmiScheduleClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task UpdateEmiAfterPayment(int loanId, int? scheduleId, decimal paidAmount, decimal paidInterest, DateTime paidDate)
        {
            var payload = new
            {
                PaidAmount = paidAmount,
                PaidInterest = paidInterest,
                PaidDate = paidDate
            };

            var url = scheduleId.HasValue
                ? $"/api/Emi/{loanId}/after-payment?scheduleId={scheduleId.Value}"
                : $"/api/Emi/{loanId}/after-payment";

            var response = await client.PutAsJsonAsync(url, payload);
            // Log but don't throw — EMI update failure should not roll back payment
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[EmiScheduleClient] Warning: EMI update returned {response.StatusCode}: {body}");
            }
        }
    }
}
