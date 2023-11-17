using MathNet.Numerics.Integration;
using Application.Interfaces;
using Domain.CalculationProbability;

namespace Application.UseCases
{
    // TODO: Убрать потом Вывод в консоль
    public class CalculationModule : ICalculationModule
    {
        public CalculationModule()
        {
        }

        public double[] MainRelayTime(Calculations calculationSettings)
        {
            var zRandom = new GaussRandom();
            double[] mainRelayTimeArr = new double[calculationSettings.ImplementationQuantity];

            for (int i = 0; i < calculationSettings.ImplementationQuantity; i++)
            {
                double tmpTime =
                    zRandom.Next(calculationSettings.MainRelayTime,
                        calculationSettings.MainRelayTime *
                        calculationSettings.StdDevMainRelayTime) +
                    zRandom.Next(calculationSettings.IntermediateRelayTime,
                        calculationSettings.IntermediateRelayTime *
                        calculationSettings.StdDevIntermediateRelayTime);
                mainRelayTimeArr[i] = Math.Round(tmpTime, 6);
            }

            return mainRelayTimeArr;
        }

        public double[] GetFullTime(Calculations calculationSettings)
        {
            var zRandom = new GaussRandom();
            double[] fullTime = new double[calculationSettings.ImplementationQuantity];
            double[] mainRelayTimeArr = MainRelayTime(calculationSettings);

            for (int i = 0; i < calculationSettings.ImplementationQuantity; i++)
            {
                double tmpTime = 
                    zRandom.Next(calculationSettings.CircuitBreakerTime,
                        calculationSettings.CircuitBreakerTime *
                        calculationSettings.StdDevCircuitBreakerTime)
                    + zRandom.Next(calculationSettings.AdditionalTime,
                        calculationSettings.AdditionalTime *
                        calculationSettings.StdDevAdditionalTime);

                fullTime[i] = Math.Round(tmpTime + mainRelayTimeArr[i], 6);
            }

            return fullTime;
        }
        public double[] GetTimeUROV(Calculations calculationSettings, double timeUROV)
        {
            var zRandom = new GaussRandom();
            double[] timeUROVArr = new double[calculationSettings.ImplementationQuantity];
            double[] mainRelayTimeArr = MainRelayTime(calculationSettings);

            for (int i = 0; i < calculationSettings.ImplementationQuantity; i++)
            {
                double tmpTime =
                    zRandom.Next(calculationSettings.InputTime,
                        calculationSettings.InputTime *
                        calculationSettings.StdDevInputTime)
                    + zRandom.Next(calculationSettings.AdditionalTime,
                        calculationSettings.AdditionalTime *
                        calculationSettings.StdDevAdditionalTime);

                timeUROVArr[i] = Math.Round(mainRelayTimeArr[i] + tmpTime + timeUROV, 6); ;
            }
            return timeUROVArr;
        }
        public double GetProbability(Calculations calculationSettings, double timeUROV)
        {
            double totalMean =
                calculationSettings.MainRelayTime +
                calculationSettings.IntermediateRelayTime +
                calculationSettings.CircuitBreakerTime +
                calculationSettings.AdditionalTime;

            Console.WriteLine($"totalMean: {totalMean}");

            double UROVMean = 
                calculationSettings.MainRelayTime +
                calculationSettings.IntermediateRelayTime +
                calculationSettings.AdditionalUROVTime + timeUROV;
            Console.WriteLine($"UROVMean: {UROVMean}");

            double totalStandartDev =
                calculationSettings.MainRelayTime * calculationSettings.StdDevMainRelayTime +
                calculationSettings.IntermediateRelayTime * calculationSettings.StdDevIntermediateRelayTime +
                calculationSettings.CircuitBreakerTime * calculationSettings.StdDevCircuitBreakerTime +
                calculationSettings.AdditionalTime * calculationSettings.StdDevAdditionalTime;
            Console.WriteLine($"totalStandartDev: {totalStandartDev}");

            double UROVStandartDev = 
                calculationSettings.MainRelayTime * calculationSettings.StdDevMainRelayTime +
                calculationSettings.IntermediateRelayTime * calculationSettings.StdDevIntermediateRelayTime +
                calculationSettings.AdditionalUROVTime * calculationSettings.StdDevAdditionalUROVTime;
            Console.WriteLine($"UROVStandartDev: {UROVStandartDev}");

            double logarithm = Math.Log(Math.Pow(UROVStandartDev, 2) /
                Math.Pow(totalStandartDev, 2));

            double sqrtTotalStandartDev = Math.Pow(totalStandartDev, 2);
            double sqrtUROVStandartDev = Math.Pow(UROVStandartDev, 2);

            double substractionSqrtStandartDev =
                sqrtUROVStandartDev - sqrtTotalStandartDev;

            double totalSqrt = Math.Sqrt(Math.Pow(totalMean - UROVMean, 2) +
                substractionSqrtStandartDev * logarithm);

            double x1 = (totalMean * sqrtUROVStandartDev -
                UROVMean * sqrtTotalStandartDev +
                totalStandartDev * UROVStandartDev * totalSqrt)
                / substractionSqrtStandartDev;

            double x2 = (totalMean * sqrtUROVStandartDev -
                UROVMean * sqrtTotalStandartDev -
                totalStandartDev * UROVStandartDev * totalSqrt)
                / substractionSqrtStandartDev;

            Console.WriteLine($"x1: {x1}");
            Console.WriteLine($"x2: {x2}");

            double positive = 1000000;
            double negative = -1000000;

            double firstMin = Math.Min(
                SolveIntegral(negative, x1, totalMean, totalStandartDev),
                SolveIntegral(negative, x1, UROVMean, UROVStandartDev));

            double secondMin = Math.Min(
                SolveIntegral(x1, x2, totalMean, totalStandartDev),
                SolveIntegral(x1, x2, UROVMean, UROVStandartDev));

            double thirdMin = Math.Min(
                SolveIntegral(x2, positive, totalMean, totalStandartDev),
                SolveIntegral(x2, positive, UROVMean, UROVStandartDev));

            double probability = firstMin + secondMin + thirdMin;

            return probability;
        }

