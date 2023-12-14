using Domain.ProcessedResult;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.DAL
{
    /// <summary>
    /// Результаты расчета для хранения в БД
    /// </summary>
    [Table("CalculationResult")]
    public class CalculationResultEntity
    {
        /// <summary>
        /// Уникальный идентификатор расчета
        /// </summary>
        [Key, Column(Order = 0)]
        public Guid CalculationId { get; set; }

        /// <summary>
        /// Номер реализации
        /// </summary>
        [Key, Column(Order = 1)]
        public int ImplementationId { get; set; }

        /// <summary>
        /// Значение выдержки времени УРОВ
        /// </summary>
        public double UROVValue { get; set; }

        /// <summary>
        /// Значение вероятности излишней работы УРОВ
        /// </summary>
        public double ProbabilityValue { get; set; }

        /// <summary>
        /// Массив сличайных времен срабатывания УРОВ
        /// </summary>
        public double[] UROVTimeArray { get; set; }

        /// <summary>
        /// Ссылка на расчет
        /// </summary>
        public CalculationEntity? Calculation { get; set; }
    }
}
