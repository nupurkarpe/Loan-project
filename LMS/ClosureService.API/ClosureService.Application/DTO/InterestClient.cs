using System.Net.Http.Json;

namespace ClosureService.Application.DTO
{
    public class InterestClient
    {
        private readonly HttpClient client;

        public InterestClient(HttpClient client) => this.client = client;

        public async Task<List<PenaltyDTO>> GetPenaltiesByLoan(int loanId)
            => await client.GetFromJsonAsync<List<PenaltyDTO>>($"/api/penalties/{loanId}") ?? new();
    }
}
