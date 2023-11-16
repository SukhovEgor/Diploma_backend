using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
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
                cfg.CreateMap<CalculationEntity, CalculationDto>().ReverseMap();
                cfg.CreateMap<CalculationResultEntity, CalculationResultDto>().ReverseMap();
            });
            _context = context;
            _mapper = new Mapper(config);
        }

        public async Task AddCalculation(CalculationDto calculations)
        {
            var calculation = _mapper.Map<CalculationEntity>(calculations);
            _context.Calculations.Add(_mapper.Map<CalculationEntity>(calculations));
            await _context.SaveChangesAsync();
        }

        public async Task AddCalculationResults(IEnumerable<CalculationResultDto> calculationResults)
        {
            foreach (var calculationResult in calculationResults)
            {
                await _context.CalculationResults.AddAsync(_mapper.Map<CalculationResultDto, CalculationResultEntity>(calculationResult));
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

        public async Task<List<CalculationDto>> GetCalculations()
        {
            throw new NotImplementedException();
            
            var calculationsEntity = _context.Calculations.OrderByDescending(c => c.CalculationStart).ToList();
            List<CalculationDto> calculations = _mapper.Map<List<CalculationDto>>(calculationsEntity);
            return calculations;
        }

        public async Task<IEnumerable<CalculationResultDto>> GetResultInitialById(string? id)
        {
            List<CalculationResultDto> calculationResults = new();
            var calculationResult = (from calculations in _context.CalculationResults
                                    where calculations.CalculationId.ToString() == id
                                    select calculations).ToList();
            calculationResults.AddRange(_mapper.Map<List<CalculationResultDto>>(calculationResult));
            return (IEnumerable<CalculationResultDto>)calculationResults;
        }

        public async Task UpdateCalculation(CalculationDto calculations)
        {
            _context.ChangeTracker.Clear();
            CalculationEntity calculation1 = _context.Calculations.AsNoTracking().FirstOrDefault(u => u.Id == calculations.CalculationId);
            string endTime = DateTime.Now.ToString("g");
            _context.Calculations.Update(calculation1);
            await _context.SaveChangesAsync();
        }
    }
}
