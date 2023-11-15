using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class ImplementationProbabilityRepository : IImplementationProbabilityRepository
    {
        private readonly DataContext _context;

        public ImplementationProbabilityRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateImplementation()
        {
            throw new NotImplementedException();
        }

        public ImplementationProbability GetImplementationProbability(int ImplementationId)
        {
            return _context.ImplementationProbabilities
                .Where(c => c.ImplementationId == ImplementationId).FirstOrDefault();
        }

        public ICollection<ImplementationProbability> GetImplementationProbabilities()
        {
            return _context.ImplementationProbabilities.OrderBy(ip => ip.ImplementationId).ToList();
        }

        public bool ImplementationExists(int ImplementationId)
        {
            return _context.ImplementationProbabilities.Any(i => i.ImplementationId == ImplementationId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
