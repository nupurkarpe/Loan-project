using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.DTO
{
    public class BranchUpdateDTO
    {
        public string? code { get; set; }
        public string? name { get; set; }
        public string? cityName { get; set; }
        public string? areaName { get; set; }
        public string? address { get; set; }
        public string? contact { get; set; }
        public string? email { get; set; }
        public string? branchManager { get; set; }
    }
}
