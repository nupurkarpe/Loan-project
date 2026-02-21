using EligibilityandScorecard.Application.DTO.ScoreCard;
using EligibilityandScorecard.Application.Interfaces.Repositories;
using EligibilityandScorecard.Domain.Models.CreditAndLOS;
using EligibilityandScorecard.Infrastructure.ExternalServices;

namespace EligibilityandScorecard.Infrastructure.Services
{
    public class ScorecardService : IScorecardService
    {
        private readonly CreditApiClient _creditApiClient;
        private readonly IScorecardRepo _scorecardRepo;

        public ScorecardService(
            CreditApiClient creditApiClient,
            IScorecardRepo scorecardRepo)
        {
            _creditApiClient = creditApiClient;
            _scorecardRepo = scorecardRepo;
        }

        public async Task<ScorecardResponseDto> GenerateAsync(GenerateScorecardRequestDto request)
        {
            var cibil = await _creditApiClient.GetLatestCibilAsync(request.CustomerId);

            if (cibil == null)
                throw new Exception("CIBIL data not found");

            decimal foir = (request.ExistingObligation / request.MonthlyIncome) * 100;

            decimal calculationScore =
                (cibil.CibilScore * 0.6m) +
                ((request.MonthlyIncome / 1000) * 0.2m) +
                ((100 - foir) * 0.2m);

            string riskCategory =
                cibil.CibilScore >= 750 && foir < 40 ? "Low" :
                cibil.CibilScore >= 650 && foir < 50 ? "Medium" :
                "High";

            decimal maxEmi = request.MonthlyIncome * 0.5m;
            decimal eligibleLoanAmount = maxEmi * 60;

            var scorecard = new Scorecard
            {
                CustomerId = request.CustomerId,
                CibilScore = cibil.CibilScore,
                MonthlyIncome = request.MonthlyIncome,
                EmploymentType = request.EmploymentType,
                Age = request.Age,
                ExistingObligation = request.ExistingObligation,
                CalculationScore = calculationScore,
                EligibleLoanAmount = eligibleLoanAmount,
                RiskCategory = riskCategory,
                GeneratedDate = DateTime.UtcNow
            };

            var saved = await _scorecardRepo.AddAsync(scorecard);

            return new ScorecardResponseDto
            {
                ScoreId = saved.ScoreId,
                CustomerId = saved.CustomerId,
                CibilScore = saved.CibilScore,
                MonthlyIncome = saved.MonthlyIncome,
                EmploymentType = saved.EmploymentType,
                Age = saved.Age,
                ExistingObligation = saved.ExistingObligation,
                CalculationScore = saved.CalculationScore,
                EligibleLoanAmount = saved.EligibleLoanAmount,
                RiskCategory = saved.RiskCategory,
                GeneratedDate = saved.GeneratedDate
            };
        }


        public async Task<ScorecardResponseDto?> GetLatestAsync(int customerId)
        {
            var record = await _scorecardRepo.GetLatestAsync(customerId);

            if (record == null)
                return null;

            return new ScorecardResponseDto
            {
                ScoreId = record.ScoreId,
                CustomerId = record.CustomerId,
                CibilScore = record.CibilScore,
                MonthlyIncome = record.MonthlyIncome,
                EmploymentType = record.EmploymentType,
                Age = record.Age,
                ExistingObligation = record.ExistingObligation,
                CalculationScore = record.CalculationScore,
                EligibleLoanAmount = record.EligibleLoanAmount,
                RiskCategory = record.RiskCategory,
                GeneratedDate = record.GeneratedDate
            };
        }

        public async Task<List<ScorecardResponseDto>> GetByCustomerIdAsync(int customerId)
        {
            var records = await _scorecardRepo.GetByCustomerIdAsync(customerId);

            return records.Select(x => new ScorecardResponseDto
            {
                ScoreId = x.ScoreId,
                CustomerId = x.CustomerId,
                CibilScore = x.CibilScore,
                MonthlyIncome = x.MonthlyIncome,
                EmploymentType = x.EmploymentType,
                Age = x.Age,
                ExistingObligation = x.ExistingObligation,
                CalculationScore = x.CalculationScore,
                EligibleLoanAmount = x.EligibleLoanAmount,
                RiskCategory = x.RiskCategory,
                GeneratedDate = x.GeneratedDate
            }).ToList();
        }


    }
}