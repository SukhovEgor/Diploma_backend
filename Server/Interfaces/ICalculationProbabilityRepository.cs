using Server.Models;

namespace Server.Interfaces
{
    public interface ICalculationProbabilityRepository
    {
        ICollection<CalculationProbability> GetCalculationProbabilities();
        CalculationProbability GetCalculationProbability(int CalculationId);
        bool CalculationExists(int CalculationId);
        bool CreateCalculation();
        bool Save();
    }
}
