using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class CalculationProbabilityRepository : ICalculationProbabilityRepository
    {
        private readonly DataContext _context;

        public CalculationProbabilityRepository(DataContext context)
        {
            _context = context;
        }

        public bool CalculationExists(int CalculationId)
        {
            return _context.CalculationProbabilities.Any(x => x.CalculationId == CalculationId);
        }

        public bool CreateCalculation()
        {
            throw new NotImplementedException();
        }

        public ICollection<CalculationProbability> GetCalculationProbabilities()
        {
            return _context.CalculationProbabilities.OrderBy(cp => cp.CalculationId).ToList();
        }

        public CalculationProbability GetCalculationProbability(int CalculationId)
        {
            return _context.CalculationProbabilities
                .Where(c => c.CalculationId == CalculationId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
