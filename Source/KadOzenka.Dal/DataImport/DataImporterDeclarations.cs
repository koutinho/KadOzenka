using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
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
using ObjectModel.Core.Shared;
using ObjectModel.Declarations;
using System.Data;
using Core.RefLib;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport.Validation;

namespace KadOzenka.Dal.DataImport
{
	public class DataImporterDeclarations : ILongProcess
	{
		public static string LongProcessName => "DataImporterDeclarations";

		public static void AddImportToQueue(long mainRegisterId, string registerViewId, string templateFileName, Stream templateFile, List<DataExportColumn> columns)
		{
			var export = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
				DataFileName = templateFileName,
				ColumnsMapping = JsonConvert.SerializeObject(columns),
				MainRegisterId = mainRegisterId,
				RegisterViewId = registerViewId
			};
			export.Save();

			FileStorageManager.Save(templateFile, DataImporterCommon.FileStorageName, export.DateCreated, DataImporterCommon.GetTemplateName(export.Id));
			LongProcessManager.AddTaskToQueue(LongProcessName, OMExportByTemplates.GetRegisterId(), export.Id);
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == objectId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (import == null)
			{
				return;
			}

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
			import.DateFinished = DateTime.Now;
			import.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : String.Empty)}";
			import.Save();
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			if (!processQueue.ObjectId.HasValue)
			{
				return;
			}

			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (import == null)
			{
				return;
			}

            WorkerCommon.SetProgress(processQueue, 0);

            import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
			import.DateStarted = DateTime.Now;
			import.Save();

			// Запустить формирование файла
			var templateFile = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, DataImporterCommon.GetTemplateName(import.Id));

			ExcelFile excelTemplate = ExcelFile.Load(templateFile, LoadOptions.XlsxDefault);
			var columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(import.ColumnsMapping);

            WorkerCommon.SetProgress(processQueue, 25);

            Stream resultFile = ImportDataFromExcel(excelTemplate, columns);

            WorkerCommon.SetProgress(processQueue, 75);

            // Сохранение файла
            FileStorageManager.Save(resultFile, DataImporterCommon.FileStorageName, import.DateCreated, DataImporterCommon.GetResultFileName(import.Id));

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
			import.DateFinished = DateTime.Now;
			import.Save();

			// Отправка уведомления о завершении загрузки
			DataImporterCommon.SendResultNotification(import);

            WorkerCommon.SetProgress(processQueue, 100);
        }

		public bool Test()
		{
			return true;
		}

		public static Stream ImportDataFromExcel(ExcelFile excelFile, List<DataExportColumn> columns)
		{
			var mainWorkSheet = excelFile.Worksheets[0];
			var areKeyColumnsIncluded = columns.Any(x => x.IsKey);

			var cancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 10
			};
			int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();

			mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат сохранения");

			var columnNames = new List<string>();
			for (var i = 0; i < maxColumns; i++)
			{
				columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
			}

			Parallel.ForEach(mainWorkSheet.Rows, options, row =>
			{
				try
				{
					if (row.Index != 0) //все, кроме заголовков
					{
						if (areKeyColumnsIncluded)
						{
							var dt = FindObjectsByKeysColumns(columns.Where(x => x.IsKey).ToList(), columnNames, row);
							if (dt.Rows.Count == 1)
							{
								var objectId = dt.Rows[0]["ID"].ParseToLong();
								ProcessObject(columns, columnNames, mainWorkSheet, row, maxColumns, objectId);
							}
							else if (dt.Rows.Count == 0)
							{
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"Не успешно: объект с ключевыми признаками не найден в БД");
							}
							else
							{
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"Не успешно: в БД найдено несколько объектов по ключевым признакам");
							}
						}
						else
						{
							ProcessObject(columns, columnNames, mainWorkSheet, row, maxColumns);
						}
					}
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"{ex.Message} (подробно в журнале №{errorId})");
				}
			});

			MemoryStream stream = new MemoryStream();
			excelFile.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			return stream;
		}

		private static DataTable FindObjectsByKeysColumns(List<DataExportColumn> columns, List<string> columnNames, ExcelRow row)
		{
			var conditions = new List<QSCondition>();
			foreach (var keyColumn in columns)
			{
				int index = columnNames.IndexOf(keyColumn.ColumnName);
				object rowValue = row.Cells[index].Value?.ToString();
				QSCondition condition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = new QSColumnSimple((int) keyColumn.AttributrId),
					RightOperand = new QSColumnConstant(rowValue)
				};
				conditions.Add(condition);
			}

			QSQuery query = new QSQuery
			{
				MainRegisterID = OMDeclaration.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = conditions
				}
			};
			DataTable dt = query.ExecuteQuery();
			return dt;
		}

		private static void ProcessObject(List<DataExportColumn> columns, List<string> columnNames, ExcelWorksheet mainWorkSheet, ExcelRow row,
			int maxColumns, long? objectId = null)
		{
			var isNewObject = !objectId.HasValue;
			if (!isNewObject)
			{
				columns = columns.Where(y => !y.IsKey).ToList();
			}

			var registerObject = new RegisterObject(OMDeclaration.GetRegisterId(), (int)objectId.GetValueOrDefault(-1));
			foreach (var column in columns)
			{
				var parseResult = HandleColumnData(columnNames, column, mainWorkSheet, row, registerObject);
				if (!parseResult)
				{
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"Не успешно: не удалось определить значение в столбце {column.ColumnName}");
					return;
				};
			}
			if (isNewObject)
			{
				SetAutoFilledProperties(registerObject);
			}

			var declaration = !isNewObject
				? OMDeclaration
					.Where(x => x.Id == objectId)
					.SelectAll()
					.ExecuteFirstOrDefault()
				: null;

			var validationResult = isNewObject
				? DataImporterDeclarationsValidator.ValidateDeclarationCreationFromRegisterObject(registerObject)
				: DataImporterDeclarationsValidator.ValidateDeclarationModificationFromRegisterObject(registerObject, declaration);
			if (validationResult.Count == 0)
			{
				RegisterStorage.Save(registerObject);
				mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue(!isNewObject ? "Объект успешно обновлен" : "Объект успешно создан");
			}
			else
			{
				mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"Не успешно: {string.Join(", ",validationResult)}");
			}
		}

		private static bool HandleColumnData(List<string> columnNames, DataExportColumn column, ExcelWorksheet mainWorkSheet,
			ExcelRow row, RegisterObject registerObject)
		{
			int cell = columnNames.IndexOf(column.ColumnName);
			object value = mainWorkSheet.Rows[row.Index].Cells[cell].Value;

			var attributeData = RegisterCache.GetAttributeData((int)column.AttributrId);
			int referenceItemId = -1;

			if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
			{
				OMReference reference =
					OMReference.Where(x => x.ReferenceId == attributeData.ReferenceId).ExecuteFirstOrDefault();
				var items = ReferencesCommon.GetItems(reference.ReferenceId.ParseToLong());
				var valueStr = value != null ? value.ToString().Trim() : string.Empty;
				OMReferenceItem item = items.FirstOrDefault(x => x.Value == valueStr || x.Code == valueStr);
				if (item == null && !string.IsNullOrEmpty(valueStr))
				{
					return false;
				}
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
					value = value.ParseToStringNullable();
					break;
				case RegisterAttributeType.DATE:
					value = value.ParseToDateTimeNullable();
					break;
			}

			registerObject.SetAttributeValue((int)column.AttributrId, value, referenceItemId);
			return true;
		}

		private static void SetAutoFilledProperties(RegisterObject registerObject)
		{
			var attrValues = registerObject.AttributesValues;
			registerObject.SetAttributeValue(OMDeclaration.GetAttributeData(x => x.UserReg_Id).Id, SRDSession.GetCurrentUserId(), -1);
			registerObject.SetAttributeValue(OMDeclaration.GetAttributeData(x => x.UserIsp_Id).Id, SRDSession.GetCurrentUserId(), -1);
		}
	}
}
