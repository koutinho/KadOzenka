using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Messages;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.Directory.ES;
using ObjectModel.ES;
using ObjectModel.KO;

namespace KadOzenka.Dal.ExpressScore
{
    public class DictionaryService
    {
        public const string DateCreatedStringFormat = "yyyyMMddHHmmss";
        public int AllRows { get; private set; } = 1;
		public int CurrentRow { get; private set; }

        private const long MaxRow = 10000;
        private static readonly int MainRegisterId = OMEsReference.GetRegisterId();
        private static readonly string RegisterViewId = "EsReferences";

        public bool UseLongProcess(Stream fileStream)
        {
	        if (GetCountRows(fileStream) > MaxRow)
	        {
		        return true;
	        }

	        return false;
        }

        #region Dictionary

        public OMModelingDictionary GetDictionaryById(long id)
        {
	        var dictionary = OMModelingDictionary.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
	        if (dictionary == null)
		        throw new Exception($"Не найден справочник с ИД {id}");

	        return dictionary;
        }

        public long CreateDictionary(string name, ReferenceItemCodeType valueType)
        {
	        ValidateDictionary(name, -1);

	        return new OMModelingDictionary
	        {
		        Name = name, 
		        Type_Code = valueType
	        }.Save();
        }

        public void UpdateDictionary(long id, string newName, ReferenceItemCodeType newValueType)
        {
	        var dictionary = GetDictionaryById(id);

	        ValidateDictionary(newName, id);

	        if (dictionary.Type_Code != newValueType)
	        {
		        var hasValues = OMModelingDictionariesValues.Where(x => x.DictionaryId == id).ExecuteExists();
		        if (hasValues)
			        throw new Exception("Нельзя изменить тип для непустого справочника");
	        }

	        dictionary.Name = newName;
	        dictionary.Type_Code = newValueType;
	        dictionary.Save();
        }

        public void DeleteDictionary(long id)
        {
	        var reference = GetDictionaryById(id);

	        using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
	        {
		        var referenceItems = OMModelingDictionariesValues.Where(x => x.DictionaryId == id).Execute();
		        referenceItems.ForEach(x => x.Destroy());

		        reference.Destroy();

		        ts.Complete();
	        }
        }

        #region Support Methods

        public void ValidateDictionary(string name, long id)
        {
	        if (string.IsNullOrWhiteSpace(name))
		        throw new Exception("Невозможно создать справочник с пустым именем");

	        var isExistsDictionaryWithTheSameName = OMModelingDictionary.Where(x => x.Name == name && x.Id != id).ExecuteExists();
	        if (isExistsDictionaryWithTheSameName)
		        throw new Exception($"Справочник '{name}' уже существует");
        }

        #endregion

        #endregion


        #region Values

        public OMModelingDictionariesValues GetDictionaryValueById(long id)
        {
	        var dictionaryValue = OMModelingDictionariesValues.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            if (dictionaryValue == null)
		        throw new Exception($"Не найдено значение с ИД = '{id}'");

	        return dictionaryValue;
        }

        public long CreateDictionaryValue(DictionaryValueDto dto)
        {
	        var reference = GetDictionaryById(dto.DictionaryId);

	        ValidateDictionaryValue(reference, dto.Value, -1);

	        return new OMModelingDictionariesValues
            {
                DictionaryId = dto.DictionaryId,
                Value = dto.Value,
                CalculationValue = dto.CalcValue
            }.Save();
        }

        public void UpdateDictionaryValue(DictionaryValueDto dto)
        {
	        var dictionaryValue = GetDictionaryValueById(dto.Id);

            var dictionary = GetDictionaryById(dto.DictionaryId);

            ValidateDictionaryValue(dictionary, dto.Value, dto.Id);

            dictionaryValue.Value = dto.Value;
            dictionaryValue.CalculationValue = dto.CalcValue;
            dictionaryValue.Save();
        }

        public void DeleteDictionaryValue(long id)
        {
	        var dictionaryValue = GetDictionaryValueById(id);

	        dictionaryValue.Destroy();
        }

        #region Support Methods

        public void ValidateDictionaryValue(OMModelingDictionary dictionary, string value, long dictionaryValueId)
        {
	        var isEmptyValue = string.IsNullOrWhiteSpace(value);

            var canParseToNumber = !isEmptyValue && dictionary.Type_Code == ReferenceItemCodeType.Number && decimal.TryParse(value, out _);
	        var canParseToDate = !isEmptyValue && dictionary.Type_Code == ReferenceItemCodeType.Date && DateTime.TryParse(value, out _);

	        if (!isEmptyValue && !canParseToNumber && !canParseToDate)
		        throw new Exception($"Значение '{value}' не может быть приведено к типу '{dictionary.Type_Code.GetEnumDescription()}'");

	        var isExistsTheSameReferenceItem = OMModelingDictionariesValues
		        .Where(x => x.Id != dictionaryValueId && x.DictionaryId == dictionary.Id && x.Value == value)
		        .ExecuteExists();
	        if (isExistsTheSameReferenceItem)
		        throw new Exception($"Значение '{value}' в справочнике '{dictionary.Name}' уже существует.");
        }

