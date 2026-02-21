using GeographicService.Application.DTO;
using GeographicService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.Interface
{
    public interface IBranchRepo
    {
        Task<bool> branchNameExists(string name);
        Task<Branch> AddBranch(BranchAddDTO dto);
        Task<bool> CityExists(int cityId);
        Task<bool> AreaExists(int areaId);
        Task<bool> BranchExists(int branchId);
    
        Task<BranchResponseDTO> FetchBranchById(int branchId);
        Task<BranchResponseDTO> UpdateBranch(int branchId, BranchUpdateDTO dto);
        Task<BranchResponseDTO> DeleteBranch(int branchId);
        Task<PagedResult<BranchResponseDTO>> FetchAllBranch(int page, int pageSize, string? search);
    }
}
