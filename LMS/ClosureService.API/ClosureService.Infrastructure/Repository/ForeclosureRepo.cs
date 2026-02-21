using AutoMapper;
using ClosureService.Application.DTO;
using ClosureService.Application.Interfaces;
using ClosureService.Domain.Models;
using ClosureService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClosureService.Infrastructure.Repository
{
    public class ForeclosureRepo : IForeclosureRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly LoanTableClient loanTableClient;
        private readonly InterestChargesClient interestChargesClient;

        public ForeclosureRepo(ApplicationDbContext db, IMapper mapper, LoanTableClient loanTableClient, InterestChargesClient interestChargesClient)
        {
            this.db = db;
            this.mapper = mapper;
            this.loanTableClient = loanTableClient;
            this.interestChargesClient = interestChargesClient;
        }

        public async Task<ForeclosureCalculationResponse> RequestForeclosureAsync(ForeclosureRequestDto request)
        {
            var loan = await loanTableClient.GetLoanById(request.LoanId)
                ?? throw new Exception("Loan not found.");

            var calc = await CalculateDetails(loan);

            var foreclosure = mapper.Map<Foreclosure>(request);
            foreclosure.ForeclosureType = request.ForeclosureType ?? "full";
            foreclosure.PrincipalOutstanding = loan.OutstandingPrincipal;
            foreclosure.InterestOutstanding = loan.OutstandingInterest;
            foreclosure.PenaltyOutstanding = calc.PenaltyOutstanding;
            foreclosure.ForeclosureCharges = calc.ForeclosureCharges;
            foreclosure.RebateAmount = calc.RebateAmount;
            foreclosure.TotalPayable = calc.TotalPayable;
            foreclosure.RemainingTenure = loan.RemainingTenure;
            foreclosure.SavingsAmount = calc.SavingsAmount;
            foreclosure.ApprovalStatus = ForeclosureStatus.Requested;

            db.Foreclosures.Add(foreclosure);
            await db.SaveChangesAsync();
            return calc;
        }

        public async Task<ForeclosureCalculationResponse> CalculateForeclosureAsync(int loanId)
        {
            var loan = await loanTableClient.GetLoanById(loanId)
                ?? throw new Exception("Loan not found.");
            return await CalculateDetails(loan);
        }

        public async Task<bool> ApproveForeclosureAsync(int id, ForeclosureApprovalRequest request)
        {
            var foreclosure = await db.Foreclosures.FindAsync(id);
            if (foreclosure == null || foreclosure.ApprovalStatus != ForeclosureStatus.Requested) return false;

            foreclosure.ApprovalStatus = ForeclosureStatus.Approved;
            foreclosure.ApprovedBy = request.ApprovedBy;
            foreclosure.ApprovalDate = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectForeclosureAsync(int id)
        {
            var foreclosure = await db.Foreclosures.FindAsync(id);
            if (foreclosure == null || foreclosure.ApprovalStatus != ForeclosureStatus.Requested) return false;

            foreclosure.ApprovalStatus = ForeclosureStatus.Rejected;
            foreclosure.ModifiedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteForeclosureAsync(int id)
        {
            var foreclosure = await db.Foreclosures.FindAsync(id);
            if (foreclosure == null || foreclosure.ApprovalStatus != ForeclosureStatus.Approved) return false;

            foreclosure.ApprovalStatus = ForeclosureStatus.Completed;
            foreclosure.ForeclosureDate = DateTime.UtcNow;
            foreclosure.PaymentDate = DateTime.UtcNow;
            foreclosure.AmountPaid = foreclosure.TotalPayable;
            foreclosure.CertificateGenerated = true;
            foreclosure.CertificatePath = $"/foreclosures/{id}/certificate.pdf";
            foreclosure.ModifiedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            await loanTableClient.UpdateLoanStatus(foreclosure.LoanId, "Foreclosed");
            return true;
        }

        private async Task<ForeclosureCalculationResponse> CalculateDetails(LoanDTO loan)
        {
            var penalties = await interestChargesClient.GetPenaltiesByLoan(loan.LoanId);
            var totalPenalties = penalties
                .Where(p => p.PenaltyStatus == "Pending")
                .Sum(p => p.OutstandingAmount);

            var emiSchedule = await loanTableClient.GetEmiSchedule(loan.LoanId);
            var remainingEmiTotal = emiSchedule
                .Where(e => e.PaymentStatus == "Pending")
                .Sum(e => e.EmiAmount);

            decimal outstanding = loan.OutstandingPrincipal + loan.OutstandingInterest + totalPenalties;
            decimal charges = Math.Round(outstanding * 0.02m, 2);
            decimal rebate = Math.Round(loan.OutstandingInterest * 0.1m, 2);
            decimal totalPayable = outstanding + charges - rebate;
            decimal savings = remainingEmiTotal - totalPayable;
            if (savings < 0) savings = 0;

            return new ForeclosureCalculationResponse
            {
                LoanId = loan.LoanId,
                PrincipalOutstanding = loan.OutstandingPrincipal,
                InterestOutstanding = loan.OutstandingInterest,
                PenaltyOutstanding = totalPenalties,
                ForeclosureCharges = charges,
                RebateAmount = rebate,
                TotalPayable = totalPayable,
                RemainingTenure = loan.RemainingTenure,
                SavingsAmount = savings
            };
        }
    }
}
