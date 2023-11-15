namespace BLL.Service
{
    public class GaussRandom
    {
        private Random _random = new Random();
        private double _z1 = double.NegativeInfinity;

        public double Next()
        {
            double result;
            if (!double.IsNegativeInfinity(_z1))
            {
                result = _z1;
                _z1 = double.NegativeInfinity;
            }
            else
            {
                double x, y, s;
                do
                {
                    x = _random.NextDouble() * 2 - 1;
                    y = _random.NextDouble() * 2 - 1;
                    s = x * x + y * y;
                }
                while (s <= 0 || s > 1);

                result = x * Math.Sqrt(-2.0 * Math.Log(s) / s);
                _z1 = y * Math.Sqrt(-2.0 * Math.Log(s) / s);
            }
            return result;
        }

        public double Next(double mean, double stddev)
        {
            return Next() * stddev + mean;
        }
    }
}
