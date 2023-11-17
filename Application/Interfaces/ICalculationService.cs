using Application.DTOs;
using Domain.CalculationProbability;

namespace Application.Interfaces
{
    public interface ICalculationService
    {
        public Task StartCalculation(CalculationSettings calculationSettings);
        public List<Calculations> GetCalculations();
        public IEnumerable<CalculationResult> GetCalculationById(string id);
        public Task DeleteCalculationById(string id);

    }
}
