using InterestAndChargesServices.Application.DTO;

namespace InterestAndChargesServices.Application.Interfaces
{
    public interface IPenaltyRepo
    {
        Task<List<PenaltyResponse>> GetPenaltiesByLoanAsync(int loanId);
        Task<PenaltyResponse> CalculatePenaltyAsync(CalculatePenaltyRequest request);
        Task<bool> WaivePenaltyAsync(WaivePenaltyRequest request);
    }
}
