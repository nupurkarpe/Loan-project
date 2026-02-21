using System.Net.Http.Json;

namespace PaymentService.Application.DTO
{
    public class AuthClient
    {
        private readonly HttpClient client;

        public AuthClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<LoanDTO> GetLoanById(int loanId)
        {
            return await client.GetFromJsonAsync<LoanDTO>($"/api/loans/{loanId}");
        }

        public async Task<List<PenaltyDTO>> GetPenaltiesByLoan(int loanId)
        {
            return await client.GetFromJsonAsync<List<PenaltyDTO>>($"/api/penalties/{loanId}");
        }
    }
}
