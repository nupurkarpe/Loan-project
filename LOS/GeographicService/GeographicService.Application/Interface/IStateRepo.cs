using GeographicService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.Interface
{
    public interface IStateRepo
    {
        Task<PagedResult<StateResponseDTO>> FetchAllState(int page = 1, int pageSize = 10, string? name = null);
        Task<List<StateResponseDTO>> AddState(string code);
        Task<StateResponseDTO> FetchStateByCode(string code);
        Task<PagedResult<StateResponseDTO>> FetchStateByCountry(string code,int page = 1, int pageSize = 10, string? name = null);
    }
}
