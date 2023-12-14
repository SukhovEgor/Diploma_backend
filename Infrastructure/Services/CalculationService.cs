using Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using Domain.CalculationProbability;
using Application.UseCases;

namespace Infrastructure.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly ICalculationResultRepository _calculationResultRepository;
        private readonly ICalculationModule _calculationModule;
        private readonly IResultProcessService _resultProcessService;

        public CalculationService(ICalculationResultRepository calculationResultRepository, ICalculationModule calculationModule, IResultProcessService resultProcessService)
        {
            _calculationResultRepository = calculationResultRepository;
            _calculationModule = calculationModule;
            _resultProcessService = resultProcessService;
        }
        public async Task DeleteCalculationById(string id)
        {
            await _calculationResultRepository.DeleteCalculationsById(id);
        }

        public IEnumerable<CalculationResult> GetCalculationById(string id)
        {
            IEnumerable<CalculationResult> calcResultInitial = 
                _calculationResultRepository.GetResultInitialById(id).Result;

            if (calcResultInitial.ToList().Count == 0)
            {
                throw new Exception($"Ошибка. Расчета с ID {id} не существует.");
            }
            return calcResultInitial;
        }

        public List<Calculations> GetCalculations()
        {
            return _calculationResultRepository.GetCalculations().Result;
        }

        public async Task StartCalculation(CalculationSettings calculationSettings)
        {
            Calculations calculation = new()
            {
                Name = calculationSettings.Name,
                MainRelayTime = calculationSettings.MainRelayTime / 1000,
                IntermediateRelayTime = calculationSettings.IntermediateRelayTime / 1000,
                CircuitBreakerTime = calculationSettings.CircuitBreakerTime / 1000,
                AdditionalTime = calculationSettings.AdditionalTime / 1000,
                AdditionalUROVTime = calculationSettings.AdditionalUROVTime / 1000,
                InputTime = calculationSettings.InputTime / 1000,

                StdDevMainRelayTime = Math.Round(calculationSettings.StdDevMainRelayTime / 300, 4),
                StdDevIntermediateRelayTime = Math.Round(calculationSettings.StdDevIntermediateRelayTime / 300, 4),
                StdDevCircuitBreakerTime = Math.Round(calculationSettings.StdDevCircuitBreakerTime / 300, 4) ,
                StdDevAdditionalTime = Math.Round(calculationSettings.StdDevAdditionalTime / 300, 4),
                StdDevAdditionalUROVTime = Math.Round(calculationSettings.StdDevAdditionalUROVTime / 300, 4),
                StdDevInputTime = Math.Round(calculationSettings.StdDevInputTime / 300, 4),

                ImplementationQuantity = calculationSettings.ImplementationQuantity,
                InitialValueUROV = calculationSettings.InitialValueUROV / 1000,
                FinalValueUROV = calculationSettings.FinalValueUROV / 1000,
                StepValue = calculationSettings.StepValue / 1000
            };
            Console.WriteLine("Start Calculation");
            calculation.RelayTimeArray = _calculationModule.GetFullTime(calculation);
            await _calculationResultRepository.AddCalculation(calculation);

            List<CalculationResult> calcResultInitial = new();
            int count = 0;
            var step = calculation.StepValue;
            for (var timeUROV = calculation.InitialValueUROV;
                timeUROV >= calculation.FinalValueUROV; timeUROV -= step)
            {
                count++;
                var probability = _calculationModule.GetProbability(calculation, timeUROV);
                Console.WriteLine($"Вероятность излишней работы УРОВ " +
                    $"{Math.Round(100 * probability, 2)}, при выдержке времени {Math.Round(1000 * timeUROV, 2)}");
                var UROVTimeArray = _calculationModule.GetTimeUROV(calculation, timeUROV);
                var calcResult = new CalculationResult(calculation.Id, count, Math.Round(timeUROV, 3), Math.Round(probability, 6), UROVTimeArray);
                calcResultInitial.Add(calcResult);
            }
            await _calculationResultRepository.AddCalculationResults(calcResultInitial);
            
        }
    }
}
