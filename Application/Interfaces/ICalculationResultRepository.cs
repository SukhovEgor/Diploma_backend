using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICalculationResultRepository
    {
        Task<List<CalculationDto>> GetCalculations();
        Task<IEnumerable<CalculationResultDto>> GetResultInitialById(string? id);
        Task AddCalculation(CalculationDto calculations);
        Task AddCalculationResults(IEnumerable<CalculationResultDto> calculationResults);
        Task UpdateCalculation(CalculationDto calculations);
        Task DeleteCalculationsById(string? id);
    }
}
