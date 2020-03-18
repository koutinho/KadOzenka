using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;

using Core.Main.FileStorages;
using Core.ErrorManagment;
using Core.Shared.Exceptions;

namespace KadOzenka.Dal.SystemStatistics
{
    class SystemStatisticsManager : ILongProcess
    {
        public void LogError(long? objectId, Exception ex, long? errorId = null) { }

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            DbCommand command = DBMngr.Main.GetSqlStringCommand("select fill_system_daily_statistics()");
            DBMngr.Main.ExecuteNonQuery(command);

            DateTime statDate = DateTime.Today.AddDays(-1);
            string statDatePostgres = CrossDBSQL.ToDate(statDate, CrossDBSQL.Providers.PrvPostgres);

            Exception fileStorageException = null;
            long? fileStorageExceptionId = null;

            Exception dgiDataException = null;
            long? dgiDataExceptionId = null;

            /* Размер директории в файлового хранилища */
            try
            {
                long directorySize = 0;
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.Append("insert into system_daily_stat_file_stor(stat_date, file_key, description, size_mb) VALUES");

                List<string> values = new List<string>(FileStorageManager.FileStorages.FileStorages.Count);
                foreach (var fileStorage in FileStorageManager.FileStorages.FileStorages)
                {
                    if (FileSystemHelper.TryGetDirectoryFullSize(fileStorage.Path, ref directorySize))
                    {
                        directorySize = directorySize / 1024 / 1024;
                        values.Add($"({statDatePostgres}, '{fileStorage.Key}', '{fileStorage.Description}', {directorySize})");
                    }
                }

                sbInsert.Append(string.Join(",\r\n", values));
                sbInsert.Append(";");

                DbCommand commandInsert = DBMngr.Main.GetSqlStringCommand(sbInsert.ToString());
                DBMngr.Main.ExecuteNonQuery(commandInsert);
            }
            catch (Exception ex)
            {
                fileStorageException = ex;
                fileStorageExceptionId = ErrorManager.LogError(ex);
            }

            if (fileStorageException != null || dgiDataException != null)
            {
                throw ExceptionInitializer.Create("Ошибка формирования системной статистики",
                    $"Подробно в журнале: ошибка статистики размера хранилищ - {fileStorageExceptionId}; ошибка статистики загрузки объектов БТИ и ЕГРН - {dgiDataExceptionId};",
                    fileStorageException != null ? fileStorageException : dgiDataException);
            }
        }

        public bool Test() => true;

    }
}
