using GeographicService.Application.DTO;
using GeographicService.Application.Interface;
using GeographicService.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeographicService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class areaController : ControllerBase
    {
        IAreaRepo repo;
        public areaController(IAreaRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AddArea(AreaAddDTO dto)
        {         
            var res = await repo.AddArea(dto);
            return Ok(ApiResponse.Success("Area added successfully", res));
        }

        [HttpGet("{areaId}")]
        public async Task<IActionResult> FetchAreaById(int areaId)
        {           
            var res = await repo.FetchAreaById(areaId);
            return Ok(ApiResponse.Success("Area Details Fetched successfully", res));
        }

        [HttpPatch("{areaId}")]
        public async Task<IActionResult> UpdateArea(int areaId, AreaUpdateDTO dto)
        {                                
            var res = await repo.UpdateArea(areaId, dto);
            return Ok(ApiResponse.Success("Area Details Updated successfully", res));
        }

        [HttpDelete("{areaId}")]
        public async Task<IActionResult> DeleteArea(int areaId)
        {
            var res = await repo.DeleteArea(areaId);
            return Ok(ApiResponse.Success("Area Details Deleted successfully", res));
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllArea(int page = 1, int pageSize = 10, string? name = null)
        {
            var res = await repo.FetchAllArea(page, pageSize, name);
            if (res.Items.Count == 0)
            {
                return Ok(ApiResponse.Success("No Area found ", res));
            }
            return Ok(ApiResponse.Success("Areas fetched successfully", res));
        }

        [HttpGet("city/{cityname}")]
        public async Task<IActionResult> FetchAreaByCity(string cityname, int page = 1, int pageSize = 10, string? name = null)
        {
            var res = await repo.FetchAreaByCity(cityname, page, pageSize, name);
            if (res.Items.Count == 0)
            {
                return Ok(ApiResponse.Success("No Area found", res));
            }
            return Ok(ApiResponse.Success("Areas fetched successfully", res));
        }
    }
}
    
