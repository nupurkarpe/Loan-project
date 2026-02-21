using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CreditService.Application.DTO.Cibils
{
    public class ClientGetCoustomerId
    {

        private readonly HttpClient client;

        public ClientGetCoustomerId(HttpClient client)
        {
            this.client = client;
        }
        public async Task<CibilCheckRequestDto> GetCoustomerById(int customerId)
        {
            var response = await client.GetFromJsonAsync<CibilCheckRequestDto>($"{customerId}");
            return response;
        }
    }
}
