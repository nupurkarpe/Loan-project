using ClosureService.API.Common;
using ClosureService.Application.DTO;
using ClosureService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClosureService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClosureController : ControllerBase
    {
        private readonly IClosureRepo repo;

        public ClosureController(IClosureRepo repo) => this.repo = repo;

        [HttpPost("initiate")]
        public async Task<IActionResult> InitiateClosure([FromBody] InitiateClosureRequest request)
        {
            var result = await repo.InitiateClosureAsync(request);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpGet("{loanId}")]
        public async Task<IActionResult> GetClosure(int loanId)
        {
            var result = await repo.GetClosureByLoanAsync(loanId)
                ?? throw new KeyNotFoundException("No closure found for this loan.");
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpGet("{loanId}/noc")]
        public async Task<IActionResult> GenerateNoc(int loanId)
        {
            var result = await repo.GenerateNocAsync(loanId);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpGet("{loanId}/certificate")]
        public async Task<IActionResult> GenerateCertificate(int loanId)
        {
            var result = await repo.GenerateCertificateAsync(loanId);
            return Ok(ApiResponse<object>.Ok(result));
        }
    }
}
