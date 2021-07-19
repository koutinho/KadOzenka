using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register;
using GemBox.Spreadsheet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ObjectBuilder2;
using ModelingBusiness.Objects.Entities;
using ModelingBusiness.Objects.Repositories;
using ObjectModel.KO;
using ObjectModel.Modeling;
using Serilog;

namespace ModelingBusiness.Objects
{
	public class ModelObjectsService : IModelObjectsService
	{
		private readonly ILogger _log = Log.ForContext<ModelObjectsService>();
		private IModelObjectsRepository ModelObjectsRepository { get; }

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

		#endregion

		public ModelObjectsService(IModelObjectsRepository modelObjectsRepository = null)
		{
			ModelObjectsRepository = modelObjectsRepository ?? new ModelObjectsRepository();
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
			CommonSdks.DataExportCommon.AddRow(mainWorkSheet, 0, columnHeaders);

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

				CommonSdks.DataExportCommon.AddRow(mainWorkSheet, rowCounter++, values);
			});

			var stream = new MemoryStream();
            excelTemplate.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
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

		#endregion
	}
}
