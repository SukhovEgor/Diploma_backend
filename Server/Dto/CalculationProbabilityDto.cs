namespace Server.Dto
{
    public class CalculationProbabilityDto
    {
        public uint CalculationId { get; set; }
        public string CalcuilationName { get; set; }
        public DateTime CalculationDate { get; set; }
        public int MainRelayTime { get; set; }
        public int IntermediateRelayTime { get; set; }
        public int CircuitBreakerTime { get; set; }
        public int Deviation { get; set; }
        public int InitialValueUROV { get; set; }
        public int StepValue { get; set; }
    }
}
