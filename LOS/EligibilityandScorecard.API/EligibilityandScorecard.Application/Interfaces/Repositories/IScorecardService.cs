using EligibilityandScorecard.Application.DTO.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Application.Interfaces.Repositories
{
    public interface IScorecardService
    {
        Task<ScorecardResponseDto> GenerateAsync(GenerateScorecardRequestDto request);
        Task<ScorecardResponseDto?> GetLatestAsync(int customerId);
        Task<List<ScorecardResponseDto>> GetByCustomerIdAsync(int customerId);
    }
}
