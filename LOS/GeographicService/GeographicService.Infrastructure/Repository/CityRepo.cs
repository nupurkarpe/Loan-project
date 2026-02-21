using AutoMapper;
using GeographicService.Application.DTO;
using GeographicService.Application.Interface;
using GeographicService.Domain.Models;
using GeographicService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace GeographicService.Infrastructure.Repository
{
    public class CityRepo : ICityRepo
    {
        ApplicationDbContext db;
        IMapper mapper;
        HttpClient client;

        public CityRepo(ApplicationDbContext db, IMapper mapper, HttpClient client)
        {
            this.db = db;
            this.mapper = mapper;
            this.client = client;
            client.DefaultRequestHeaders.Add("X-CSCAPI-KEY", "acd2719ded8d6877b7aba2e6a1077617b1ac3fee7d8cfc6f747d12e0d44ecb7a");
        }
        public async Task<List<CityResponseDTO>> AddCity(string countryCode, string stateCode)
        {
            var country = await db.country.FirstOrDefaultAsync(x => x.code == countryCode);
            if (country == null)
            {
                throw new KeyNotFoundException("Country code does not exists");
            }
            var state = await db.state.FirstOrDefaultAsync(x => x.code == stateCode);
            if (state == null)
            {
                throw new KeyNotFoundException("State code does not exists");
            }
            var city = await client.GetFromJsonAsync<List<CityAddDTO>>($" https://api.countrystatecity.in/v1/countries/{countryCode}/states/{stateCode}/cities");

            if (city == null || !city.Any())
                return new List<CityResponseDTO>();
            var cities = mapper.Map<List<City>>(city);
            foreach (var item in cities)
            {
                item.stateId = state.stateId;               
            }
            db.city.AddRange(cities);
            await db.SaveChangesAsync();
            return mapper.Map<List<CityResponseDTO>>(cities);
        }

        public async Task<PagedResult<CityResponseDTO>> FetchAllCity(int page = 1, int pageSize = 10, string? name = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var city = db.city.Where(o => o.deletedAt == null).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                city = city.Where(o => o.name.ToLower() == name);
            }
            var totalItems = await city.CountAsync();

            var cities = await city.Include(c => c.state).OrderByDescending(o => o.createdAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PagedResult<CityResponseDTO>
            {
                Items = mapper.Map<List<CityResponseDTO>>(cities),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return result;
        }

        public async Task<CityResponseDTO> FetchCityByName(string name)
        {
            var c = await db.city.Include(c => c.state).FirstOrDefaultAsync(x => x.name == name.ToUpper());
            if (c == null)
            {
                throw new KeyNotFoundException("City not found");
            }
            var res = mapper.Map<CityResponseDTO>(c);
            return res;
        }

        public async Task<PagedResult<CityResponseDTO>> FetchCityByState(string code, int page = 1, int pageSize = 10, string? name = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var city = db.city.Where(o => o.deletedAt == null && o.state.code == code).AsQueryable();
            if (city == null)
            {
                throw new KeyNotFoundException("State not found");
            }
            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                city = city.Where(o => o.name.ToLower() == name);
            }
            var totalItems = await city.CountAsync();

            var cities = await city.Include(c => c.state).OrderByDescending(o => o.createdAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PagedResult<CityResponseDTO>
            {
                Items = mapper.Map<List<CityResponseDTO>>(cities),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return result;
        }
    }
}
