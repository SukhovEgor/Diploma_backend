using Application.DTOs;
using AutoMapper;
using Domain.CalculationProbability;
using Domain.ProcessedResult;

namespace Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Calculations, CalculationDto>().ReverseMap();
            CreateMap<CalculationSettingsRequest, CalculationSettings>().ReverseMap();
            CreateMap<CalculationResult, CalculationResultDto>();
            CreateMap<ResultProcessed, ProcessedResultDto>().ReverseMap();
            CreateMap<CalculationResultDto, CalculationResultData>().ReverseMap();

        }
    }
}
