using CreditService.Application.DTO.Cibils;
using CreditService.Domain.Models.CreditandLOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditService.Application.Interfaces.Repositories
{
    public interface ICreditRepo
    {
        Task<CibilReport> AddCibilAsync(CibilReport report);
        Task<CibilResponseDto> GetLatestCibilAsync(int customerId);
    }
}
