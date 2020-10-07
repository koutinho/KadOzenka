﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using KadOzenka.Dal.ScoreCommon;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.Ko;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
    public class Training : AModelingTemplate
    {
        private TrainingRequest RequestForService { get; set; }
        protected GeneralModelingInputParameters InputParameters { get; set; }
        protected OMModelingModel Model { get; }
        protected ScoreCommonService ScoreCommonService { get; set; }
        protected override string SubjectForMessageInNotification => $"Процесс обучения модели '{Model.Name}'";

        public Training(string inputParametersXml, OMQueue processQueue)
            : base(processQueue)
        {
            InputParameters = inputParametersXml.DeserializeFromXml<GeneralModelingInputParameters>();
            Model = GetModel(InputParameters.ModelId);
            ScoreCommonService = new ScoreCommonService();
        }


        protected override string GetUrl()
        {
            var baseUrl = ModelingProcessConfig.Current.TrainingBaseUrl;
            switch (InputParameters.ModelType)
            {
                case ModelType.Linear:
                    return $"{baseUrl}/{ModelingProcessConfig.Current.TrainingLinearTypeUrl}/{Model.InternalName}";
                case ModelType.Exponential:
                    return $"{baseUrl}/{ModelingProcessConfig.Current.TrainingExponentialTypeUrl}/{Model.InternalName}";
                case ModelType.Multiplicative:
                    return $"{baseUrl}/{ModelingProcessConfig.Current.TrainingMultiplicativeTypeUrl}/{Model.InternalName}";
                default:
                    throw new Exception($"Не известный тип модели: {InputParameters.ModelType.GetEnumDescription()}");
            }
        }

        protected override void PrepareData()
        {
            AddLog($"Начата работа с моделью '{Model.Name}', тип модели: '{InputParameters.ModelType.GetEnumDescription()}'.");

            var marketObjects = GetMarketObjects();
            AddLog($"Найдено {marketObjects.Count} объекта.");

            ModelingService.DestroyModelMarketObjects(Model.Id);
            AddLog("Удалены предыдущие данные.");

            var modelAttributes = ModelingService.GetModelAttributes(Model.Id);
            AddLog($"Найдено {modelAttributes?.Count} атрибутов для модели.");
            var groupedModelAttributes = modelAttributes.GroupBy(x => x.RegisterId, (k, g) => new ModelingService.GroupedModelAttributes
            {
	            RegisterId = (int)k,
	            Attributes = g.ToList()
            }).ToList();
            var marketObjectAttributes = groupedModelAttributes.Where(x => x.RegisterId == OMCoreObject.GetRegisterId())
	            .SelectMany(x => x.Attributes).ToList();
            var tourFactorsAttributes = groupedModelAttributes.Where(x => x.RegisterId != OMCoreObject.GetRegisterId()).ToList();

            var dictionaries = ModelingService.GetDictionaries(modelAttributes);
            AddLog($"Найдено {dictionaries?.Count} словарей для атрибутов  модели.");

            var unitsDictionary = GetUnits(marketObjects);
            AddLog($"Получено {unitsDictionary.Sum(x => x.Value?.Count)} Единиц оценки для всех объектов.");

            var i = 0;
            AddLog("Обработано объектов: ");
            var packageSize = 500;
            var packageIndex = 0;
            for (var packageCounter = packageIndex * packageSize; packageCounter < (packageIndex + 1) * packageSize; packageCounter++)
            {
	            var marketObjectsPage = marketObjects.Skip(packageIndex * packageSize).Take(packageSize).ToList();
                if(marketObjectsPage.Count == 0)
                    break;
                
                var marketObjectIds = marketObjectsPage.Select(x => x.Id).ToList();
                var units = unitsDictionary.Where(x => marketObjectIds.Contains(x.Key)).ToList();
                var unitIds = units.SelectMany(x => x.Value).ToList();

                var marketObjectCoefficients =
	                ModelingService.GetCoefficientsFromMarketObject(marketObjectIds, dictionaries,
		                marketObjectAttributes);
                var unitsCoefficients =
	                ModelingService.GetCoefficientsFromTourFactors(unitIds, dictionaries, tourFactorsAttributes);

                marketObjectsPage.ForEach(marketObject =>
                {
	                var isForTraining = i < marketObjects.Count / 2.0;
	                i++;
	                var modelObject = new OMModelToMarketObjects
	                {
		                ModelId = Model.Id,
		                CadastralNumber = marketObject.CadastralNumber,
		                Price = marketObject.Price,
		                IsForTraining = isForTraining
	                };

	                var currentMarketObjectCoefficients = marketObjectCoefficients.ContainsKey(marketObject.Id)
		                ? marketObjectCoefficients[marketObject.Id]
		                : new List<CoefficientForObject>();

                    var currentUnits = units.Where(x => x.Key == marketObject.Id).SelectMany(x => x.Value).ToList();
	                var currentUnitsCoefficients = unitsCoefficients.Where(x => currentUnits.Contains(x.Key)).SelectMany(x => x.Value).ToList();

                    currentMarketObjectCoefficients.AddRange(currentUnitsCoefficients);

	                modelObject.Coefficients = currentMarketObjectCoefficients.SerializeToXml();
	                modelObject.Save();

	                if (i % 100 == 0)
		                AddLog($"{i}, ", false);
                });

                packageIndex++;
            }
	       
	        AddLog($"{i}.", false);
        }

        protected override object GetRequestForService()
        {
            RequestForService = new TrainingRequest();

            var allAttributes = ModelingService.GetModelAttributes(InputParameters.ModelId);
            RequestForService.AttributeNames.AddRange(allAttributes.Select(x => PreProcessAttributeName(x.AttributeName)));
            RequestForService.AttributeIds.AddRange(allAttributes.Select(x => x.AttributeId));

            var modelObjects = ModelingService.GetIncludedModelObjects(InputParameters.ModelId, true);
            modelObjects.ForEach(modelObject =>
            {
                var modelObjectAttributes = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
                if (modelObjectAttributes == null || modelObjectAttributes.Count == 0)
                    return;

                var coefficients = new List<decimal?>();
                allAttributes.ForEach(modelAttribute =>
                {
                    coefficients.Add(modelObjectAttributes.FirstOrDefault(x => x.AttributeId == modelAttribute.AttributeId)?.Coefficient);
                });

                //TODO эта проверка будет в сервисе
                if (coefficients.All(x => x != null))
                {
                    RequestForService.Coefficients.Add(coefficients);
                    RequestForService.Prices.Add(new List<decimal>{modelObject.Price});
                    RequestForService.CadastralNumbers.Add(modelObject.CadastralNumber);
                }
            });

            if (RequestForService.Coefficients.Count < 2)
                throw new Exception("Недостаточно данных для построения модели (у которых значения всех атрибутов не пустые)");

            return RequestForService;
        }

        protected override void ProcessServiceResponse(GeneralResponse generalResponse)
        {
            var trainingResult = JsonConvert.DeserializeObject<TrainingResponse>(generalResponse.Data.ToString());
            PreprocessTrainingResult(trainingResult);

            ResetPredictedPrice();

            ResetCoefficientsForPredictedPrice();

            var jsonTrainingResult = JsonConvert.SerializeObject(trainingResult);
            UpdateModelTrainingResult(jsonTrainingResult);
        }

        protected override void RollBackResult()
        {
            UpdateModelTrainingResult(null);
        }


        #region Support Methods

        private List<MarketObjectPure> GetMarketObjects()
        {
            var groupToMarketSegmentRelation = GetGroupToMarketSegmentRelation();
            AddLog($"Найден тип: {groupToMarketSegmentRelation.MarketSegment_Code.GetEnumDescription()}");

            //TODO ждем выполнения CIPJSKO-307
            //var territoryCondition = ModelingService.GetConditionForTerritoryType(groupToMarketSegmentRelation.TerritoryType_Code);

            return OMCoreObject.Where(x =>
					x.PropertyMarketSegment_Code == groupToMarketSegmentRelation.MarketSegment_Code &&
					x.CadastralNumber != null &&
					x.ProcessType_Code != ProcessStep.Excluded
					//x.Id == 17812712
					)
                //TODO ждем выполнения CIPJSKO-307
                //.And(territoryCondition)
                .Select(x => new
                {
	                x.CadastralNumber,
	                x.Price
                })
				////TODO для тестирования
				//.SetPackageIndex(0)
				//.SetPackageSize(2000)
                .Execute()
                .GroupBy(x => new
                {
                    x.CadastralNumber,
                    x.Price
                })
                .Select(x => new MarketObjectPure
                {
                    Id = x.Max(y => y.Id),
                    CadastralNumber = x.Key.CadastralNumber,
                    Price = x.Key.Price.GetValueOrDefault()
                }).ToList();
        }

        private OMGroupToMarketSegmentRelation GetGroupToMarketSegmentRelation()
        {
            var relation = OMGroupToMarketSegmentRelation
                .Where(x => x.GroupId == Model.GroupId)
                .Select(x => x.MarketSegment_Code)
                .Select(x => x.TerritoryType_Code)
                .ExecuteFirstOrDefault();

            if(relation == null)
                throw new Exception($"Не найдено соотношение группы и сегмента. Id группы: '{Model.GroupId}'");

            return relation;
        }

        private Dictionary<long, List<long>> GetUnits(List<MarketObjectPure> groupedObjects)
        {
            var cadastralNumbers = groupedObjects.Select(x => x.CadastralNumber).Distinct().ToList();

            var units = GetUnitsByCadastralNumbers(cadastralNumbers, (int) Model.TourId);

            return units.GroupBy(x => x.MarketObjectId).ToDictionary(k => k.Key, v => v.Select(x => x.UnitId).ToList());
        }

        public List<MarketObjectToUnitRelation> GetUnitsByCadastralNumbers(List<string> cadastralNumbers, int tourId)
        {
	        if (cadastralNumbers == null || cadastralNumbers.Count == 0)
		        return new List<MarketObjectToUnitRelation>();

	        //сделано строкой, т.к. ОРМ делает дополнительные ненужные джоины, если делать через QSQuery
            var cadastralNumbersStr = new StringBuilder();
	        cadastralNumbers.ForEach(x => { cadastralNumbersStr.Append("'").Append(x).Append("'").Append(","); });
	        cadastralNumbersStr.Length--;

            var sql = $@"SELECT 
				MarketObject.ID AS {nameof(MarketObjectToUnitRelation.MarketObjectId)},
				Unit.ID AS {nameof(MarketObjectToUnitRelation.UnitId)}
		        FROM MARKET_CORE_OBJECT MarketObject
			        LEFT JOIN KO_UNIT Unit ON (MarketObject.CADASTRAL_NUMBER = Unit.CADASTRAL_NUMBER)
		        WHERE
		        (MarketObject.CADASTRAL_NUMBER IN({cadastralNumbersStr}) AND Unit.TOUR_ID = {tourId})";

            return QSQuery.ExecuteSql<MarketObjectToUnitRelation>(sql);
        }

        /// <summary>
        /// Заменяем имена атрибутов на их Id
        /// </summary>
        /// <param name="result"></param>
        private void PreprocessTrainingResult(TrainingResponse result)
        {
            var newCoefficients = new Dictionary<string, decimal>();
            var oldCoefficients = result.CoefficientsForAttributes;

            for (var i = 0; i < oldCoefficients.Count; i++)
            {
                var entry = oldCoefficients.ElementAtOrDefault(i);
                var attributeId = RequestForService.AttributeIds.ElementAtOrDefault(i);

                newCoefficients[attributeId.ToString()] = entry.Value;
            }

            result.CoefficientsForAttributes = newCoefficients;
        }

        private void ResetPredictedPrice()
        {
            var modelObjects = OMModelToMarketObjects.Where(x => x.ModelId == Model.Id && x.PriceFromModel != null)
                .SelectAll().Execute();

            modelObjects.ForEach(x =>
            {
                x.PriceFromModel = null;
                x.Save();
            });
        }

        private void ResetCoefficientsForPredictedPrice()
        {
            var modelAttributeRelations = OMModelAttributesRelation.Where(x => x.ModelId == Model.Id && x.Coefficient != null)
                .SelectAll().Execute();

            modelAttributeRelations.ForEach(x =>
            {
                x.Coefficient = null;
                x.Save();
            });
        }

        private void UpdateModelTrainingResult(string trainingResult)
        {
            switch (InputParameters.ModelType)
            {
                case ModelType.Linear:
                    Model.LinearTrainingResult = trainingResult;
                    break;
                case ModelType.Exponential:
                    Model.ExponentialTrainingResult = trainingResult;
                    break;
                case ModelType.Multiplicative:
                    Model.MultiplicativeTrainingResult = trainingResult;
                    break;
                default:
                    throw new Exception($"Не известный тип модели: {InputParameters.ModelType.GetEnumDescription()}");
            }

            Model.Save();
        }

        #endregion

        #region Entities

        public class MarketObjectToUnitRelation
        {
	        public long MarketObjectId { get; set; }
	        public long UnitId { get; set; }
        }

        #endregion
    }
}
