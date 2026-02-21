using AutoMapper;
using InterestAndChargesServices.Application.DTO;
using InterestAndChargesServices.Application.Interfaces;
using InterestAndChargesServices.Domain.Models;
using InterestAndChargesServices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InterestAndChargesServices.Infrastructure.Repository
{
    public class PenaltyRepo : IPenaltyRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly LoanTableClient authClient;

        public PenaltyRepo(ApplicationDbContext db, IMapper mapper, LoanTableClient authClient)
        {
            this.db = db;
            this.mapper = mapper;
            this.authClient = authClient;
        }

        public async Task<List<PenaltyResponse>> GetPenaltiesByLoanAsync(int loanId)
        {
            var penalties = await db.PenaltyCharges
                .Where(p => p.LoanId == loanId)
                .OrderByDescending(p => p.ChargeDate)
                .ToListAsync();
            return mapper.Map<List<PenaltyResponse>>(penalties);
        }

        public async Task<PenaltyResponse> CalculatePenaltyAsync(CalculatePenaltyRequest request)
        {
            if (!Enum.TryParse<PenaltyType>(request.ChargeType, ignoreCase: true, out var penaltyType))
                throw new Exception($"Invalid penalty type: {request.ChargeType}");

            var loan = await authClient.GetLoanById(request.LoanId)
                ?? throw new Exception($"Loan with ID {request.LoanId} not found.");

            var penalty = new PenaltyCharge
            {
                LoanId = request.LoanId,
                ScheduleId = request.ScheduleId,
                ChargeType = penaltyType,
                ChargeAmount = request.ChargeAmount,
                OutstandingAmount = request.ChargeAmount,
                PenaltyRate = request.PenaltyRate,
                CalculationBase = loan.OutstandingPrincipal,
                DaysOverdue = loan.Dpd,
                PenaltyStatus = PenaltyStatus.Pending,
                ChargeDate = DateTime.UtcNow
            };

            db.PenaltyCharges.Add(penalty);
            await db.SaveChangesAsync();
            return mapper.Map<PenaltyResponse>(penalty);
        }

        public async Task<bool> WaivePenaltyAsync(WaivePenaltyRequest request)
        {
            var penalty = await db.PenaltyCharges.FindAsync(request.PenaltyId);
            if (penalty == null || penalty.PenaltyStatus == PenaltyStatus.Waived) return false;

            penalty.WaiverAmount = request.WaiverAmount > 0 ? request.WaiverAmount : penalty.OutstandingAmount;
            penalty.OutstandingAmount -= penalty.WaiverAmount;
            if (penalty.OutstandingAmount < 0) penalty.OutstandingAmount = 0;
            penalty.WaiverReason = request.WaiverReason;
            penalty.WaiverApprovedBy = request.WaiverApprovedBy;
            penalty.WaiverDate = DateTime.UtcNow;
            penalty.PenaltyStatus = penalty.OutstandingAmount <= 0 ? PenaltyStatus.Waived : PenaltyStatus.PartiallyPaid;
            penalty.ModifiedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return true;
        }
    }
}
