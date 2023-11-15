using Application.DTOs;
namespace Application.Interfaces
{
    public interface ICalculationModule
    {
        void GetRandomData(CalculationSettingsRequest calculationSettings);

        double[] MainRelayTime(CalculationSettingsRequest calculationSettings);

        double[] FullTime(CalculationSettingsRequest calculationSettings);
        void TimeUROV(CalculationSettingsRequest calculationSettings);
        double GetProbability(CalculationSettingsRequest calculationSettings, double timeUROV);

        double SolveIntegral(double startPoint, double endPoint,
            double mean, double standartDev);
    }
}
