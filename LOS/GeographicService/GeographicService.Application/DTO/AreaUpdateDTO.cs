using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.DTO
{
    public class AreaUpdateDTO
    {
        public string? code { get; set; }
        public string? name { get; set; }
        public int? cityId { get; set; }
    }
}
