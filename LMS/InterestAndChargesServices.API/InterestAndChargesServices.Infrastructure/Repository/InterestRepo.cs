using AutoMapper;
using InterestAndChargesServices.Application.DTO;
using InterestAndChargesServices.Application.Interfaces;
using InterestAndChargesServices.Domain.Models;
using InterestAndChargesServices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InterestAndChargesServices.Infrastructure.Repository
{
    public class InterestRepo : IInterestRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly LoanTableClient authClient;

        public InterestRepo(ApplicationDbContext db, IMapper mapper, LoanTableClient authClient)
        {
            this.db = db;
            this.mapper = mapper;
            this.authClient = authClient;
        }

        public async Task<List<InterestAccrualResponse>> GetAccrualsAsync(int loanId)
        {
            var accruals = await db.InterestAccruals
                .Where(a => a.LoanId == loanId)
                .OrderByDescending(a => a.AccrualDate)
                .ToListAsync();
            return mapper.Map<List<InterestAccrualResponse>>(accruals);
        }

        public async Task<int> RunDailyAccrualAsync()
        {
            var activeLoans = await authClient.GetActiveLoans();
            int count = 0;

            foreach (var loan in activeLoans)
            {
                var dailyRate = loan.InterestRate / 365m / 100m;
                var accrued = Math.Round(loan.OutstandingPrincipal * dailyRate, 2);

                var lastAccrual = await db.InterestAccruals
                    .Where(a => a.LoanId == loan.LoanId)
                    .OrderByDescending(a => a.AccrualDate)
                    .FirstOrDefaultAsync();

                db.InterestAccruals.Add(new InterestAccrual
                {
                    LoanId = loan.LoanId,
                    AccrualDate = DateTime.UtcNow.Date,
                    PrincipalBalance = loan.OutstandingPrincipal,
                    InterestRate = loan.InterestRate,
                    DailyInterestRate = dailyRate,
                    AccruedInterest = accrued,
                    CumulativeInterest = (lastAccrual?.CumulativeInterest ?? 0) + accrued,
                    AccrualType = AccrualType.Regular,
                    CalculationMethod = CalculationMethod.Reducing,
                    DaysInMonth = DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month),
                    AccrualStatus = AccrualStatus.Posted
                });
                count++;
            }

            await db.SaveChangesAsync();
            return count;
        }
    }
}
