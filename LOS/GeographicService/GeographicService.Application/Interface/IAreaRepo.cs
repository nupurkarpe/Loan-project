using GeographicService.Application.DTO;
using GeographicService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.Interface
{
    public interface IAreaRepo
    {
        Task<bool> AreaNameExists(string name);
        Task<Area> AddArea(AreaAddDTO dto);
        Task<bool> CityExists(int cityId);
        Task<bool> AreaExists(int areaId);
        Task<AreaResponseDTO> FetchAreaById(int areaId);
        Task<AreaResponseDTO> UpdateArea(int areaId, AreaUpdateDTO dto);
        Task<AreaResponseDTO> DeleteArea(int areaId);
        Task<PagedResult<AreaResponseDTO>> FetchAllArea(int page, int pageSize, string? search);
        Task<PagedResult<AreaResponseDTO>> FetchAreaByCity(string cityname, int page = 1, int pageSize = 10, string? name = null);
    }
}
