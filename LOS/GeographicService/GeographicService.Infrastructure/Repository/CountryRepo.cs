using AutoMapper;
using GeographicService.Application.DTO;
using GeographicService.Application.Interface;
using GeographicService.Domain.Models;
using GeographicService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace GeographicService.Infrastructure.Repository
{
    public class CountryRepo : ICountryRepo
    {
        ApplicationDbContext db;
        IMapper mapper;
        HttpClient client;

        public CountryRepo(ApplicationDbContext db,IMapper mapper,HttpClient client)
        {
            this.db = db;
            this.mapper = mapper;
            this.client = client;
            client.DefaultRequestHeaders.Add("X-CSCAPI-KEY", "acd2719ded8d6877b7aba2e6a1077617b1ac3fee7d8cfc6f747d12e0d44ecb7a");
        }
        public async Task<List<CountryResponseDTO>> AddCountry()
        {
            var apiCountries = await client.GetFromJsonAsync<List<CountryAddDTO>>("https://api.countrystatecity.in/v1/countries");

            if (apiCountries == null || !apiCountries.Any())
                return new List<CountryResponseDTO>();
            var countries = mapper.Map<List<Country>>(apiCountries);
            db.country.AddRange(countries);
            await db.SaveChangesAsync();
            return mapper.Map<List<CountryResponseDTO>>(countries);
        }


        public async Task<CountryResponseDTO> FetchCountryByCode(string code)
        {
            var coun = await db.country.FirstOrDefaultAsync(d => d.code == code.ToUpper());
            if (coun == null)
            {
                throw new KeyNotFoundException("Country not found");
            }
            var res = mapper.Map<CountryResponseDTO>(coun);
            return res;
        }

        public async Task<PagedResult<CountryResponseDTO>> FetchAllCountry(int page = 1, int pageSize = 10, string? name = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var coun = db.country.Where(o => o.deletedAt == null).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                coun = coun.Where(o => o.name.ToLower() == name);
            }
            var totalItems = await coun.CountAsync();
            var country = await coun.OrderByDescending(o => o.createdAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var result = new PagedResult<CountryResponseDTO>
            {
                Items = mapper.Map<List<CountryResponseDTO>>(country),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
            return result;

        }
    }
}
