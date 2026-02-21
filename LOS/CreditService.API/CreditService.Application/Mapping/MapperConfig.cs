using AutoMapper;
using CreditService.Application.DTO.Cibils;
using CreditService.Domain.Models.CreditandLOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditService.Application.Mapping
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<CibilResponseDto, CibilReport>().ReverseMap();
        }
    }
}
