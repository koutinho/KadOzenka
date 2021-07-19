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
using Core.Register;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ModelingBusiness.Dictionaries.Entities;
using ModelingBusiness.Dictionaries.Exceptions;
using ModelingBusiness.Dictionaries.Repositories;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Dictionaries
{
	public class ModelDictionaryService : IModelDictionaryService
	{
		public int RowsCount { get; set; } = 1;
		public int CurrentRow { get; set; }
		private IModelDictionaryRepository ModelDictionaryRepository { get; }
		private IModelMarksRepository ModelMarksRepository { get; }

		private const long MaxRowInFileDuringImport = 1000;
		private static readonly int MainRegisterId = OMModelingDictionary.GetRegisterId();
		private static readonly string RegisterViewId = "ModelingDictionaries";


		public ModelDictionaryService(IModelDictionaryRepository modelDictionaryRepository = null,
			IModelMarksRepository modelMarksRepository = null)
		{
			ModelDictionaryRepository = modelDictionaryRepository ?? new ModelDictionaryRepository();
			ModelMarksRepository = modelMarksRepository ?? new ModelMarksRepository();
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

			var marks = OMModelingDictionariesValues.Where(x => dictionaryIds.Contains(x.DictionaryId)).SelectAll()
				.Execute().ToList();

			dictionaries.ForEach(dictionary =>
			{
				dictionary.ModelingDictionariesValues = marks.Where(item => item.DictionaryId == dictionary.Id).ToList();
			});

			return dictionaries;
		}

		public OMModelingDictionary GetDictionaryById(long id)
		{
			var dictionary = ModelDictionaryRepository.GetById(id, null);
			if (dictionary == null)
				throw new Exception($"Не найден справочник с ИД {id}");

			return dictionary;
		}

		public long CreateDictionary(string name, RegisterAttributeType factorType, List<long> modelDictionariesIds)
		{
			ValidateDictionary(name, modelDictionariesIds);

			var dictionaryType = MapDictionaryType(factorType);

			return new OMModelingDictionary
			{
				Name = name,
				Type_Code = dictionaryType
			}.Save();
		}

		public void UpdateDictionary(long id, string newName, List<long> modelDictionariesIds)
		{
			var dictionary = GetDictionaryById(id);
			if (dictionary.Name == newName)
				return;

			ValidateDictionary(newName, modelDictionariesIds);

			dictionary.Name = newName;
			dictionary.Save();
		}

		public int DeleteDictionary(long? id)
		{
			if (id == null)
				return 0;

			int deletedMarksCount;
			var dictionary = ModelDictionaryRepository.GetById(id.Value, null);
			using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
			{
				deletedMarksCount = DeleteMarks(id);

				dictionary?.Destroy();

				ts.Complete();
			}

			return deletedMarksCount;
		}

		public decimal GetCoefficientFromStringFactor(string stringValue, OMModelingDictionary dictionary)
		{
			if (dictionary == null)
				return 0;

			if (dictionary.Type_Code == ModelDictionaryType.String)
			{
				var marks = dictionary.ModelingDictionariesValues ?? GetMarks(dictionary.Id);

				return marks?.FirstOrDefault(x => x.Value == stringValue)?.CalculationValue ?? 1;
			}

			return 0;
		}

		public decimal GetCoefficientFromDateFactor(DateTime? date, OMModelingDictionary dictionary)
		{
			if (dictionary == null || date == null)
				return 0;

			if (dictionary.Type_Code == ModelDictionaryType.Date)
			{
				var marks = dictionary.ModelingDictionariesValues ?? GetMarks(dictionary.Id);

				return marks?.Select(x => new
				{
					Key = x.Value.TryParseToDateTime(out var parsedDate) ? parsedDate : (DateTime?)null,
					Value = x.CalculationValue
				}).FirstOrDefault(x => x.Key == date)?.Value ?? 1;
			}
			return 0;
		}

		public decimal GetCoefficientFromNumberFactor(decimal? number, OMModelingDictionary dictionary)
		{
			if (dictionary == null || number == null)
				return number ?? 0;

			//todo separate
			if (dictionary.Type_Code == ModelDictionaryType.Integer || dictionary.Type_Code == ModelDictionaryType.Decimal)
			{
				var marks = dictionary.ModelingDictionariesValues ?? GetMarks(dictionary.Id);

				return marks?.Select(x => new
				{
					Key = x.Value.TryParseToDecimal(out var res) ? res : 0,
					Value = x.CalculationValue
				}).FirstOrDefault(x => x.Key == number)?.Value ?? 1;
			}
			return 0;
		}


		#region Support Methods

		private void ValidateDictionary(string name, List<long> modelDictionariesIds)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new Exception("Нельзя создать словарь с пустым именем");

			if (modelDictionariesIds.Count > 0)
			{
				var existedDictionaries = GetDictionaries(modelDictionariesIds, false);
				if (existedDictionaries.Select(x => x.Name).Contains(name))
					throw new DictionaryAlreadyExistsException(name);
			}
		}

		private ModelDictionaryType MapDictionaryType(RegisterAttributeType factorType)
		{
			ModelDictionaryType dictionaryType;
			switch (factorType)
			{
				case RegisterAttributeType.INTEGER:
					dictionaryType = ModelDictionaryType.Integer;
					break;
				case RegisterAttributeType.DECIMAL:
					dictionaryType = ModelDictionaryType.Decimal;
					break;
				case RegisterAttributeType.BOOLEAN:
					dictionaryType = ModelDictionaryType.Boolean;
					break;
				case RegisterAttributeType.STRING:
					dictionaryType = ModelDictionaryType.String;
					break;
				case RegisterAttributeType.DATE:
					dictionaryType = ModelDictionaryType.Date;
					break;
				case RegisterAttributeType.REFERENCE:
					dictionaryType = ModelDictionaryType.Reference;
					break;
				default:
					throw new ArgumentOutOfRangeException(
						$"Для фактора с типом '{factorType.GetEnumDescription()}' нельзя создать словарь меток");
			}

			return dictionaryType;
		}

		#endregion

		#endregion


		#region Marks

		public List<OMModelingDictionariesValues> GetMarks(long dictionaryId)
		{
			return OMModelingDictionariesValues.Where(x => x.DictionaryId == dictionaryId).SelectAll().Execute();
		}

		public List<OMModelingDictionariesValues> GetMarks(List<long?> dictionaryIds)
		{
			if (dictionaryIds.IsEmpty())
				return new List<OMModelingDictionariesValues>();

			return OMModelingDictionariesValues.Where(x => dictionaryIds.Contains(x.DictionaryId)).SelectAll().Execute();
		}

		public OMModelingDictionariesValues GetMark(long id)
		{
			var mark = OMModelingDictionariesValues.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			if (mark == null)
				throw new Exception($"Не найдено значение с ИД = '{id}'");

			return mark;
		}

		public long CreateMark(DictionaryMarkDto dto)
		{
			var dictionary = GetDictionaryById(dto.DictionaryId);

			ValidateSingleMark(dictionary, dto);

			var mark = new OMModelingDictionariesValues
			{
				DictionaryId = dto.DictionaryId,
				Value = dto.Value,
				CalculationValue = dto.CalculationValue.GetValueOrDefault()
			};

			return ModelMarksRepository.Save(mark);
		}

		public void UpdateMark(DictionaryMarkDto dto)
		{
			var mark = GetMark(dto.Id);

			var dictionary = GetDictionaryById(dto.DictionaryId);

			ValidateSingleMark(dictionary, dto);

			mark.Value = dto.Value;
			mark.CalculationValue = dto.CalculationValue.GetValueOrDefault();
			mark.Save();
		}

		public void DeleteMark(long markId)
		{
			var mark = GetMark(markId);

			mark.Destroy();
		}

		public int DeleteMarks(long? dictionaryId)
		{
			if (dictionaryId.GetValueOrDefault() == 0)
				return 0;

			var sql = $"delete from ko_modeling_dictionaries_values where dictionary_id = {dictionaryId}";

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			return DBMngr.Main.ExecuteNonQuery(command);
		}


		#region Support Methods

		private void ValidateSingleMark(OMModelingDictionary dictionary, DictionaryMarkDto mark)
		{
			ValidateMark(dictionary.Type_Code, mark.Value, mark.CalculationValue);
			
			var isTheSameMarkExists = ModelMarksRepository.IsTheSameMarkExists(dictionary.Id, mark.Id, mark.Value);
			if (isTheSameMarkExists)
				throw new TheSameMarkExistsException(dictionary.Name, mark.Value);
		}

		private void ValidateMark(ModelDictionaryType dictionaryType, string value, decimal? calculationValue)
		{
			var isEmptyValue = string.IsNullOrWhiteSpace(value);
			if (isEmptyValue)
				throw new EmptyMarkValueException();
			if (calculationValue == null)
				throw new EmptyMarkCalculationValueException();

			var canParseToNumber = (dictionaryType == ModelDictionaryType.Integer || dictionaryType == ModelDictionaryType.Decimal) && value.TryParseToDecimal(out _);
			var canParseToDate = dictionaryType == ModelDictionaryType.Date && value.TryParseToDateTime(out _);
			var canParseToBoolean = dictionaryType == ModelDictionaryType.Boolean && value.TryParseToBoolean(out _);

			if (!canParseToNumber && !canParseToDate && !canParseToBoolean && dictionaryType != ModelDictionaryType.String)
				throw new MarkValueConvertingException(value, dictionaryType);
		}

		#endregion

		#endregion


		#region Import from Excel

		public bool MustUseLongProcess(Stream fileStream)
		{
			fileStream.Seek(0, SeekOrigin.Begin);

			var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);

			var mainWorkSheet = excelFile.Worksheets[0];

			return mainWorkSheet.Rows.Count > MaxRowInFileDuringImport;
		}

		public void UpdateDictionaryFromExcel(OMImportDataLog import, DictionaryImportFileInfoDto fileImportInfo,
			long dictionaryId, bool isDeleteExistedMarks)
		{
			var dictionary = GetDictionaryById(dictionaryId);

			if (isDeleteExistedMarks)
			{
				DeleteMarks(dictionary.Id);
			}

			ImportDictionaryMarks(import, dictionary, fileImportInfo);
		}

		public OMImportDataLog CreateDataFileImport(Stream fileStream, string inputFileName)
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

		private void ImportDictionaryMarks(OMImportDataLog import, OMModelingDictionary dictionary,
			DictionaryImportFileInfoDto fileImportInfo)
		{
			try
			{
				import.Status_Code = ImportStatus.Running;
				import.DateStarted = DateTime.Now;
				import.Save();

				var fileStream = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated,
					import.DataFileName);
				var resultFileStream = ProcessDictionaryMarksInExcel(fileStream, dictionary, fileImportInfo);
				SaveResultFile(import, resultFileStream);

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

		private Stream ProcessDictionaryMarksInExcel(Stream fileStream, OMModelingDictionary dictionary,
			DictionaryImportFileInfoDto fileImportInfo)
		{
			fileStream.Seek(0, SeekOrigin.Begin);
			var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
			var mainWorkSheet = excelFile.Worksheets[0];
			RowsCount = CommonSdks.DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);

			var locker = new object();
			var cancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 5
			};

			var existedMarks = GetMarks(dictionary.Id);
			var columnIndexes = GetColumnIndexes(fileImportInfo, mainWorkSheet);
			mainWorkSheet.Rows[0].Cells[columnIndexes.ResultIndex].SetValue("Результат сохранения");
			Parallel.ForEach(mainWorkSheet.Rows.Where(x => x.Index > 0 && x.Index <= RowsCount).ToList(), options, row =>
			{
				try
				{
					var valueFromCell = row.Cells[columnIndexes.ValueIndex].Value;
					var calculationValueFromCell = row.Cells[columnIndexes.CalculationValueIndex].Value;

					if (!calculationValueFromCell.TryParseToDecimal(out var calculationValue))
						throw new Exception($"Значение '{calculationValueFromCell}' не может быть приведено к числу");

					var valueString = GetValueFromExcelCell(dictionary.Type_Code, valueFromCell);
					
					ValidateMark(dictionary.Type_Code, valueString, calculationValue);
					
					var currentMark = existedMarks.FirstOrDefault(x => x.Value == valueString);
					if (currentMark != null)
					{
						if (currentMark.CalculationValue != calculationValue)
						{
							currentMark.CalculationValue = calculationValue;
							currentMark.Save();

							SetImportResultMessage(row, columnIndexes.ResultIndex, "Значение успешно обновлено", locker);
						}
						else
						{
							SetImportResultMessage(row, columnIndexes.ResultIndex, "Значение было добавлено ранее", locker);
						}
					}
					else
					{
						new OMModelingDictionariesValues
						{
							DictionaryId = dictionary.Id,
							Value = valueString,
							CalculationValue = calculationValue
						}.Save();

						SetImportResultMessage(row, columnIndexes.ResultIndex, "Значение успешно создано", locker);
					}

					lock (locker)
					{
						CurrentRow++;
					}
				}
				catch (Exception ex)
				{
					SetImportResultMessage(row, columnIndexes.ResultIndex, $"Ошибка: {ex.Message}", locker);
					lock (locker)
					{
						for (var i = 0; i < columnIndexes.MaxColumnsCount; i++)
						{
							row.Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
						}
					}
				}
			});

			var stream = new MemoryStream();
			excelFile.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			return stream;
		}

		private ColumnIndexes GetColumnIndexes(DictionaryImportFileInfoDto fileImportInfo, ExcelWorksheet mainWorkSheet)
		{
			var maxColumnsCount = CommonSdks.DataExportCommon.GetLastUsedColumnIndex(mainWorkSheet);
			var resultColumnIndex = maxColumnsCount + 1;
			var valueIndex = -1;
			var calculationValueIndex = -1;
			for (var i = 0; i <= maxColumnsCount; i++)
			{
				if (mainWorkSheet.Rows[0].Cells[i].Value == null)
					continue;
				var columnName = mainWorkSheet.Rows[0].Cells[i].Value.ToString();

				if (columnName == fileImportInfo.ValueColumnName)
				{
					valueIndex = i;
				}

				if (columnName == fileImportInfo.CalcValueColumnName)
				{
					calculationValueIndex = i;
				}
			}

			if (valueIndex == -1 || calculationValueIndex == -1)
				throw new Exception("Не удалось определить индексы колонок в файле");

			return new ColumnIndexes
			{
				MaxColumnsCount = maxColumnsCount,
				ResultIndex = resultColumnIndex,
				ValueIndex = valueIndex,
				CalculationValueIndex = calculationValueIndex
			};
		}

		private void SetImportResultMessage(ExcelRow row, int columnIndex, string value, object locker)
		{
			lock (locker)
			{
				row.Cells[columnIndex].SetValue(value);
			}
		}

		private string GetValueFromExcelCell(ModelDictionaryType valueType, object cellValue)
		{
			if (cellValue == null)
				return null;

			string valueString = null;
			switch (valueType)
			{
				case ModelDictionaryType.Integer:
				case ModelDictionaryType.Decimal:
					if (!cellValue.TryParseToDecimal(out var number))
						throw new Exception($"Значение '{cellValue}' не может быть приведено к типу 'Число'");
					valueString = number.ToString();
					break;
				case ModelDictionaryType.String:
					valueString = cellValue.ToString();
					break;
				case ModelDictionaryType.Date:
					if (!cellValue.TryParseToDateTime(out var date))
						throw new Exception($"Значение '{cellValue}'не может быть приведено к типу 'Дата'");
					valueString = date.ToString(CultureInfo.CurrentCulture);
					break;
				case ModelDictionaryType.Boolean:
					if (!cellValue.TryParseToBoolean(out var boolValue))
						throw new Exception($"Значение '{cellValue}'не может быть приведено к типу 'Логическое значение'");
					valueString = boolValue.ToString(CultureInfo.CurrentCulture);
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
					new MessageAddressersDto { UserIds = new long[] { SRDSession.GetCurrentUserId().GetValueOrDefault() } },
				Subject = $"Результат загрузки данных в справочник: {dictionaryName} от ({DateTime.Now.GetString()})",
				Message = $@"Загрузка файла ""{fileName}"" была завершена.
					<a href=""/DataImport/DownloadImportResultFile?importId={importId}"">Скачать результат</a>
					<a href=""/DataImport/DownloadImportDataFile?importId={importId}"">Скачать исходный файл</a>",
				IsUrgent = true,
				IsEmail = true,
				ExpireDate = DateTime.Now.AddHours(2)
			});
		}

		private class ColumnIndexes
		{
			public int MaxColumnsCount { get; set; }
			public int ResultIndex { get; set; }
			public int ValueIndex { get; set; }
			public int CalculationValueIndex { get; set; }
		}


		#endregion

		#endregion
	}
}
