namespace Domain.CalculationProbability
{
    /// <summary>
    /// Параметры расчета, полученные от пользователя
    /// </summary>
    public class CalculationSettings
    {
        /// <summary>
        /// Название расчета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Время работы основной защиты
        /// </summary>
        public double MainRelayTime { get; set; }

        /// <summary>
        /// Время работы промежуточного реле
        /// </summary>
        public double IntermediateRelayTime { get; set; }

        /// <summary>
        /// Время отключения своего выключателя
        /// </summary>
        public double CircuitBreakerTime { get; set; }

        /// <summary>
        /// Дополнительное время основной защиты
        /// </summary>
        public double AdditionalTime { get; set; }

        /// <summary>
        /// Дополнительное время срабатывания УРОВ
        /// </summary>
        public double AdditionalUROVTime { get; set; }

        /// <summary>
        /// Время срабатывания дискретного входа
        /// </summary>
        public double InputTime { get; set; }

        /// <summary>
        /// Разброс основной защиты
        /// </summary>
        public double StdDevMainRelayTime { get; set; }

        /// <summary> 
        /// Разброс промежуточного реле
        /// </summary>
        public double StdDevIntermediateRelayTime { get; set; }

        /// <summary>
        /// Разброс своего выключателя
        /// </summary>
        public double StdDevCircuitBreakerTime { get; set; }

        /// <summary>
        /// Разброс дополнительного времени основной защиты
        /// </summary>
        public double StdDevAdditionalTime { get; set; }

        /// <summary>
        /// Разброс дополнительного времени срабатывания УРОВ
        /// </summary>
        public double StdDevAdditionalUROVTime { get; set; }

        /// <summary>
        /// Разброс дискретного входа
        /// </summary>
        public double StdDevInputTime { get; set; }

        /// <summary>
        /// Количество реализаций
        /// </summary>
        public int ImplementationQuantity { get; set; }

        /// <summary>
        /// Стартовое значение выдержки времени УРОВ
        /// </summary>
        public double InitialValueUROV { get; set; }

        /// <summary>
        /// Конечное значение выдержки времени УРОВ
        /// </summary>
        public double FinalValueUROV { get; set; }

        /// <summary>
        /// Шаг снижения выдержки времени УРОВ
        /// </summary>
        public double StepValue { get; set; }
    }
}
