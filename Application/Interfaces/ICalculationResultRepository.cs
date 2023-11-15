using Domain.CalculationProbability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
