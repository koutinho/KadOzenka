using Core.Main.FileStorages;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using Core.Messages;

namespace KadOzenka.Dal.DataExport
{
    public class DataExporterCommon : ILongProcess
    {
		public const string LongProcessName = "DataExporterByTemplate";

		public const string FileStorageName = "DataExporterByTemplate";

		private static string GetTemplateName(long exportDataId)
		{
			return $"{exportDataId}_Template";
		}

		private static string GetResultFileName(long exportDataId)
		{
			return $"{exportDataId}_Result";
		}
		
		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			if (!processQueue.ObjectId.HasValue)
			{
				return;
			}

			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
				return;
			}

            WorkerCommon.SetProgress(processQueue, 0);

            export.Status = 1;
			export.DateStarted = DateTime.Now;
			export.Save();

			// Запустить формирование файла
			var templateFile = FileStorageManager.GetFileStream(FileStorageName, export.DateCreated, GetTemplateName(export.Id));
			
			ExcelFile excelTemplate = ExcelFile.Load(templateFile, LoadOptions.XlsxDefault);
			List<DataExportColumn> columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(export.ColumnsMapping);

            WorkerCommon.SetProgress(processQueue, 25);

            Stream resultFile = ExportDataToExcel((int)export.MainRegisterId, excelTemplate, columns);

            WorkerCommon.SetProgress(processQueue, 75);

            // Сохранение файла
            export.Status = 2;
			export.DateFinished = DateTime.Now;
			export.Save();

			FileStorageManager.Save(resultFile, FileStorageName, export.DateFinished.Value, GetResultFileName(export.Id));

            SendResultNotification(export);

            WorkerCommon.SetProgress(processQueue, 100);
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == objectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
				return;
			}

			export.Status = 3;
			export.DateFinished = DateTime.Now;
			export.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : String.Empty)}";
			export.Save();
		}
		
		public bool Test()
		{
			return true;
		}

		public static void AddExportToQueue(long mainRegisterId, string registerViewId, string templateFileName, Stream templateFile, List<DataExportColumn> columns)
		{
			string jsonstring = JsonConvert.SerializeObject(columns);

			var export = new OMExportByTemplates
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status = 0, // TODO: доработать платформу, чтоб формировался Enum
				TemplateFileName = templateFileName,
				ColumnsMapping = jsonstring, 
				MainRegisterId = mainRegisterId,
				RegisterViewId = registerViewId				
			};
			export.Save();

			FileStorageManager.Save(templateFile, FileStorageName, export.DateCreated, GetTemplateName(export.Id));
			
            LongProcessManager.AddTaskToQueue(LongProcessName, OMExportByTemplates.GetRegisterId(), export.Id);
        }
		
		public static Stream ExportDataToExcel(int mainRegisterId, ExcelFile excelTemplate, List<DataExportColumn> columns)
		{
			// Получить значение из ключевой колонки пакетами по 1000
			int packageNum = 0;
			int packageSize = 1000;
			var mainWorkSheet = excelTemplate.Worksheets[0];
			bool isFinish = false;

			if (mainWorkSheet.Rows.Count <= 1)  //файл пустой или в нем есть только заголовок
			{
				throw new Exception("В указанном файле отсутствуют данные");
			}
			if (!columns.Any(x => x.IsKey))
			{
				throw new Exception("Не указана ни одна ключевая колонка");
			}

			int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();

			List<string> columnNames = new List<string>();
			for (int i = 0; i < maxColumns; i++)
			{
				columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
			}
			
			List<string> keyValues = new List<string>();			

			while (!isFinish)
			{
				for (int i = packageNum * packageSize; i < (packageNum + 1) * packageSize; i++)		
				{
					if (i == mainWorkSheet.Rows.Count - 1) //одна строка - заголовок
					{
						isFinish = true;
						break;
					}

					keyValues.Add(mainWorkSheet.Rows[i + 1].Cells[0].Value?.ToString()); 
				}
				
				// Получение данных для 1000 строк
				QSQuery query = new QSQuery
				{
					MainRegisterID = mainRegisterId,
					Columns = columns.Select(x => (QSColumn)new QSColumnSimple((int)x.AttributrId)).ToList(),
					Condition = new QSConditionSimple
					{
						ConditionType = QSConditionType.In,
						LeftOperand = new QSColumnSimple((int)columns.FirstOrDefault(x => x.IsKey).AttributrId),
						RightOperand = new QSColumnConstant(keyValues)
					}
				};

				DataTable dt = query.ExecuteQuery();

				for (int i = packageNum * packageSize; i < (packageNum + 1) * packageSize; i++)
				{
					if (i == mainWorkSheet.Rows.Count - 1)
					{
						break;
					}

					string keyValue = mainWorkSheet.Rows[i + 1].Cells[0].Value?.ToString();

					//считаем, что ключевая колонка только одна
					DataExportColumn key = columns.Where(x => x.IsKey).FirstOrDefault();

					DataTable filtredTable = dt.FilteringAndSortingTable($"[{key.AttributrId}] = '{keyValue}'");
					if (filtredTable.Rows.Count == 0)
					{
						continue;
					}

					DataRow row = filtredTable.Rows[0];    

					foreach (var column in columns)
					{						
						if (column.IsKey)
						{
							continue;
						}						

						// Заполнение данных в Excel
						
						int cell = columnNames.IndexOf(column.ColumnName);
						string dtColumnName = column.AttributrId.ToString();

						var attributeType = RegisterCache.GetAttributeData(column.AttributrId.ParseToInt()).Type;

						switch (attributeType)
						{
							case RegisterAttributeType.INTEGER:
								mainWorkSheet.Rows[i + 1].Cells[cell].SetValue(row[dtColumnName].ParseToLong());
								break;
							case RegisterAttributeType.DECIMAL:
								mainWorkSheet.Rows[i + 1].Cells[cell].SetValue(row[dtColumnName].ParseToDouble());
								break;
							case RegisterAttributeType.BOOLEAN:
								string value = row[dtColumnName].ParseToBoolean() == true ? "Да" : "Нет";
								mainWorkSheet.Rows[i + 1].Cells[cell].SetValue(value);
								break;
							case RegisterAttributeType.STRING:
								mainWorkSheet.Rows[i + 1].Cells[cell].SetValue(row[dtColumnName].ToString());
								break;
							case RegisterAttributeType.DATE:
								mainWorkSheet.Rows[i + 1].Cells[cell].SetValue(row[dtColumnName].ParseToDateTime());
								break;
							default:
								throw new Exception($"Неподдерживаемый тип: {attributeType}");
						}
					}
				}

				packageNum++;
			}

			//return excelTemplate;

			MemoryStream stream = new MemoryStream();
			excelTemplate.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			return stream;
		}


        #region Notification

        public static FileStream GetExportResultFileStream(long exportId)
        {
            var export = OMExportByTemplates
                .Where(x => x.Id == exportId)
                .SelectAll()
                .Execute()
                .FirstOrDefault();

            return FileStorageManager.GetFileStream(FileStorageName, export.DateFinished.Value, GetResultFileName(export.Id));
        }

        internal static void SendResultNotification(OMExportByTemplates export)
        {
            new MessageService().SendMessages(new MessageDto
            {
                Addressers = new MessageAddressersDto { UserIds = new[] { export.UserId } },
                Subject = $"Результаты Выгрузки данных по списку от: {export.DateCreated})",
                Message = $@"Процесс Выгрузки данных по списку завершен. <a href=""/DataExport/DownloadExportData?exportId={export.Id}"">Скачать результаты</a>",
                IsUrgent = true,
                IsEmail = true
            });
        }

        #endregion
    }
}
