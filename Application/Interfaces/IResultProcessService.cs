using Application.DTOs;
using Domain.CalculationProbability;
using Domain.ProcessedResult;

namespace Application.Interfaces
{
    public interface IResultProcessService
    {
        List<HistogramData> Processing(Calculations calculation);
        List<CalculationResultData> Processing(List<CalculationResultDto> calculationResults);

    }
}
