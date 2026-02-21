using System.Net.Http.Json;

namespace ClosureService.Application.DTO
{
    public class AuthClient
    {
        private readonly HttpClient client;

        public AuthClient(HttpClient client) => this.client = client;

        public async Task<LoanDTO?> GetLoanById(int loanId)
            => await client.GetFromJsonAsync<LoanDTO>($"/api/loans/{loanId}");

        public async Task<List<EmiScheduleDTO>> GetEmiSchedule(int loanId)
            => await client.GetFromJsonAsync<List<EmiScheduleDTO>>($"/api/loans/{loanId}/schedule") ?? new();

        public async Task<bool> UpdateLoanStatus(int loanId, string status)
        {
            var response = await client.PutAsJsonAsync($"/api/loans/{loanId}/status", new { Status = status });
            return response.IsSuccessStatusCode;
        }
    }
}
