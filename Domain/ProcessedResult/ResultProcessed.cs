﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProcessedResult
{
    public class ResultProcessed
    {
        /// <summary>
        /// Максимум выборки
        /// </summary>
        public double Maximum { get; set; }

        /// <summary>
        /// Высота прямоугольника
        /// </summary>
        public double Minimum { get; set; }

        /// <summary>
        /// Математическое ожидание
        /// </summary>
        public double Mean { get; set; }

        /// <summary>
        /// Среднеквадратическое отклонение
        /// </summary>
        public double StD { get; set; }

        /// <summary>
        /// Столбцы гистограммы
        /// </summary>
        public List<HistogramData>? HistogramData { get; set; } = new();

        public List<HistogramData> GetStatistic(List<double> values)
        {
            int intervalCount = Convert.ToInt32(Math.Log10(values.Count) + Math.Sqrt(values.Count) + 10);
            Maximum = values.Max();
            Minimum = values.Min();
            Mean = Math.Round(values.Average(), 4);
            double dispersion = values.Sum(a => (a - Mean) * (a - Mean)) / (values.Count - 1);
            StD = Math.Sqrt(dispersion);
            double step = (Maximum - Minimum) / intervalCount;
            double sec = Minimum;
            double first = 0, height = 0;
            for (int i = 0; i < intervalCount; i++)
            {
                int count = 0;
                sec += step;
                first = sec - step;
                count += (from double v in values
                          where v >= first && v <= sec
                          select v).Count();
                height = Math.Round((Convert.ToDouble(count) / Convert.ToDouble(values.Count)) * 100, 4);
                HistogramData.Add(new HistogramData() { Interval = $"{Math.Round(first, 4)} - {Math.Round(sec, 4)}", Height = height });
            }
            return HistogramData;
        }
    }
}
