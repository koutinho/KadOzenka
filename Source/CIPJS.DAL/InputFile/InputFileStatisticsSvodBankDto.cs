namespace CIPJS.DAL.InputFile
{
    public class InputFileStatisticsSvodBankDto
    {
        /// <summary>
        /// Идентификатор района
        /// </summary>
        public long DistrictId { get; set; }

        /// <summary>
        /// Наименование района
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Код района
        /// </summary>
        public long? DistrictCode { get; set; }

        /// <summary>
        /// Кол-во строк (МФЦ)
        /// </summary>
        public long CountStrMfc { get; set; }

        /// <summary>
        /// Кол-во строк (Банк) Поставщик 1
        /// </summary>
        public long CountStrPost1 { get; set; }

        /// <summary>
        /// Кол-во строк (Банк) Поставщик 2
        /// </summary>
        public long CountStrPost2 { get; set; }

        /// <summary>
        /// Кол-во строк (Банк), всего
        /// </summary>
        public long CountStrSvodBank { get; set; }

        /// <summary>
        /// Зачислено, МФЦ
        /// </summary>
        public decimal? SumOpl { get; set; }

        /// <summary>
        /// Зачислено (Банк) Поставщик 1
        /// </summary>
        public decimal? SumOplPost1 { get; set; }

        /// <summary>
        /// Зачислено (Банк) Поставщик 2
        /// </summary>
        public decimal? SumOplPost2 { get; set; }

        /// <summary>
        /// Зачислено (Банк), всего
        /// </summary>
        public decimal? SumOplSvodBank { get; set; }

        /// <summary>
        /// Кол-во расхождений в строках
        /// </summary>
        public long CountStrDifference { get; set; }

        /// <summary>
        /// Расхождение в суммах
        /// </summary>
        public decimal SumOplDifference { get; set; }

        /// <summary>
        /// Ошибочно МФЦ , кол-во строк
        /// </summary>
        public long CountStrMfcError { get; set; }

        /// <summary>
        /// Ошибочно МФЦ , сумма
        /// </summary>
        public decimal? SumOplError { get; set; }

        /// <summary>
        /// Код поставщика 1
        /// </summary>
        public long? KodPost1 { get; set; }

        /// <summary>
        /// Код поставщика 2
        /// </summary>
        public long? KodPost2 { get; set; }
    }
}
