using AutoMapper;
using StatusDisplayApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusDisplayApi.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EngWordModel, EngTranslatedWordModel>()
                .ForMember(dest => dest.definitions, opt => opt.MapFrom(src => src.definitions))
                .ForMember(dest => dest.examples, opt => opt.MapFrom(src => src.examples));
            CreateMap<FactDto, Facts>();
            CreateMap<Night, Part>()
                .ForMember(dest => dest.PartOfDay, opt => opt.MapFrom(src => "Ночью"));
            CreateMap<Morning, Part>()
                .ForMember(dest => dest.PartOfDay, opt => opt.MapFrom(src => "Утром"));
            CreateMap<Day, Part>()
                .ForMember(dest => dest.PartOfDay, opt => opt.MapFrom(src => "Днем"));
            CreateMap<Evening, Part>()
               .ForMember(dest => dest.PartOfDay, opt => opt.MapFrom(src => "Вечером"));
        }
    }
}
