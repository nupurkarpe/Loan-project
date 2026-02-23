using InterestService.Application.DTO;
using InterestService.Application.Interfaces;
using InterestService.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterestService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmiController : ControllerBase
  {

    IEmi emiservice;

    public EmiController(IEmi e) {
      emiservice = e;
    }

    [HttpPost("generate-emi")]
    public async Task<IActionResult> GenerateEmi([FromBody] EmiRequestDTO request)
    {
      var result = await emiservice.GenerateSchedule(request);
      return Ok(result);
    }

    [HttpPut("{loanId}/after-payment")]
    public async Task<IActionResult> UpdateEmiAfterPayment(
      int loanId,
      [FromQuery] int? scheduleId,
      [FromBody] UpdateEmiAfterPaymentRequest req)
    {
      var result = await emiservice.UpdateEmiAfterPaymentAsync(loanId, scheduleId, req);
      if (!result)
        return NotFound(new { message = $"No pending EMI found for loan {loanId}." });
      return Ok(new { message = "EMI updated after payment successfully." });
    }
  }
}
