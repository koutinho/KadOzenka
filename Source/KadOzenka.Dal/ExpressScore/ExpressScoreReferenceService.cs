//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Transactions;
//using Core.ErrorManagment;
//using Core.Main.FileStorages;
//using Core.Messages;
//using Core.Shared.Extensions;
//using Core.Shared.Misc;
//using Core.SRD;
//using GemBox.Spreadsheet;
//using KadOzenka.Dal.DataExport;
//using KadOzenka.Dal.DataImport;
//using KadOzenka.Dal.ExpressScore.Dto;
//using ObjectModel.Common;
//using ObjectModel.Directory.Common;
//using ObjectModel.Directory.ES;
//using ObjectModel.ES;
//using Serilog;

//namespace KadOzenka.Dal.ExpressScore
//{
//    public class ExpressScoreReferenceService
//    {
//	    private readonly ILogger _log = Log.ForContext<ExpressScoreReferenceService>();

//        public const string DateCreatedStringFormat = "yyyyMMddHHmmss";
//        public int AllRows { get; private set; } = 1;
//		public int CurrentRow { get; private set; }

//        private const long MaxRow = 10000;
//        private static readonly int MainRegisterId = OMEsReference.GetRegisterId();
//        private static readonly string RegisterViewId = "EsReferences";

//        public bool UseLongProcess(Stream fileStream)
//        {
//	        if (GetCountRows(fileStream) > MaxRow)
//	        {
//		        return true;
//	        }

//	        return false;
//        }

//        public long CreateReference(string name, ReferenceItemCodeType valueType, bool useInterval)
//        {
//            var isExistsReferencesWithTheSameName = OMEsReference.Where(x => x.Name == name).ExecuteExists();
//            if (isExistsReferencesWithTheSameName)
//            {
//                throw new Exception($"Справочник '{name}' уже существует");
//            }

//            var reference = new OMEsReference
//            {
//                Name = name, 
//                ValueType_Code = valueType,
//                UseInterval = useInterval
//            };
//            var id = reference.Save();

//            return id;
//        }

//        public void UpdateReference(long id, string name, ReferenceItemCodeType valueType, bool useInterval)
//        {
//            var reference = OMEsReference.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
//            if (reference == null)
//            {
//                throw new Exception($"Не найден справочник с ИД {id}");
//            }

//            var isExistsReferenceWithTheSameName = OMEsReference.Where(x => x.Name == name && x.Id != id).ExecuteExists();
//            if (isExistsReferenceWithTheSameName)
//            {
//                throw new Exception($"Справочник '{name}' уже существует");
//            }

//            if (reference.ValueType_Code != valueType || reference.UseInterval != useInterval)
//            {
//                var isReferenceNotEmpty = OMEsReferenceItem.Where(x => x.ReferenceId == id).ExecuteExists();
//                if (isReferenceNotEmpty)
//                {
//                    throw new Exception($"Нельзя изменить тип или вид для непустого справочника");
//                }
//            }


//            reference.Name = name;
//            reference.ValueType_Code = valueType;
//            reference.UseInterval = useInterval;
//            reference.Save();
//        }

//        public void DeleteReference(long id, ReferenceItemCodeType? valueType = null)
//        {
//            var reference = OMEsReference.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
//            if (reference == null)
//            {
//                throw new Exception($"Не найден справочник с ИД {id}");
//            }

//            using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
//            {
//                var referenceItems = OMEsReferenceItem.Where(x => x.ReferenceId == id).Execute();
//                foreach (var referenceItem in referenceItems)
//                {
//                    referenceItem.Destroy();
//                }
//                reference.Destroy();

//                ts.Complete();
//            }
//        }

//        public long SaveReferenceItem(ReferenceItemDto dto)
//        {
//	        OMEsReferenceItem item = new OMEsReferenceItem();

//            if (dto.Id != -1)
//            {
//	            item = OMEsReferenceItem.Where(x => x.Id == dto.Id).SelectAll().ExecuteFirstOrDefault();
//	            if (item == null)
//	            {
//		            throw new Exception($"Не найдено значение справочника с ИД {dto.Id}");
//	            }

//            }

//            var reference = OMEsReference.Where(x => x.Id == dto.ReferenceId).SelectAll().ExecuteFirstOrDefault();
//	        if (reference == null)
//	        {
//		        throw new Exception($"Не найден справочник с ИД {dto.ReferenceId}");
//	        }

