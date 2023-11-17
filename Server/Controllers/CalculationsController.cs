using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.CalculationProbability;
using Microsoft.AspNetCore.Mvc;


namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationsController : ControllerBase
    {
        private readonly ICalculationService _calculationService;
        private readonly IMapper _mapper;

        public CalculationsController(ICalculationService calculationService,IMapper mapper)
        {
            _calculationService = calculationService;
            _mapper = mapper;
        }
        /// <summary>
        /// Начать расчет
        /// </summary>
        /// <param name="calculationSettings"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostCalculations")]
        public async Task<IActionResult> PostCalculations([FromBody] CalculationSettingsRequest calculationSettingsRequest)
        {
            var calculationSettings = _mapper.Map<CalculationSettingsRequest, CalculationSettings>(calculationSettingsRequest);
            await _calculationService.StartCalculation(calculationSettings);
            Console.WriteLine("Расчет завершен");
            return Ok("Расчет завершен.");
        }
        
        /// <summary>
        /// Получить описания всех расчетов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCalculations")]
        public async Task<IActionResult> GetCalculations()
        {
            var calculations = _calculationService.GetCalculations();
            var response = new GetCalculationsResponse
            {
                CalculationAmount = calculations.Count,
                Calculations = _mapper.Map<List<Calculations>, List<CalculationDto>>(calculations)
            };
            return Ok(response);
        }
        
        /// <summary>
        /// Получить результаты определенного расчета
        /// </summary>  
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCalculationResult/{id}")]
        public async Task<IActionResult> GetCalculationById(string? id)
        {
            var calcResultInit = _calculationService.GetCalculationById(id);
            List<CalculationResult> calculationResults = new();
            
            foreach (var calc in calcResultInit)
            {
                calculationResults.Add(calc);
            }
            var response = new CalculationResultInfoResponse()
            {
                CalculationResults = _mapper.Map<List<CalculationResult>, List<CalculationResultDto>>(calculationResults)
            };
            return Ok(response);
        }

        /// <summary>
        /// Удалить определенный расчет
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteCalculations/{id}")]
        public async Task<IActionResult> DeleteCalculationsById(string? id)
        {
            await _calculationService.DeleteCalculationById(id);
            return Ok();
        }

    }
}
