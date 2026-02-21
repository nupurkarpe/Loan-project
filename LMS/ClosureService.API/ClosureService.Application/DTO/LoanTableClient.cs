using System.Net.Http.Json;

namespace ClosureService.Application.DTO
{
    public class LoanTableClient
    {
        private readonly HttpClient client;

        public LoanTableClient(HttpClient client) => this.client = client;

        public async Task<LoanDTO?> GetLoanById(int loanId)
        {
            var response = await client.GetFromJsonAsync<LoanTableApiResponse<LoanDTO>>($"/api/Loan/{loanId}");
            return response?.Data;
        }

        public async Task<List<EmiScheduleDTO>> GetEmiSchedule(int loanId)
        {
            var response = await client.GetFromJsonAsync<LoanTableApiResponse<List<EmiScheduleDTO>>>($"/loans/{loanId}/schedule");
            return response?.Data ?? new();
        }

        public async Task<bool> UpdateLoanStatus(int loanId, string status)
        {
            var response = await client.PatchAsJsonAsync($"/update/{loanId}?status={status}", new { });
            return response.IsSuccessStatusCode;
        }
    }

    public class LoanTableApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