//	        ValidateReferenceItemValue(dto, reference);
//	        ValidateExistsReferenceItemValue(dto, reference);

//	        if (dto.Id == -1)
//	        {
//		        item.ReferenceId = dto.ReferenceId;
//	        }

//            item.Value = dto.Value;
//	        item.CommonValue = dto.CommonValue;
//	        item.ValueFrom = dto.ValueFrom;
//	        item.ValueTo = dto.ValueTo;
//	        item.CalculationValue = dto.CalcValue;
//	        item.Save();

//	        return item.Id;
//        }
//        public void DeleteReferenceItem(long id)
//        {
//            var item = OMEsReferenceItem.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
//            if (item == null)
//            {
//                throw new Exception($"Не найдено значение справочника с ИД {id}");
//            }

//            item.Destroy();
//        }

//        public void CreateOrUpdateReferenceThroughLongProcess(OMImportDataLog import, ImportFileFromExcelDto settings)
//        {
//	        OMEsReference reference;
//	        if (settings.IsNewReference)
//	        {
//		        long referenceId = CreateReference(settings.NewReferenceName, settings.FileInfo.ValueType, settings.FileInfo.UseInterval);
//		        reference = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault();
//	        }
//	        else
//	        {
//		        reference = OMEsReference.Where(x => x.Id == settings.IdReference).SelectAll().ExecuteFirstOrDefault();
//		        if (reference == null)
//		        {
//			        throw new Exception($"Не найден справочник с ИД {settings.IdReference}");
//		        }

//		        if (reference.ValueType_Code != settings.FileInfo.ValueType && !settings.DeleteOldValues)
//		        {
//			        throw new Exception($"Нельзя изменить тип справочника без удаления старых значений");
//		        }

//		        if (settings.DeleteOldValues)
//		        {
//			        using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
//			        {
//				        var referenceItems = OMEsReferenceItem.Where(x => x.ReferenceId == settings.IdReference).Execute();
//				        foreach (var referenceItem in referenceItems)
//				        {
//					        referenceItem.Destroy();
//				        }

//				        reference.ValueType_Code = settings.FileInfo.ValueType;
//				        reference.Save();
//				        ts.Complete();
//			        }
//				}
//			}

//	        var dataFileStream = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated,
//		        import.DataFileName);

//            var resFileStream = ImportReferenceItemsFromExcel(dataFileStream, reference, settings.FileInfo);
//            SaveResultFile(import, resFileStream);
//            SendImportResultNotification(reference, DataImporterCommon.GetDataFileTitle(settings.FileInfo.FileName), import.Id, true);
//		}

//        public void UpdateReferenceFromExcel(Stream fileStream, ImportReferenceFileInfoDto fileImportInfo, long referenceId, bool deleteOldValues)
//        {
//            var reference = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault();
//            if (reference == null)
//            {
//                throw new Exception($"Не найден справочник с ИД {referenceId}");
//            }

//            if (reference.ValueType_Code != fileImportInfo.ValueType && !deleteOldValues)
//            {
//                throw new Exception($"Нельзя изменить тип справочника без удаления старых значений");
//            }

//            if (deleteOldValues)
//            {
//                using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
//                {
//                    var referenceItems = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).Execute();
//                    foreach (var referenceItem in referenceItems)
//                    {
//                        referenceItem.Destroy();
//                    }

//                    reference.ValueType_Code = fileImportInfo.ValueType;
//                    reference.Save();
//                    ts.Complete();
//                }
//            }
//			PreparingForImportAndImport(fileStream, reference, fileImportInfo);
//		}

//        public long CreateReferenceFromExcel(Stream fileStream, ImportReferenceFileInfoDto fileImportInfo, string referenceName)
//        {
	   
//			long referenceId = CreateReference(referenceName, fileImportInfo.ValueType, fileImportInfo.UseInterval);
//            var reference = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault();


//            PreparingForImportAndImport(fileStream, reference, fileImportInfo);
//			return referenceId;
//        }

//        public void PreparingForImportAndImport(Stream fileStream, OMEsReference reference,
//	        ImportReferenceFileInfoDto fileImportInfo)
//        {
//	        var import = CreateDataFileImport(fileStream, fileImportInfo);
//	        try
//	        {
//		        import.Status_Code = ImportStatus.Running;
//		        import.DateStarted = DateTime.Now;
//		        import.Save();

//		        var resFileStream = ImportReferenceItemsFromExcel(fileStream, reference, fileImportInfo);
//		        SaveResultFile(import, resFileStream);

