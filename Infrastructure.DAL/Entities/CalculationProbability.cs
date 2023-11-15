namespace Infrastructure.DAL.Entities
{
    public class CalculationProbability
    {
        public uint CalculationId { get; set; }
        public string CalcuilationName { get; set; }
        public DateTime CalculationDate { get; set; }

        public double MainRelayTime { get; set; }
        public double IntermediateRelayTime { get; set; }
        public double CircuitBreakerTime { get; set; }
        public double AdditionalTime { get; set; }
        public double AdditionalUROVTime { get; set; }
        public double InputTime { get; set; }

        public double DevMainRelayTime { get; set; }
        public double DevIntermediateRelayTime { get; set; }
        public double DevCircuitBreakerTime { get; set; }
        public double DevAdditionalTime { get; set; }
        public double DevAdditionalUROVTime { get; set; }
        public double DevInputTime { get; set; }

        public int ImplementationQuantity { get; set; }
        public double InitialValueUROV { get; set; }
        public double StepValue { get; set; }
        public ICollection<ImplementationProbability> Implementations { get; set; }
    }
}
