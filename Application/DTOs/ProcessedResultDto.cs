﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProcessedResultDto
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
        public List<HistogramDataDto> HistogramData { get; set; } = new();
    }
}
