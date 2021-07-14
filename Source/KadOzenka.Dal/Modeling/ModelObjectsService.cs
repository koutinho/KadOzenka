using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.KO;
using ObjectModel.Modeling;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.Modeling.Entities;
using KadOzenka.Dal.Modeling.Repositories;
using Serilog;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using DevExpress.CodeParser;
using KadOzenka.Dal.DataImport.DataImportKoFactory.ImportKoFactoryCommon;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ObjectBuilder2;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Modeling
{
	public class ModelObjectsService : IModelObjectsService
	{
		private readonly ILogger _log = Log.ForContext<ModelObjectsService>();
		private IModelObjectsRepository ModelObjectsRepository { get; }
		private object _locker;

		#region Информация для выгрузки/загрузки объектов моделирования

		private const int IdColumnIndex = 0;
		private const int IsForTrainingColumnIndex = 1;
		private const int IsForControlColumnIndex = 2;
		private const int IsExcludedColumnIndex = 3;
		private const int CadastralNumberColumnIndex = 4;
		private const int ObjectTypeColumnIndex = 5;
		private const int PriceColumnIndex = 6;
		private const int PredictedPriceColumnIndex = 7;
		private const int DeviationFromPredictablePriceColumnIndex = 8;
		private int _maxNumberOfColumns = 9;
		public string PrefixForFactor => "_";
		public string PrefixForValueInNormalizedColumn => $"{PrefixForFactor}1";
		public string PrefixForCoefficientInNormalizedColumn => $"{PrefixForFactor}2";
		public int MaxRowsCountInFileForUpdating { get; set; } = 1;
		public int CurrentRowIndexInFileForUpdating { get; set; }

		#endregion

		public ModelObjectsService(IModelObjectsRepository modelObjectsRepository = null)
		{
			ModelObjectsRepository = modelObjectsRepository ?? new ModelObjectsRepository();
			_locker = new object();
		}




        public List<OMModelToMarketObjects> GetModelObjects(long modelId)
		{
			return OMModelToMarketObjects.Where(x => x.ModelId == modelId)
                .OrderBy(x => x.CadastralNumber)
                .SelectAll()
                .Execute();
		}

        public int DestroyModelMarketObjects(OMModel model)
        {
	        var sql = $"delete from MODELING_MODEL_TO_MARKET_OBJECTS where MODEL_ID = {model.Id}";
	        var command = DBMngr.Main.GetSqlStringCommand(sql);
	        var deletedModelObjectsCount = DBMngr.Main.ExecuteNonQuery(command);

			model.ObjectsStatistic = null;
	        model.Save();

	        return deletedModelObjectsCount;
        }

        public void ChangeObjectsStatusInCalculation(List<ModelMarketObjectRelationDto> objects)
		{
			var ids = objects.Select(x => x.Id).ToList();
            if (ids.Count == 0)
                return;

            var objectsFromDb = OMModelToMarketObjects.Where(x => ids.Contains(x.Id)).Select(x => new
            {
                x.IsExcluded,
                x.IsForTraining,
                x.IsForControl
            }).Execute();
            objects.ForEach(obj =>
            {
                var objFromDb = objectsFromDb.FirstOrDefault(x => x.Id == obj.Id);
                if (objFromDb == null)
                    return;

                if (objFromDb.IsExcluded.GetValueOrDefault() != obj.IsExcluded ||
                    objFromDb.IsForTraining.GetValueOrDefault() != obj.IsForTraining ||
                    objFromDb.IsForControl.GetValueOrDefault() != obj.IsForControl)
                {
	                if (obj.IsForTraining && obj.IsForControl)
		                throw new Exception($"Объект с КН '{obj.CadastralNumber}' не может одновременно быть и в обучающей, и в контрольной выборках");

	                objFromDb.IsExcluded = obj.IsExcluded;
	                objFromDb.IsForTraining = obj.IsForTraining;
	                objFromDb.IsForControl = obj.IsForControl;
	                objFromDb.Save();
                }
            });
        }

        public Stream ExportMarketObjectsToExcel(long modelId, List<OMModelFactor> factors)
        {
	        //var model = OMModel.Where(x => x.Id == modelId).Select(x => x.A0ForExponential).ExecuteFirstOrDefault();
	        //if (model == null)
	        //    throw new Exception($"Не найдена модель с ИД '{modelId}'");
	        var excelTemplate = new ExcelFile();
            var mainWorkSheet = excelTemplate.Worksheets.Add("Объекты модели");

            var groupedFactors = GetFactorColumnsForModelObjectsInFile(factors);
            var columnHeaders = new object[_maxNumberOfColumns];
            columnHeaders[IdColumnIndex] = "ИД";
            columnHeaders[IsForTrainingColumnIndex] = "Признак выбора аналога в обучающую модель";
            columnHeaders[IsForControlColumnIndex] = "Признак выбора аналога в контрольную модель";
            columnHeaders[IsExcludedColumnIndex] = "Признак исключения из расчета";
            columnHeaders[CadastralNumberColumnIndex] = "Кадастровый номер";
            columnHeaders[ObjectTypeColumnIndex] = "Тип объекта";
            columnHeaders[PriceColumnIndex] = "Цена";
            columnHeaders[PredictedPriceColumnIndex] = "Спрогнозированная цена";
            columnHeaders[DeviationFromPredictablePriceColumnIndex] = "Отклонение цены от прогнозной, %";
            groupedFactors.SelectMany(x => x.ToList()).ForEach(x => columnHeaders[x.ColumnIndex] = x.Name);
            //TODO код закомментирован по просьбе заказчиков, в дальнейшем он будет использоваться
            //columnHeaders.AddRange(new List<string>{ "МС", "%" });
            DataExportCommon.AddRow(mainWorkSheet, 0, columnHeaders);

			var rowCounter = 1;
			var marketObjects = GetModelObjects(modelId);
			marketObjects.ForEach(obj =>
			{
				var values = new object[_maxNumberOfColumns];
				values[IdColumnIndex] = obj.Id;
				values[IsForTrainingColumnIndex] = obj.IsForTraining.GetValueOrDefault();
				values[IsForControlColumnIndex] = obj.IsForControl.GetValueOrDefault();
				values[IsExcludedColumnIndex] = obj.IsExcluded.GetValueOrDefault();
				values[CadastralNumberColumnIndex] = obj.CadastralNumber;
				values[ObjectTypeColumnIndex] = obj.UnitPropertyType;
				values[PriceColumnIndex] = obj.Price;
				values[PredictedPriceColumnIndex] = obj.PriceFromModel;
				values[DeviationFromPredictablePriceColumnIndex] = CalculatePercent(obj.PriceFromModel, obj.Price);

				var coefficients = obj.DeserializeCoefficient();
				groupedFactors.ForEach(factor =>
				{
					var coefficient = coefficients.FirstOrDefault(x => x.AttributeId == factor.Key);
					factor.ToList().ForEach(x =>
					{
						if (x.IsColumnWithValue)
						{
							values[x.ColumnIndex] = coefficient?.Value;
						}
						else
						{
							values[x.ColumnIndex] = coefficient?.Coefficient;
						}
					});
				});

				//var calculationParameters = GetModelCalculationParameters(model.A0ForExponentialTypeInPreviousTour, obj.Price,
				// factors, coefficients, obj.CadastralNumber); 

				//values.Add(calculationParameters.ModelingPrice); 
				//values.Add(calculationParameters.Percent);

				DataExportCommon.AddRow(mainWorkSheet, rowCounter++, values);
			});

			var stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public Stream ChangeModelObjects(ExcelFile file, ModelObjectsConstructor modelObjectsConstructor)
        {
	        var loggerBasePhrase = "Обновление объектов моделирования:";
			_log.Debug("{LoggerBasePhrase} старт", loggerBasePhrase);

			var sheet = file.Worksheets[0];
			var maxColumnIndex = DataExportCommon.GetLastUsedColumnIndex(sheet) + 1;
			sheet.Rows[0].Cells[maxColumnIndex].SetValue("Результат обработки");

			var objectsFromExcel = GetInfoFromFile(sheet, modelObjectsConstructor.ColumnsMapping);
			_log.Debug("{LoggerBasePhrase} в файле {RowsCount} строк", loggerBasePhrase, MaxRowsCountInFileForUpdating);

			var modelObjectsIds = objectsFromExcel.Select(x => x.Id).ToList();
			if (modelObjectsIds.Count == 0)
				throw new Exception("В файле не было найдено ИД объектов");

			var objectsFromDb = OMModelToMarketObjects.Where(x => modelObjectsIds.Contains(x.Id)).SelectAll().Execute();
			_log.Debug("{LoggerBasePhrase} найдено {ModelObjectsCount} объектов в БД", loggerBasePhrase, objectsFromDb.Count);

			var objectTypes = System.Enum.GetValues(typeof(PropertyTypes)).Cast<PropertyTypes>()
				.Select(x => new ObjectTypeInfo
				{
					EnumValue = x,
					Str = x.GetEnumDescription()
				}).ToList();

			var cancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 100
			};
			Parallel.ForEach(objectsFromExcel, options, objectFromExcel =>
			{
				try
				{
					lock (_locker)
					{
						CurrentRowIndexInFileForUpdating++;
					}
					if(CurrentRowIndexInFileForUpdating % 1000 == 0)
						_log.Debug("{LoggerBasePhrase} обрабатывается объект № {CurrentCount}", loggerBasePhrase, CurrentRowIndexInFileForUpdating);

					var objectFromDb = objectsFromDb.FirstOrDefault(o => o.Id == objectFromExcel.Id);
					if (objectFromDb == null)
					{
						ImportKoCommon.AddErrorCell(sheet, objectFromExcel.RowIndexInFile, maxColumnIndex, $"Объект с ИД {objectFromExcel.Id} не найден в БД");
						return;
					}

					var coefficientsFromDb = objectFromDb.DeserializeCoefficient();

					bool isForControl = false, isForTraining = false;
					var omModelToMarketObject = new RegisterObject(OMModelToMarketObjects.GetRegisterId(), (int)objectFromDb.Id);
					objectFromExcel.Columns.ForEach(column =>
					{
						if (column.AttributeId == OMModelToMarketObjects.GetColumnAttributeId(x => x.Id))
							return;

						if (column.AttributeId != 0)
						{
							if (column.AttributeId == OMModelToMarketObjects.GetColumnAttributeId(x => x.UnitPropertyType_Code))
							{
								var type = GetObjectTypeInfo(objectTypes, column.ValueToUpdate?.ToString());
								omModelToMarketObject.SetAttributeValue((int)column.AttributeId, type.Str, (int)type.EnumValue);
								return;
							}

							omModelToMarketObject.SetAttributeValue((int)column.AttributeId, column.ValueToUpdate);

							if (column.AttributeId == OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForControl))
							{
								isForControl = column.ValueToUpdate?.ToString()?.ToLower() == "да";
							}
							if (column.AttributeId == OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForTraining))
							{
								isForTraining = column.ValueToUpdate?.ToString()?.ToLower() == "да";
							}
						}
						else
						{
							//из нормализованного атрибута вида ххх_1 вытаскиваем ххх (ИД)
							var match = Regex.Match(column.AttributeStr, @$"^[^{PrefixForFactor}]*");
							var attributeIdStr = match.Groups[0].Value;
							long.TryParse(attributeIdStr, out var attributeId);

							var coefficientFromDb = coefficientsFromDb.FirstOrDefault(с => с.AttributeId == attributeId);
							if (coefficientFromDb == null)
								throw new Exception($"У объекта с ИД {objectFromExcel.Id} не найден атрибут '{RegisterCache.GetAttributeData(attributeId).Name}'");

							//если фактор нормализованный
							if (column.AttributeStr.Contains(PrefixForValueInNormalizedColumn))
							{
								coefficientFromDb.Value = column.ValueToUpdate.ParseToStringNullable();
							}
							else if(column.AttributeStr.Contains(PrefixForCoefficientInNormalizedColumn))
							{
								coefficientFromDb.Coefficient = column.ValueToUpdate.ParseToDecimalNullable();
							}
							//если фактор не нормализованный
							else
							{
								coefficientFromDb.Value = column.ValueToUpdate.ParseToStringNullable();
								coefficientFromDb.Coefficient = column.ValueToUpdate.ParseToDecimalNullable();
							}

							omModelToMarketObject.SetAttributeValue(
								(int)OMModelToMarketObjects.GetColumnAttributeId(c => c.Coefficients),
								coefficientsFromDb.SerializeCoefficient());
						}
					});

					if ((isForControl && objectFromDb.IsForTraining.GetValueOrDefault()) ||
						(isForTraining && objectFromDb.IsForControl.GetValueOrDefault()) ||
						(isForTraining && isForControl))
						throw new Exception("Объект не может быть в контрольной и обучающей выборках одновременно");

					RegisterStorage.Save(omModelToMarketObject);
					sheet.Rows[objectFromExcel.RowIndexInFile].Cells[maxColumnIndex].SetValue("Обработано");
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					ImportKoCommon.AddErrorCell(sheet, objectFromExcel.RowIndexInFile, maxColumnIndex,
						$"Ошибка: {ex.Message} (подробно в журнале №{errorId})");
				}
			});

			var stream = new MemoryStream();
			file.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			return stream;
        }

        private ObjectTypeInfo GetObjectTypeInfo(List<ObjectTypeInfo> descriptions, string typeFromFile)
		{
			if (string.IsNullOrWhiteSpace(typeFromFile))
				throw new Exception("Не указан тип объекта");

			var enumInfo = descriptions.FirstOrDefault(x => x.Str == typeFromFile);
			if (enumInfo == null)
				throw new Exception("Не указан тип объекта");

			return enumInfo;
		}

		public ModelObjectsCalculationParameters GetModelCalculationParameters(decimal? a0, decimal? objectPrice,
	        List<OMModelFactor> factors, List<CoefficientForObject> objectCoefficients, string cadastralNumber)
        {
	        try
	        {
		        decimal modelingPriceCounter = 0;
		        foreach (var factor in factors)
		        {
			        var objectCoefficient = objectCoefficients?.FirstOrDefault(x =>
				        x.AttributeId == factor.FactorId && !string.IsNullOrWhiteSpace(x.Value));

			        var metka = objectCoefficient?.Coefficient;

			        modelingPriceCounter = modelingPriceCounter +
			                               (metka.GetValueOrDefault(1) * factor.PreviousWeight.GetValueOrDefault(1));
		        }

		        var resultModelingPrice = (decimal?) Math.Exp((double) (a0.GetValueOrDefault() + modelingPriceCounter));
		        var modelingPrice = Math.Round(resultModelingPrice.GetValueOrDefault(), 2);
		        decimal? percent = null;
		        if (objectPrice.GetValueOrDefault() != 1)
		        {
			        percent = CalculatePercent(modelingPrice, objectPrice);
		        }

		        return new ModelObjectsCalculationParameters
		        {
			        ModelingPrice = modelingPrice,
			        Percent = percent
		        };
	        }
	        catch (Exception exception)
	        {
		        _log.ForContext("CadastralNumber", cadastralNumber)
			        .ForContext("A0", a0)
			        .ForContext("ObjectPrice", objectPrice)
			        .Error(exception, "Ошибка во время расчета МС и % для объекта моделирования");
		        return new ModelObjectsCalculationParameters();
	        }
        }

        public static decimal? CalculatePercent(decimal? calculatedPrice, decimal? initialPrice)
        {
	        decimal? percent = null;
	        if (calculatedPrice != null && initialPrice.GetValueOrDefault() != 0)
	        {
		        percent = (calculatedPrice / initialPrice.GetValueOrDefault() - 1) * 100;
	        }

	        return percent;
        }

        public void ExcludeObjectFromCalculation(long objectId)
        {
	        var obj = ModelObjectsRepository.GetById(objectId, x => new {x.IsExcluded});
	        if (obj == null)
		        throw new Exception($"Не найден объект с ИД '{objectId}'");

	        if (obj.IsExcluded.GetValueOrDefault() == false)
	        {
		        obj.IsExcluded = true;
		        obj.Save();
	        }
        }


        #region Support Methods

		private List<ModelObjectsFromExcelData> GetInfoFromFile(ExcelWorksheet sheet, List<ColumnToAttributeMapping> columnsMapping)
		{
			var rows = sheet.Rows;
			MaxRowsCountInFileForUpdating = DataExportCommon.GetLastUsedRowIndex(sheet);
			var modelObjectsFromExcel = new List<ModelObjectsFromExcelData>();

			var columnsMappingWithoutPrimaryKey = columnsMapping.Where(x =>
				x.AttributeId != OMModelToMarketObjects.GetColumnAttributeId(y => y.Id).ToString()).ToList();

			for (var i = 1; i <= MaxRowsCountInFileForUpdating; i++)
			{
				var cells = rows[i].Cells;

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
					Id = cells[IdColumnIndex].Value.ParseToLongNullable(),
					RowIndexInFile = i,
					Columns = columnsWithValues
				});
			}

			return modelObjectsFromExcel;
		}

		private List<IGrouping<long, FactorInFileInfo>> GetFactorColumnsForModelObjectsInFile(List<OMModelFactor> factors)
		{
			var result = new List<FactorInFileInfo>();
			factors.ForEach(x =>
			{
				var factorId = x.FactorId.GetValueOrDefault();
				var factorName = RegisterCache.GetAttributeData((int) x.FactorId.GetValueOrDefault()).Name;
				if (x.DictionaryId == null)
				{
					result.Add(new FactorInFileInfo
					{
						AttributeId = factorId,
						Name = factorName
					});
				}
				else
				{
					result.Add(new FactorInFileInfo
					{
						AttributeId = factorId,
						Name = $"{factorName} (Коэффициент)"
					});
					result.Add(new FactorInFileInfo
					{
						AttributeId = factorId,
						Name = $"{factorName} (Значение)",
						IsColumnWithValue = true
					});
				}
			});

			result = result.OrderBy(x => x.Name).ToList();
			result.ForEach(x => x.ColumnIndex = _maxNumberOfColumns++);

			//если есть нормализация - показывать два столбца (с коэфициентом и со значением)
			var groupedFactors = result.OrderBy(x => x.ColumnIndex).GroupBy(x => x.AttributeId).ToList();

			return groupedFactors;
		}


		#endregion


		#region Entities

		private class FactorInFileInfo
		{
			public long AttributeId { get; set; }
			public string Name { get; set; }
			public int ColumnIndex { get; set; }
			public bool IsColumnWithValue { get; set; }
		}

		public class ModelObjectsCalculationParameters
        {
	        public decimal? ModelingPrice { get; set; }
	        public decimal? Percent { get; set; }
        }

        private class ModelObjectsFromExcelData
        {
	        public long? Id { get; set; }
	        public int RowIndexInFile { get; set; }
			public List<Column> Columns { get; set; }

	        public ModelObjectsFromExcelData()
	        {
		        Columns = new List<Column>();
	        }
		}

        private class Column
        {
	        public string AttributeStr { get; set; }
	        public long AttributeId { get; set; }
	        public object ValueToUpdate { get; set; }
        }

        private class ObjectTypeInfo
        {
	        public PropertyTypes EnumValue { get; set; }
	        public string Str { get; set; }
        }

	#endregion
	}
}
