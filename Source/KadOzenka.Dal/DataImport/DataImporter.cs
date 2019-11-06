using Core.Register;
using Core.Register.QuerySubsystem;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using Core.Main.FileStorages;
using ObjectModel.Common;
using Newtonsoft.Json;
using System.IO;
using Core.SRD;
using Core.ErrorManagment;
using ObjectModel.Core.Shared;

namespace KadOzenka.Dal.DataImport
{
	public class DataImporter : ILongProcess
	{
		public const string LongProcessName = "DataImporterFromTemplate";

		public const string FileStorageName = "DataImporterFromTemplate";

		public static string GetTemplateName(long importDataId)
		{
			return $"{importDataId}_Template";
		}

		public static string GetResultFileName(long importDataId)
		{
			return $"{importDataId}_Result";
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			if (!processQueue.ObjectId.HasValue)
			{
				return;
			}

			OMImportFromTemplates import = OMImportFromTemplates
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (import == null)
			{
				return;
			}

			import.Status = 1;
			import.DateStarted = DateTime.Now;
			import.Save();

			// Запустить формирование файла
			var templateFile = FileStorageManager.GetFileStream(FileStorageName, import.DateCreated, GetTemplateName(import.Id));

			ExcelFile excelTemplate = ExcelFile.Load(templateFile, LoadOptions.XlsxDefault);
			List<DataExportColumn> columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(import.ColumnsMapping);
			
			Stream resultFile = ImportDataFromExcel((int)import.MainRegisterId, excelTemplate, columns);

			// Сохранение файла
			import.Status = 2;
			import.DateFinished = DateTime.Now;
			import.Save();
			
			FileStorageManager.Save(resultFile, FileStorageName, import.DateFinished.Value, GetResultFileName(import.Id));
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			OMImportFromTemplates import = OMImportFromTemplates
				.Where(x => x.Id == objectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (import == null)
			{
				return;
			}

			import.Status = 3;
			import.DateFinished = DateTime.Now;
			import.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : String.Empty)}";
			import.Save();
		}

		public bool Test()
		{
			return true;
		}

		public static void AddImportToQueue(long mainRegisterId, string registerViewId, string templateFileName, Stream templateFile, List<DataExportColumn> columns)
		{
			string jsonstring = JsonConvert.SerializeObject(columns);

			var export = new OMImportFromTemplates
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

		public static Stream ImportDataFromExcel(int mainRegisterId, ExcelFile excelFile, List<DataExportColumn> columns)
		{
			if (!columns.Any(x => x.IsKey))
			{
				throw new Exception("Не указана ни одна ключевая колонка");
			}

			var mainWorkSheet = excelFile.Worksheets[0];

			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 1
			};

			int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();

			mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат сохранения");

			List<string> columnNames = new List<string>();
			for (int i = 0; i < maxColumns; i++)
			{
				columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
			}

			Parallel.ForEach(mainWorkSheet.Rows, options, row =>
			{
				try
				{
					if (row.Index != 0) //все, кроме заголовков
					{
						// Найти ИД объекта по ключевым полям
												
						List<QSCondition> conditions = new List<QSCondition>();
											
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

						if (dt.Rows.Count == 1)
						{
							long objectId = dt.Rows[0]["ID"].ParseToLong();
													   							 						  
							RegisterObject registerObject = new RegisterObject((int)mainRegisterId, (int)objectId);

							foreach (var column in columns.Where(y => !y.IsKey))
							{
								int cell = columnNames.IndexOf(column.ColumnName);
								object value = mainWorkSheet.Rows[row.Index].Cells[cell].Value;

								var attributeData = RegisterCache.GetAttributeData((int)column.AttributrId);
								int referenceItemId = -1;

								if(attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
								{
									OMReferenceItem item = OMReferenceItem.Where(x => x.ReferenceId == x.ReferenceId && x.Value == value).ExecuteFirstOrDefault();
									if (item != null) referenceItemId = (int)item.ItemId;
								}

								switch(attributeData.Type)
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

							RegisterStorage.Save(registerObject);

							mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Успешно");
						}
						else if(dt.Rows.Count == 0)
						{
							mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Не успешно: не найден объект в БД");
						}
						else
						{
							mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"Не успешно: в БД найдено несколько объектов: {dt.Rows.Count}");
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
	}
}
