using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.DTO
{
    public class AreaResponseDTO
    {
        public int areaId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int cityId { get; set; }
        public string cityName { get; set; }
    }
}
