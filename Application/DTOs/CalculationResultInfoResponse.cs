using Domain.ProcessedResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CalculationResultInfoResponse
    {
        public List<HistogramData> MainTimeHistogramData { get; set; } = new();
        public List<CalculationResultData> CalculationResults { get; set; } = new();
    }
}
