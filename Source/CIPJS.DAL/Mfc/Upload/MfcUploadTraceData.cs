using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIPJS.DAL.Mfc.Upload
{
    /// <summary>
    /// Формирует LOG-файл загрузки (INSUR_INPUT_FILE.TRACEDATA), содержащий следующую информацию о загрузке пакета файлов:
    ///-Количество загруженных файлов (по каждому типу файлов)
    ///-Ошибки загрузки, с указанием названия файлов и причин ошибок.
    ///-Количество идентифицированных записей
    ///-Количество не идентифицированных записей
    ///-Время загрузки
    /// </summary>
    public class MfcUploadTraceData
    {
        /// <summary>
        /// Статус загрузки
        /// </summary>
        public MfcUploadFileStatus Status { get; set; }

        /// <summary>
        /// Количество загруженных файлов реестра оплат
        /// </summary>
        public int OplFilesCount { get; set; }

        /// <summary>
        /// Количество загруженный файлов реестра начислений
        /// </summary>
        public int NachFilesCount { get; set; }

        /// <summary>
        /// Количество загруженных файлов реестра зачислений
        /// </summary>
        public int PlatFileCount { get; set; }

        /// <summary>
        /// Ошибки загрузки
        /// </summary>
        public List<string> Errors { get; set; }

        ///// <summary>
        ///// Количество частично идентифицированных записей реестра зачислений
        ///// </summary>
        //public int PlatPartiallyIdentifCount { get; set; }

        ///// <summary>
        ///// Количество неидентифицированных записей реестра зачислений
        ///// </summary>
        //public int PlatUnidentifCount { get; set; }

        /// <summary>
        /// Время начала загрузки
        /// </summary>
        public DateTime? ProcessStartDate { get; set; }

        /// <summary>
        /// Время окончания загрузки
        /// </summary>
        public DateTime? ProcessEndDate { get; set; }

        /// <summary>
        /// Идентификаторы загружаемых пакетов
        /// </summary>
        public List<long?> InputPackageIds { get; set; }

        /// <summary>
        /// Идентификатор лога загрузки
        /// </summary>
        public long LogFileId { get; set; }

        public MfcUploadTraceData(long logFileId)
        {
            LogFileId = logFileId;
            ProcessStartDate = DateTime.Now;
            Errors = new List<string>();
            InputPackageIds = new List<long?>();
        }

        public override string ToString()
        {
            DateTime currentDate = DateTime.Now;
            StringBuilder sb = new StringBuilder();

            int ordinal = 0;

            sb.AppendLine($"{++ordinal}. Количество загруженных файлов: {OplFilesCount + NachFilesCount + PlatFileCount}");
            sb.AppendLine($"    Количество загруженных файлов оплат: {OplFilesCount}");
            sb.AppendLine($"    Количество загруженных файлов начислений: {NachFilesCount}");
            sb.AppendLine($"    Количество загруженных файлов зачислений: {PlatFileCount}");
            if (Errors.Count == 1)
            {
                sb.AppendLine($"{++ordinal}. Ошибка загрузки: {Errors[0]}");
            }
            else if (Errors.Count > 1)
            {
                sb.AppendLine($"{++ordinal}. Ошибки загрузки:");
                foreach(string error in Errors)
                {
                    sb.AppendLine($"    - {error}");
                }
            }
            //sb.AppendLine($"{++ordinal}. Количество частично идентифицированных записей зачислений {PlatPartiallyIdentifCount}");
            //sb.AppendLine($"{++ordinal}. Количество неидентифицированных записей зачислений {PlatUnidentifCount}");

            if (ProcessEndDate.HasValue && ProcessStartDate.HasValue && ProcessStartDate < ProcessEndDate.Value)
            {
                sb.AppendLine($"{++ordinal}. Время выполнения (сек): {(ProcessEndDate.Value - ProcessStartDate.Value).TotalSeconds}");
            }
            else if (ProcessStartDate.HasValue && ProcessStartDate.Value < currentDate)
            {
                sb.AppendLine($"{++ordinal}. Время выполнения (сек): {(currentDate - ProcessStartDate.Value).TotalSeconds}");
            }
            else
            {
                sb.AppendLine($"{++ordinal}. Время выполнения (сек): не удалось определить дату окончания");
            }
            return sb.ToString();
        }

        public void SaveLogFile()
        {
            OMLogFile logFile = OMLogFile
                .Where(x => x.EmpId == LogFileId)
                .SelectAll()
                .Execute()
                .FirstOrDefault();

            if (logFile == null)
            {
                throw new Exception($"Не удалось определить запись для логирования загрузки с идентификатором {LogFileId}");
            }
            
            logFile.StartDate = ProcessStartDate;
            logFile.EndDate = ProcessEndDate;
            logFile.Status_Code = Status;
            logFile.GeneralStatus_Code = Status != MfcUploadFileStatus.Error && Status != MfcUploadFileStatus.Finished ? 
                MfcGeneralUploadStatus.Loading : MfcGeneralUploadStatus.Loaded;
            logFile.Tracedata = ToString();
            logFile.Save();
        }
    }
}
