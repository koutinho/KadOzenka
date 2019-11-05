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

			OMImportFromTemplates export = OMImportFromTemplates
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

			ExcelFile excelTemplate = ExcelFile.Load(templateFile, LoadOptions.XlsxDefault);
			List<DataExportColumn> columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(export.ColumnsMapping);
			
			ImportDataFromExcel((int)export.MainRegisterId, excelTemplate, columns);

			// Сохранение файла
			export.Status = 2;
			export.DateFinished = DateTime.Now;
			export.Save();
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			OMImportFromTemplates export = OMImportFromTemplates
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
			export.ResultMessage = $"{ex.Message} (журнал № {errorId})";
			export.Save();
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

		public static void ImportDataFromExcel(int mainRegisterId, ExcelFile excelFile, List<DataExportColumn> columns)
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
				MaxDegreeOfParallelism = 10
			};

			List<string> columnNames = new List<string>();
			for (int i = 0; i < mainWorkSheet.CalculateMaxUsedColumns(); i++)
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

						if (dt.Rows.Count > 0)
						{
							long objectId = dt.Rows[0]["ID"].ParseToLong();
													   							 						  
							RegisterObject registerObject = new RegisterObject((int)mainRegisterId, (int)objectId);

							foreach (var column in columns.Where(y => !y.IsKey))
							{
								int cell = columnNames.IndexOf(column.ColumnName);
								object value = mainWorkSheet.Rows[row.Index].Cells[cell].Value;

								registerObject.SetAttributeValue((int)column.AttributrId, value);
							}

							RegisterStorage.Save(registerObject);
						}
					}
				}
				catch (Exception ex)
				{
					ErrorManager.LogError(ex);
				}
			});
		}
	}
}
