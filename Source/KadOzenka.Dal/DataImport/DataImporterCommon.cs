﻿using Core.Register;
using Core.Register.QuerySubsystem;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using Core.Main.FileStorages;
using ObjectModel.Common;
using Newtonsoft.Json;
using System.IO;
using System.Transactions;
using Core.SRD;
using Core.ErrorManagment;
using ObjectModel.Core.Shared;
using Core.Messages;
using Core.Shared.Misc;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.DataImport
{
    public class DataImporterCommon : ILongProcess
	{
		public const string LongProcessName = "DataImporterFromTemplate";
		public const string FileStorageName = "DataImporterFromTemplate";

		public static string GetTemplateName(long importDataId) => $"{importDataId}_Template";

		public static string GetResultFileName(long importDataId) => $"{importDataId}_Result";

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
            if (!processQueue.ObjectId.HasValue)
            {
                WorkerCommon.SetMessage(processQueue, LongProcess.Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
                WorkerCommon.SetProgress(processQueue, LongProcess.Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
                return;
            } 

			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();
            if (import == null)
            {
                WorkerCommon.SetMessage(processQueue, LongProcess.Consts.Consts.GetMessageForProcessInterruptedBecauseOfNoDataLog(processQueue.ObjectId.Value));
                WorkerCommon.SetProgress(processQueue, LongProcess.Consts.Consts.ProgressForProcessInterruptedBecauseOfNoDataLog);
                return;
            }

            WorkerCommon.SetProgress(processQueue, 0);

            import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
			import.DateStarted = DateTime.Now;
			import.Save();

			// Запустить формирование файла
			var templateFile = FileStorageManager.GetFileStream(FileStorageName, import.DateCreated, GetTemplateName(import.Id));
			ExcelFile excelTemplate = ExcelFile.Load(templateFile, LoadOptions.XlsxDefault);

            WorkerCommon.SetProgress(processQueue, 25);

            List<DataExportColumn> columns = JsonConvert.DeserializeObject<List<DataExportColumn>>(import.ColumnsMapping);
		    Stream resultFile = ImportDataFromExcel((int)import.MainRegisterId, excelTemplate, columns, out var success);

            WorkerCommon.SetProgress(processQueue, 75);

            // Сохранение файла
		    import.DateFinished = DateTime.Now;
            FileStorageManager.Save(resultFile, FileStorageName, import.DateFinished.Value, GetResultFileName(import.Id));

		    if (!success)
		    {
		        import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
		        SendResultNotification(import);
                throw new Exception("Файл содержит некорректные данные");
            }

		    import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
            import.Save();
		    
		    SendResultNotification(import);
		    WorkerCommon.SetProgress(processQueue, 100);
        }

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			OMImportDataLog import = OMImportDataLog.Where(x => x.Id == objectId).SelectAll().Execute().FirstOrDefault();
			if (import == null) return;
			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
			import.DateFinished = DateTime.Now;
			import.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : String.Empty)}";
			import.Save();
        }
		
		public bool Test() => true;

		internal static void SendResultNotification(OMImportDataLog import)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = new MessageAddressersDto{UserIds = new long[] { import.UserId } },
				Subject = $"Результат загрузки данных в реестр: {RegisterCache.GetRegisterData((int)import.MainRegisterId).Description} от ({import.DateCreated.GetString()})",
				Message = $@"Загрузка файла ""{import.DataFileName}"" была завершена.
