using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks.Excel;
using Core.ErrorManagment;
using Core.Register;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using Microsoft.Practices.ObjectBuilder2;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Factors;
using ModelingBusiness.Modeling;
using ModelingBusiness.Objects.Entities;
using ModelingBusiness.Objects.Exceptions;
using ModelingBusiness.Objects.Import.Entities;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
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
		private readonly HashSet<long> _modelObjectsRegisterAttributes;

		public int MaxRowsCount;
		public int CurrentRowCount;

		private IModelDictionaryService ModelDictionaryService { get; }
		private IModelingService ModelingService { get; }
		private IModelFactorsService ModelFactorsService { get; }


		public ModelObjectsImporter()
		{
			_locker = new object();
			ModelDictionaryService = new ModelDictionaryService();
			ModelingService = new ModelingService();
			ModelFactorsService = new ModelFactorsService();

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
			_modelObjectsRegisterAttributes = RegisterCache.GetAttributeDataList(OMModelToMarketObjects.GetRegisterId()).Select(x => x.Id).ToHashSet();
		}


		public Stream ChangeObjects(ExcelFile file, ModelObjectsConstructor modelObjectsConstructor)
		{
			_log.Debug("{LoggerBasePhrase} старт. Создание - {isCreation}", LoggerBasePhrase, modelObjectsConstructor.IsCreation);

			var sheet = file.Worksheets[0];
			var maxColumnIndex = CommonSdks.ExcelFileHelper.GetLastUsedColumnIndex(sheet) + 1;
			sheet.Rows[0].Cells[maxColumnIndex].SetValue("Результат обработки");

			var objectsFromExcel = GetObjectsFromFile(sheet, modelObjectsConstructor);
			_log.Debug("{LoggerBasePhrase} в файле {RowsCount} строк", LoggerBasePhrase, MaxRowsCount);

			var nonCodedModelFactorIds = ModelFactorsService.GetGeneralModelFactors(modelObjectsConstructor.ModelId)
				.Where(x => x.MarkType != MarkType.Default).Select(x => x.AttributeId).ToHashSet();
			_log.Debug("{LoggerBasePhrase} у модели с ИД '{ModelId}' {RowsCount} некодированных факторов", LoggerBasePhrase, modelObjectsConstructor.ModelId, nonCodedModelFactorIds.Count);

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

					ProcessObjectFromExcel(importer, objectFromExcel, nonCodedModelFactorIds);

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

			new ModelFactorsService().GetGeneralModelFactors(modelObjectsConstructor.ModelId)
				.Where(x => x.IsNormalized).Select(x => x.DictionaryId.GetValueOrDefault())
				.ForEach(x => ModelDictionaryService.DeleteMarks(x));
			
			ModelingService.ResetTrainingResults(modelObjectsConstructor.ModelId, KoAlgoritmType.None);

			var stream = new MemoryStream();
			file.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			return stream;
		}

		public static void ValidateCreationParameters(long modelId, List<ColumnToAttributeMapping> columnsMapping)
		{
			var cadastralNumberAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.MarketObjectInfo);
			var priceAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.Price);
			var attributeIds = columnsMapping.Select(x => x.AttributeId).ToList();

			if (!attributeIds.Contains(cadastralNumberAttributeId) || !attributeIds.Contains(priceAttributeId))
				throw new Exception("Для создания объектов обязательно нужны: Описание ОА и Цена");
		}


		#region Support Methods

		private IModelObjectsImporter GetImporter(ModelObjectsConstructor modelObjectsConstructor,
			List<ModelObjectsFromExcelData> objectsFromExcel)
		{
			if (modelObjectsConstructor.IsCreation)
			{
				ValidateCreationParameters(modelObjectsConstructor.ModelId, modelObjectsConstructor.ColumnsMapping);

				return new ModelObjectsImporterForCreation(modelObjectsConstructor.ModelId,
					OMModelToMarketObjects.GetColumnAttributeId(x => x.ModelId), _coefficientsAttributeId,
					_isForTrainingAttributeId, _isForControlAttributeId);
			}

			return new ModelObjectsImporterForUpdating(objectsFromExcel, _isForTrainingAttributeId,
				_isForControlAttributeId, _coefficientsAttributeId, _log);
		}

		private List<ModelObjectsFromExcelData> GetObjectsFromFile(ExcelWorksheet sheet, ModelObjectsConstructor config)
		{
			MaxRowsCount = CommonSdks.ExcelFileHelper.GetLastUsedRowIndex(sheet);

			var columnsMappingWithoutPrimaryKey = config.ColumnsMapping.Where(x =>
				x.AttributeId != OMModelToMarketObjects.GetColumnAttributeId(y => y.Id)).ToList();

			var modelObjectsFromExcel = new List<ModelObjectsFromExcelData>();
			for (var i = 1; i <= MaxRowsCount; i++)
			{
				var cells = sheet.Rows[i].Cells;

				var columnsWithValues = new List<Column>();
				columnsMappingWithoutPrimaryKey.ForEach(x =>
				{
					columnsWithValues.Add(new Column
					{
						AttributeId = x.AttributeId,
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

		public void ProcessObjectFromExcel(IModelObjectsImporter importer, ModelObjectsFromExcelData objectFromExcel,
			HashSet<long> nonCodedModelFactorIds)
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

				if (_modelObjectsRegisterAttributes.Contains(column.AttributeId))
				{
					//платформе нужен referenceItemId для обновления атрибута типа Reference
					if (column.AttributeId == _unitPropertyTypeAttributeId)
					{
						var type = GetObjectTypeInfo(column.ValueToUpdate?.ToString());
						modelToMarketObject.SetAttributeValue((int)column.AttributeId, type.Str, (int)type.EnumValue);
					}
					else
					{
						modelToMarketObject.SetAttributeValue((int)column.AttributeId, column.ValueToUpdate);
					}
				}
				else
				{
					//обновление коэффициентов - единственный список атрибутов не из таблицы с объектами моделирования
					var coefficientFromDb = importer.GetCoefficient(coefficientsFromDb, column.AttributeId);
					//у некодированных факторов значение = коэффициенту, для кодированных факторов коэффициент расчитывается вместе с метками
					if (nonCodedModelFactorIds.Contains(column.AttributeId))
					{
						coefficientFromDb.Coefficient = column.ValueToUpdate.ParseToDecimalNullable();
					}
					coefficientFromDb.Value = column.ValueToUpdate.ParseToStringNullable();

					modelToMarketObject.SetAttributeValue((int)_coefficientsAttributeId, coefficientsFromDb.SerializeCoefficient());
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
