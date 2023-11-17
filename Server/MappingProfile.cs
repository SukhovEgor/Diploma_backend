using Application.DTOs;
using AutoMapper;
using Domain.CalculationProbability;

namespace Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Calculations, CalculationDto>().ReverseMap();
            CreateMap<CalculationSettingsRequest, CalculationSettings>().ReverseMap();
        }
    }
}
