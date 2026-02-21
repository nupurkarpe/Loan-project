using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditService.Application.DTO.Cibils
{
    public class CibilResponseDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CibilScore { get; set; }
        public string CreditHistory { get; set; } = string.Empty;
        public DateTime CheckDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
