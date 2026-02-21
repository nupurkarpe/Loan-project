using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.DTO
{
    public class StateAddDTO
    {
        public int stateId { get; set; }
        public string iso2 { get; set; }
        public string name { get; set; }
        public int countryId { get; set; }
    }
}
