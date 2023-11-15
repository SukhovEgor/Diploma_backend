using Application.DTOs;
using Domain.CalculationProbability;

namespace Application.Interfaces
{
    public interface ICalculationService
    {
        public Task StartCalculation(CalculationSettingsRequest calculationSettings);
        public List<CalculationDto> GetCalculations();
        public IEnumerable<CalculationResultDto> GetCalculationById(string id);
        public Task DeleteCalculationById(string id);

    }
}
