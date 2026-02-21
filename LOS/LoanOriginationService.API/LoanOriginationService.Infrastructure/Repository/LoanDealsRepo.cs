using AutoMapper;
using LoanOriginationService.Application.DTO.LoanDeals;
using LoanOriginationService.Application.Interfaces;
using LoanOriginationService.Domain.Enum;
using LoanOriginationService.Domain.Models;
using LoanOriginationService.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Infrastructure.Repository
{
    public class LoanDealsRepo : IloanDealsRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public LoanDealsRepo(ApplicationDbContext db, IMapper mapper)
        {
            this.mapper = mapper;
            this.db = db;
        }
        public void AddLoanDeals(LoanDealsResponseDto dto)
        {
            var loanid = db.LoanTypes.FirstOrDefault(c => c.loanTypeId == dto.loanTypeId);

            if (loanid == null)
            {
                throw new ArgumentException("Loan Type Not Found");
            }

            if (dto.riskRating < 1 || dto.riskRating > 10)
            {
                throw new ArgumentException("Risk Rating must be between 1 and 10");
            }


            if (dto.eligibleAmount <dto.approvedAmount)
            {
                throw new ArgumentException("Approved Amount Should be greater the Eligible Amount");
            }

            var d = mapper.Map<LoanDeals>(dto);
            db.LoanDeals.Add(d);
            db.SaveChanges();
        }

        public void ApproveLoanDeal(int id, LoanDecisionDto e)
        {
           
            var d = db.LoanDeals.FirstOrDefault(c => c.dealId == id);

            if (d == null)
            {
                throw new ArgumentException("Loan Deal does not exist");
            }
            if (d.currentStatus != LoanDealDecisionStatus.Review)
            {
                throw new ArgumentException("Loan Deal is not in Review Stage");
            }

            db.DealReviews.Add(new DealReview
            {
                dealId = id,

                officerId = e.officerId,

                decision = "Approved",

                decisionReason = e.Reason,

                reviewDate = DateTime.Now,
            });

            d.currentStatus = LoanDealDecisionStatus.Approved;
            d.modifiedAt = DateTime.Now;
            db.SaveChanges();
        }

        public void RejectLoanDeal(int id, LoanDecisionDto e)
        {
            var d = db.LoanDeals.FirstOrDefault(c => c.dealId == id);

            if (d == null)
            {
                throw new Exception("Loan Deal does not exist");
            }
            if (d.currentStatus != LoanDealDecisionStatus.Review)
            {
                throw new Exception("Loan Deal is not in Review Stage");
            }

            


            db.DealReviews.Add(new DealReview
            {
                dealId = id,

                officerId = e.officerId,

                decision = "Rejected",

                decisionReason = e.Reason,

                reviewDate = DateTime.Now,
            });

            d.currentStatus = LoanDealDecisionStatus.Rejected;
            d.modifiedAt = DateTime.Now;
            db.SaveChanges();

        }



        public async Task<List<LoanDealsDto>> GetAllLoanDeals()
        {
            var d = await db.LoanDeals
                .Include(x=>x.LoanType)
                .ToListAsync();
            var res = mapper.Map<List<LoanDealsDto>>(d);
            
            return res;
        }

        public async Task<LoanDealsDto> GetLoanDealsByCustId(int cid)
        {
            var d = await db.LoanDeals.FirstOrDefaultAsync(c => c.custId == cid && c.isActive);
            var res = mapper.Map<LoanDealsDto>(d);
            return res;
        }

        public async Task<LoanDealsDto> GetLoanDealsById(int id)
        {
            var d = await db.LoanDeals.FindAsync(id);
            var res = mapper.Map<LoanDealsDto>(d);
            return res;
        }


    }
}
