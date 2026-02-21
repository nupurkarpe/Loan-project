using InterestAndChargesServices.API.Common;
using InterestAndChargesServices.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterestAndChargesServices.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestController : ControllerBase
    {
        private readonly IInterestRepo repo;

        public InterestController(IInterestRepo repo) => this.repo = repo;

        [HttpGet("accruals/{loanId}")]
        public async Task<IActionResult> GetAccruals(int loanId)
        {
            var result = await repo.GetAccrualsAsync(loanId);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpPost("daily-accrual")]
        public async Task<IActionResult> RunDailyAccrual()
        {
            var count = await repo.RunDailyAccrualAsync();
            return Ok(ApiResponse<object>.Ok(new { count }, $"Accrual completed for {count} active loan(s)."));
        }
    }
}
