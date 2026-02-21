using ClosureService.API.Common;
using ClosureService.Application.DTO;
using ClosureService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClosureService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForeclosureController : ControllerBase
    {
        private readonly IForeclosureRepo repo;

        public ForeclosureController(IForeclosureRepo repo) => this.repo = repo;

        [HttpPost("request")]
        public async Task<IActionResult> RequestForeclosure([FromBody] ForeclosureRequestDto request)
        {
            var result = await repo.RequestForeclosureAsync(request);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpGet("{loanId}/calculate")]
        public async Task<IActionResult> Calculate(int loanId)
        {
            var result = await repo.CalculateForeclosureAsync(loanId);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> Approve(int id, [FromBody] ForeclosureApprovalRequest request)
        {
            if (!await repo.ApproveForeclosureAsync(id, request))
                throw new InvalidOperationException("Foreclosure not found or not in requested status.");
            return Ok(ApiResponse<object>.Ok(new { message = "Foreclosure approved." }));
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(int id)
        {
            if (!await repo.RejectForeclosureAsync(id))
                throw new InvalidOperationException("Foreclosure not found or not in requested status.");
            return Ok(ApiResponse<object>.Ok(new { message = "Foreclosure rejected." }));
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            if (!await repo.CompleteForeclosureAsync(id))
                throw new InvalidOperationException("Foreclosure not approved or already completed.");
            return Ok(ApiResponse<object>.Ok(new { message = "Foreclosure completed. Loan closed." }));
        }
    }
}
