using GeographicService.Application.Interface;
using GeographicService.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeographicService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        ICountryRepo repo;
        public CountryController(ICountryRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry()
        {
            var res=await repo.AddCountry();
            return Ok(ApiResponse.Success("Country added successfully", res));
        }
        
        [HttpGet]
        public async Task<IActionResult> FetchAllCountry(int page = 1, int pageSize = 10, string? name = null)
        {

            var res = await repo.FetchAllCountry(page, pageSize, name);
            if (res.Items.Count == 0)
            {
                return Ok(ApiResponse.Success("No country found ", res));
            }
            return Ok(ApiResponse.Success("Countries fetched successfully", res));
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> FetchCountryById(string code)
        {
            var res = await repo.FetchCountryByCode(code);
            return Ok(ApiResponse.Success("Country Details Fetched successfully", res));
        }
    }
}
