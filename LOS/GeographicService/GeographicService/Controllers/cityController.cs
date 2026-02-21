using GeographicService.Application.Interface;
using GeographicService.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeographicService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class cityController : ControllerBase
    {
        ICityRepo repo;
        public cityController(ICityRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost("{countryCode}/{stateCode}")]
        public async Task<IActionResult> AddState(string countryCode, string stateCode)
        {
            var res = await repo.AddCity(countryCode, stateCode);
            return Ok(ApiResponse.Success("City added successfully", res));
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> FetchCityByName(string name)
        {
            var res = await repo.FetchCityByName(name);
            return Ok(ApiResponse.Success("State Fetched successfully", res));
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllState(int page = 1, int pageSize = 10, string? name = null)
        {

            var res = await repo.FetchAllCity(page, pageSize, name);
            if (res.Items.Count == 0)
            {
                return Ok(ApiResponse.Success("No State found ", res));
            }
            return Ok(ApiResponse.Success("States fetched successfully", res));
        }

        [HttpGet("state/{code}")]
        public async Task<IActionResult> FetchCityByState(string code, int page = 1, int pageSize = 10, string? name = null)
        {
            var res = await repo.FetchCityByState(code, page, pageSize, name);
            if (res.Items.Count == 0)
            {
                return Ok(ApiResponse.Success("No State found ", res));
            }
            return Ok(ApiResponse.Success("States fetched successfully", res));
        }
    }
}
