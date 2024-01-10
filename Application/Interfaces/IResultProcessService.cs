using Application.DTOs;
using Domain.CalculationProbability;

namespace Application.Interfaces
{
    public interface IResultProcessService
    {
       List<CalculationResultData> Processing(List<CalculationResultDto> calculationResults);

    }
}
