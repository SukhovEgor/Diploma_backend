namespace Infrastructure.DAL.Entities
{
    public class ImplementationProbability
    {
        public uint ImplementationId { get; set; }
        public int ValueUROV { get; set; }
        public double Probability { get; set; }
        public CalculationProbability CalculationProbability { get; set; }
    }
}
