﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.CalculationProbability
{
    public class Calculations
    {
        /// <summary>
        /// Уникальный идентификатор расчета
        /// </summary>
        public Guid CalculationId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Название расчета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Время начала расчета
        /// </summary>
        public string CalculationStart { get; set; } = DateTime.Now.ToString("g");

        /// <summary>
        /// Времена
        /// </summary>
        public double MainRelayTime { get; set; }
        public double IntermediateRelayTime { get; set; }
        public double CircuitBreakerTime { get; set; }
        public double AdditionalTime { get; set; }
        public double AdditionalUROVTime { get; set; }
        public double InputTime { get; set; }

        /// <summary>
        /// Стандартное отклонение
        /// </summary>
        public double StdDevMainRelayTime { get; set; }
        public double StdDevIntermediateRelayTime { get; set; }
        public double StdDevCircuitBreakerTime { get; set; }
        public double StdDevAdditionalTime { get; set; }
        public double StdDevAdditionalUROVTime { get; set; }
        public double StdDevInputTime { get; set; }

        public int ImplementationQuantity { get; set; }
        public double InitialValueUROV { get; set; }
        public double FinalValueUROV { get; set; }
        public double StepValue { get; set; }

        public double[] RelayTimeArray { get; set; }
    }
}
