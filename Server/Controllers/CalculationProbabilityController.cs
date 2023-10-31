using AutoMapper;
using Master_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationProbabilityController : Controller
    {
        private readonly ICalculationProbabilityRepository _calculationProbabilityRepository;
        private readonly IMapper _mapper;

        public CalculationProbabilityController
            (ICalculationProbabilityRepository calculationProbabilityRepository,
            IMapper mapper)
        {
            _calculationProbabilityRepository = calculationProbabilityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CalculationProbability>))]
        public IActionResult GetCalculationProbabilities()
        {
            var calculations = _calculationProbabilityRepository.GetCalculationProbabilities();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(calculations);
        }

        [HttpGet("{CalculationId}")]
        [ProducesResponseType(200, Type = typeof(CalculationProbability))]
        [ProducesResponseType(400)]
        public IActionResult GetCalculationProbability(int CalculationId)
        {
            if (!_calculationProbabilityRepository.CalculationExists(CalculationId))
                return NotFound();

            var calculation = _mapper.Map<CalculationProbabilityDto>
                (_calculationProbabilityRepository.GetCalculationProbability(CalculationId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(calculation);
        }
    }
}
