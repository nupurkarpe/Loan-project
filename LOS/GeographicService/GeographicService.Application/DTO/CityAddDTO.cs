using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.DTO
{
    public class CityAddDTO
    {
        public int cityId { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int stateId { get; set; }
    }
}
