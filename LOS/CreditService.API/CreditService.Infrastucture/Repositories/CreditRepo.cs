using AutoMapper;
using CreditService.Application.DTO.Cibils;
using CreditService.Application.Interfaces.Repositories;
using CreditService.Domain.Models.CreditandLOS;
using CreditService.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditService.Infrastructure.Repositories
{
    public class CreditRepo : ICreditRepo
    {
        private readonly ApplicationDbContext _context;

        ClientGetCoustomerId client;
        IMapper mapper;
        public CreditRepo(ApplicationDbContext context, ClientGetCoustomerId client, IMapper mapper)
        {
            _context = context;
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<CibilReport> AddCibilAsync(CibilReport report)
        {
            var cousExists = await client.GetCoustomerById(report.CustomerId);

            if (cousExists == null)
            {
                throw new KeyNotFoundException("Coustomer Not Found");
            }
            _context.CibilReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<CibilResponseDto> GetLatestCibilAsync(int customerId)
        {
            var cus= await  _context.CibilReports
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.CheckDate)
                .FirstOrDefaultAsync();

            return mapper.Map<CibilResponseDto>(cus);

        }
      
    }
}
