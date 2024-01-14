using Application.UseCases;
using Domain.CalculationProbability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class CalculationModuleTests
    {
        private readonly CalculationModule _calculationModule;
        public CalculationModuleTests()
        {
            _calculationModule = new();
            
        }

        [Fact]
        public async Task MainRelayTime_ShouldReturnTrue()
        {
            Calculations calculations = new Calculations()
            {
                Name = "testName",
                UserId = 1,
                MainRelayTime = 0.040,
                IntermediateRelayTime = 0.005,
                CircuitBreakerTime = 0.080,
                AdditionalTime = 0.005,
                AdditionalUROVTime = 0.005,
                InputTime = 0.010,

                StdDevMainRelayTime = 0.15,
                StdDevIntermediateRelayTime = 0.15,
                StdDevCircuitBreakerTime = 0.15,
                StdDevAdditionalTime = 0.15,
                StdDevAdditionalUROVTime = 0.15,
                StdDevInputTime = 0.15,

                ImplementationQuantity = 1000,
                InitialValueUROV = 0.25,
                FinalValueUROV = 0.05,
                StepValue = 0.01
            };
            var timeArray = _calculationModule.MainRelayTime(calculations);
            Assert.True(timeArray.Any());
        }

        [Fact]
        public async Task GetFullTime_ShouldReturnTrue()
        {
            Calculations calculations = new Calculations()
            {
                Name = "testName",
                UserId = 1,
                MainRelayTime = 0.040,
                IntermediateRelayTime = 0.005,
                CircuitBreakerTime = 0.080,
                AdditionalTime = 0.005,
                AdditionalUROVTime = 0.005,
                InputTime = 0.010,

                StdDevMainRelayTime = 0.15,
                StdDevIntermediateRelayTime = 0.15,
                StdDevCircuitBreakerTime = 0.15,
                StdDevAdditionalTime = 0.15,
                StdDevAdditionalUROVTime = 0.15,
                StdDevInputTime = 0.15,

                ImplementationQuantity = 1000,
                InitialValueUROV = 0.25,
                FinalValueUROV = 0.05,
                StepValue = 0.01
            };
            var timeArray = _calculationModule.GetFullTime(calculations);
            Assert.True(timeArray.Any());
        }

        [Fact]
        public async Task GetTimeUROV_ShouldReturnTrue()
        {
            Calculations calculations = new Calculations()
            {
                Name = "testName",
                UserId = 1,
                MainRelayTime = 0.040,
                IntermediateRelayTime = 0.005,
                CircuitBreakerTime = 0.080,
                AdditionalTime = 0.005,
                AdditionalUROVTime = 0.005,
                InputTime = 0.010,

                StdDevMainRelayTime = 0.15,
                StdDevIntermediateRelayTime = 0.15,
                StdDevCircuitBreakerTime = 0.15,
                StdDevAdditionalTime = 0.15,
                StdDevAdditionalUROVTime = 0.15,
                StdDevInputTime = 0.15,

                ImplementationQuantity = 1000,
                InitialValueUROV = 0.25,
                FinalValueUROV = 0.05,
                StepValue = 0.01
            };
            var timeArray = _calculationModule.GetTimeUROV(calculations, 0.150);
            Assert.True(timeArray.Any());
        }

        [Fact]
        public async Task GetProbability_ShouldReturnTrue()
        {
            Calculations calculations = new Calculations()
            {
                Name = "testName",
                UserId = 1,
                MainRelayTime = 0.040,
                IntermediateRelayTime = 0.005,
                CircuitBreakerTime = 0.080,
                AdditionalTime = 0.005,
                AdditionalUROVTime = 0.005,
                InputTime = 0.010,

                StdDevMainRelayTime = 0.15,
                StdDevIntermediateRelayTime = 0.15,
                StdDevCircuitBreakerTime = 0.15,
                StdDevAdditionalTime = 0.15,
                StdDevAdditionalUROVTime = 0.15,
                StdDevInputTime = 0.15,

                ImplementationQuantity = 1000,
                InitialValueUROV = 0.25,
                FinalValueUROV = 0.05,
                StepValue = 0.01
            };
            var probability = _calculationModule.GetProbability(calculations, 0.07);
            Assert.True(probability >= 0);
        }
    }
}
