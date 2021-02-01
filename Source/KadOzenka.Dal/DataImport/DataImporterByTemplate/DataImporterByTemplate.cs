using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using ObjectModel.Core.Shared;

namespace KadOzenka.Dal.DataImport.DataImporterByTemplate
{
	public class DataImporterByTemplate
	{
		public int MainRegisterId { get; }

		public DataImporterByTemplate(int mainRegisterId)
		{
			MainRegisterId = mainRegisterId;
		}

		public void ValidateColumns(List<DataExportColumn> columns)
		{
			if (columns.All(x => x.IsKey))
			{
				throw new Exception("Не указаны неключевые поля");
			}
		}

		public DataImportResult ImportDataFromExcel(ExcelFile excelFile, List<DataExportColumn> columns, long? documentId = null)
		{
			var mainWorkSheet = excelFile.Worksheets[0];
			var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
			var usedRowCount = lastUsedRowIndex + 1;
			if (usedRowCount <= 1)  //файл пустой или в нем есть только заголовок
				throw new Exception("В указанном файле отсутствуют данные");

			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 100
			};
			int maxColumns = DataExportCommon.GetLastUsedColumnIndex(mainWorkSheet) + 1;
			mainWorkSheet.Rows[0].Cells[maxColumns].SetValue("Результат обработки");
			mainWorkSheet.Rows[0].Cells[maxColumns + 1].SetValue("Создание объекта");
			List<string> columnNames = new List<string>();
			for (int i = 0; i < maxColumns; i++)
			{
				if (mainWorkSheet.Rows[0].Cells[i].Value != null)
					columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
			}

			var importStatus = Process(columns, mainWorkSheet, options, lastUsedRowIndex, maxColumns, columnNames, MainRegisterId,
				documentId);

			MemoryStream stream = new MemoryStream();
			excelFile.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			return new DataImportResult(stream, importStatus);
		}

		protected virtual DataImportStatus Process(List<DataExportColumn> columns, ExcelWorksheet mainWorkSheet,
			ParallelOptions options, int lastUsedRowIndex, int maxColumns, List<string> columnNames, int mainRegisterId,
			long? documentId)
		{
			object locked = new object();
			var successLoadedObjectsCount = 0;
			Parallel.ForEach(mainWorkSheet.Rows, options, row =>
			{
				try
				{
					if (row.Index != 0 && row.Index <= lastUsedRowIndex) //все, кроме заголовков и пустых строк в конце страницы
					{
						// Найти ИД объекта по ключевым полям					
						List<QSCondition> conditions = new List<QSCondition>();
						long objectId = -1;
						if (columns.Any(x => x.IsKey))
						{
							foreach (var keyColumn in columns.Where(x => x.IsKey))
							{
								int index = columnNames.IndexOf(keyColumn.ColumnName);
								object rowValue = row.Cells[index].Value.ToString();
								QSCondition con1 = new QSConditionSimple
								{
									ConditionType = QSConditionType.Equal,
									LeftOperand = new QSColumnSimple((int)keyColumn.AttributrId),
									RightOperand = new QSColumnConstant(rowValue)
								};
								conditions.Add(con1);
							}
							QSQuery query = new QSQuery
							{
								MainRegisterID = mainRegisterId,
								Condition = new QSConditionGroup
								{
									Type = QSConditionGroupType.And,
									Conditions = conditions
								}
							};
							DataTable dt = query.ExecuteQuery();
							if (dt.Rows.Count >= 1) objectId = dt.Rows[0]["ID"].ParseToLong();
						}
						RegisterObject registerObject = new RegisterObject((int)mainRegisterId, (int)objectId);
						foreach (var column in columns.Where(y => !y.IsKey))
						{
							int cell = columnNames.IndexOf(column.ColumnName);
							object value = mainWorkSheet.Rows[row.Index].Cells[cell].Value;
							var attributeData = RegisterCache.GetAttributeData((int)column.AttributrId);
							int referenceItemId = -1;
							if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
							{
								OMReferenceItem item = OMReferenceItem.Where(x => x.ReferenceId == attributeData.ReferenceId && x.Value == (string)value).ExecuteFirstOrDefault();
								if (item != null) referenceItemId = (int)item.ItemId;
							}
							switch (attributeData.Type)
							{
								case RegisterAttributeType.INTEGER:
									value = value.ParseToLongNullable();
									break;
								case RegisterAttributeType.DECIMAL:
									value = value.ParseToDecimalNullable();
									break;
								case RegisterAttributeType.BOOLEAN:
									value = value.ParseToBooleanNullable();
									break;
								case RegisterAttributeType.STRING:
									value = value == null ? "" : value.ToString();
									break;
								case RegisterAttributeType.DATE:
									value = value.ParseToDateTimeNullable();
									break;
							}
							registerObject.SetAttributeValue((int)column.AttributrId, value, referenceItemId);
						}
						RegisterStorage.Save(registerObject);
						mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Успешно");
						if (objectId == -1)
							mainWorkSheet.Rows[row.Index].Cells[maxColumns + 1].SetValue("Объект создан");
						lock (locked)
						{
							successLoadedObjectsCount++;
						}
					}
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"{ex.Message} (подробно в журнале №{errorId})");
				}
			});

			return successLoadedObjectsCount > 0
				? successLoadedObjectsCount == lastUsedRowIndex
					? DataImportStatus.Success
					: DataImportStatus.PartiallyLoaded
				: DataImportStatus.Failed;
		}
	}
}
