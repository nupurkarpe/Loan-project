using CreditService.Application.DTO.Cibils;
using CreditService.Application.Helpers;
using CreditService.Infrastucture.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly CreditSer _service;

        public CreditController(CreditSer service)
        {
            _service = service;
        }

        [HttpPost("cibil-check")]
        public async Task<IActionResult> CheckCibil([FromBody] CibilCheckRequestDto request)
        {
            var result = await _service.CheckCibilAsync(request);

            return Ok(ResponseHelper.Success("CIBIL checked successfully", result));
        }

        [HttpGet("risk/{customerId}")]
        public async Task<IActionResult> GetRisk(int customerId)
        {
            var risk = await _service.CalculateRiskAsync(customerId);
            return Ok(risk);
        }


    }
}
