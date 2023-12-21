using Application.DTOs;
using Application.Interfaces;
using Domain.CalculationProbability;
using Domain.ProcessedResult;

namespace Infrastructure.Services
{
    public class ResultProcessService : IResultProcessService
    {
        //public List<HistogramData> Processing(List<double> processedResults)
        //{
        //    ResultProcessed histogramData = new();
        //    histogramData.GetStatistic(processedResults);
        //    return histogramData.HistogramData;
        //}
        public List<HistogramData> Processing(Calculations calculation)
        {
            ResultProcessed histogramData = new();
            histogramData.GetStatistic(calculation.RelayTimeArray.ToList());
            return histogramData.HistogramData;
        }
        public List<CalculationResultData> Processing(List<CalculationResultDto> calculationResults)
        {
            List<CalculationResultData> resultsProcessed = new();
            foreach( var results in calculationResults)
            {
                ResultProcessed histogramData = new();
                histogramData.GetStatistic(results.UROVTimeArray.ToList());
                resultsProcessed.Add(new CalculationResultData()
                {
                    ImplementationId = results.ImplementationId,
                    UROVValue = results.UROVValue,
                    ProbabilityValue = results.ProbabilityValue,
                    UROVTimeArray = results.UROVTimeArray,
                    HistogramData = histogramData.HistogramData
                });
            }
            List<CalculationResultData> sortedList = resultsProcessed.OrderBy(o => o.ImplementationId).ToList();
            return sortedList;
        }
    }
}
