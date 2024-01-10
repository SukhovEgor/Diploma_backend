using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CalculationProbability;

namespace Application.DTOs
{
    public class CalculationResultData
    {
        /// <summary>
        /// Уникальный идентификатор расчета
        /// </summary>
        //public Guid CalculationId { get; set; }

        /// <summary>
        /// Номер реализации
        /// </summary>
        public int ImplementationId { get; set; }

        /// <summary>
        /// Значение выдержки времени УРОВ
        /// </summary>
        public double UROVValue { get; set; }

        /// <summary>
        /// Значение вероятности излишней работы УРОВ
        /// </summary>
        public double ProbabilityValue { get; set; }

        /// <summary>
        /// Массив сличайных времен срабатывания УРОВ
        /// </summary>
        public double[] UROVTimeArray { get; set; }

       
        
        public CalculationResultData(int implementationId,
            double urovValue, double probabilityValue, double[] urovTimeArray)
        {
            //CalculationId = calculationId;
            ImplementationId = implementationId;
            UROVValue = urovValue;
            ProbabilityValue = probabilityValue;
            UROVTimeArray = urovTimeArray;
            
        }

        public CalculationResultData()
        {
        }
    }
}
