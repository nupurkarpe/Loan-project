using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GeographicService.Application.DTO
{
    public class CountryAddDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string iso2 { get; set; }

    }
}
