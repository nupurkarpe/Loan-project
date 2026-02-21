using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditService.Application.DTO.Cibils
{
    public class CibilCheckRequestDto
    {
        public int CustomerId { get; set; }
        public string PanNo { get; set; } = string.Empty;
    }
}
