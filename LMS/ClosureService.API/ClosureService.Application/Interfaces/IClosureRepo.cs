using ClosureService.Application.DTO;

namespace ClosureService.Application.Interfaces
{
    public interface IClosureRepo
    {
        Task<ClosureResponse> InitiateClosureAsync(InitiateClosureRequest request);
        Task<ClosureResponse?> GetClosureByLoanAsync(int loanId);
        Task<ClosureResponse> GenerateNocAsync(int loanId);
        Task<ClosureResponse> GenerateCertificateAsync(int loanId);
    }
}
