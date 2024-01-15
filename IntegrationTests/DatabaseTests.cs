using Infrastructure.DAL;
using Infrastructure.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class DatabaseTest
    {
        private readonly DataContext _context;

        public DatabaseTest()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkNpgsql().BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseNpgsql("Server=localhost;Port=5433;Database=Diploma_databaseTest;User ID=postgres;Password=qwerty").
                UseInternalServiceProvider(serviceProvider);
            _context = new DataContext(builder.Options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task Users_ShouldAddAndGetEntity()
        {
            _context.Users.Add(new UserEntity()
            {
                Id = 1,
                Login = "admin",
                Password = "admin",
                Name = "name",
                SurName = "surname",
                LastName = "lastname",
                Post = "post"
            });
            _context.SaveChanges();
            var userEntity = _context.Users.ToList();
            Assert.Equal("admin", userEntity[0].Login);
        }

        [Fact]
        public async Task Calculations_ShouldAddAndGetEntity()
        {
            _context.Users.Add(new UserEntity()
            {
                Id = 1,
                Login = "admin",
                Password = "admin",
                Name = "name",
                SurName = "surname",
                LastName = "lastname",
                Post = "post"
            });
            _context.SaveChanges();
            _context.Calculations.Add(new CalculationEntity() 
            { 
                Id = Guid.NewGuid(),
                UserId = 1,
                Name = "test",
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
            });
            _context.SaveChanges();
            var calculationsEntity = _context.Calculations.ToList();
            Assert.Equal("test", calculationsEntity[0].Name);
        }

        [Fact]
        public async Task CalculationResults_ShouldAddAndGetEntity()
        {
            _context.Users.Add(new UserEntity()
            {
                Id = 1,
                Login = "admin",
                Password = "admin",
                Name = "name",
                SurName = "surname",
                LastName = "lastname",
                Post = "post"
            });
            _context.SaveChanges();
            Guid uid = Guid.NewGuid();
            _context.Calculations.Add(new CalculationEntity()
            {
                Id = uid,
                UserId = 1,
                Name = "test",
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
            });
            _context.CalculationResults.Add(new CalculationResultEntity() { CalculationId = uid, ImplementationId = 1, UROVValue = 0.07, ProbabilityValue = 0.01 });
            _context.SaveChanges();
            var powerFlowResultEntity = _context.CalculationResults.ToList();
            Assert.Equal(0.01, powerFlowResultEntity[0].ProbabilityValue);
        }


    }
}
