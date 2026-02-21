using InterestAndChargesServices.API.Common;
using InterestAndChargesServices.Application.DTO;
using InterestAndChargesServices.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterestAndChargesServices.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PenaltiesController : ControllerBase
    {
        private readonly IPenaltyRepo repo;

        public PenaltiesController(IPenaltyRepo repo) => this.repo = repo;

        [HttpGet("{loanId}")]
        public async Task<IActionResult> GetPenalties(int loanId)
        {
            var result = await repo.GetPenaltiesByLoanAsync(loanId);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculatePenalty([FromBody] CalculatePenaltyRequest request)
        {
            var result = await repo.CalculatePenaltyAsync(request);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpPost("waive")]
        public async Task<IActionResult> WaivePenalty([FromBody] WaivePenaltyRequest request)
        {
            if (!await repo.WaivePenaltyAsync(request))
                throw new InvalidOperationException("Penalty not found or already waived.");
            return Ok(ApiResponse<object>.Ok(new { message = "Penalty waived successfully." }));
        }
    }
}
