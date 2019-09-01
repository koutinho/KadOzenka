namespace CIPJS.DAL.InputFile
{
    public class InputFileStatisticsPlatDto
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
        /// Идентификатор файла
        /// </summary>
        public long? InputFileId { get; set; }

        /// <summary>
        /// Файл МФЦ
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Код статуса файла
        /// </summary>
        public long? StatusCode { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Количество строк загружено
        /// </summary>
        public long CountStrLoad { get; set; }

        /// <summary>
        /// Количество строк
        /// </summary>
        public long CountStr { get; set; }

        /// <summary>
        /// Начислено, руб
        /// </summary>
        public decimal? SumOpl { get; set; }

        /// <summary>
        /// Количество ошибок
        /// </summary>
        public long CountError { get; set; }

        /// <summary>
        /// Подтверждено банком, кол-во строк
        /// </summary>
        public long IdentifiedCount { get; set; }

        /// <summary>
        /// Не подтверждено банком, кол-во строк
        /// </summary>
        public long NotIdentifiedCount { get; set; }

        /// <summary>
        /// Строки банка, не связанные с зачислениями МФЦ
        /// </summary>
        public long NotIdentifiedBankCount { get; set; }

        /// <summary>
        /// Учтено на ФСП, кол-во строк
        /// </summary>
        public long IdentifiedFullCount { get; set; }

        /// <summary>
        /// Всего, кол-во строк
        /// </summary>
        public long InputPlatSumNotNullCount { get; set; }

        /// <summary>
        /// Кол-во частично-идентифицированных записей
        /// </summary>
        public long PartiallyIdentifiedCount { get; set; }

        /// <summary>
        /// Ошибочно МФЦ
        /// </summary>
        public long NotConfirmedByBankCount { get; set; }
    }
}
