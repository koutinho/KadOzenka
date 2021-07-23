using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using ModelingBusiness.Objects.Entities;
using ModelingBusiness.Objects.Exceptions;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Modeling;
using Serilog;

namespace ModelingBusiness.Objects.Import
{
	public interface IBaseModelObjectsImporter
	{
		Stream ChangeObjects(ExcelFile file, ModelObjectsConstructor modelObjectsConstructor);
	}

	public class ModelObjectsImporter : IBaseModelObjectsImporter
	{
		private const string LoggerBasePhrase = "Импорт объектов моделирования:";
		private readonly ILogger _log = Log.ForContext<ModelObjectsImporter>();
		private readonly object _locker;
		private readonly long _coefficientsAttributeId;
		private readonly long _idAttributeId;
		private readonly long _unitPropertyTypeAttributeId;
		private readonly long _isForTrainingAttributeId;
		private readonly long _isForControlAttributeId;
		private readonly List<ObjectTypeInfo> _objectTypes;

		public int MaxRowsCount;
		public int CurrentRowCount;


		public ModelObjectsImporter()
		{
			_locker = new object();

			_objectTypes = System.Enum.GetValues(typeof(PropertyTypes)).Cast<PropertyTypes>()
				.Select(x => new ObjectTypeInfo
				{
					EnumValue = x,
					Str = x.GetEnumDescription()
				}).ToList();

			//вынесено отдельно, чтобы избежать рефлексии в цикле
			_coefficientsAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.Coefficients);
			_idAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.Id);
			_unitPropertyTypeAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.UnitPropertyType_Code);
			_isForTrainingAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForTraining);
			_isForControlAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForControl);
		}

		public ModelObjectsImporter(long coefficientsAttributeId)
		{

		}


		public Stream ChangeObjects(ExcelFile file, ModelObjectsConstructor modelObjectsConstructor)
		{
			_log.Debug("{LoggerBasePhrase} старт. Создание - {isCreation}", LoggerBasePhrase, modelObjectsConstructor.IsCreation);

			var sheet = file.Worksheets[0];
			var maxColumnIndex = CommonSdks.ExcelFileHelper.GetLastUsedColumnIndex(sheet) + 1;
			sheet.Rows[0].Cells[maxColumnIndex].SetValue("Результат обработки");

			var objectsFromExcel = GetObjectsFromFile(sheet, modelObjectsConstructor);
			_log.Debug("{LoggerBasePhrase} в файле {RowsCount} строк", LoggerBasePhrase, MaxRowsCount);

			var importer = GetImporter(modelObjectsConstructor, objectsFromExcel);

			var cancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 20
			};
			Parallel.ForEach(objectsFromExcel, options, objectFromExcel =>
			{
				try
				{
					Interlocked.Increment(ref CurrentRowCount);
					if (CurrentRowCount % 1000 == 0)
						_log.Debug("{LoggerBasePhrase} обрабатывается объект №{CurrentCount} из {MaxCount}", LoggerBasePhrase, CurrentRowCount, MaxRowsCount);

					ProcessObjectFromExcel(importer, objectFromExcel);

					lock (_locker)
					{
						sheet.Rows[objectFromExcel.RowIndexInFile].Cells[maxColumnIndex].SetValue("Обработано");
					}
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					lock (_locker)
					{
						CommonSdks.ExcelFileHelper.AddErrorCell(sheet, objectFromExcel.RowIndexInFile, maxColumnIndex,
							$"Ошибка: {ex.Message} (подробно в журнале №{errorId})");
					}
				}
			});

			var stream = new MemoryStream();
			file.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			return stream;
		}

		public static void ValidateCreationParameters(long? modelId, List<ColumnToAttributeMapping> columnsMapping)
		{
			if (modelId == null)
				throw new Exception("Не передан ИД модели для создания объектов моделирования");

			var cadastralNumberAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.CadastralNumber);
			var priceAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.Price);
			var attributeIds = columnsMapping.Where(x => long.TryParse(x.AttributeId, out _)).Select(x => long.Parse(x.AttributeId)).ToList();

			if (!attributeIds.Contains(cadastralNumberAttributeId) || !attributeIds.Contains(priceAttributeId))
				throw new Exception("Для создания объектов обязательно нужны: Кадастровый номер и Цена ОА");
		}


		#region Support Methods

		private IModelObjectsImporter GetImporter(ModelObjectsConstructor modelObjectsConstructor,
			List<ModelObjectsFromExcelData> objectsFromExcel)
		{
			if (modelObjectsConstructor.IsCreation)
			{
				ValidateCreationParameters(modelObjectsConstructor.ModelId, modelObjectsConstructor.ColumnsMapping);

				return new ModelObjectsImporterForCreation(modelObjectsConstructor.ModelId.Value,
					OMModelToMarketObjects.GetColumnAttributeId(x => x.ModelId), _coefficientsAttributeId);
			}

			return new ModelObjectsImporterForUpdating(objectsFromExcel, _isForTrainingAttributeId,
				_isForControlAttributeId, _coefficientsAttributeId, _log);
		}

		private List<ModelObjectsFromExcelData> GetObjectsFromFile(ExcelWorksheet sheet, ModelObjectsConstructor config)
		{
			MaxRowsCount = CommonSdks.ExcelFileHelper.GetLastUsedRowIndex(sheet);

			var columnsMappingWithoutPrimaryKey = config.ColumnsMapping.Where(x =>
				x.AttributeId != OMModelToMarketObjects.GetColumnAttributeId(y => y.Id).ToString()).ToList();

			var modelObjectsFromExcel = new List<ModelObjectsFromExcelData>();
			for (var i = 1; i <= MaxRowsCount; i++)
			{
				var cells = sheet.Rows[i].Cells;

				var columnsWithValues = new List<Column>();
				columnsMappingWithoutPrimaryKey.ForEach(x =>
				{
					long.TryParse(x.AttributeId, out var attributeNumber);
					columnsWithValues.Add(new Column
					{
						AttributeStr = x.AttributeId,
						AttributeId = attributeNumber,
						ValueToUpdate = cells[x.ColumnIndex].Value
					});
				});

				modelObjectsFromExcel.Add(new ModelObjectsFromExcelData
				{
					Id = config.IdColumnIndex == null ? null : cells[config.IdColumnIndex.Value].Value.ParseToLongNullable(),
					RowIndexInFile = i,
					Columns = columnsWithValues
				});
			}

			return modelObjectsFromExcel;
		}

		public void ProcessObjectFromExcel(IModelObjectsImporter importer, ModelObjectsFromExcelData objectFromExcel)
		{
			var modelToMarketObject = importer.CreateObject(objectFromExcel.Id);

			var coefficientsStr = modelToMarketObject.AttributesValues[_coefficientsAttributeId].Value?.ToString();
			var coefficientsFromDb = string.IsNullOrWhiteSpace(coefficientsStr)
				? new List<CoefficientForObject>()
				: JsonConvert.DeserializeObject<List<CoefficientForObject>>(coefficientsStr);

			objectFromExcel.Columns.ForEach(column =>
			{
				if (column.AttributeId == _idAttributeId)
					return;

				if (column.AttributeId != 0)
				{
					//платформе нужен referenceItemId для обновления атрибута типа Reference
					if (column.AttributeId == _unitPropertyTypeAttributeId)
					{
						var type = GetObjectTypeInfo(column.ValueToUpdate?.ToString());
						modelToMarketObject.SetAttributeValue((int) column.AttributeId, type.Str, (int) type.EnumValue);
					}
					else
					{
						modelToMarketObject.SetAttributeValue((int) column.AttributeId, column.ValueToUpdate);
					}
				}
				else
				{
					//из нормализованного атрибута вида ххх_1 вытаскиваем ххх (ИД)
					var match = Regex.Match(column.AttributeStr, @$"^[^{Consts.PrefixForFactor}]*");
					var attributeIdStr = match.Groups[0].Value;
					long.TryParse(attributeIdStr, out var attributeId);

					var coefficientFromDb = importer.GetCoefficient(coefficientsFromDb, attributeId);

					//если фактор нормализованный
					if (column.AttributeStr.Contains(Consts.PrefixForValueInNormalizedColumn))
					{
						coefficientFromDb.Value = column.ValueToUpdate.ParseToStringNullable();
					}
					else if (column.AttributeStr.Contains(Consts.PrefixForCoefficientInNormalizedColumn))
					{
						coefficientFromDb.Coefficient = column.ValueToUpdate.ParseToDecimalNullable();
					}
					//если фактор не нормализованный
					else
					{
						coefficientFromDb.Value = column.ValueToUpdate.ParseToStringNullable();
						coefficientFromDb.Coefficient = column.ValueToUpdate.ParseToDecimalNullable();
					}

					modelToMarketObject.SetAttributeValue((int) _coefficientsAttributeId, coefficientsFromDb.SerializeCoefficient());
				}
			});

			if (IsInValidObject(modelToMarketObject))
				throw new ObjectIsForControlAndForTrainingAtTheSameTimeException();

			RegisterStorage.Save(modelToMarketObject);
		}

		private ObjectTypeInfo GetObjectTypeInfo(string typeFromFile)
		{
			if (string.IsNullOrWhiteSpace(typeFromFile))
				throw new Exception("Не указан тип объекта");

			var enumInfo = _objectTypes.FirstOrDefault(x => x.Str == typeFromFile);
			if (enumInfo == null)
				throw new Exception($"Указан недопустимый тип объекта '{typeFromFile}'");

			return enumInfo;
		}

		private bool IsInValidObject(RegisterObject modelToMarketObject)
		{
			var isForTraining = modelToMarketObject.AttributesValues[_isForTrainingAttributeId].Value?.ParseToBooleanNullable();
			var isForControl = modelToMarketObject.AttributesValues[_isForControlAttributeId].Value?.ParseToBooleanNullable();

			return isForTraining.GetValueOrDefault() && isForControl.GetValueOrDefault();
		}
		
		#endregion


		#region Entities

		private class ObjectTypeInfo
		{
			public PropertyTypes EnumValue { get; init; }
			public string Str { get; init; }
		}

		#endregion
	}
}
