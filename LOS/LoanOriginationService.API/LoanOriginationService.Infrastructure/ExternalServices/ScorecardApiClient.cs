using LoanOriginationService.Application.DTO.Scorecard;
using System.Net.Http.Json;

namespace LoanOriginationService.Infrastructure.ExternalServices
{
    public class ScorecardApiClient
    {
        private readonly HttpClient _httpClient;

        public ScorecardApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ScorecardDataDto?> GetLatestScorecardAsync(int customerId)
        {
            var response = await _httpClient.GetAsync($"api/ScoreCard/latest/{customerId}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ScorecardDataDto>();
        }
    }
}
