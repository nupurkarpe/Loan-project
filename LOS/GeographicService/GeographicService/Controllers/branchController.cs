using GeographicService.Application.DTO;
using GeographicService.Application.Interface;
using GeographicService.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeographicService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class branchController : ControllerBase
    {
        IBranchRepo repo;
        public branchController(IBranchRepo repo)
        {
            this.repo = repo;
        }
        [HttpPost]
        public async Task<IActionResult> AddArea(BranchAddDTO dto)
        {           
            var res = await repo.AddBranch(dto);
            return Ok(ApiResponse.Success("Branch added successfully", res));
        }

        [HttpGet("{branchId}")]
        public async Task<IActionResult> FetchAreaById(int branchId)
        {         
            var res = await repo.FetchBranchById(branchId);
            return Ok(ApiResponse.Success("Branch Details Fetched successfully", res));
        }

        [HttpPatch("{branchId}")]
        public async Task<IActionResult> UpdateArea(int branchId, BranchUpdateDTO dto)
        {
            var res = await repo.UpdateBranch(branchId, dto);
            return Ok(ApiResponse.Success("Branch Details Updated successfully", res));
        }

        [HttpDelete("{branchId}")]
        public async Task<IActionResult> DeleteArea(int branchId)
        {           
            var res = await repo.DeleteBranch(branchId);
            return Ok(ApiResponse.Success("Branch Details Deleted successfully", res));
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllBranch(int page = 1, int pageSize = 10, string? name = null)
        {
            var res = await repo.FetchAllBranch(page, pageSize, name);
            if (res.Items.Count == 0)
            {
                return Ok(ApiResponse.Success("No Branch found ", res));
            }
            return Ok(ApiResponse.Success("Branch fetched successfully", res));
        }
    }
}
