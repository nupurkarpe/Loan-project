using CreditService.Application.DTO.Cibils;
using CreditService.Application.Interfaces.Repositories;
using CreditService.Domain.Models.CreditandLOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditService.Infrastucture.Services
{
    public class CreditSer
    {
        private readonly ICreditRepo _repo;

        public CreditSer(ICreditRepo repo)
        {
            _repo = repo;
        }

        public async Task<CibilResponseDto> CheckCibilAsync(CibilCheckRequestDto request)
        {
            var report = new CibilReport
            {
                CustomerId = request.CustomerId,
                PanNo = request.PanNo,
                CibilScore = 720,
                CreditHistory = "Good",
                CheckDate = DateTime.UtcNow,
                ApiRefNo = Guid.NewGuid().ToString(),
                Status = "Success"
            };

            var saved = await _repo.AddCibilAsync(report);

            return new CibilResponseDto
            {
                Id = saved.Id,
                CustomerId = saved.CustomerId,
                CibilScore = saved.CibilScore,
                CreditHistory = saved.CreditHistory,
                CheckDate = saved.CheckDate,
                Status = saved.Status
            };
        }

        public async Task<string> CalculateRiskAsync(int customerId)
        {
            var cibil = await _repo.GetLatestCibilAsync(customerId);

            if (cibil == null)
                throw new Exception("CIBIL not found");

            if (cibil.CibilScore >= 750) return "Low";
            if (cibil.CibilScore >= 650) return "Medium";
            return "High";
        }


    }
}