Статус загрузки: {import.Status_Code.GetEnumDescription()}
<a href=""/DataImporterLayout/Download?downloadResult=true&importId={import.Id}"">Скачать результат</a>
<a href=""/DataImporterLayout/Download?downloadResult=false&importId={import.Id}"">Скачать исходный файл</a>
<a href=""/RegistersView/DataImporter?Transition=1&80100100={import.Id}"">Перейти в журнал загрузки</a>",
				IsUrgent = true,
				IsEmail = true
			});
		}

		public static void AddImportToQueue(long mainRegisterId, string registerViewId, string templateFileName, Stream templateFile, List<DataExportColumn> columns)
		{
			string jsonstring = JsonConvert.SerializeObject(columns);
			var export = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added, // TODO: доработать платформу, чтоб формировался Enum
				DataFileName = templateFileName,
				ColumnsMapping = jsonstring,
				MainRegisterId = mainRegisterId,
				RegisterViewId = registerViewId
			};
			export.Save();
			FileStorageManager.Save(templateFile, FileStorageName, export.DateCreated, GetTemplateName(export.Id));
			LongProcessManager.AddTaskToQueue(LongProcessName, OMExportByTemplates.GetRegisterId(), export.Id);
		}

		public static Stream ImportDataFromExcel(int mainRegisterId, ExcelFile excelFile, List<DataExportColumn> columns, out bool success)
		{
			var mainWorkSheet = excelFile.Worksheets[0];
			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 100
			};
			int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();
			mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат обработки");
			List<string> columnNames = new List<string>();
			for (int i = 0; i < maxColumns; i++) columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());

			var register = OMRegister.Where(x => x.RegisterId == mainRegisterId).Select(x => x.AllpriTable).ExecuteFirstOrDefault();
			bool isAllpri = !string.IsNullOrEmpty(register.AllpriTable);

			if (isAllpri)
			    success = ProcessAllpri(columns, mainWorkSheet, options, maxColumns, columnNames);
			else
			    success = ProcessNotAllpri(columns, mainWorkSheet, options, maxColumns, columnNames, mainRegisterId);
					   			 		  
			MemoryStream stream = new MemoryStream();
			excelFile.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);
			return stream;
		}

		private static bool ProcessAllpri(List<DataExportColumn> columns, ExcelWorksheet mainWorkSheet, 
			ParallelOptions options, int maxColumns, List<string> columnNames)
		{
		    Parallel.ForEach(mainWorkSheet.Rows, options, row =>
			{
				try
				{
					if (row.Index != 0) //все, кроме заголовков
					{
						long objectId = -1;

						//ключ - кадастровый номер, колонка №2						
						string cadastralNumber = row.Cells[1].Value.ToString();
						OMMainObject mainObject = OMMainObject.Where(x => x.CadastralNumber == cadastralNumber)
							.Select(x => x.Id).ExecuteFirstOrDefault();

						if (mainObject == null)
						{
							mainObject = new OMMainObject
							{
								CadastralNumber = cadastralNumber,
								ObjectType_Code = ObjectModel.Directory.PropertyTypes.Building
							};
							mainObject.Save();
						}
						objectId = mainObject.Id;

						var loadColumns = columns.Where(y => !y.IsKey).ToList();
						List<long> loadColumnIds = columns.Where(y => !y.IsKey).Select(x => x.AttributrId).ToList();

						var registers = OMAttribute.Where(x => loadColumnIds.Contains(x.Id))
							.Select(x => x.RegisterId)
							.Execute();

						var registerGroups = registers.GroupBy(x => x.RegisterId, y => y.Id).ToList();

						foreach (var group in registerGroups)
						{
							long registerId = group.Key;
							List<long> attributeIds = group.Select(x => x).ToList();

							RegisterObject registerObject = new RegisterObject((int)registerId, (int)objectId);

							foreach (long attribute in attributeIds)
							{
								DataExportColumn column = columns.Where(x => x.AttributrId == attribute).First();

								int cell = columnNames.IndexOf(column.ColumnName);
								object value = mainWorkSheet.Rows[row.Index].Cells[cell].Value;

								var attributeData = RegisterCache.GetAttributeData((int)column.AttributrId);

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

								registerObject.SetAttributeValue((int)column.AttributrId, value);
							}

						    RegisterStorage.Save(registerObject);
						    mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Успешно");
						}
					}
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"{ex.Message} (подробно в журнале №{errorId})");
				}
			});

            return true;
		}

		private static bool ProcessNotAllpri(List<DataExportColumn> columns, ExcelWorksheet mainWorkSheet, 
			ParallelOptions options, int maxColumns, List<string> columnNames, int mainRegisterId)
		{
		    var success = true;
		    var errorCount = 0;
		    var handledObjects = new Dictionary<int, RegisterObject>();
		    object locked = new object();
            Parallel.ForEach(mainWorkSheet.Rows, options, row =>
			{
				try
				{
					if (row.Index != 0) //все, кроме заголовков
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
                                OMReference reference = OMReference.Where(x => x.ReferenceId == attributeData.ReferenceId).ExecuteFirstOrDefault();
                                OMReferenceItem item = OMReferenceItem.Where(x => x.ReferenceId == attributeData.ReferenceId && x.Value == (string)value).ExecuteFirstOrDefault();
							    var valueStr = value != null ? value.ToString().Trim() : string.Empty;
                                if (item == null && !string.IsNullOrEmpty(valueStr))
                                {
                                    throw new Exception($"Некорректное значение в ячейке ({row}, {cell + 1}) для справочника: '{value.ToString()}'");
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
                                    value = value == null ? "" : value.ToString();
                                    break;
                                case RegisterAttributeType.DATE:
                                    value = value.ParseToDateTimeNullable();
                                    break;
                            }
                            registerObject.SetAttributeValue((int)column.AttributrId, value, referenceItemId);

						    if (registerObject.AttributesValues[(int) column.AttributrId].Value == null &&
						        !attributeData.IsNullable)
						    {
						        throw new Exception(
						            $"Значение атрибута {attributeData.Name} (ячейка {cell}) не может быть пустым");
						    }
						}

					    if (objectId == -1)
					    {
					        var allRegisterAttributes = RegisterCache.RegisterAttributes.Values.Where(p => p.RegisterId == (int)mainRegisterId && !p.IsPrimaryKey);
					        var missedRequiredAttributeNames = new List<string>();
                            foreach (var registerAttribute in allRegisterAttributes)
					        {
					            
					            if (!registerAttribute.IsNullable && !registerObject.AttributesValues.ContainsKey(registerAttribute.Id))
					            {
					                missedRequiredAttributeNames.Add(registerAttribute.Name);
					            }
					        }

					        if (missedRequiredAttributeNames.Count > 0)
					        {
					            throw new Exception(
					                $"Не переданы значения для ненулевых атрибутов: {string.Join(", ", missedRequiredAttributeNames)}");
                            }
					    }

                        lock (locked)
					    {
					        handledObjects.Add(row.Index, registerObject);
                        }
                        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Успешно");
                        lock (locked)
                        {
	                        for (int i = 0; i < maxColumns; i++)
	                        {
		                        mainWorkSheet.Rows[row.Index].Cells[i].Style.FillPattern
			                        .SetSolid(SpreadsheetColor.FromArgb(200, 255, 200));
	                        }
                        }
					}
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"{ex.Message} (подробно в журнале №{errorId})");
					lock (locked)
					{
						for (int i = 0; i < maxColumns; i++)
						{
							mainWorkSheet.Rows[row.Index].Cells[i].Style.FillPattern
								.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
						}
					}

					lock (locked)
				    {
				        errorCount++;
				    }
                }
			});

		    if (errorCount > 0)
		    {
		        success = false;
		    }
		    else
		    {
		        try
		        {
		            using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
		            {
		                for (var i = 1; i < mainWorkSheet.Rows.Count; i++)
		                {
		                    try
		                    {
		                        var registerObject = handledObjects[i];
		                        RegisterStorage.Save(registerObject);
		                    }
		                    catch (Exception ex)
		                    {
		                        long errorId = ErrorManager.LogError(ex);
		                        mainWorkSheet.Rows[i].Cells[maxColumns]
		                            .SetValue($"{ex.Message} (подробно в журнале №{errorId})");
		                        for (int j = 0; j < maxColumns; j++)
		                        {
		                            mainWorkSheet.Rows[i].Cells[j].Style.FillPattern
		                                .SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
		                        }

		                        throw;
		                    }
		                }

		                ts.Complete();
		            }
		        }
		        catch (Exception ex)
		        {
		            success = false;
                }
		    }

		    return success;
        }		
	}
}
