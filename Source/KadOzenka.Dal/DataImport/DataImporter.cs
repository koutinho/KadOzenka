using Core.Register;
using Core.Register.QuerySubsystem;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using ObjectModel.Commission;
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

        public static Stream ImportDataCommissionFromExcel(ExcelFile excelFile)
        {

            var mainWorkSheet = excelFile.Worksheets[0];

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };

            int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();

            mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат сохранения");
            Parallel.ForEach(mainWorkSheet.Rows, options, row =>
            {
                try
                {
                    if (row.Index != 0) //все, кроме заголовков
                    {

                        string kn = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
                        string num_s = mainWorkSheet.Rows[row.Index].Cells[2].Value.ParseToString();
                        DateTime? date_s = mainWorkSheet.Rows[row.Index].Cells[3].Value.ParseToDateTimeNullable();
                        ObjectModel.Commission.OMCost existObject = ObjectModel.Commission.OMCost.Where(x => x.Kn == kn && x.StatementNumber == num_s && x.StatementDate == date_s).SelectAll().ExecuteFirstOrDefault();
                        bool newobj = false;
                        if (existObject == null)
                        {
                            existObject = new ObjectModel.Commission.OMCost
                            {
                                Id = -1,
                                StatementDate = date_s,
                                StatementNumber = num_s,
                                Kn = kn,
                            };
                            newobj = true;
                        }

                        ObjectModel.Directory.Commission.DecisionResult dr = ObjectModel.Directory.Commission.DecisionResult.Rejected;
                        ObjectModel.Directory.Commission.CommissionType ct = ObjectModel.Directory.Commission.CommissionType.OnUnreliability;
                        string tcom = mainWorkSheet.Rows[row.Index].Cells[10].Value.ParseToString();
                        string rd = mainWorkSheet.Rows[row.Index].Cells[6].Value.ParseToString();

                        if (tcom.ToUpper() == "Установление стоимости".ToUpper()) ct = ObjectModel.Directory.Commission.CommissionType.OnSetCadCost;
                        if (rd.ToUpper() == "положительное решение".ToUpper()) dr = ObjectModel.Directory.Commission.DecisionResult.Approved;

                        existObject.DecisionResult_Code = dr;
                        existObject.CommissionType_Code = ct;

                        decimal? d_kc = mainWorkSheet.Rows[row.Index].Cells[8].Value.ParseToDecimalNullable();
                        existObject.Kc = (d_kc != null) ? ((d_kc == 0) ? null : d_kc) : (d_kc);
                        existObject.DateKc = mainWorkSheet.Rows[row.Index].Cells[9].Value.ParseToDateTimeNullable();
                        existObject.DecisionNumber = mainWorkSheet.Rows[row.Index].Cells[4].Value.ParseToString();
                        existObject.DecisionDate = mainWorkSheet.Rows[row.Index].Cells[5].Value.ParseToDateTimeNullable();

                        decimal? d_mv = mainWorkSheet.Rows[row.Index].Cells[14].Value.ParseToDecimalNullable();
                        existObject.MarketValue = (d_mv != null) ? ((d_mv == 0) ? null : d_mv) : (d_mv);


                        decimal? d_ckc = mainWorkSheet.Rows[row.Index].Cells[7].Value.ParseToDecimalNullable();
                        existObject.CommissionKc = (d_ckc != null) ? ((d_ckc == 0) ? null : d_ckc) : (d_ckc);
                        existObject.CommissionGroup = mainWorkSheet.Rows[row.Index].Cells[11].Value.ParseToString();
                        existObject.CommissionChange = mainWorkSheet.Rows[row.Index].Cells[12].Value.ParseToString();

                        existObject.ApplicantStatus_Code = (ObjectModel.Directory.Commission.ApplicantStatus)EnumExtensions.GetEnumByDescription<ObjectModel.Directory.Commission.ApplicantStatus>(mainWorkSheet.Rows[row.Index].Cells[13].Value.ParseToString());

                        existObject.Save();


                        if (newobj)
                        {
                            mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Новый объект");
                        }
                        else
                        {
                            mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Обновлено");
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



            var dateStarted = DateTime.Now; 
            var fileName = excelFile.DocumentProperties.Custom["FileName"].ToString();
			var file = new OMCommissionFileStorage
			{
	            FileName = string.IsNullOrEmpty(fileName) ? excelFile.Worksheets[0].Name : fileName,
	            DateStarted = dateStarted
			};
			try
			{
				FileStorageManager.Save(
					stream,
					"CommissionFilesStorage",
					dateStarted,
					file.Save().ToString()
				);
			}
			catch (Exception ex)
			{
				ErrorManager.LogError(ex);
			}

			return stream;
        }

    }
}
