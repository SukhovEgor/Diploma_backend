using Application.DTOs;
using Application.DTOs.Requests;
using Application.Interfaces;
using Domain.CalculationProbability;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IntegrationTests
{
    public class CalculationsControllerTest: WebApplicationFactory<Program>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public CalculationsControllerTest()
        {
            _factory = new();
            _client = _factory.CreateClient();

        }

        [Fact]
        public async Task GetCalculations_ShouldReturnOk()
        {
            var response = await _client.GetAsync("https://localhost:7295/api/Calculations/GetCalculations/3");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetCalculationById_ShouldReturnOk()
        {
            var response = await _client.GetAsync("https://localhost:7295/api/Calculations/GetCalculationResult/376e17c8-b2e7-4e9c-ae0f-64431719c8a3");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteCalculationsById_ShouldReturnOk()
        {
            var response = await _client.GetAsync("https://localhost:7295/api/Calculations/GetCalculationResult/d4b61e55-e9b3-43a5-90ac-6d4b4b36e82c");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task StartCalculation_ShouldReturnOk()
        {

            var httpContent = new StringContent(JsonConvert.SerializeObject(new CalculationSettingsRequest()
            {
                Name = "testName",
                UserId = 3,
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
            }),
                Encoding.UTF8, "application/json"); ;
            var response = await _client.PostAsync("https://localhost:7295/api/Calculations/PostCalculations", httpContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}