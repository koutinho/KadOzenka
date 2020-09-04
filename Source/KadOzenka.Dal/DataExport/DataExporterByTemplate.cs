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
using System.Text;
using System.Threading;

namespace KadOzenka.Dal.DataExport
{
	public class DataExporterByTemplate : ILongProcess
	{
		public const string LongProcessName = "DataExporterByTemplate";

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			if (!processQueue.ObjectId.HasValue)
                return;

            OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
                return;

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

        public virtual void ValidateColumns(List<DataExportColumn> columns)
        {
            if (columns.All(x => x.IsKey == false))
            {
                throw new Exception("Должен быть выбран хотя бы один ключевой параметр");
            }

            if (columns.Count(x => x.IsKey) > 1)
            {
                throw new Exception("Должен быть выбран только один ключевой параметр");
            }
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

		public long AddExportToQueue(long mainRegisterId, string registerViewId, string fileName, Stream templateFile, List<DataExportColumn> columns)
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

        public virtual Stream ExportDataToExcel(int mainRegisterId, ExcelFile excelTemplate, List<DataExportColumn> columns)
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
            var keyColumns = columns.Where(x => x.IsKey).ToList();

			while (!isFinish)
			{
                var conditions = new List<QSCondition>();
                keyColumns.ForEach(keyColumn =>
                {
                    var keyValues = new List<string>();
                    for (int i = packageNum * packageSize; i < (packageNum + 1) * packageSize; i++)
                    {
                        if (i == mainWorkSheet.Rows.Count - 1) //одна строка - заголовок
                        {
                            isFinish = true;
                            break;
                        }

                        var keyColumnCellPosition = columnNames.IndexOf(keyColumn.ColumnName);
                        var keyColumnValue = GetCellValue(mainWorkSheet, i + 1, keyColumnCellPosition);
                        keyValues.Add(keyColumnValue);
                    }

                    conditions.Add(new QSConditionSimple
                    {
                        ConditionType = QSConditionType.In,
                        LeftOperand = new QSColumnSimple(keyColumn.AttributrId),
                        RightOperand = new QSColumnConstant(keyValues)
                    });
                });

                // Получение данных для 1000 строк
                var query = new QSQuery
				{
					MainRegisterID = mainRegisterId,
					Columns = columns.Select(x => (QSColumn)new QSColumnSimple((int)x.AttributrId)).ToList(),
                    Condition = new QSConditionGroup
                    {
                        Type = QSConditionGroupType.And,
                        Conditions = conditions
                    }
                };

                var dt = query.ExecuteQuery();

                for (int rowInFileIndex = packageNum * packageSize;
                    rowInFileIndex < (packageNum + 1) * packageSize;
                    rowInFileIndex++)
                {
                    if (rowInFileIndex == mainWorkSheet.Rows.Count - 1)
                        break;

                    var filteredTable = GetDataForCurrentRowInFile(keyColumns, columnNames, mainWorkSheet, rowInFileIndex, dt);
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
                                mainWorkSheet.Rows[rowInFileIndex + 1].Cells[cell].SetValue(row[dtColumnName].ParseToLong());
                                break;
                            case RegisterAttributeType.DECIMAL:
                                mainWorkSheet.Rows[rowInFileIndex + 1].Cells[cell].SetValue(row[dtColumnName].ParseToDouble());
                                break;
                            case RegisterAttributeType.BOOLEAN:
                                string value = row[dtColumnName].ParseToBoolean() == true ? "Да" : "Нет";
                                mainWorkSheet.Rows[rowInFileIndex + 1].Cells[cell].SetValue(value);
                                break;
                            case RegisterAttributeType.STRING:
                                mainWorkSheet.Rows[rowInFileIndex + 1].Cells[cell].SetValue(row[dtColumnName].ToString());
                                break;
                            case RegisterAttributeType.DATE:
                                var date = row[dtColumnName].ParseToDateTimeNullable();
                                if (date == null)
                                {
                                    mainWorkSheet.Rows[rowInFileIndex + 1].Cells[cell].SetValue(string.Empty);
                                }
                                else
                                {
                                    mainWorkSheet.Rows[rowInFileIndex + 1].Cells[cell].SetValue(date.Value);
                                }
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

        private static DataTable GetDataForCurrentRowInFile(List<DataExportColumn> keyColumns, List<string> columnNames, ExcelWorksheet mainWorkSheet,
            int rowInFileIndex, DataTable dt)
        {
            var searchExpression = new StringBuilder();
            foreach (var keyColumn in keyColumns)
            {
                var keyCellPosition = columnNames.IndexOf(keyColumn.ColumnName);
                var keyValue = GetCellValue(mainWorkSheet, rowInFileIndex + 1, keyCellPosition);

                if (searchExpression.Length != 0)
                    searchExpression.Append(" and ");

                searchExpression.Append($"[{keyColumn.AttributrId}] = '{keyValue}'");
            }

            return dt.FilteringAndSortingTable(expression: searchExpression.ToString());
        }


        #region Support Methods

		protected static List<string> GetAllColumnNames(ExcelWorksheet mainWorkSheet)
		{
			var columnNames = new List<string>();
			var maxColumns = mainWorkSheet.CalculateMaxUsedColumns();
			for (var i = 0; i < maxColumns; i++)
			{
                var value = GetCellValue(mainWorkSheet, 0, i);
                columnNames.Add(value);
            }

			return columnNames;
		}

        protected static string GetCellValue(ExcelWorksheet sheet, int row, int cell)
        {
            var value = sheet.Rows[row].Cells[cell].Value?.ToString();
            if (!string.IsNullOrWhiteSpace(value) && decimal.TryParse(value, out _))
            {
                value = value.Replace(",", ".");
            }

            return value;
		}

        protected static string GetFileTemplateTitle(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }

        protected static string GetFileResultTitleFromTemplateTitle(string fileTemplateTitle)
        {
            return $"{fileTemplateTitle}_Result";
        }

        #endregion
    }
}
