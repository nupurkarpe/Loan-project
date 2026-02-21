using PaymentService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Interfaces
{
    public interface IPaymentRepo
    {
        Task<PaymentResponse> CreatePaymentAsync(CreatePaymentRequest request);
        Task<List<PaymentResponse>> GetPaymentsByLoanAsync(int loanId);
        Task<PaymentResponse?> GetPaymentByIdAsync(int paymentId);
        Task<PaymentResponse> ProcessPaymentAsync(int paymentId);
    }
}
