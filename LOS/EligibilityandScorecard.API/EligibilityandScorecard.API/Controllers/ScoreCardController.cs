using EligibilityandScorecard.Application.DTO.ScoreCard;
using EligibilityandScorecard.Application.Interfaces.Repositories;
using EligibilityandScorecard.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EligibilityandScorecard.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreCardController : ControllerBase
    {
        private readonly IScorecardService _scorecardService;

        public ScoreCardController(IScorecardService scorecardService)
        {
            _scorecardService = scorecardService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] GenerateScorecardRequestDto request)
        {
            var result = await _scorecardService.GenerateAsync(request);
            return Ok(result);
        }

        [HttpGet("latest/{customerId}")]
        public async Task<IActionResult> GetLatest(int customerId)
        {
            var result = await _scorecardService.GetLatestAsync(customerId);
            return Ok(result);
        }

        [HttpGet("all/{customerId}")]
        public async Task<IActionResult> GetAll(int customerId)
        {
            var result = await _scorecardService.GetByCustomerIdAsync(customerId);
            return Ok(result);
        }
    }
}