using Application.Interfaces;
using Application.UseCases;
using Domain;
using Domain.CalculationProbability;
using Infrastructure.Services;
using Moq;

namespace UnitTests
{
    public class CalculationServiceTests
    {
        private readonly Mock<ICalculationResultRepository> _calculationRepositoryMock;
        private readonly Mock<ICalculationModule> _calculationModuleMock;

        public CalculationServiceTests()
        {
            _calculationRepositoryMock = new Mock<ICalculationResultRepository>();
            _calculationModuleMock = new Mock<ICalculationModule>();
        }

        [Fact]
        public async Task StartCalculation_ShouldReturnTrue()
        {
            CalculationSettings calculationSettings = new()
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
            CalculationModule calculationModule = new();
            
            
            var service = new CalculationService(_calculationRepositoryMock.Object, calculationModule);

            //act
            var result = service.StartCalculation(calculationSettings);

            //assert
            Assert.True(result.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task GetCalculations_ShouldGetCalculations()
        {
            _calculationRepositoryMock
                .Setup(x => x.GetCalculations(1))
                .Returns(() => Task.FromResult(new List<Calculations>() { new Calculations() { Id = Guid.NewGuid(), Name = "test"} }))
                .Verifiable();
            var service = new CalculationService(_calculationRepositoryMock.Object, _calculationModuleMock.Object);
            var result = service.GetCalculations(1);

            _calculationRepositoryMock.Verify(x => x.GetCalculations(1), Times.Once);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task GetCalculationsByid_ShouldGetCalculationResult()
        {
            var array = new double[1];
            _calculationRepositoryMock
                .Setup(x => x.GetResultInitialById("testId"))
                .Returns(() => Task.FromResult(new List<CalculationResult>() { new CalculationResult(Guid.NewGuid(), 1, 1, 1, array) } as IEnumerable<CalculationResult>))
                .Verifiable();
            var service = new CalculationService(_calculationRepositoryMock.Object, _calculationModuleMock.Object);

            var result = service.GetCalculationById("testId");

            _calculationRepositoryMock.Verify(x => x.GetResultInitialById("testId"), Times.Once);
            Assert.True(result.ToList().Count > 0);
        }

        [Fact]
        public async Task DeleteCalculation_ShouldReturnTrue()
        {
            _calculationRepositoryMock
                .Setup(x => x.DeleteCalculationsById("test"));
            var service = new CalculationService(_calculationRepositoryMock.Object, _calculationModuleMock.Object);
            var result = service.DeleteCalculationById("test");

            _calculationRepositoryMock.Verify(x => x.DeleteCalculationsById("test"), Times.Once);
            Assert.True(result.IsCompletedSuccessfully);
        }
    }
}
