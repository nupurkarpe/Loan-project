using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaymentService.Application.DTO;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.Models;
using PaymentService.Infrastructure.Data;

namespace PaymentService.Infrastructure.Repository
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly LoanTableClient loanTableClient;
        private readonly InterestChargesClient interestChargesClient;

        public PaymentRepo(ApplicationDbContext db, IMapper mapper, LoanTableClient loanTableClient, InterestChargesClient interestChargesClient)
        {
            this.db = db;
            this.mapper = mapper;
            this.loanTableClient = loanTableClient;
            this.interestChargesClient = interestChargesClient;
        }

        public async Task<PaymentResponse> CreatePaymentAsync(CreatePaymentRequest request)
        {
            if (!Enum.TryParse<PaymentMethod>(request.PaymentMethod, ignoreCase: true, out var method))
                throw new Exception($"Invalid payment method: {request.PaymentMethod}");

            LoanDTO loan = await loanTableClient.GetLoanById(request.LoanId);
            if (loan == null || loan.LoanId == 0)
                throw new Exception($"Loan with ID {request.LoanId} not found.");

            if (loan.AccountStatus != "Active")
                throw new Exception($"Loan account is not active. Current status: {loan.AccountStatus}");

            var payment = new LoanPayment
            {
                LoanId = request.LoanId,
                ScheduleId = request.ScheduleId,
                PaymentAmount = request.PaymentAmount,
                PaymentMethod = method,
                TransactionId = request.TransactionId,
                TransactionReference = request.TransactionReference,
                BankName = request.BankName,
                AccountNumber = request.AccountNumber,
                Remarks = request.Remarks,
                PaymentStatus = PaymentStatus.Pending,
                PaymentDate = DateTime.UtcNow
            };

            db.LoanPayments.Add(payment);
            await db.SaveChangesAsync();
            return mapper.Map<PaymentResponse>(payment);
        }

        public async Task<List<PaymentResponse>> GetPaymentsByLoanAsync(int loanId)
        {
            var payments = await db.LoanPayments
                .Where(p => p.LoanId == loanId && p.IsActive)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            return mapper.Map<List<PaymentResponse>>(payments);
        }

        public async Task<PaymentResponse?> GetPaymentByIdAsync(int paymentId)
        {
            var payment = await db.LoanPayments.FindAsync(paymentId);
            return payment == null ? null : mapper.Map<PaymentResponse>(payment);
        }

        public async Task<PaymentResponse> ProcessPaymentAsync(int paymentId)
        {
            var payment = await db.LoanPayments.FindAsync(paymentId)
                ?? throw new Exception("Payment not found.");

            if (payment.PaymentStatus == PaymentStatus.Processed)
                throw new Exception("Payment already processed.");

            LoanDTO loan = await loanTableClient.GetLoanById(payment.LoanId.Value);
            if (loan == null || loan.LoanId == 0)
                throw new Exception("Loan not found in LoanTable service.");

            List<PenaltyDTO> penalties = await interestChargesClient.GetPenaltiesByLoan(payment.LoanId.Value);
            decimal totalPenalty = penalties
                .Where(p => p.PenaltyStatus == "Pending")
                .Sum(p => p.OutstandingAmount);

            decimal remaining = payment.PaymentAmount;

            decimal penaltyPaid = Math.Min(remaining, totalPenalty);
            remaining -= penaltyPaid;

            decimal interestPaid = Math.Min(remaining, loan.OutstandingInterest);
            remaining -= interestPaid;

            decimal principalPaid = Math.Min(remaining, loan.OutstandingPrincipal);
            remaining -= principalPaid;

            payment.PenaltyPaid = penaltyPaid;
            payment.InterestPaid = interestPaid;
            payment.PrincipalPaid = principalPaid;
            payment.AdvancePayment = remaining;
            payment.PaymentStatus = PaymentStatus.Processed;
            payment.ReceiptNumber = $"RCP-{DateTime.UtcNow:yyyyMMddHHmmss}-{payment.PaymentId}";
            payment.ModifiedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return mapper.Map<PaymentResponse>(payment);
        }
    }
}
