using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeographicService.Domain.Models
{
    public class City
    {
        [Key]
        public int cityId { get; set; }
        public string code { get; set; }
        public string name { get; set; }

        [ForeignKey("state")]
        public int stateId { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public int createdBy { get; set; }
        public DateTime? modifiedAt { get; set; }
        public int? modifiedBy { get; set; }
        public DateTime? deletedAt { get; set; }
        public int? deletedBy { get; set; }
        public State state { get; set; }

    }
}
