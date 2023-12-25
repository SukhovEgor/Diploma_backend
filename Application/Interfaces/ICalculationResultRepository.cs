using Domain.CalculationProbability;

namespace Application.Interfaces
{
    public interface ICalculationResultRepository
    {
        Task<List<Calculations>> GetCalculations(int userId);
        Task<List<Calculations>> GetAllCalculations();
        Task<IEnumerable<CalculationResult>> GetResultInitialById(string? id);
        Task AddCalculation(Calculations calculations);
        Task AddCalculationResults(IEnumerable<CalculationResult> calculationResults);
        Task UpdateCalculation(Calculations calculations);
        Task DeleteCalculationsById(string? id);
    }
}