//		        import.Status_Code = ImportStatus.Completed;
//		        import.Save();

//                SendImportResultNotification(reference, DataImporterCommon.GetDataFileTitle(fileImportInfo.FileName), import.Id);
//	        }
//	        catch (Exception ex)
//	        {
//		        long errorId = ErrorManager.LogError(ex);
//		        import.Status_Code = ImportStatus.Faulted;
//		        import.DateFinished = DateTime.Now;
//		        import.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
//		        import.Save();

//		        throw;
//            }
//        }

//        public static OMImportDataLog CreateDataFileImport(Stream fileStream, ImportReferenceFileInfoDto fileImportInfo)
//        {
//	        var currentTime = DateTime.Now;
//	        var fileName = $"{DataImporterCommon.GetDataFileTitle(fileImportInfo.FileName)} ({currentTime.GetString().Replace(":", "_")})";

//            var import = new OMImportDataLog
//	        {
//		        UserId = SRDSession.GetCurrentUserId().Value,
//		        DateCreated = currentTime,
//		        Status_Code = ImportStatus.Added,
//		        DataFileTitle = fileName,
//		        FileExtension = DataImporterCommon.GetFileExtension(fileImportInfo.FileName),
//		        MainRegisterId = MainRegisterId,
//		        RegisterViewId = RegisterViewId
//	        };
//	        import.Save();

//	        import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
//	        FileStorageManager.Save(fileStream, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
//	        import.Save();

//	        return import;
//        }

//        private Stream ImportReferenceItemsFromExcel(Stream fileStream, OMEsReference reference,
//            ImportReferenceFileInfoDto fileImportInfo)
//        {
//	        _log.ForContext("ReferenceName",reference.Name).Debug("Начинается процесс разбора файла и добавления значений в справочник");

//            fileStream.Seek(0, SeekOrigin.Begin);
//			var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
//			var mainWorkSheet = excelFile.Worksheets[0];

//			AllRows = mainWorkSheet.Rows.Count;
//            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
//            ParallelOptions options = new ParallelOptions
//            {
//                CancellationToken = cancelTokenSource.Token,
//                MaxDegreeOfParallelism = 10
//            };
//            object locked = new object();

//            int maxColumns = DataExportCommon.GetLastUsedColumnIndex(mainWorkSheet) + 1;
//            var columnNames = new List<string>();
//            for (var i = 0; i < maxColumns; i++)
//            {
//	            if (mainWorkSheet.Rows[0].Cells[i].Value != null)
//                    columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
//            }
            
//            mainWorkSheet.Rows[0].Cells[maxColumns].SetValue("Результат сохранения");
//            var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
//            var dataRows = mainWorkSheet.Rows.Where(x => x.Index > 0 && x.Index <= lastUsedRowIndex);


//            Parallel.ForEach(dataRows, options, row =>
//            {
//                try
//                {
//	                var cellCommonValue = mainWorkSheet.Rows[row.Index]
//		                .Cells[columnNames.IndexOf(fileImportInfo.CommonValueColumnName)];
//                    var cellValue = !fileImportInfo.UseInterval ? mainWorkSheet.Rows[row.Index]
//                        .Cells[columnNames.IndexOf(fileImportInfo.ValueColumnName)] : null;
//                    var cellCalcValue = mainWorkSheet.Rows[row.Index]
//                        .Cells[columnNames.IndexOf(fileImportInfo.CalcValueColumnName)];
//                    var cellValueFrom = fileImportInfo.UseInterval ? mainWorkSheet.Rows[row.Index]
//	                    .Cells[columnNames.IndexOf(fileImportInfo.ValueFromColumnName)] : null;
//                    var cellValueTo = fileImportInfo.UseInterval ? mainWorkSheet.Rows[row.Index]
//	                    .Cells[columnNames.IndexOf(fileImportInfo.ValueToColumnName)] : null;

//                    if (!cellCalcValue.Value.TryParseToDecimal(out var calcValue))
//                    {
//                        throw new Exception(
//                            $"Значение '{cellCalcValue.Value}' не может быть приведено к типу '{ReferenceItemCodeType.Number.GetEnumDescription()}'");
//                    }

//                    _log.Debug("Значение для расчета получено, {calcValue}", calcValue);

