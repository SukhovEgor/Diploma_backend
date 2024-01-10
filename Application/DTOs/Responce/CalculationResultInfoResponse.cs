using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responce
{
    public class CalculationResultInfoResponse
    {
        public List<CalculationResultDto> CalculationResults { get; set; } = new();
    }
}
