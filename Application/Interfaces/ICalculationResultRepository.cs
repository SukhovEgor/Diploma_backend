using Domain.CalculationProbability;

namespace Application.Interfaces
{
    public interface ICalculationResultRepository
    {
        Task<List<Calculations>> GetCalculations();
        Task<IEnumerable<CalculationResult>> GetResultInitialById(string? id);
        Task AddCalculation(Calculations calculations, int? userId = null);
        Task AddCalculationResults(IEnumerable<CalculationResult> calculationResults);
        Task UpdateCalculation(Calculations calculations);
        Task DeleteCalculationsById(string? id);
    }
}
