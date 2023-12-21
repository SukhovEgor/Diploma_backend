using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responce
{
    public class GetCalculationsResponse
    {
        public int CalculationAmount { get; set; }
        public List<CalculationDto> Calculations { get; set; } = new();
    }
}
