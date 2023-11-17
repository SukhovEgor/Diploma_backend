using Domain.CalculationProbability;

namespace Application.Interfaces
{
    public interface ICalculationModule
    {
        double[] MainRelayTime(Calculations calculationSettings);

        double[] GetFullTime(Calculations calculation);
        double[] GetTimeUROV(Calculations calculationSettings, double timeUROV);
        double GetProbability(Calculations calculationSettings, double timeUROV);

        double SolveIntegral(double startPoint, double endPoint,
            double mean, double standartDev);
    }
}
