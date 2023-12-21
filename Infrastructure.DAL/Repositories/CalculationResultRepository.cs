using Application.Interfaces;
using AutoMapper;
using Domain.CalculationProbability;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.DAL.Repositories
{
    public class CalculationResultRepository : ICalculationResultRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CalculationResultRepository(DataContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CalculationEntity, Calculations>().ReverseMap();
                cfg.CreateMap<CalculationResultEntity, CalculationResult>().ReverseMap();
            });
            _context = context;
            _mapper = new Mapper(config);
        }

        public async Task AddCalculation(Calculations calculations, int? userId = null)
        {
            _context.Calculations.Add(_mapper.Map<CalculationEntity>(calculations));
            await _context.SaveChangesAsync();
        }

        public async Task AddCalculationResults(IEnumerable<CalculationResult> calculationResults)
        {
            foreach (var calculationResult in calculationResults)
            {
                await _context.CalculationResults.AddAsync(_mapper.Map<CalculationResult, CalculationResultEntity>(calculationResult));
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCalculationsById(string? id)
        {
            CalculationEntity calculations1 = (from calculations in _context.Calculations
                                               where calculations.Id.ToString() == id
                                               select calculations).FirstOrDefault();
            List<CalculationResultEntity> calculationResults = (from calculations in _context.CalculationResults
                                                                where calculations.CalculationId.ToString() == id
                                                              select calculations).ToList();
            _context.Calculations.Remove(calculations1);
            _context.CalculationResults.RemoveRange(calculationResults);
            _context.SaveChanges();
        }

        public async Task<List<Calculations>> GetCalculations()
        {
            var calculationsEntity = _context.Calculations.OrderByDescending(c => c.CalculationStart).ToList();
            List<Calculations> calculations = _mapper.Map<List<Calculations>>(calculationsEntity);
            return calculations;
        }

        public async Task<IEnumerable<CalculationResult>> GetResultInitialById(string? id)
        {
            List<CalculationResult> calculationResults = new();
            var calculationResult = (from calculations in _context.CalculationResults
                                    where calculations.CalculationId.ToString() == id
                                    select calculations).ToList();
            calculationResults.AddRange(_mapper.Map<List<CalculationResult>>(calculationResult));
            return (IEnumerable<CalculationResult>)calculationResults;
        }

        public async Task UpdateCalculation(Calculations calculations)
        {
            _context.ChangeTracker.Clear();
            CalculationEntity calculation1 = _context.Calculations.AsNoTracking().FirstOrDefault(u => u.Id == calculations.Id);
            string endTime = DateTime.Now.ToString("g");
            _context.Calculations.Update(calculation1);
            await _context.SaveChangesAsync();
        }
    }
}
