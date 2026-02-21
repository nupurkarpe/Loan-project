using EligibilityandScorecard.Application.DTO.Eligibility;
using EligibilityandScorecard.Application.Helpers;
using EligibilityandScorecard.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EligibilityandScorecard.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EligibilityController : ControllerBase
    {
        private readonly EligibilityService _service;

        public EligibilityController(EligibilityService service)
        {
            _service = service;
        }

        [HttpPost("evaluate")]
        public async Task<IActionResult> Evaluate([FromBody] EvaluateEligibilityRequest request)
        {
            var result = await _service.CheckEligibilityAsync(request.CustomerId);
            return Ok(ResponseHelper.Success("Eligibility evaluated successfully", result));
        }


        // GET /api/v1/eligibility/{customerId}
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            var result = await _service.GetEligibilityHistoryAsync(customerId);
            return Ok(ResponseHelper.Success("Eligibility evaluated successfully", result));
        }
    }
}

