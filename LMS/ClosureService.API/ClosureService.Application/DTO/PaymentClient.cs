using System.Net.Http.Json;

namespace ClosureService.Application.DTO
{
    public class PaymentClient
    {
        private readonly HttpClient client;

        public PaymentClient(HttpClient client) => this.client = client;

        public async Task<List<PaymentSummaryDTO>> GetPaymentsByLoan(int loanId)
            => await client.GetFromJsonAsync<List<PaymentSummaryDTO>>($"/api/payments/{loanId}") ?? new();
    }
}
