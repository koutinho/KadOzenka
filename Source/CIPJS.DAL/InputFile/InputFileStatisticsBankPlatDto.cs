namespace CIPJS.DAL.InputFile
{
    public class InputFileStatisticsBankPlatDto
    {
        /// <summary>
        /// Код поставщика
        /// </summary>
        public long? KodPost { get; set; }

        /// <summary>
        /// Строк всего
        /// </summary>
        public long CountStr { get; set; }

        /// <summary>
        /// Строк без признака распределения
        /// </summary>
        public long CountStrNotFlagVozvr { get; set; }

        /// <summary>
        /// Строк с признаком распределения
        /// </summary>
        public long CountStrFlagVozvr { get; set; }

        /// <summary>
        /// Сумма всего
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма без признака распределения
        /// </summary>
        public decimal SumNotFlagVozvr { get; set; }

        /// <summary>
        /// Сумма с признаком распределения
        /// </summary>
        public decimal SumFlagVozvr { get; set; }

        /// <summary>
        /// Разность сумм
        /// </summary>
        public decimal SumDifference { get; set; }
    }
}
