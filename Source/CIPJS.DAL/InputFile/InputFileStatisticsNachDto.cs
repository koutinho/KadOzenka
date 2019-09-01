﻿namespace CIPJS.DAL.InputFile
{
    public class InputFileStatisticsNachDto
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
        public decimal? SumNach { get; set; }

        /// <summary>
        /// Количество ошибок
        /// </summary>
        public long CountError { get; set; }
    }
}