        #endregion

        #endregion

        public void CreateOrUpdateReferenceThroughLongProcess(OMImportDataLog import, ImportFileFromExcelDto settings)
        {
	        OMEsReference reference;
	        if (settings.IsNewReference)
	        {
		        long referenceId = CreateDictionary(settings.NewReferenceName, settings.FileInfo.ValueType);
		        reference = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault();
	        }
	        else
	        {
		        reference = OMEsReference.Where(x => x.Id == settings.IdReference).SelectAll().ExecuteFirstOrDefault();
		        if (reference == null)
		        {
			        throw new Exception($"Не найден справочник с ИД {settings.IdReference}");
		        }

		        if (reference.ValueType_Code != settings.FileInfo.ValueType && !settings.DeleteOldValues)
		        {
			        throw new Exception($"Нельзя изменить тип справочника без удаления старых значений");
		        }

		        if (settings.DeleteOldValues)
		        {
			        using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
			        {
				        var referenceItems = OMEsReferenceItem.Where(x => x.ReferenceId == settings.IdReference).Execute();
				        foreach (var referenceItem in referenceItems)
				        {
					        referenceItem.Destroy();
				        }

				        reference.ValueType_Code = settings.FileInfo.ValueType;
				        reference.Save();
				        ts.Complete();
			        }
				}
			}

	        var dataFileStream = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated,
		        import.DataFileName);

            var resFileStream = ImportReferenceItemsFromExcel(dataFileStream, reference, settings.FileInfo);
            SaveResultFile(import, resFileStream);
            SendImportResultNotification(reference, DataImporterCommon.GetDataFileTitle(settings.FileInfo.FileName), import.Id);
		}

