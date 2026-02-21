using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeographicService.Domain.Models
{
    public class Branch
    {
        [Key]
        public int branchId { get; set; }
        public string code { get; set; }
        public string name { get; set; }

        [ForeignKey("city")]
        public int cityId { get; set; }

        [ForeignKey("area")]
        public int areaId { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string branchManager { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public int createdBy { get; set; }
        public DateTime? modifiedAt { get; set; }
        public int? modifiedBy { get; set; }
        public DateTime? deletedAt { get; set; }
        public int? deletedBy { get; set; }
        public City city { get; set; }
        public Area area { get; set; }
    }
}
