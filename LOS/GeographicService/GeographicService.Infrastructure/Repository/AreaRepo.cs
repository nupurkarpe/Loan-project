using AutoMapper;
using GeographicService.Application.DTO;
using GeographicService.Application.Interface;
using GeographicService.Domain.Models;
using GeographicService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Infrastructure.Repository
{
    public class AreaRepo : IAreaRepo

    {
        ApplicationDbContext db;
        IMapper mapper;
        public AreaRepo(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<bool> AreaNameExists(string name)
        {
            return await db.area.AnyAsync(c => c.name.ToLower() == name.ToLower());
        }
        public async Task<Area> AddArea(AreaAddDTO dto)
        {
            var city = await db.city.FirstOrDefaultAsync(c => c.name.ToLower() == dto.cityName.ToLower());
            if (city == null)
                throw new KeyNotFoundException("City not found");
            if (await AreaNameExists(dto.name))
            {
                throw new InvalidOperationException("Area already exists");
            }
            var data = mapper.Map<Area>(dto);
            data.cityId = city.cityId;
            await db.AddAsync(data);
            await db.SaveChangesAsync();
            return data;
        }

        public async Task<bool> AreaExists(int areaId)
        {
            return await db.area.AnyAsync(x => x.areaId == areaId);
        }
        public async Task<bool> CityExists(int cityId)
        {
            return await db.city.AnyAsync(x => x.cityId == cityId);
        }
        public async Task<AreaResponseDTO> FetchAreaById(int areaId)
        {
            var area = await db.area.Include(c => c.city).FirstOrDefaultAsync(s => s.areaId == areaId);
            if (area == null)
                throw new KeyNotFoundException("Area not found");
            var res = mapper.Map<AreaResponseDTO>(area);
            return res;
        }
        public async Task<AreaResponseDTO> UpdateArea(int areaId, AreaUpdateDTO dto)
        {
            var area = await db.area.Include(c => c.city).FirstOrDefaultAsync(s => s.areaId == areaId);
            if (area == null)
                throw new KeyNotFoundException("Area not found");
            var data = mapper.Map(dto, area);
           
            if (dto.cityId.HasValue)
            {
                if (!await CityExists(dto.cityId.Value))
                {
                    throw new InvalidOperationException("City not found");
                }
                else 
                { 
                    area.cityId = dto.cityId.Value;
                }
                
            }
            if (!string.IsNullOrEmpty(dto.name))
            {
                if (await AreaNameExists(dto.name))
                {
                    throw new InvalidOperationException("Area already exists");
                }
            }
            data.modifiedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return mapper.Map<AreaResponseDTO>(area);
        }

        public async Task<AreaResponseDTO> DeleteArea(int areaId)
        {
            var area = await db.area.Include(c => c.city).FirstOrDefaultAsync(s => s.areaId == areaId);
            if (area == null)
                throw new KeyNotFoundException("Area not found");
            area.deletedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return mapper.Map<AreaResponseDTO>(area);
        }

        public async Task<PagedResult<AreaResponseDTO>> FetchAllArea(int page, int pageSize, string? name)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var area = db.area.Where(o => o.deletedAt == null).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                area = area.Where(o => o.name.ToLower() == name);
            }
            var totalItems = await area.CountAsync();

            var areas = await area.OrderByDescending(o => o.createdAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PagedResult<AreaResponseDTO>
            {
                Items = mapper.Map<List<AreaResponseDTO>>(areas),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return result;
        }
        public async Task<PagedResult<AreaResponseDTO>> FetchAreaByCity(string cityname, int page = 1, int pageSize = 10, string? name = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var area = db.area.Where(o => o.deletedAt == null && o.city.name == cityname).AsQueryable();
            if (area == null)
            {
                throw new KeyNotFoundException("City not found");
            }
            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                area = area.Where(o => o.name.ToLower() == name);
            }
            var totalItems = await area.CountAsync();

            var areas = await area.Include(c => c.city).OrderByDescending(o => o.createdAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PagedResult<AreaResponseDTO>
            {
                Items = mapper.Map<List<AreaResponseDTO>>(areas),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
            return result;
        }
    }
}
