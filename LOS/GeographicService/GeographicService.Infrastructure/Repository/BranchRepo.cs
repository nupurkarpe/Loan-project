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
    public class BranchRepo : IBranchRepo
    {
        ApplicationDbContext db;
        IMapper mapper;
        public BranchRepo(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<bool> branchNameExists(string name)
        {
            return await db.branch.AnyAsync(c => c.name.ToLower() == name.ToLower());
        }
        public async Task<Branch> AddBranch(BranchAddDTO dto)
        {
            var city = await db.city.FirstOrDefaultAsync(c =>c.name.ToLower() == dto.cityName.ToLower());

            if (city == null)
                throw new InvalidOperationException("City not found");
           
            var area = await db.area.FirstOrDefaultAsync(a =>a.name.ToLower() == dto.areaName.ToLower() && a.cityId == city.cityId);

            if (area == null)
                throw new InvalidOperationException("Area not found under this city");
            if(await branchNameExists(dto.name))
            {
                throw new InvalidOperationException("Branch Name already exists");
            }
            var data = mapper.Map<Branch>(dto);
            data.cityId = city.cityId;
            data.areaId = area.areaId;       
            await db.AddAsync(data);
            await db.SaveChangesAsync();
            return data;
        }

        public async Task<bool> AreaExists(int areaId)
        {
            return await db.area.AnyAsync(x => x.areaId == areaId);
        }

        public async Task<bool> BranchExists(int branchId)
        {
            return await db.branch.AnyAsync(x => x.branchId == branchId);
        }       
        public async Task<bool> CityExists(int cityId)
        {
            return await db.city.AnyAsync(x => x.cityId == cityId);
        }

        public async Task<BranchResponseDTO> FetchBranchById(int branchId)
        {
            var branch = await db.branch.Include(c => c.city).Include(a => a.area).FirstOrDefaultAsync(s => s.branchId == branchId);
            var res = mapper.Map<BranchResponseDTO>(branch);
            return res;
        }
        public async Task<BranchResponseDTO> UpdateBranch(int branchId, BranchUpdateDTO dto)
        {
            var branch = await db.branch.Include(b => b.city).Include(b => b.area).FirstOrDefaultAsync(b => b.branchId == branchId);
            if (branch == null)
                throw new InvalidOperationException("Branch not found");
    
            mapper.Map(dto, branch);

            if (!string.IsNullOrEmpty(dto.cityName))
            {
                var city = await db.city.FirstOrDefaultAsync(c =>c.name.ToLower() == dto.cityName.ToLower());
                if (city == null)
                    throw new InvalidOperationException("City not found");

                branch.cityId = city.cityId;

                if (!string.IsNullOrEmpty(dto.areaName))
                {
                    var area = await db.area.FirstOrDefaultAsync(a =>a.name.ToLower() == dto.areaName.ToLower() && a.cityId == city.cityId);
                    if (area == null)
                        throw new InvalidOperationException("Area not found under this city");
                    branch.areaId = area.areaId;
                }
            }
            else if (!string.IsNullOrEmpty(dto.areaName))
            {              
                var area = await db.area.FirstOrDefaultAsync(a =>a.name.ToLower() == dto.areaName.ToLower() &&a.cityId == branch.cityId);

                if (area == null)
                    throw new InvalidOperationException("Area not found under current city");

                branch.areaId = area.areaId;
            }
            if (!string.IsNullOrEmpty(dto.name))
            {
                if (await branchNameExists(dto.name))
                {
                    throw new InvalidOperationException("Branch Name already exists");
                }
            }
            branch.modifiedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return mapper.Map<BranchResponseDTO>(branch);
        }

        public async Task<BranchResponseDTO> DeleteBranch(int branchId)
        {
            var b = await db.branch.Include(c => c.city).Include(a => a.area).FirstOrDefaultAsync(s => s.branchId == branchId);
            if (b == null)
                throw new InvalidOperationException("Branch not found");
            b.deletedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return mapper.Map<BranchResponseDTO>(b);
        }

        public async Task<PagedResult<BranchResponseDTO>> FetchAllBranch(int page, int pageSize, string? name)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var branch = db.branch.Include(c => c.city).Include(a => a.area).Where(o => o.deletedAt == null).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                branch = branch.Where(o => o.name.ToLower() == name);
            }
            var totalItems = await branch.CountAsync();

            var branches = await branch.OrderByDescending(o => o.createdAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PagedResult<BranchResponseDTO>
            {
                Items = mapper.Map<List<BranchResponseDTO>>(branches),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return result;
        }
    }
}
