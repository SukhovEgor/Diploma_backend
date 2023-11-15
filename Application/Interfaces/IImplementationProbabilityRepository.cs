using Server.Models;

namespace Server.Interfaces
{
    public interface IImplementationProbabilityRepository
    {
        ICollection<ImplementationProbability> GetImplementationProbabilities();
        ImplementationProbability GetImplementationProbability(int ImplementationId);
        bool ImplementationExists(int ImplementationId);
        bool CreateImplementation();
        bool Save();
    }
}
