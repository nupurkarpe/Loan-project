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

        public async Task UpdateLoanAfterPayment(int loanId, decimal principalPaid, decimal interestPaid, DateTime paymentDate)
        {
            var payload = new
            {
                PrincipalPaid = principalPaid,
                InterestPaid = interestPaid,
                PaymentDate = paymentDate
            };
            var response = await client.PutAsJsonAsync($"/api/Loan/{loanId}/after-payment", payload);
            response.EnsureSuccessStatusCode();
        }
    }

    public class LoanTableApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}

