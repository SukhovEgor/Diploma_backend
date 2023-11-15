using Application.Interfaces;
using Application.DTOs;

namespace Infrastructure.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly ICalculationResultRepository _calculationResultRepository;
        private readonly ICalculationModule _calculationModule;

        public CalculationService(ICalculationResultRepository calculationResultRepository, ICalculationModule calculationModule)
        {
            _calculationResultRepository = calculationResultRepository;
            _calculationModule = calculationModule;
        }
        public async Task DeleteCalculationById(string id)
        {
            await _calculationResultRepository.DeleteCalculationsById(id);
        }

        public IEnumerable<CalculationResultDto> GetCalculationById(string id)
        {
            IEnumerable<CalculationResultDto> calcResultInitial = 
                _calculationResultRepository.GetResultInitialById(id).Result;
            if (calcResultInitial.ToList().Count == 0)
            {
                throw new Exception($"Ошибка. Расчета с ID {id} не существует.");
            }
            return calcResultInitial;
        }

        public List<CalculationDto> GetCalculations()
        {
            return _calculationResultRepository.GetCalculations().Result;
        }

        public Task StartCalculation(CalculationSettingsRequest calculationSettings)
        {
            throw new NotImplementedException();
        }
    }
}
