using Application.DTOs;
using Application.DTOs.Requests;
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
            CreateMap<CalculationResult, CalculationResultDto>();
            CreateMap<CalculationResultDto, CalculationResultData>().ReverseMap();
            CreateMap<User, UserDto>().ForMember(m => m.Name,
                    opt => opt.MapFrom(src => $"{src.SurName} {src.Name} {src.LastName}"));
            CreateMap<User, CreateUserRequest>().ReverseMap();
        }
    }
}
