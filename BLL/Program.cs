using BLL.Service;

namespace BLL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CalculationSetting calculationSetting = new CalculationSetting()
            {
                MainRelayTime = 0.03,
                IntermediateRelayTime = 0.005,
                CircuitBreakerTime = 0.045,
                AdditionalTime = 0,
                AdditionalUROVTime = 0,
                InputTime = 0.005,

                StdDevMainRelayTime = 0.2 / 3,
                StdDevIntermediateRelayTime = 0.2 / 3,
                StdDevCircuitBreakerTime = 0.2 / 3,
                StdDevAdditionalTime = 0.2 / 3,
                StdDevAdditionalUROVTime = 0.2 / 3,
                StdDevInputTime = 0.2 / 3,

                ImplementationQuantity = 10,
                InitialValueUROV = 0.3,
                StepValue = 0.01
            };

            Calculation.GetRandomData(calculationSetting);
        }
    }
}