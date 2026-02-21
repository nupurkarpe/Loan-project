using GeographicService.Application.Interface;
using GeographicService.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeographicService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class stateController : ControllerBase
    {
        IStateRepo repo;
        public stateController(IStateRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost("{code}")]
        public async Task<IActionResult> AddState(string code)
        {
            var res = await repo.AddState(code);
            return Ok(ApiResponse.Success("State added successfully", res));
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> FetchStateByCode(string code)
        {
            var res = await repo.FetchStateByCode(code);
            return Ok(ApiResponse.Success("State Fetched successfully", res));
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllState(int page = 1, int pageSize = 10, string? name = null)
        {

            var res = await repo.FetchAllState(page, pageSize, name);
            if (res.Items.Count == 0)
            {
                return Ok(ApiResponse.Success("No State found ", res));
            }
            return Ok(ApiResponse.Success("States fetched successfully", res));
        }

        [HttpGet("country/{code}")]
        public async Task<IActionResult> FetchStateByCountry(string code,int page = 1, int pageSize = 10, string? name = null)
        {
            var res = await repo.FetchStateByCountry(code,page, pageSize, name);
            if (res.Items.Count == 0)
            {
                return Ok(ApiResponse.Success("No State found ", res));
            }
            return Ok(ApiResponse.Success("States fetched successfully", res));
        }
    }
}
