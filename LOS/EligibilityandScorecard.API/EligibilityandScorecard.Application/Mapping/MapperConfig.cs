using AutoMapper;
using EligibilityandScorecard.Application.DTO.Eligibility;
using EligibilityandScorecard.Domain.Models.CreditAndLOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EligibilityandScorecard.Application.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<EvaluateEligibilityRequest, Eligibility>();
            CreateMap<Eligibility, EligibilityResponseDto>();
        }
    }
}
