using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.DTO
{
    public class CountryResponseDTO
    {
        public int countryId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
}
