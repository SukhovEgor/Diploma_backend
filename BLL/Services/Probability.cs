using MathNet.Numerics.Integration;

namespace BLL.Service
{
    public class Probability
    {
        public static double GetProbability(double mainRelayTime,
                                            double intermediateRelayTime,
                                            double circuitBreakerTime,
                                            double standartDev,
                                            double timeUROV)
        {
            double totalMean =
                mainRelayTime + intermediateRelayTime + circuitBreakerTime;
            Console.WriteLine($"totalMean: {totalMean}");
            double UROVMean = mainRelayTime + timeUROV;
            Console.WriteLine($"UROVMean: {UROVMean}");

            double totalStandartDev = totalMean * standartDev;
            Console.WriteLine($"totalStandartDev: {totalStandartDev}");
            double UROVStandartDev = mainRelayTime * standartDev;
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
            double mean, double sigma)
        {
            double composite = DoubleExponentialTransformation.Integrate(x =>
                Math.Exp(-0.5 * Math.Pow((x - mean) / sigma, 2)) /
                (sigma * Math.Sqrt(2 * Math.PI)), startPoint, endPoint, 1e-5);
            return composite;
        }
    }
}
