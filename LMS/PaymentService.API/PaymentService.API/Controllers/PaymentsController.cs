using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.DTO;
using PaymentService.Application.Interfaces;

namespace PaymentService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepo repo;
        public PaymentsController(IPaymentRepo repo)
        {
            this.repo = repo;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment( CreatePaymentRequest request)
        {
            var result = await repo.CreatePaymentAsync(request);
            return Ok(result);
        }
        [HttpGet("{loanId}")]
        public async Task<IActionResult> GetPaymentsByLoan(int loanId)
        {
            return Ok(await repo.GetPaymentsByLoanAsync(loanId));
        }
        [HttpGet("detail/{paymentId}")]
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            var result = await repo.GetPaymentByIdAsync(paymentId)
            ?? throw new KeyNotFoundException("Payment not found.");
            return Ok(result);
        }
        [HttpPost("process/{paymentId}")]
        public async Task<IActionResult> ProcessPayment(int paymentId)
        {
            var result = await repo.ProcessPaymentAsync(paymentId);
            return Ok(result);
        }
    }
}
