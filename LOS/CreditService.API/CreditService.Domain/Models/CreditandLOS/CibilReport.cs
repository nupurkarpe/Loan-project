using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditService.Domain.Models.CreditandLOS
{
    public class CibilReport
    {

        public int Id { get; set; }

        public int CustomerId { get; set; }
        

        public string PanNo { get; set; }
        public int CibilScore { get; set; }
        public string CreditHistory { get; set; }

        public DateTime CheckDate { get; set; }
        public string ApiRefNo { get; set; }
        public string Status { get; set; }
    }
}
