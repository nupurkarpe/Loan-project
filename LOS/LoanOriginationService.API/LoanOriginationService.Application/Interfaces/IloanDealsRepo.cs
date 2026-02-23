using LoanOriginationService.Application.DTO.LoanDeals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.Interfaces
{
    public interface IloanDealsRepo
    {
        Task AddLoanDeals(CreateLoanDealRequestDto dto);

        Task<List<LoanDealsDto>> GetAllLoanDeals();

        Task<LoanDealsDto> GetLoanDealsById(int id);

        Task<LoanDealsDto> GetLoanDealsByCustId(int id);

        void RejectLoanDeal(int id, LoanDecisionDto e);

        void ApproveLoanDeal(int id, LoanDecisionDto e);


    }
}
