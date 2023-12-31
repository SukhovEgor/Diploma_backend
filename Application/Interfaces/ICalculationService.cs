﻿using Application.DTOs;
using Domain.CalculationProbability;

namespace Application.Interfaces
{
    public interface ICalculationService
    {
        public Task StartCalculation(CalculationSettings calculationSettings);
        public List<Calculations> GetCalculations(int userId);
        public Calculations GetCalculationInfoById(string id);
        public IEnumerable<CalculationResult> GetCalculationById(string id);
        public Task DeleteCalculationById(string id);

    }
}
