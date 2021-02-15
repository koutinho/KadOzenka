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
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.Directory.ES;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling
{
	public class DictionaryService : IDictionaryService
	{
	    public int RowsCount { get; set; } = 1;
		public int CurrentRow { get; set; }

        private const long MaxRowInFileDuringImport = 1000;
        private static readonly int MainRegisterId = OMModelingDictionary.GetRegisterId();
        private static readonly string RegisterViewId = "ModelingDictionaries";

        public bool MustUseLongProcess(Stream fileStream)
        {
	        var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
	        
	        var mainWorkSheet = excelFile.Worksheets[0];

	        return mainWorkSheet.Rows.Count > MaxRowInFileDuringImport;
        }

		#region Dictionary

		public List<OMModelingDictionary> GetDictionaries()
		{
			return OMModelingDictionary.Where(x => true).OrderBy(x => x.Name).SelectAll().Execute();
		}

		public List<OMModelingDictionary> GetDictionaries(List<long> dictionaryIds, bool withItems = true)
		{
			if (dictionaryIds == null || dictionaryIds.Count == 0)
				return new List<OMModelingDictionary>();

			var dictionaries = OMModelingDictionary.Where(x => dictionaryIds.Contains(x.Id)).SelectAll().Execute();

			if (!withItems)
				return dictionaries;

			var dictionariesItems = OMModelingDictionariesValues.Where(x => dictionaryIds.Contains(x.DictionaryId)).SelectAll()
				.Execute().ToList();

			dictionaries.ForEach(dictionary =>
			{
				dictionary.ModelingDictionariesValues = dictionariesItems.Where(item => item.DictionaryId == dictionary.Id).ToList();
			});

			return dictionaries;
		}

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
	        var dictionary = GetDictionaryById(id);

	        using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
	        {
		        DeleteDictionaryValues(id);

				dictionary.Destroy();

				ts.Complete();
	        }
        }

        public decimal GetCoefficientFromStringFactor(string stringValue, OMModelingDictionary dictionary)
        {
	        if (dictionary == null)
		        return 0;

	        if (dictionary.Type_Code == ReferenceItemCodeType.String)
	        {
		        var referenceItems = dictionary.ModelingDictionariesValues ?? GetDictionaryValues(dictionary.Id);

		        return referenceItems?.FirstOrDefault(x => x.Value == stringValue)?.CalculationValue ?? 1;
	        }

	        return 0;
        }

        public decimal GetCoefficientFromDateFactor(DateTime? date, OMModelingDictionary dictionary)
        {
	        if (dictionary == null || date == null)
		        return 0;

	        if (dictionary.Type_Code == ReferenceItemCodeType.Date)
	        {
		        var referenceItems = dictionary.ModelingDictionariesValues ?? GetDictionaryValues(dictionary.Id);

		        return referenceItems?.Select(x => new
		        {
			        Key = DateTime.TryParse(x.Value, out var parsedDate) ? parsedDate : (DateTime?) null,
			        Value = x.CalculationValue
		        }).FirstOrDefault(x => x.Key == date)?.Value ?? 1;
	        }
	        return 0;
        }

        public decimal GetCoefficientFromNumberFactor(decimal? number, OMModelingDictionary dictionary)
        {
	        if (dictionary == null || number == null)
		        return number ?? 0;

	        if (dictionary.Type_Code == ReferenceItemCodeType.Number)
	        {
		        var referenceItems = dictionary.ModelingDictionariesValues ?? GetDictionaryValues(dictionary.Id);

		        return referenceItems?.Select(x => new
		        {
			        Key = decimal.TryParse(x.Value, out var res) ? res : decimal.Zero,
			        Value = x.CalculationValue
		        }).FirstOrDefault(x => x.Key == number)?.Value ?? 1;
	        }
	        return 0;
        }


		#region Support Methods

		private void ValidateDictionary(string name, long id)
        {
	        if (string.IsNullOrWhiteSpace(name))
		        throw new Exception("Невозможно создать справочник с пустым именем");

	        var isExistsDictionaryWithTheSameName = OMModelingDictionary.Where(x => x.Name == name && x.Id != id).ExecuteExists();
	        if (isExistsDictionaryWithTheSameName)
		        throw new Exception($"Справочник '{name}' уже существует");
        }

        private void DeleteDictionaryValues(long dictionaryId)
        {
	        var dictionaryValues = OMModelingDictionariesValues.Where(x => x.DictionaryId == dictionaryId).Execute();
	        dictionaryValues.ForEach(x => x.Destroy());
        }

		#endregion

		#endregion


		#region Values

		public List<OMModelingDictionariesValues> GetDictionaryValues(long dictionaryId)
		{
			return OMModelingDictionariesValues.Where(x => x.DictionaryId == dictionaryId).SelectAll().Execute();
		}

		public OMModelingDictionariesValues GetDictionaryValueById(long id)
        {
	        var dictionaryValue = OMModelingDictionariesValues.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            if (dictionaryValue == null)
		        throw new Exception($"Не найдено значение с ИД = '{id}'");

	        return dictionaryValue;
        }

        public long CreateDictionaryValue(DictionaryValueDto dto)
        {
	        var dictionary = GetDictionaryById(dto.DictionaryId);

	        ValidateDictionaryValue(dictionary, dto.Value, -1);

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

	        if (!isEmptyValue && !canParseToNumber && !canParseToDate && dictionary.Type_Code != ReferenceItemCodeType.String)
		        throw new Exception($"Значение '{value}' не может быть приведено к типу '{dictionary.Type_Code.GetEnumDescription()}'");

	        var isExistsTheSameDictionaryValue = OMModelingDictionariesValues
		        .Where(x => x.Id != dictionaryValueId && x.DictionaryId == dictionary.Id && x.Value == value)
		        .ExecuteExists();
	        if (isExistsTheSameDictionaryValue)
		        throw new Exception($"Значение '{value}' в справочнике '{dictionary.Name}' уже существует.");
        }

        #endregion

        #endregion

        #region Import from Excel

        public long CreateDictionaryFromExcel(Stream fileStream, DictionaryImportFileInfoDto fileImportInfo,
	        string newDictionaryName, OMImportDataLog import)
        {
	        var dictionaryId = CreateDictionary(newDictionaryName, fileImportInfo.ValueType);
	        var dictionary = GetDictionaryById(dictionaryId);

	        ImportDictionaryValues(fileStream, dictionary, fileImportInfo, import);

	        return dictionaryId;
        }

        public void UpdateDictionaryFromExcel(Stream fileStream, DictionaryImportFileInfoDto fileImportInfo,
	        long dictionaryId, bool deleteOldValues, OMImportDataLog import)
        {
	        var existedDictionary = GetDictionaryById(dictionaryId);
	        if (existedDictionary.Type_Code != fileImportInfo.ValueType && !deleteOldValues)
		        throw new Exception("Нельзя изменить тип справочника без удаления старых значений");

	        if (deleteOldValues)
	        {
		        using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
		        {
			        DeleteDictionaryValues(existedDictionary.Id);

			        existedDictionary.Type_Code = fileImportInfo.ValueType;
			        existedDictionary.Save();

			        ts.Complete();
		        }
	        }

	        ImportDictionaryValues(fileStream, existedDictionary, fileImportInfo, import);
        }

        public static OMImportDataLog CreateDataFileImport(Stream fileStream, string inputFileName)
        {
	        var currentTime = DateTime.Now;
	        var fileName = $"{DataImporterCommon.GetDataFileTitle(inputFileName)} ({currentTime.GetString().Replace(":", "_")})";

	        var import = new OMImportDataLog
	        {
		        UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
		        DateCreated = currentTime,
		        Status_Code = ImportStatus.Added,
		        DataFileTitle = fileName,
		        FileExtension = DataImporterCommon.GetFileExtension(inputFileName),
		        MainRegisterId = MainRegisterId,
		        RegisterViewId = RegisterViewId
	        };
	        import.Save();

	        import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
	        FileStorageManager.Save(fileStream, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
	        import.Save();

	        return import;
        }

		#region Support Methods

		private void ImportDictionaryValues(Stream fileStream, OMModelingDictionary dictionary,
			DictionaryImportFileInfoDto fileImportInfo, OMImportDataLog import)
		{
			try
			{
				import.Status_Code = ImportStatus.Running;
				import.DateStarted = DateTime.Now;
				import.Save();

				var resFileStream = ProcessDictionaryValuesInExcel(fileStream, dictionary, fileImportInfo);
				SaveResultFile(import, resFileStream);

				import.Status_Code = ImportStatus.Completed;
				import.Save();

				SendImportResultNotification(dictionary.Name, DataImporterCommon.GetDataFileTitle(fileImportInfo.FileName), import.Id);
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

		private Stream ProcessDictionaryValuesInExcel(Stream fileStream, OMModelingDictionary dictionary,
	        DictionaryImportFileInfoDto fileImportInfo)
        {
	        fileStream.Seek(0, SeekOrigin.Begin);
	        var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
	        var mainWorkSheet = excelFile.Worksheets[0];

	        RowsCount = mainWorkSheet.Rows.Count;
	        var cancelTokenSource = new CancellationTokenSource();
	        var options = new ParallelOptions
	        {
		        CancellationToken = cancelTokenSource.Token,
		        MaxDegreeOfParallelism = 1
	        };
	        var locked = new object();

			int maxColumns = DataExportCommon.GetLastUsedColumnIndex(mainWorkSheet) + 1;
			var columnNames = new List<string>();
	        for (var i = 0; i < maxColumns; i++)
	        {
		        if (mainWorkSheet.Rows[0].Cells[i].Value != null)
					columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
	        }
            
	        mainWorkSheet.Rows[0].Cells[maxColumns].SetValue("Результат сохранения");
	        var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
			var dataRows = mainWorkSheet.Rows.Where(x => x.Index > 0 && x.Index <= lastUsedRowIndex).ToList();

	        Parallel.ForEach(dataRows, options, row =>
	        {
		        try
		        {
			        var value = mainWorkSheet.Rows[row.Index].Cells[columnNames.IndexOf(fileImportInfo.ValueColumnName)].Value;
			        var calculationValue = mainWorkSheet.Rows[row.Index].Cells[columnNames.IndexOf(fileImportInfo.CalcValueColumnName)].Value;

			        if (!calculationValue.TryParseToDecimal(out var calcValue))
				        throw new Exception($"Значение '{value}' не может быть приведено к числу");

			        var valueString = GetValueFromExcelCell(fileImportInfo.ValueType, value);
			        var obj = OMModelingDictionariesValues.Where(x => x.DictionaryId == dictionary.Id && x.Value == valueString)
				        .SelectAll().ExecuteFirstOrDefault();
			        if (obj != null && obj.CalculationValue != calcValue)
			        {
				        obj.CalculationValue = calcValue;
						obj.Save();
						mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение успешно обновлено");
			        }
			        else
			        {
						new OMModelingDictionariesValues
						{
							DictionaryId = dictionary.Id,
							Value = valueString,
							CalculationValue = calcValue
						}.Save();
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
			        for (var i = 0; i < maxColumns; i++)
			        {
				        mainWorkSheet.Rows[row.Index].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
			        }
		        }
	        });

	        var stream = new MemoryStream();
	        excelFile.Save(stream, SaveOptions.XlsxDefault);
	        stream.Seek(0, SeekOrigin.Begin);

	        return stream;
        }

        private string GetValueFromExcelCell(ReferenceItemCodeType valueType, object cellValue)
        {
	        if (cellValue == null)
		        return null;

	        string valueString = null;
	        switch (valueType)
	        {
		        case ReferenceItemCodeType.Number:
			        if (!cellValue.TryParseToDecimal(out var number))
				        throw new Exception($"Значение '{cellValue}' не может быть приведено к типу 'Число'");
			        valueString = number.ToString();
			        break;
		        case ReferenceItemCodeType.String:
			        valueString = cellValue.ToString();
			        break;
		        case ReferenceItemCodeType.Date:
			        if (!cellValue.TryParseToDateTime(out var date))
				        throw new Exception($"Значение '{cellValue}'не может быть приведено к типу 'Дата'");
			        valueString = date.ToString(CultureInfo.CurrentCulture);
			        break;
	        }

	        return valueString;
        }

        private void SaveResultFile(OMImportDataLog import, Stream streamResult)
        {
	        import.ResultFileTitle = DataImporterCommon.GetFileResultTitleFromDataTitle(import);
	        import.ResultFileName = DataImporterCommon.GetStorageResultFileName(import.Id);
	        import.DateFinished = DateTime.Now;
	        FileStorageManager.Save(streamResult, DataImporterCommon.FileStorageName, import.DateFinished.Value, import.ResultFileName);
	        import.Save();
        }

        private void SendImportResultNotification(string dictionaryName, string fileName, long importId)
        {
	        new MessageService().SendMessages(new MessageDto
	        {
		        Addressers =
			        new MessageAddressersDto {UserIds = new long[] {SRDSession.GetCurrentUserId().GetValueOrDefault()}},
		        Subject = $"Результат загрузки данных в справочник: {dictionaryName} от ({DateTime.Now.GetString()})",
		        Message = $@"Загрузка файла ""{fileName}"" была завершена.
<a href=""/DataImport/DownloadImportResultFile?importId={importId}"">Скачать результат</a>
<a href=""/DataImport/DownloadImportDataFile?importId={importId}"">Скачать исходный файл</a>",
		        IsUrgent = true,
		        IsEmail = true,
		        ExpireDate = DateTime.Now.AddHours(2)
	        });
        }

		#endregion

		#endregion
    }
}
