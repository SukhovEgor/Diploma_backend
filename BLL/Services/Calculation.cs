namespace BLL.Service
{
    public class Calculation
    {
        public static void GetRandomData(double mainRelayTime,
                                         double intermediateRelayTime,
                                         double circuitBreakerTime,
                                         double valueUROV,
                                         double standartDev,
                                         int implementationQuantity)
        {

            double[] fullTimeArr = FullTime(mainRelayTime,
                                            implementationQuantity,
                                            standartDev,
                                            intermediateRelayTime,
                                            circuitBreakerTime);
            Console.WriteLine("fullTimeArr");
            foreach (double dbl in fullTimeArr)
            {
                Console.WriteLine(dbl);
            }

            double[] timeUROVArr = TimeUROV(mainRelayTime,
                                            implementationQuantity,
                                            standartDev,
                                            valueUROV);
            Console.WriteLine("timeUROVArr");
            foreach (double dbl in timeUROVArr)
            {
                Console.WriteLine(dbl);
            }

        }

        public static double[] MainRelayTime(int implementationQuantity,
                                             double mainRelayTime,
                                             double standartDev)
        {
            var zRandom = new GaussRandom();
            double[] mainRelayTimeArr = new double[implementationQuantity];

            for (int i = 0; i < implementationQuantity; i++)
            {
                double tmpTime = zRandom.Next(mainRelayTime, mainRelayTime * standartDev);
                mainRelayTimeArr[i] = Math.Round(tmpTime, 6);
            }

            return mainRelayTimeArr;
        }

        public static double[] FullTime(double mainRelayTime,
                                        int implementationQuantity,
                                        double standartDev,
                                        double intermediateRelayTime,
                                        double circuitBreakerTime)
        {
            var zRandom = new GaussRandom();
            double[] fullTime = new double[implementationQuantity];
            double[] mainRelayTimeArr = MainRelayTime(implementationQuantity,
                                          mainRelayTime,
                                          standartDev);

            for (int i = 0; i < implementationQuantity; i++)
            {
                double tmpTime = zRandom.Next(intermediateRelayTime, intermediateRelayTime * standartDev)
                    + zRandom.Next(circuitBreakerTime, circuitBreakerTime * standartDev);

                fullTime[i] = Math.Round(tmpTime + mainRelayTimeArr[i], 6);
            }

            return fullTime;
        }
        public static double[] TimeUROV(double mainRelayTime,
                                        int implementationQuantity,
                                        double standartDev,
                                        double valueUROV)
        {
            double[] timeUROVArr = new double[implementationQuantity];
            double[] mainRelayTimeArr = MainRelayTime(implementationQuantity,
                              mainRelayTime,
                              standartDev);

            for (int i = 0; i < implementationQuantity; i++)
            {
                timeUROVArr[i] = Math.Round(mainRelayTimeArr[i] + valueUROV, 6); ;
            }

            return timeUROVArr;
        }
    }
}
