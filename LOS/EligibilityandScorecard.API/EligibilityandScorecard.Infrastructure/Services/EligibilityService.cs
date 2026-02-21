using EligibilityandScorecard.Application.DTO.Eligibility;
using EligibilityandScorecard.Application.Interfaces.Repositories;
using EligibilityandScorecard.Domain.Models.CreditAndLOS;
using EligibilityandScorecard.Infrastructure.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Infrastructure.Services
{
    public class EligibilityService
    {
        private readonly CreditApiClient _creditApiClient;

        private readonly IEligibilityRepo _eligibilityRepo;

        public EligibilityService(CreditApiClient creditApiClient,
                           IEligibilityRepo eligibilityRepo)
        {
            _creditApiClient = creditApiClient;
            _eligibilityRepo = eligibilityRepo;
        }

        public async Task<EligibilityResponseDto> CheckEligibilityAsync(int customerId)
        {
            var cibil = await _creditApiClient.GetLatestCibilAsync(customerId);


            if (cibil == null)
                throw new Exception("CIBIL not found");

            var eligibility = new Eligibility
            {
                CustomerId = customerId,
                CibilId = cibil.Id,
                EvaluatedDate = DateTime.UtcNow,
                IsEligible = cibil.CibilScore >= 650,
                RejectionReason =  cibil.CibilScore < 650
                    ? "Low CIBIL Score"
                    : "Approved"

            };
            Console.WriteLine("CIBIL SCORE RECEIVED: " + cibil.CibilScore);


            var saved = await _eligibilityRepo.AddEligibilityAsync(eligibility);

            return new EligibilityResponseDto
            {
                CustomerId = saved.CustomerId,
                IsEligible = saved.IsEligible,
                RejectionReason = saved.RejectionReason,
                EvaluatedDate = saved.EvaluatedDate
            };

        }

        public async Task<List<EligibilityResponseDto>> GetEligibilityHistoryAsync(int customerId)
        {
            var records = await _eligibilityRepo.GetEligibilityByCustomerIdAsync(customerId);

            return records.Select(e => new EligibilityResponseDto
            {
                CustomerId = e.CustomerId,
                IsEligible = e.IsEligible,
                RejectionReason = e.RejectionReason,
                EvaluatedDate = e.EvaluatedDate
            }).ToList();
        }
    }
}
