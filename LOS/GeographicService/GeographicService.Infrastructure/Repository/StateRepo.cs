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
    public class StateRepo : IStateRepo
    {
        ApplicationDbContext db;
        IMapper mapper;
        HttpClient client;

        public StateRepo(ApplicationDbContext db, IMapper mapper, HttpClient client)
        {
            this.db = db;
            this.mapper = mapper;
            this.client = client;
            client.DefaultRequestHeaders.Add("X-CSCAPI-KEY", "acd2719ded8d6877b7aba2e6a1077617b1ac3fee7d8cfc6f747d12e0d44ecb7a");
        }
        public async Task<List<StateResponseDTO>> AddState(string code)
        {
            var country =await db.country.FirstOrDefaultAsync(x => x.code == code);
            if (country == null)
            {
                throw new KeyNotFoundException("Country code does not exists");
            }
            var states = await client.GetFromJsonAsync<List<StateAddDTO>>($"https://api.countrystatecity.in/v1/countries/{code}/states");

            if (states == null || !states.Any())
                return new List<StateResponseDTO>();
            var state = mapper.Map<List<State>>(states);
            foreach(var item in state)
            {
                item.countryId = country.countryId;
            }
            db.state.AddRange(state);
            await db.SaveChangesAsync();
            return mapper.Map<List<StateResponseDTO>>(state);
        }

        public async Task<PagedResult<StateResponseDTO>> FetchAllState(int page = 1, int pageSize = 10, string? name = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var state = db.state.Where(o => o.deletedAt == null).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                state = state.Where(o => o.name.ToLower() == name);
            }
            var totalItems = await state.CountAsync();

            var states = await state.Include(c => c.country).OrderByDescending(o => o.createdAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PagedResult<StateResponseDTO>
            {
                Items = mapper.Map<List<StateResponseDTO>>(states),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return result;
        }

        public async Task<StateResponseDTO> FetchStateByCode(string code)
        {
            var s = await db.state.Include(c=>c.country).FirstOrDefaultAsync(x => x.code == code.ToUpper());
            if (s == null)
            {
                throw new KeyNotFoundException("State not found");
            }
            var res = mapper.Map<StateResponseDTO>(s);
            return res;
        }

        public async Task<PagedResult<StateResponseDTO>> FetchStateByCountry(string code,int page = 1, int pageSize = 10, string? name = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var state = db.state.Where(o => o.deletedAt == null && o.country.code == code).AsQueryable();
            if (state == null)
            {
                throw new KeyNotFoundException("Country not found");
            }
            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                state = state.Where(o => o.name.ToLower() == name);
            }
            var totalItems = await state.CountAsync();

            var states = await state.Include(c => c.country).OrderByDescending(o => o.createdAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PagedResult<StateResponseDTO>
            {
                Items = mapper.Map<List<StateResponseDTO>>(states),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return result;
        }
    }
}
