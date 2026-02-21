using EligibilityandScorecard.Application.DTO.Cibil;
using EligibilityandScorecard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Infrastructure.ExternalServices
{
    public class CreditApiClient
    {
        private readonly HttpClient _httpClient;

        public CreditApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CibilDto> GetLatestCibilAsync(int customerId)
        {
            var request = new CibilDto
            {
                CustomerId = customerId
            };

            var response = await _httpClient.PostAsJsonAsync(
                "api/Credit/cibil-check",
                request);

            response.EnsureSuccessStatusCode();

            var apiResponse =
     await response.Content.ReadFromJsonAsync<ApiResponse<CibilDto>>();

            return apiResponse?.data;

        }

    }
}