        public void UpdateReferenceFromExcel(Stream fileStream, ImportReferenceFileInfoDto fileImportInfo, long referenceId, bool deleteOldValues)
        {
            var reference = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault();
            if (reference == null)
            {
                throw new Exception($"Не найден справочник с ИД {referenceId}");
            }

            if (reference.ValueType_Code != fileImportInfo.ValueType && !deleteOldValues)
            {
                throw new Exception($"Нельзя изменить тип справочника без удаления старых значений");
            }

            if (deleteOldValues)
            {
                using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
                {
                    var referenceItems = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).Execute();
                    foreach (var referenceItem in referenceItems)
                    {
                        referenceItem.Destroy();
                    }

                    reference.ValueType_Code = fileImportInfo.ValueType;
                    reference.Save();
                    ts.Complete();
                }
            }
			PreparingForImportAndImport(fileStream, reference, fileImportInfo);
		}

        public long CreateReferenceFromExcel(Stream fileStream, ImportReferenceFileInfoDto fileImportInfo, string referenceName)
        {
	   
			long referenceId = CreateDictionary(referenceName, fileImportInfo.ValueType);
            var reference = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault();


            PreparingForImportAndImport(fileStream, reference, fileImportInfo);
			return referenceId;
        }

        public void PreparingForImportAndImport(Stream fileStream, OMEsReference reference,
	        ImportReferenceFileInfoDto fileImportInfo)
        {
	        var import = CreateDataFileImport(fileStream, fileImportInfo);
	        try
	        {
		        import.Status_Code = ImportStatus.Running;
		        import.DateStarted = DateTime.Now;
		        import.Save();

		        var resFileStream = ImportReferenceItemsFromExcel(fileStream, reference, fileImportInfo);
		        SaveResultFile(import, resFileStream);

		        import.Status_Code = ImportStatus.Completed;
		        import.Save();

                SendImportResultNotification(reference, DataImporterCommon.GetDataFileTitle(fileImportInfo.FileName), import.Id);
	        }
	        catch (Exception ex)
	        {
		        long errorId = ErrorManager.LogError(ex);
		        import.Status_Code = ImportStatus.Faulted;
		        import.DateFinished = DateTime.Now;
		        import.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
		        import.Save();

		        throw;
            }
        }

        public static OMImportDataLog CreateDataFileImport(Stream fileStream, ImportReferenceFileInfoDto fileImportInfo)
        {
	        var currentTime = DateTime.Now;
	        var fileName = $"{DataImporterCommon.GetDataFileTitle(fileImportInfo.FileName)} ({currentTime.GetString().Replace(":", "_")})";

            var import = new OMImportDataLog
	        {
		        UserId = SRDSession.GetCurrentUserId().Value,
		        DateCreated = currentTime,
		        Status_Code = ImportStatus.Added,
		        DataFileTitle = fileName,
		        FileExtension = DataImporterCommon.GetFileExtension(fileImportInfo.FileName),
		        MainRegisterId = MainRegisterId,
		        RegisterViewId = RegisterViewId
	        };
	        import.Save();

	        import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
	        FileStorageManager.Save(fileStream, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
	        import.Save();

	        return import;
        }

        private Stream ImportReferenceItemsFromExcel(Stream fileStream, OMEsReference reference,
            ImportReferenceFileInfoDto fileImportInfo)
        {
	        fileStream.Seek(0, SeekOrigin.Begin);
			var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
			var mainWorkSheet = excelFile.Worksheets[0];

			AllRows = mainWorkSheet.Rows.Count;
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };
            object locked = new object();

            var maxColumns = mainWorkSheet.CalculateMaxUsedColumns();
            var columnNames = new List<string>();
            for (var i = 0; i < maxColumns; i++)
            {
                columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
            }
            
            mainWorkSheet.Rows[0].Cells[maxColumns].SetValue("Результат сохранения");
            var dataRows = mainWorkSheet.Rows.Where(x => x.Index > 0);

            Parallel.ForEach(dataRows, options, row =>
            {
                try
                {
                    var cellValue = mainWorkSheet.Rows[row.Index]
                        .Cells[columnNames.IndexOf(fileImportInfo.ValueColumnName)];
                    var cellCalcValue = mainWorkSheet.Rows[row.Index]
                        .Cells[columnNames.IndexOf(fileImportInfo.CalcValueColumnName)];

                    if (!cellCalcValue.Value.TryParseToDecimal(out var calcValue))
                    {
                        throw new Exception(
                            $"Значение '{cellValue.Value.ToString()}' не может быть приведено к типу '{ReferenceItemCodeType.Number.GetEnumDescription()}'");
                    }

                    string valueString = null;
                    switch (fileImportInfo.ValueType)
                    {
                        case ReferenceItemCodeType.Number:
                            if (!cellValue.Value.TryParseToDecimal(out var number))
                            {
                                throw new Exception(
                                    $"Значение '{cellValue.Value.ToString()}' не может быть приведено к типу '{ReferenceItemCodeType.Number.GetEnumDescription()}'");
                            }

                            valueString = number.ToString();
                            break;
                        case ReferenceItemCodeType.String:
                            valueString = cellValue.Value.ToString();
                            break;
                        case ReferenceItemCodeType.Date:
                            if (!cellValue.Value.TryParseToDateTime(out var date))
                            {
                                throw new Exception(
                                    $"Значение '{cellValue.Value.ToString()}'не может быть приведено к типу '{ReferenceItemCodeType.Date.GetEnumDescription()}'");
                            }

                            valueString = date.ToString(CultureInfo.CurrentCulture);
                            break;
                    }

                    OMEsReferenceItem obj;
					//lock (locked)
					{
						obj = OMEsReferenceItem.Where(x => x.ReferenceId == reference.Id && x.Value == valueString).SelectAll().ExecuteFirstOrDefault();
					}

					if (obj != null)
                    {
                        obj.CalculationValue = calcValue;
						//lock (locked)
						{
                            obj.Save();
                        }
                        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение успешно обновлено");
                    }
                    else
                    {
						//lock (locked)
						{
                            new OMEsReferenceItem
                            {
                                ReferenceId = reference.Id,
                                Value = valueString,
                                CalculationValue = calcValue
                            }.Save();
                        }
                        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение успешно создано");
                    }
					lock(locked)
					{
						CurrentRow++;
					}
                }
                catch (Exception ex)
                {
                    mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"Ошибка: {ex.Message}");
                    for (int i = 0; i < maxColumns; i++)
                    {
                        mainWorkSheet.Rows[row.Index].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
                    }
                }
            });

            MemoryStream stream;
            stream = new MemoryStream();
            excelFile.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);

			return stream;
        }

        private void SaveResultFile(OMImportDataLog import, Stream streamResult)
        {
	        import.ResultFileTitle = DataImporterCommon.GetFileResultTitleFromDataTitle(import);
	        import.ResultFileName = DataImporterCommon.GetStorageResultFileName(import.Id);
	        import.DateFinished = DateTime.Now;
	        FileStorageManager.Save(streamResult, DataImporterCommon.FileStorageName, import.DateFinished.Value, import.ResultFileName);
	        import.Save();
        }

        private void SendImportResultNotification(OMEsReference reference, string fileName, long importId)
        {
            new MessageService().SendMessages(new MessageDto
            {
                Addressers =
                    new MessageAddressersDto {UserIds = new long[] {SRDSession.GetCurrentUserId().GetValueOrDefault()}},
                Subject = $"Результат загрузки данных в справочник: {reference.Name} от ({DateTime.Now.GetString()})",
                Message = $@"Загрузка файла ""{fileName}"" была завершена.
<a href=""/DataImport/DownloadImportResultFile?importId={importId}"">Скачать результат</a>
<a href=""/DataImport/DownloadImportDataFile?importId={importId}"">Скачать исходный файл</a>",
                IsUrgent = true,
                IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
            });
        }

        private long GetCountRows(Stream fileStream)
        {
	        var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
	        var mainWorkSheet = excelFile.Worksheets[0];
	        long res = mainWorkSheet.Rows.Count;
	        return res;
        }
    }
}
