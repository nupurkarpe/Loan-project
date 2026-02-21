using AutoMapper;
using ClosureService.Application.DTO;
using ClosureService.Domain.Models;

namespace ClosureService.Application.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Foreclosure, ForeclosureResponse>()
                .ForMember(d => d.ApprovalStatus, o => o.MapFrom(s => s.ApprovalStatus.ToString()));

            CreateMap<ForeclosureRequestDto, Foreclosure>();

            CreateMap<LoanClosure, ClosureResponse>()
                .ForMember(d => d.ClosureType, o => o.MapFrom(s => s.ClosureType.ToString()))
                .ForMember(d => d.ClosureStatus, o => o.MapFrom(s => s.ClosureStatus.ToString()));
        }
    }
}
