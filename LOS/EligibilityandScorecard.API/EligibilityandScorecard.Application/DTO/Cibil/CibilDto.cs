using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Application.DTO.Cibil
{
    public class CibilDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        [JsonPropertyName("cibilScore")]
        public int CibilScore { get; set; }
    }
}
