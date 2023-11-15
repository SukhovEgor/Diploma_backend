namespace Domain.CalculationProbability
{
    /// <summary>
    /// Параметры расчета, полученные от пользователя
    /// </summary>
    public class CalculationSettings
    {
        public string Name { get; set; }

        public double MainRelayTime { get; set; }
        public double IntermediateRelayTime { get; set; }
        public double CircuitBreakerTime { get; set; }
        public double AdditionalTime { get; set; }
        public double AdditionalUROVTime { get; set; }
        public double InputTime { get; set; }

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
    }
}
