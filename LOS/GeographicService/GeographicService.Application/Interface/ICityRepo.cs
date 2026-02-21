using GeographicService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.Interface
{
    public interface ICityRepo
    {
        Task<PagedResult<CityResponseDTO>> FetchAllCity(int page = 1, int pageSize = 10, string? name = null);
        Task<List<CityResponseDTO>> AddCity(string countryCode, string stateCode);
        Task<CityResponseDTO> FetchCityByName(string name);
        Task<PagedResult<CityResponseDTO>> FetchCityByState(string code, int page = 1, int pageSize = 10, string? name = null);
    }
}
