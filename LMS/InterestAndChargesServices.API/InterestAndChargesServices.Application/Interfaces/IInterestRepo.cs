using InterestAndChargesServices.Application.DTO;

namespace InterestAndChargesServices.Application.Interfaces
{
    public interface IInterestRepo
    {
        Task<List<InterestAccrualResponse>> GetAccrualsAsync(int loanId);
        Task<int> RunDailyAccrualAsync();
    }
}
