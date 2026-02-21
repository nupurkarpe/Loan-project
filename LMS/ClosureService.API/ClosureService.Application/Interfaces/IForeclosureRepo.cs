using ClosureService.Application.DTO;

namespace ClosureService.Application.Interfaces
{
    public interface IForeclosureRepo
    {
        Task<ForeclosureCalculationResponse> RequestForeclosureAsync(ForeclosureRequestDto request);
        Task<ForeclosureCalculationResponse> CalculateForeclosureAsync(int loanId);
        Task<bool> ApproveForeclosureAsync(int id, ForeclosureApprovalRequest request);
        Task<bool> RejectForeclosureAsync(int id);
        Task<bool> CompleteForeclosureAsync(int id);
    }
}