//                    string valueString = null;
//                    string valueFrom = null;
//                    string valueTo = null;
//                    switch (fileImportInfo.ValueType)
//                    {
//                        case ReferenceItemCodeType.Number:
//                            if(!fileImportInfo.UseInterval) valueString = GetDecimalValue(cellValue?.Value).ToString(CultureInfo.InvariantCulture);
//                            if (fileImportInfo.UseInterval)
//                            {
//	                            valueFrom = GetDecimalValue(cellValueFrom?.Value).ToString(CultureInfo.InvariantCulture);
//	                            valueTo = GetDecimalValue(cellValueTo?.Value).ToString(CultureInfo.InvariantCulture);
//                            }

//	                        break;
//                        case ReferenceItemCodeType.String:
//	                        if (!fileImportInfo.UseInterval) valueString = cellValue?.Value.ToString();
//	                        break;
//                        case ReferenceItemCodeType.Date:
//	                        if (!fileImportInfo.UseInterval) valueString = GetDateValue(cellValue?.Value).ToString(CultureInfo.CurrentCulture);
//	                        if (fileImportInfo.UseInterval)
//	                        {
//		                        valueFrom = GetDateValue(cellValueFrom?.Value).ToShortDateString();
//		                        valueTo = GetDateValue(cellValueTo?.Value).ToShortDateString();
//	                        }
//                            break;
//                    }

//                    OMEsReferenceItem obj =  null;
//					if(!fileImportInfo.UseInterval)
//					{
//						obj = OMEsReferenceItem.Where(x => x.ReferenceId == reference.Id && x.Value == valueString).SelectAll()
//							.ExecuteFirstOrDefault();
//					}

//					if (fileImportInfo.UseInterval)
//					{
//						obj = OMEsReferenceItem.Where(x => x.ReferenceId == reference.Id && x.ValueTo == valueTo && x.ValueFrom == valueFrom)
//							.SelectAll().ExecuteFirstOrDefault();

//					}

//					if (obj != null)
//                    {
//	                    obj.CalculationValue = calcValue;
//                        obj.CommonValue = cellCommonValue?.Value?.ToString();
//                        //lock (locked)
//                        {
//                            obj.Save();
//                        }
//                        _log.Debug("Значение было обновлено, т.к в справочнике {refName} уже было расчетное значение для {valueString}", reference.Name, valueString );
//                        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение успешно обновлено");
//                    }
//                    else
//                    {
//						//lock (locked)
//						{
//                            new OMEsReferenceItem
//							{
//								ReferenceId = reference.Id,
//								Value = valueString,
//								CommonValue = cellCommonValue?.Value?.ToString(),
//								CalculationValue = calcValue,
//                                ValueFrom = valueFrom,
//                                ValueTo = valueTo
//							}.Save();
//						}
//						_log.Debug("Значение было создано {valueString}", valueString);
//                        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение успешно создано");
//                    }
//					lock(locked)
//					{
//						CurrentRow++;
//					}
//                }
//                catch (Exception ex)
//                {
//                    _log.Error(ex.Message);
//                    mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"Ошибка: {ex.Message}");
//                    for (int i = 0; i < maxColumns; i++)
//                    {
//                        mainWorkSheet.Rows[row.Index].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
//                    }
//                }
//            });

//            MemoryStream stream = new MemoryStream();
//            excelFile.Save(stream, SaveOptions.XlsxDefault);
//            stream.Seek(0, SeekOrigin.Begin);

//			return stream;
//        }

//        public decimal GetDecimalValue(object value)
//        {
//	        if (!value.TryParseToDecimal(out var number))
//	        {
//		        throw new Exception(
//			        $"Значение '{value}' не может быть приведено к типу '{ReferenceItemCodeType.Number.GetEnumDescription()}'");
//	        }

//	        return number;
//        }

//        public DateTime GetDateValue(object value)
//        {
//	        if (!value.TryParseToDateTime(out var date))
//	        {
//		        throw new Exception(
//			        $"Значение '{value}'не может быть приведено к типу '{ReferenceItemCodeType.Date.GetEnumDescription()}'");
//	        }

//	        return date;
//        }

//        private void SaveResultFile(OMImportDataLog import, Stream streamResult)
//        {
//	        import.ResultFileTitle = DataImporterCommon.GetFileResultTitleFromDataTitle(import);
//	        import.ResultFileName = DataImporterCommon.GetStorageResultFileName(import.Id);
//	        import.DateFinished = DateTime.Now;
//	        FileStorageManager.Save(streamResult, DataImporterCommon.FileStorageName, import.DateFinished.Value, import.ResultFileName);
//	        import.Save();
//        }