        public double SolveIntegral(double startPoint, double endPoint,
            double mean, double standartDev)
        {
            double composite = DoubleExponentialTransformation.Integrate(x =>
                Math.Exp(-0.5 * Math.Pow((x - mean) / standartDev, 2)) /
                (standartDev * Math.Sqrt(2 * Math.PI)), startPoint, endPoint, 1e-5);
            return composite;
        }
        /*
        public void TimeUROV(CalculationSettingsRequest calculationSettings)
        {
            var zRandom = new GaussRandom();
            double[] timeUROVArr = new double[calculationSettings.ImplementationQuantity];
            double[] mainRelayTimeArr = MainRelayTime(calculationSettings);

            var step = calculationSettings.StepValue;

            for (var timeUROV = calculationSettings.InitialValueUROV;
                timeUROV >= calculationSettings.FinalValueUROV; timeUROV -= step)
            {
                var probability = GetProbability(calculationSettings, timeUROV);
                Console.WriteLine($"Вероятность излишней работы УРОВ " +
                    $"{Math.Round(100 * probability, 2)}, при выдержке времени {Math.Round(1000 * timeUROV, 2)}");
                for (int i = 0; i < calculationSettings.ImplementationQuantity; i++)
                {
                    double tmpTime =
                        zRandom.Next(calculationSettings.InputTime,
                            calculationSettings.InputTime *
                            calculationSettings.StdDevInputTime)
                        + zRandom.Next(calculationSettings.AdditionalTime,
                            calculationSettings.AdditionalTime *
                            calculationSettings.StdDevAdditionalTime);

                    timeUROVArr[i] = Math.Round(mainRelayTimeArr[i] + tmpTime + timeUROV, 6); ;
                }

            }
        }*/
    }
}
