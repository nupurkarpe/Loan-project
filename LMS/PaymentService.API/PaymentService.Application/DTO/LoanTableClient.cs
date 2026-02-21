using System.Net.Http.Json;

namespace PaymentService.Application.DTO
{
    public class LoanTableClient
    {
        private readonly HttpClient client;

        public LoanTableClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<LoanDTO> GetLoanById(int loanId)
        {
            var response = await client.GetFromJsonAsync<LoanTableApiResponse<LoanDTO>>($"/api/Loan/{loanId}");
            return response?.Data;
        }
    }

    public class LoanTableApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
