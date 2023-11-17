using Domain;
using AutoMapper;
using Domain.CalculationProbability;

namespace Infrastructure.DAL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CalculationEntity, Calculations>().ReverseMap();
            CreateMap<CalculationResultEntity, CalculationResult>().ReverseMap();
        }
    }
}
