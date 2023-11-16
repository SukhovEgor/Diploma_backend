using Application.DTOs;
namespace Application.Interfaces
{
    public interface ICalculationModule
    {
        double[] MainRelayTime(CalculationSettingsRequest calculationSettings);

        double[] GetFullTime(CalculationSettingsRequest calculationSettings);
        double[] GetTimeUROV(CalculationSettingsRequest calculationSettings, double timeUROV);
        double GetProbability(CalculationSettingsRequest calculationSettings, double timeUROV);

        double SolveIntegral(double startPoint, double endPoint,
            double mean, double standartDev);
    }
}
