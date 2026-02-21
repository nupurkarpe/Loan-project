using AutoMapper;
using GeographicService.Application.DTO;
using GeographicService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeographicService.Application.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CountryAddDTO, Country>()
            .ForMember(dest => dest.code, opt => opt.MapFrom(src => src.iso2));                
            CreateMap<CountryResponseDTO, Country>().ReverseMap();

            CreateMap<StateAddDTO, State>()
                .ForMember(dest => dest.code, opt => opt.MapFrom(src => src.iso2)); ;
            CreateMap<State, StateResponseDTO>()
                .ForMember(dest => dest.countryname, opt => opt.MapFrom(src => src.country.name));

            CreateMap<CityAddDTO, City>()
               .ForMember(dest => dest.code, opt => opt.MapFrom(src => src.id)); ;
            CreateMap<City, CityResponseDTO>()
                .ForMember(dest => dest.stateName, opt => opt.MapFrom(src => src.state.name));

            CreateMap<AreaAddDTO, Area>();
            CreateMap<AreaUpdateDTO, Area>()
                .ForMember(dest => dest.cityId, opt => opt.Ignore())
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Area, AreaResponseDTO>()
                .ForMember(dest => dest.cityName, opt => opt.MapFrom(src => src.city.name));

            CreateMap<BranchAddDTO, Branch>();
            CreateMap<BranchUpdateDTO, Branch>()
                .ForMember(dest => dest.cityId, opt => opt.Ignore())
                .ForMember(dest => dest.areaId, opt => opt.Ignore())
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Branch, BranchResponseDTO>()
     .ForMember(dest => dest.cityName,
         opt => opt.MapFrom(src => src.city!.name))
     .ForMember(dest => dest.areaName,
         opt => opt.MapFrom(src => src.area!.name));
        }
    }
}
