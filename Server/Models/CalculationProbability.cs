namespace Server.Models
{
    public class CalculationProbability
    {
        public uint CalculationId { get; set; }
        public string CalcuilationName { get; set; }
        public DateTime CalculationDate { get; set; }
        public double MainRelayTime { get; set; }
        public double IntermediateRelayTime { get; set; }
        public double CircuitBreakerTime { get; set; }
        public double Deviation { get; set; }
        public int ImplementationQuantity { get; set; }
        public double InitialValueUROV { get; set; }
        public double StepValue { get; set; }
        public int StepQuantity { get; set; }
        public ICollection<ImplementationProbability> Implementations { get; set; }
    }
}
