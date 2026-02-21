using AutoMapper;
using InterestAndChargesServices.Application.DTO;
using InterestAndChargesServices.Domain.Models;

namespace InterestAndChargesServices.Application.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<InterestAccrual, InterestAccrualResponse>()
                .ForMember(d => d.AccrualType, o => o.MapFrom(s => s.AccrualType.ToString()))
                .ForMember(d => d.CalculationMethod, o => o.MapFrom(s => s.CalculationMethod.ToString()))
                .ForMember(d => d.AccrualStatus, o => o.MapFrom(s => s.AccrualStatus.ToString()));

            CreateMap<PenaltyCharge, PenaltyResponse>()
                .ForMember(d => d.ChargeType, o => o.MapFrom(s => s.ChargeType.ToString()))
                .ForMember(d => d.PenaltyStatus, o => o.MapFrom(s => s.PenaltyStatus.ToString()));
        }
    }
}
