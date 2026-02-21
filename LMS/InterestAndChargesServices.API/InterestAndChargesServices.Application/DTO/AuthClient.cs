using System.Net.Http.Json;

namespace InterestAndChargesServices.Application.DTO
{
    public class AuthClient
    {
        private readonly HttpClient client;

        public AuthClient(HttpClient client) => this.client = client;

        public async Task<LoanDTO?> GetLoanById(int loanId)
            => await client.GetFromJsonAsync<LoanDTO>($"/api/loans/{loanId}");

        public async Task<List<LoanDTO>> GetActiveLoans()
            => await client.GetFromJsonAsync<List<LoanDTO>>("/api/loans/active") ?? new();
    }
}
