using AutoMapper;
using ClosureService.Application.DTO;
using ClosureService.Application.Interfaces;
using ClosureService.Domain.Models;
using ClosureService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClosureService.Infrastructure.Repository
{
    public class ClosureRepo : IClosureRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly LoanTableClient loanTableClient;
        private readonly PaymentClient paymentClient;

        public ClosureRepo(ApplicationDbContext db, IMapper mapper, LoanTableClient loanTableClient, PaymentClient paymentClient)
        {
            this.db = db;
            this.mapper = mapper;
            this.loanTableClient = loanTableClient;
            this.paymentClient = paymentClient;
        }

        public async Task<ClosureResponse> InitiateClosureAsync(InitiateClosureRequest request)
        {
            if (!Enum.TryParse<ClosureType>(request.ClosureType, ignoreCase: true, out var closureType))
                throw new Exception($"Invalid closure type: {request.ClosureType}");

            var loan = await loanTableClient.GetLoanById(request.LoanId)
                ?? throw new Exception("Loan not found.");

            var payments = await paymentClient.GetPaymentsByLoan(request.LoanId);
            var processedPayments = payments.Where(p => p.PaymentStatus == "Processed").ToList();

            var totalPrincipalPaid = processedPayments.Sum(p => p.PrincipalPaid);
            var totalInterestPaid = processedPayments.Sum(p => p.InterestPaid);
            var totalPenaltyPaid = processedPayments.Sum(p => p.PenaltyPaid);

            var closure = new LoanClosure
            {
                LoanId = request.LoanId,
                ClosureType = closureType,
                ClosureDate = DateTime.UtcNow,
                ClosureReason = request.ClosureReason,
                OriginalMaturityDate = loan.MaturityDate,
                ActualMaturityDate = DateTime.UtcNow,
                TotalDisbursed = loan.DisbursedAmount,
                TotalPrincipalPaid = totalPrincipalPaid,
                TotalInterestPaid = totalInterestPaid,
                TotalPenaltyPaid = totalPenaltyPaid,
                TotalAmountPaid = totalPrincipalPaid + totalInterestPaid + totalPenaltyPaid,
                OutstandingAtClosure = loan.TotalOutstanding,
                ClosureStatus = ClosureStatus.Pending
            };

            db.LoanClosures.Add(closure);
            await db.SaveChangesAsync();

            var newStatus = closureType switch
            {
                ClosureType.Foreclosure => "Foreclosed",
                ClosureType.WrittenOff => "WrittenOff",
                _ => "Closed"
            };
            await loanTableClient.UpdateLoanStatus(request.LoanId, newStatus);

            return mapper.Map<ClosureResponse>(closure);
        }

        public async Task<ClosureResponse?> GetClosureByLoanAsync(int loanId)
        {
            var closure = await db.LoanClosures
                .Where(c => c.LoanId == loanId)
                .OrderByDescending(c => c.ClosureDate)
                .FirstOrDefaultAsync();
            return closure == null ? null : mapper.Map<ClosureResponse>(closure);
        }

        public async Task<ClosureResponse> GenerateNocAsync(int loanId)
        {
            var closure = await db.LoanClosures
                .Where(c => c.LoanId == loanId)
                .OrderByDescending(c => c.ClosureDate)
                .FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException("No closure found for this loan.");

            closure.NocIssued = "Yes";
            closure.NocIssueDate = DateTime.UtcNow;
            closure.NocNumber = $"NOC-{DateTime.UtcNow:yyyyMMdd}-{loanId}";
            closure.NocFilePath = $"/closures/{closure.ClosureId}/noc.pdf";
            closure.ModifiedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return mapper.Map<ClosureResponse>(closure);
        }

        public async Task<ClosureResponse> GenerateCertificateAsync(int loanId)
        {
            var closure = await db.LoanClosures
                .Where(c => c.LoanId == loanId)
                .OrderByDescending(c => c.ClosureDate)
                .FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException("No closure found for this loan.");

            closure.ClosureCertificatePath = $"/closures/{closure.ClosureId}/closure-certificate.pdf";
            closure.ClosureStatus = ClosureStatus.Completed;
            closure.ModifiedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return mapper.Map<ClosureResponse>(closure);
        }
    }
}
