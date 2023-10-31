using AutoMapper;
using Server.Dto;
using Server.Models;

namespace Server.Helper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<CalculationProbability, CalculationProbabilityDto>();
            CreateMap<ImplementationProbability, ImplementationProbabilityDto>();
        }
    }
}
