using GeographicService.Application.DTO;
using GeographicService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.Interface
{
    public interface ICountryRepo
    {
        Task<PagedResult<CountryResponseDTO>> FetchAllCountry(int page = 1, int pageSize = 10, string? name = null);
        Task<List<CountryResponseDTO>> AddCountry();
        Task<CountryResponseDTO> FetchCountryByCode(string code);
    }
}
