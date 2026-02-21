using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditService.Domain.Common
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public ApiError error { get; set; }
    }

    public class ApiError
    {
        public string code { get; set; }
        public string details { get; set; }
    }
}

