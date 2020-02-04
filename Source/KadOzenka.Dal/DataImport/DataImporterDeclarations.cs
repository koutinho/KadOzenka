using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using ObjectModel.Directory.Declarations;

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

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
			import.DateStarted = DateTime.Now;
			import.Save();

			// Запустить формирование файла
			var templateFile = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, DataImporterCommon.GetTemplateName(import.Id));

			ExcelFile excelTemplate = ExcelFile.Load(templateFile, LoadOptions.XlsxDefault);
			var columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(import.ColumnsMapping);
			Stream resultFile = ImportDataFromExcel(excelTemplate, columns);


			// Сохранение файла
			FileStorageManager.Save(resultFile, DataImporterCommon.FileStorageName, import.DateCreated, DataImporterCommon.GetResultFileName(import.Id));

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
			import.DateFinished = DateTime.Now;
			import.Save();

			// Отправка уведомления о завершении загрузки
			DataImporterCommon.SendResultNotification(import);
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
				object rowValue = row.Cells[index].Value.ToString();
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
				HandleColumnData(columnNames, column, mainWorkSheet, row, registerObject);
			}
			if (isNewObject)
			{
				SetAutoFilledProperties(registerObject);
			}

			var validationResult = ValidateRegisterObject(registerObject);
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

		private static void HandleColumnData(List<string> columnNames, DataExportColumn column, ExcelWorksheet mainWorkSheet,
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
				var valueStr = value != null ? value.ToString() : string.Empty;
				OMReferenceItem item = items.FirstOrDefault(x => x.Value == valueStr);
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
					value = value.ToString();
					break;
				case RegisterAttributeType.DATE:
					value = value.ParseToDateTimeNullable();
					break;
			}

			registerObject.SetAttributeValue((int)column.AttributrId, value, referenceItemId);
		}

		private static void SetAutoFilledProperties(RegisterObject registerObject)
		{
			var attrValues = registerObject.AttributesValues;
			registerObject.SetAttributeValue(OMDeclaration.GetAttributeData(x => x.UserReg_Id).Id, SRDSession.GetCurrentUserId(), -1);
			registerObject.SetAttributeValue(OMDeclaration.GetAttributeData(x => x.UserIsp_Id).Id, SRDSession.GetCurrentUserId(), -1);
		}

		private static List<string> ValidateRegisterObject(RegisterObject registerObject)
		{
			var result = new List<string>();
			var attrValues = registerObject.AttributesValues;

			var bookIdAttr = OMDeclaration.GetAttributeData(x => x.Book_Id);
			var bookId = attrValues.ContainsKey(bookIdAttr.Id) ? attrValues[bookIdAttr.Id].Value.ParseToLongNullable() : (long?) null;
			if (!bookId.HasValue)
			{
				result.Add($"атрибут {bookIdAttr.Name} обязательный");
			}
			else
			{
				var book = OMBook
					.Where(x => x.Id == bookId.Value)
					.SelectAll()
					.ExecuteFirstOrDefault();
				if (book == null)
				{
					result.Add($"книга с указанным ID {bookId.Value.ToString()} не найдена");
				}
			}

			var objTypeAttr = OMDeclaration.GetAttributeData(x => x.TypeObj);
			var objTypeVal = attrValues.ContainsKey(objTypeAttr.Id) ? attrValues[objTypeAttr.Id].Value.ParseToStringNullable() : null;
			if (string.IsNullOrEmpty(objTypeVal))
			{
				result.Add($"атрибут {objTypeAttr.Name} обязательный");
			}

			var numInAttr = OMDeclaration.GetAttributeData(x => x.NumIn);
			var numInVal = attrValues.ContainsKey(numInAttr.Id) ? attrValues[numInAttr.Id].Value.ParseToStringNullable() : null;
			if (string.IsNullOrEmpty(numInVal))
			{
				result.Add($"атрибут {numInAttr.Name} обязательный");
			}

			var cadNumAttr = OMDeclaration.GetAttributeData(x => x.CadastralNumObj);
			var cadNumVal = attrValues.ContainsKey(cadNumAttr.Id) ? attrValues[cadNumAttr.Id].Value.ParseToStringNullable() : null;
			if (string.IsNullOrEmpty(cadNumVal))
			{
				result.Add($"атрибут {cadNumAttr.Name} обязательный");
			}

			var ownerIdAttr = OMDeclaration.GetAttributeData(x => x.Owner_Id);
			var ownerId = attrValues.ContainsKey(ownerIdAttr.Id) ?  attrValues[ownerIdAttr.Id].Value.ParseToLongNullable() : (long?)null;
			
			var agentIdAttr = OMDeclaration.GetAttributeData(x => x.Agent_Id);
			var agentId = attrValues.ContainsKey(agentIdAttr.Id) ? attrValues[agentIdAttr.Id].Value.ParseToLongNullable() : (long?)null;

			if (!ownerId.HasValue && !agentId.HasValue)
			{
				result.Add($"должен быть указан хотя бы один атрибут: {ownerIdAttr.Name} или {agentIdAttr.Name}");
			}
			else
			{
				var owner = OMSubject
					.Where(x => x.Id == ownerId)
					.SelectAll()
					.ExecuteFirstOrDefault();
				var agent = OMSubject
					.Where(x => x.Id == agentId)
					.SelectAll()
					.ExecuteFirstOrDefault();
				if (owner == null && agent == null)
				{
					result.Add($"не найдены объекты по указанным атрибутам: {ownerIdAttr.Name} или {agentIdAttr.Name}");
				}
				else
				{
					if (agent != null)
					{
						var sendUvedTypeAttr = OMDeclaration.GetAttributeData(x => x.UvedTypeAgent);
						var sendUvedType = attrValues.ContainsKey(sendUvedTypeAttr.Id) ? attrValues[sendUvedTypeAttr.Id].Value.ParseToStringNullable() : null;
						var validationResult = ValidateSubjectNotificationType(agent, sendUvedType, "Представителя");
						if (!string.IsNullOrEmpty(validationResult))
						{
							result.Add(validationResult);
						}
					}
					if (owner != null)
					{
						var sendUvedTypeAttr = OMDeclaration.GetAttributeData(x => x.UvedTypeOwner);
						var sendUvedType = attrValues.ContainsKey(sendUvedTypeAttr.Id) ? attrValues[sendUvedTypeAttr.Id].Value.ParseToStringNullable() : null;
						var validationResult = ValidateSubjectNotificationType(owner, sendUvedType, "Заявителя");
						if (!string.IsNullOrEmpty(validationResult))
						{
							result.Add(validationResult);
						}
					}
				}
			}

			return result;
		}

		private static string ValidateSubjectNotificationType(OMSubject subject, string subjectUvedType, string subjectName)
		{
			if (subjectUvedType == SendUvedType.Email.GetEnumDescription())
			{
				if (string.IsNullOrWhiteSpace(subject?.Mail))
				{
					return
						$"У выбранного {subjectName} отсутствуют необходимые данные для данного Способа получения уведомления: Адрес электронной почты";
				}
			}
			else if (string.IsNullOrWhiteSpace(subject?.Zip) || string.IsNullOrWhiteSpace(subject?.City) ||
			         string.IsNullOrWhiteSpace(subject?.Street) || string.IsNullOrWhiteSpace(subject?.House))
			{
				var emptyAddressParts = new List<string>();
				if (string.IsNullOrWhiteSpace(subject?.Zip))
				{
					emptyAddressParts.Add("Индекс");
				}

				if (string.IsNullOrWhiteSpace(subject?.City))
				{
					emptyAddressParts.Add("Город");
				}

				if (string.IsNullOrWhiteSpace(subject?.Street))
				{
					emptyAddressParts.Add("Улица");
				}

				if (string.IsNullOrWhiteSpace(subject?.House))
				{
					emptyAddressParts.Add("Дом");
				}

				return
					$"У выбранного {subjectName} отсутствуют необходимые данные для данного Способа получения уведомления: {string.Join(", ", emptyAddressParts)}";
			}

			return string.Empty;
		}
	}
}
