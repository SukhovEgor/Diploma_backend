using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {
        private readonly ICalculationService _calculationService;
        private readonly IMapper _mapper;

        public CalculationsController(ICalculationService calculationService,IMapper mapper)
        {
            _calculationService = calculationService;
            _mapper = mapper;
        }

    }
}