//        private void SendImportResultNotification(OMEsReference reference, string fileName, long importId, bool isUrgent = false)
//        {
//	        new MessageService().SendMessages(new MessageDto
//            {
//                Addressers =
//                    new MessageAddressersDto {UserIds = new long[] {SRDSession.GetCurrentUserId().GetValueOrDefault()}},
//                Subject = $"Результат загрузки данных в справочник: {reference.Name} от ({DateTime.Now.GetString()})",
//                Message = $@"Загрузка файла ""{fileName}"" была завершена.
//<a href=""/DataImport/DownloadImportResultFile?importId={importId}"">Скачать результат</a>
//<a href=""/DataImport/DownloadImportDataFile?importId={importId}"">Скачать исходный файл</a>",
//                IsUrgent = isUrgent,
//                IsEmail = true,
//                ExpireDate = DateTime.Now.AddHours(2)
//        });
//        }

//        private long GetCountRows(Stream fileStream)
//        {
//	        var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
//	        var mainWorkSheet = excelFile.Worksheets[0];
//	        long res = mainWorkSheet.Rows.Count;
//	        return res;
//        }

//        #region support methods

//        private void ValidateReferenceItemValue(ReferenceItemDto dto, OMEsReference reference)
//        {
//	        if (!dto.UseInterval && dto.Value != null && (reference.ValueType_Code == ReferenceItemCodeType.Number && !decimal.TryParse(dto.Value, out var decimalResult)
//	                                  || reference.ValueType_Code == ReferenceItemCodeType.Date && !DateTime.TryParse(dto.Value, out var dateResult)))
//	        {
//		        throw new Exception($"Значение '{dto.Value}' не может быть приведено к типу '{reference.ValueType_Code.GetEnumDescription()}'");
//	        }

//	        if (dto.UseInterval && dto.ValueFrom != null && (reference.ValueType_Code == ReferenceItemCodeType.Number && !decimal.TryParse(dto.ValueFrom, out var decimalResultFrom)
//	                                                      || reference.ValueType_Code == ReferenceItemCodeType.Date && !DateTime.TryParse(dto.ValueFrom, out var dateResultFrom)))
//	        {
//		        throw new Exception($"Значение от'{dto.ValueFrom}' не может быть приведено к типу '{reference.ValueType_Code.GetEnumDescription()}'");
//	        }

//	        if (dto.UseInterval && dto.Value != null && (reference.ValueType_Code == ReferenceItemCodeType.Number && !decimal.TryParse(dto.ValueTo, out var decimalResultTo)
//	                                                      || reference.ValueType_Code == ReferenceItemCodeType.Date && !DateTime.TryParse(dto.ValueTo, out var dateResultTo)))
//	        {
//		        throw new Exception($"Значение до'{dto.Value}' не может быть приведено к типу '{reference.ValueType_Code.GetEnumDescription()}'");
//	        }
//        }

//        private void ValidateExistsReferenceItemValue(ReferenceItemDto dto, OMEsReference reference)
//        {
//	        if (reference.UseInterval.GetValueOrDefault())
//	        {
//		        var isExistsTheSameReferenceItemValueFrom = OMEsReferenceItem.Where(x => x.ReferenceId == dto.ReferenceId && x.ValueFrom == dto.ValueFrom && x.Id != dto.Id).ExecuteExists();
//		        var isExistsTheSameReferenceItemValueTo = OMEsReferenceItem.Where(x => x.ReferenceId == dto.ReferenceId && x.ValueTo == dto.ValueTo && x.Id != dto.Id).ExecuteExists();
//                if (isExistsTheSameReferenceItemValueFrom && isExistsTheSameReferenceItemValueTo)
//		        {
//			        throw new Exception($"Значение в диапозоне от'{dto.ValueFrom}'до {dto.ValueTo} в справочнике '{reference.Name}' уже существует");
//		        }
//	        }
//	        else
//	        {
//		        var isExistsTheSameReferenceItem = OMEsReferenceItem.Where(x => x.ReferenceId == dto.ReferenceId && x.Value == dto.Value && x.Id != dto.Id).ExecuteExists();
//		        if (isExistsTheSameReferenceItem)
//		        {
//			        throw new Exception($"Значение '{dto.Value}' в справочнике '{reference.Name}' уже существует");
//		        }
//            }


	     
//        }

//        #endregion
//    }
//}
