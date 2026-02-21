using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.Models
{
    public enum PaymentMethod
    {
        UPI,
        NetBanking,
        Cash,
        Cheque,
        NEFT,
        RTGS
    }
}
