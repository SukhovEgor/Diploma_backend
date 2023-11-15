using MathNet.Numerics.Integration;
using System;

namespace BLL.Service
{
    // TODO: Убрать потом Вывод в консоль
    public class Calculation
    {
        public static void GetRandomData(CalculationSetting calculationSetting)
        {

            double[] fullTimeArr = FullTime(calculationSetting);
            Console.WriteLine("fullTimeArr");
            foreach (double dbl in fullTimeArr)
            {
                Console.WriteLine(dbl);
            }

            TimeUROV(calculationSetting);

        }

        public static double[] MainRelayTime(CalculationSetting calculationSetting)
        {
            var zRandom = new GaussRandom();
            double[] mainRelayTimeArr = new double[calculationSetting.ImplementationQuantity];

            for (int i = 0; i < calculationSetting.ImplementationQuantity; i++)
            {
                double tmpTime =
                    zRandom.Next(calculationSetting.MainRelayTime,
                        calculationSetting.MainRelayTime *
                        calculationSetting.StdDevMainRelayTime) +
                    zRandom.Next(calculationSetting.IntermediateRelayTime,
                        calculationSetting.IntermediateRelayTime *
                        calculationSetting.StdDevIntermediateRelayTime);
                mainRelayTimeArr[i] = Math.Round(tmpTime, 6);
            }

            return mainRelayTimeArr;
        }

        public static double[] FullTime(CalculationSetting calculationSetting)
        {
            var zRandom = new GaussRandom();
            double[] fullTime = new double[calculationSetting.ImplementationQuantity];
            double[] mainRelayTimeArr = MainRelayTime(calculationSetting);

            for (int i = 0; i < calculationSetting.ImplementationQuantity; i++)
            {
                double tmpTime = 
                    zRandom.Next(calculationSetting.CircuitBreakerTime,
                        calculationSetting.CircuitBreakerTime *
                        calculationSetting.StdDevCircuitBreakerTime)
                    + zRandom.Next(calculationSetting.AdditionalTime,
                        calculationSetting.AdditionalTime *
                        calculationSetting.StdDevAdditionalTime);

                fullTime[i] = Math.Round(tmpTime + mainRelayTimeArr[i], 6);
            }

            return fullTime;
        }
        public static void TimeUROV(CalculationSetting calculationSetting)
        {
            var zRandom = new GaussRandom();
            double[] timeUROVArr = new double[calculationSetting.ImplementationQuantity];
            double[] mainRelayTimeArr = MainRelayTime(calculationSetting);

            var step = calculationSetting.StepValue;

            for (var timeUROV = calculationSetting.InitialValueUROV;
                timeUROV >= calculationSetting.FinalValueUROV; timeUROV -= step)
            {
                var probability = GetProbability(calculationSetting, timeUROV);
                Console.WriteLine($"Вероятность излишней работы УРОВ " +
                    $"{Math.Round(100 * probability, 2)}, при выдержке времени {Math.Round(1000 * timeUROV, 2)}");
                for (int i = 0; i < calculationSetting.ImplementationQuantity; i++)
                {
                    double tmpTime =
                        zRandom.Next(calculationSetting.InputTime,
                            calculationSetting.InputTime *
                            calculationSetting.StdDevInputTime)
                        + zRandom.Next(calculationSetting.AdditionalTime,
                            calculationSetting.AdditionalTime *
                            calculationSetting.StdDevAdditionalTime);

                    timeUROVArr[i] = Math.Round(mainRelayTimeArr[i] + tmpTime + timeUROV, 6); ;
                }

            }
        }
        public static double GetProbability(CalculationSetting calculationSetting, double timeUROV)
        {
            double totalMean =
                calculationSetting.MainRelayTime +
                calculationSetting.IntermediateRelayTime +
                calculationSetting.CircuitBreakerTime +
                calculationSetting.AdditionalTime;

            Console.WriteLine($"totalMean: {totalMean}");

            double UROVMean = 
                calculationSetting.MainRelayTime +
                calculationSetting.IntermediateRelayTime +
                calculationSetting.AdditionalUROVTime + timeUROV;
            Console.WriteLine($"UROVMean: {UROVMean}");

            double totalStandartDev =
                calculationSetting.MainRelayTime * calculationSetting.StdDevMainRelayTime +
                calculationSetting.IntermediateRelayTime * calculationSetting.StdDevIntermediateRelayTime +
                calculationSetting.CircuitBreakerTime * calculationSetting.StdDevCircuitBreakerTime +
                calculationSetting.AdditionalTime * calculationSetting.StdDevAdditionalTime;
            Console.WriteLine($"totalStandartDev: {totalStandartDev}");

            double UROVStandartDev = 
                calculationSetting.MainRelayTime * calculationSetting.StdDevMainRelayTime +
                calculationSetting.IntermediateRelayTime * calculationSetting.StdDevIntermediateRelayTime +
                calculationSetting.AdditionalUROVTime * calculationSetting.StdDevAdditionalUROVTime;
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

        public static double SolveIntegral(double startPoint, double endPoint,
            double mean, double standartDev)
        {
            double composite = DoubleExponentialTransformation.Integrate(x =>
                Math.Exp(-0.5 * Math.Pow((x - mean) / standartDev, 2)) /
                (standartDev * Math.Sqrt(2 * Math.PI)), startPoint, endPoint, 1e-5);
            return composite;
        }
    }
}
