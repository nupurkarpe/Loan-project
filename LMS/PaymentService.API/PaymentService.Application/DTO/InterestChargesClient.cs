using System.Net.Http.Json;

namespace PaymentService.Application.DTO
{
    public class InterestChargesClient
    {
        private readonly HttpClient client;

        public InterestChargesClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<PenaltyDTO>> GetPenaltiesByLoan(int loanId)
        {
            return await client.GetFromJsonAsync<List<PenaltyDTO>>($"/api/Penalties/{loanId}") ?? new();
        }
    }
}
