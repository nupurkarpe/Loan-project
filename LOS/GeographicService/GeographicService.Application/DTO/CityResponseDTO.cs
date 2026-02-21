using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.DTO
{
    public class CityResponseDTO
    {
        public int cityId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int stateId { get; set; }
        public string stateName { get; set; }
    }
}
