using Application.DTOs;
using AutoMapper;

namespace Infrastructure.DAL.Mappings
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<CalculationEntity, CalculationDto>().ReverseMap();
            CreateMap<CalculationResultEntity, CalculationResultDto>().ReverseMap();
        }
    }
}
