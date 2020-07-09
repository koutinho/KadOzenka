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
using System.IO;
using System.Linq;
using System.Threading;

namespace KadOzenka.Dal.DataExport
{
	public class DataExporterByTemplate : ILongProcess
	{
		public const string LongProcessName = "DataExporterByTemplate";

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
			var templateFile = FileStorageManager.GetFileStream(DataExporterCommon.FileStorageName, export.DateCreated, export.TemplateFileName);

			ExcelFile excelTemplate = ExcelFile.Load(templateFile, LoadOptions.XlsxDefault);
			List<DataExportColumn> columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(export.ColumnsMapping);

			WorkerCommon.SetProgress(processQueue, 25);
			Stream resultFile = ExportDataToExcel((int)export.MainRegisterId, excelTemplate, columns);
			WorkerCommon.SetProgress(processQueue, 75);

			// Сохранение файла
			export.Status = 2;
			export.DateFinished = DateTime.Now;
			export.ResultFileName = DataExporterCommon.GetStorageResultFileName(export.Id);
			export.FileResultTitle = GetFileResultTitleFromTemplateTitle(export.FileTemplateTitle);

			FileStorageManager.Save(resultFile, DataExporterCommon.FileStorageName, export.DateFinished.Value, export.ResultFileName);
			export.Save();

			DataExporterCommon.SendResultNotification(export);
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

		public static long AddExportToQueue(long mainRegisterId, string registerViewId, string fileName, Stream templateFile, List<DataExportColumn> columns)
		{
			string jsonstring = JsonConvert.SerializeObject(columns);

			var export = new OMExportByTemplates
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status = 0, // TODO: доработать платформу, чтоб формировался Enum
				FileTemplateTitle = GetFileTemplateTitle(fileName),
				FileExtension = "xlsx",
				ColumnsMapping = jsonstring,
				MainRegisterId = mainRegisterId,
				RegisterViewId = registerViewId
			};
			export.Save();

			export.TemplateFileName  = DataExporterCommon.GetStorageTemplateFileName(export.Id);
			FileStorageManager.Save(templateFile, DataExporterCommon.FileStorageName, export.DateCreated, export.TemplateFileName);
			
			export.Save();

			LongProcessManager.AddTaskToQueue(LongProcessName, OMExportByTemplates.GetRegisterId(), export.Id);

			return export.Id;
		}

		private static string GetFileTemplateTitle(string fileName)
		{
			return Path.GetFileNameWithoutExtension(fileName);
		}

		private static string GetFileResultTitleFromTemplateTitle(string fileTemplateTitle)
		{
			return $"{fileTemplateTitle}_Result";
		}


		public static Stream ExportDataToExcel(int mainRegisterId, ExcelFile excelTemplate, List<DataExportColumn> columns)
		{
			// Получить значение из ключевой колонки пакетами по 1000
			int packageNum = 0;
			int packageSize = 1000;
			var mainWorkSheet = excelTemplate.Worksheets[0];
			bool isFinish = false;

			if (mainWorkSheet.Rows.Count <= 1)  //файл пустой или в нем есть только заголовок
				throw new Exception("В указанном файле отсутствуют данные");
			if (!columns.Any(x => x.IsKey))
				throw new Exception("Не указана ни одна ключевая колонка");

			var columnNames = GetAllColumnNames(mainWorkSheet);

			//считаем, что ключевая колонка только одна
			var keyColumn = columns.First(x => x.IsKey);
			var keyColumnCellPosition = columnNames.IndexOf(keyColumn.ColumnName);

			while (!isFinish)
			{
				var keyValues = new List<string>();
				for (int i = packageNum * packageSize; i < (packageNum + 1) * packageSize; i++)
				{
					if (i == mainWorkSheet.Rows.Count - 1) //одна строка - заголовок
					{
						isFinish = true;
						break;
					}

					keyValues.Add(GetCellValue(mainWorkSheet, i + 1, keyColumnCellPosition));
				}

				// Получение данных для 1000 строк
				var query = new QSQuery
				{
					MainRegisterID = mainRegisterId,
					Columns = columns.Select(x => (QSColumn)new QSColumnSimple((int)x.AttributrId)).ToList(),
					Condition = new QSConditionSimple
					{
						ConditionType = QSConditionType.In,
						LeftOperand = new QSColumnSimple(keyColumn.AttributrId),
						RightOperand = new QSColumnConstant(keyValues)
					}
				};

				var dt = query.ExecuteQuery();

				for (int i = packageNum * packageSize; i < (packageNum + 1) * packageSize; i++)
				{
					if (i == mainWorkSheet.Rows.Count - 1)
						break;

					var keyValue = GetCellValue(mainWorkSheet, i + 1, keyColumnCellPosition);
					var filteredTable = dt.FilteringAndSortingTable($"[{keyColumn.AttributrId}] = '{keyValue}'");
					if (filteredTable.Rows.Count == 0)
						continue;

					var row = filteredTable.Rows[0];

					foreach (var column in columns)
					{
						if (column.IsKey)
							continue;

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

			MemoryStream stream = new MemoryStream();
			excelTemplate.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			return stream;
		}



		#region Support Methods

		private static List<string> GetAllColumnNames(ExcelWorksheet mainWorkSheet)
		{
			var columnNames = new List<string>();
			var maxColumns = mainWorkSheet.CalculateMaxUsedColumns();
			for (var i = 0; i < maxColumns; i++)
			{
				columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
			}

			return columnNames;
		}

		private static string GetCellValue(ExcelWorksheet sheet, int row, int cell)
		{
			return sheet.Rows[row].Cells[cell].Value?.ToString();
		}

		#endregion
	}
}
