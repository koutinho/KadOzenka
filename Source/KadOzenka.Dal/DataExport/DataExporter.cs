using Core.Main.FileStorages;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

namespace KadOzenka.Dal.DataExport
{
    public class DataExporter : ILongProcess
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

			export.Status = 1;
			export.DateStarted = DateTime.Now;
			export.Save();

			// Запустить формирование файла
			var templateFile = FileStorageManager.GetFileStream(FileStorageName, export.DateCreated, GetTemplateName(export.Id));

			ExcelFile excelTemplate = null; // TODO: получить из Stream
			List<DataExportColumn> columns = null; // TODO: получить из JSON export.ColumnsMapping;
			Stream resultFile = ExportDataToExcel((int)export.MainRegisterId, excelTemplate, columns);

			// Сохранение файла
			export.Status = 2;
			export.DateFinished = DateTime.Now;
			export.Save();

			FileStorageManager.Save(resultFile, FileStorageName, export.DateFinished.Value, GetResultFileName(export.Id));
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
			export.ResultMessage = $"ex.Message (журнал № {errorId})";
			export.Save();
		}
		
		public bool Test()
		{
			return true;
		}

		public static void AddExportToQueue(long mainRegisterId, string registerViewId, string templateFileName, Stream templateFile, List<DataExportColumn> columns)
		{
			var export = new OMExportByTemplates
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status = 0, // TODO: доработать платформу, чтоб формировался Enum
				TemplateFileName = templateFileName,
				ColumnsMapping = "", // columns -> сериализовать в JSON
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

			List<string> keyValues = new List<string>();

			while (true)
			{
				for(int i = packageNum * packageSize; i < (packageNum + 1) * packageSize; i++)
				{
					if(i == mainWorkSheet.Rows.Count)
					{
						isFinish = true;
						break;
					}

					keyValues.Add(mainWorkSheet.Rows[i].Cells[0].Value.ToString());
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
					if (i == mainWorkSheet.Rows.Count)
					{
						break;
					}

					string keyValue = mainWorkSheet.Rows[i].Cells[0].Value.ToString();

					foreach (var column in columns)
					{
						DataRow row = dt.FilteringAndSortingTable($"10005400 = '{keyValue}'").Rows[0];

						// Заполнение данных в Excel
						var attributeType = RegisterCache.GetAttributeData(10007100).Type;

						switch (attributeType)
						{
							case RegisterAttributeType.INTEGER:
								mainWorkSheet.Rows[i].Cells[2].SetValue(row["10007100"].ParseToLong());
								break;
							case RegisterAttributeType.DECIMAL:
								mainWorkSheet.Rows[i].Cells[2].SetValue(row["10007100"].ParseToDouble());
								break;
							case RegisterAttributeType.BOOLEAN:
								break;
							case RegisterAttributeType.STRING:
								break;
							case RegisterAttributeType.DATE:
								break;
							default:
								throw new Exception($"Не поддерживаемый тип: {attributeType}");
						}
					}
				}

				if (isFinish) break;

				packageNum++;
			}
			
			return excelTemplate;
		}
	}
}
