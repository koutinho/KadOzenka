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

namespace CIPJS.DAL.SystemStatistics
{
	public class SystemStatisticsManager : ILongProcess
	{
		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			DbCommand command = DBMngr.Realty.GetSqlStringCommand("select fill_system_daily_statistics()");
			DBMngr.Realty.ExecuteNonQuery(command);

			DateTime statDate = DateTime.Today.AddDays(-1);
			string statDateOracle = CrossDBSQL.ToDate(statDate, CrossDBSQL.Providers.PrvOracle);
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

                DbCommand commandInsert = DBMngr.Realty.GetSqlStringCommand(sbInsert.ToString());
                DBMngr.Realty.ExecuteNonQuery(commandInsert);
            }
			catch(Exception ex)
			{
				fileStorageException = ex;
				fileStorageExceptionId = ErrorManager.LogError(ex);
			}

			// Получение данных из БД ДГИ (Oracle)
			int attempts = 0;

			while(attempts < 4)
			{
				try
				{
					// BTI_OBJECTS_LOADED
					command = CipjsDbManager.Dgi.GetSqlStringCommand($@"select count(1) as cnt
						from izk_rsm.cipjs_import_bti_daily_log t
					   where t.import_date >= {statDateOracle}
						 and t.import_date < {statDateOracle} + 1
						 and t.is_error = 0");
					int BTI_OBJECTS_LOADED = CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0].Rows[0]["cnt"].ParseToInt();

					// BTI_OBJECTS_LOADED_ERROR
					command = CipjsDbManager.Dgi.GetSqlStringCommand($@"select count(1) as cnt
						from izk_rsm.cipjs_import_bti_daily_log t
					   where t.import_date >= {statDateOracle}
						 and t.import_date < {statDateOracle} + 1
						 and t.is_error = 1");
					int BTI_OBJECTS_LOADED_ERROR = CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0].Rows[0]["cnt"].ParseToInt();

					// EHD_OBJECTS_LOADED
					command = CipjsDbManager.Dgi.GetSqlStringCommand($@"select count(1) as cnt
						from izk_rsm.cipjs_import_building_parcel t
					   where t.import_date >= {statDateOracle}
						 and t.import_date < {statDateOracle} + 1
						 and t.is_error = 0");
					int EHD_OBJECTS_LOADED = CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0].Rows[0]["cnt"].ParseToInt();

					// EHD_OBJECTS_LOADED_ERROR
					command = CipjsDbManager.Dgi.GetSqlStringCommand($@"select count(1) as cnt
						from izk_rsm.cipjs_import_building_parcel t
					   where t.import_date >= {statDateOracle}
						 and t.import_date < {statDateOracle} + 1
						 and t.is_error = 1");
					int EHD_OBJECTS_LOADED_ERROR = CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0].Rows[0]["cnt"].ParseToInt();
					
					command = DBMngr.Realty.GetSqlStringCommand($@"update SYSTEM_DAILY_STATISTICS set 
							BTI_OBJECTS_LOADED = {BTI_OBJECTS_LOADED},
							BTI_OBJECTS_LOADED_ERROR = {BTI_OBJECTS_LOADED_ERROR},
							EHD_OBJECTS_LOADED = {EHD_OBJECTS_LOADED},
							EHD_OBJECTS_LOADED_ERROR = {EHD_OBJECTS_LOADED_ERROR}
						where STAT_DATE = {statDatePostgres}");
					DBMngr.Realty.ExecuteNonQuery(command);

					dgiDataException = null;
					dgiDataExceptionId = null;

					break;
				}
				catch(Exception ex)
				{
					dgiDataException = ex;
					dgiDataExceptionId = ErrorManager.LogError(ex);
				}

				attempts++;
				Thread.Sleep(2000);
			}

			if(fileStorageException != null || dgiDataException != null)
			{
				throw ExceptionInitializer.Create("Ошибка формирования системной статистики", 
					$"Подробно в журнале: ошибка статистики размера хранилищ - {fileStorageExceptionId}; ошибка статистики загрузки объектов БТИ и ЕГРН - {dgiDataExceptionId};", 
					fileStorageException != null ? fileStorageException : dgiDataException);
			}
		}

        public void LogError(long? objectId, Exception ex, long? errorId = null) { }

		public bool Test()
		{
			return true;
		}
	}
}
