using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Ko;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using Serilog;

namespace KadOzenka.Dal.Modeling
{
    public class Training : AModelingTemplate
    {
        private TrainingRequest RequestForService { get; set; }
        protected GeneralModelingInputParameters InputParameters { get; set; }
        private OMModel GeneralModel { get; }
        private OMTour Tour { get; }
        private List<OMModelToMarketObjects> MarketObjectsForTraining { get; }
        protected override string SubjectForMessageInNotification => $"Процесс обучения модели '{GeneralModel.Name}'";

        public Training(string inputParametersXml, OMQueue processQueue, ILogger logger)
            : base(processQueue, logger)
        {
            InputParameters = inputParametersXml.DeserializeFromXml<GeneralModelingInputParameters>();
            GeneralModel = ModelingService.GetModelEntityById(InputParameters.ModelId);
            Tour = ModelingService.GetModelTour(GeneralModel.GroupId);
            MarketObjectsForTraining = new List<OMModelToMarketObjects>();
        }


        protected override string GetUrl()
        {
            var baseUrl = ModelingProcessConfig.Current.TrainingBaseUrl;
            switch (InputParameters.ModelType)
            {
	            case KoAlgoritmType.None:
		            return $"{baseUrl}/{ModelingProcessConfig.Current.TrainingAllTypesUrl}/{GeneralModel.InternalName}";
                case KoAlgoritmType.Line:
                    return $"{baseUrl}/{ModelingProcessConfig.Current.TrainingLinearTypeUrl}/{GeneralModel.InternalName}";
                case KoAlgoritmType.Exp:
                    return $"{baseUrl}/{ModelingProcessConfig.Current.TrainingExponentialTypeUrl}/{GeneralModel.InternalName}";
                case KoAlgoritmType.Multi:
                    return $"{baseUrl}/{ModelingProcessConfig.Current.TrainingMultiplicativeTypeUrl}/{GeneralModel.InternalName}";
                default:
                    throw new Exception($"Не известный тип модели: {InputParameters.ModelType.GetEnumDescription()}");
            }
        }

        protected override void PrepareData()
        {
            AddLog($"Начата работа с моделью '{GeneralModel.Name}', тип модели: '{InputParameters.ModelType.GetEnumDescription()}'.");

            var marketObjects = GetMarketObjects();
            AddLog($"Найдено {marketObjects.Count} объекта.");

            ModelingService.DestroyModelMarketObjects(GeneralModel.Id);
            AddLog("Удалены предыдущие данные.");

            var modelAttributes = ModelingService.GetModelFactors(GeneralModel.Id);
            AddLog($"Найдено {modelAttributes?.Count} атрибутов для модели.");
            var groupedModelAttributes = modelAttributes.GroupBy(x => x.RegisterId, (k, g) => new ModelingService.GroupedModelAttributes
            {
	            RegisterId = (int)k,
	            Attributes = g.ToList()
            }).ToList();
            var marketObjectAttributes = groupedModelAttributes.Where(x => x.RegisterId == OMCoreObject.GetRegisterId())
	            .SelectMany(x => x.Attributes).ToList();
            AddLog($"Найдено {marketObjectAttributes.Count} атрибутов для модели из таблицы с Аналогами.");
            var tourFactorsAttributes = groupedModelAttributes.Where(x => x.RegisterId != OMCoreObject.GetRegisterId()).ToList();
            AddLog($"Найдено {tourFactorsAttributes.Count} атрибутов для модели из таблицы с факторами тура.");

            var dictionaries = ModelingService.GetDictionaries(modelAttributes);
            AddLog($"Найдено {dictionaries?.Count} словарей для атрибутов  модели.");

            var marketObjectToUnitsRelation = GetMarketObjectToUnitsRelation(marketObjects, tourFactorsAttributes.Count != 0);
            AddLog($"Получено {marketObjectToUnitsRelation.Sum(x => x.UnitIds?.Count)} Единиц оценки для всех объектов.");

            var i = 0;
            AddLog("Обработано объектов: ");
            var packageSize = 500;
            var packageIndex = 0;
            for (var packageCounter = packageIndex * packageSize; packageCounter < (packageIndex + 1) * packageSize; packageCounter++)
            {
	            var marketObjectToUnitsPage = marketObjectToUnitsRelation.Skip(packageIndex * packageSize).Take(packageSize).ToList();
                if(marketObjectToUnitsPage.Count == 0)
                    break;

                var marketObjectIds = marketObjectToUnitsPage.Select(x => x.MarketObject.Id).ToList();
                var unitIds = marketObjectToUnitsPage.SelectMany(x => x.UnitIds).ToList();

                var marketObjectCoefficients =
	                ModelingService.GetCoefficientsFromMarketObject(marketObjectIds, dictionaries,
		                marketObjectAttributes);
                var unitsCoefficients =
	                ModelingService.GetCoefficientsFromTourFactors(unitIds, dictionaries, tourFactorsAttributes);

                marketObjectToUnitsPage.ForEach(marketObjectToUnitRelation =>
                {
	                var marketObject = marketObjectToUnitRelation.MarketObject;
	                var units = marketObjectToUnitRelation.UnitIds;

                    var isForTraining = i < marketObjects.Count / 2.0;
	                i++;
	                var modelToMarketObjectRelation = new OMModelToMarketObjects
	                {
		                ModelId = GeneralModel.Id,
		                CadastralNumber = marketObject.CadastralNumber,
		                Price = marketObject.PricePerMeter,
		                IsForTraining = isForTraining
	                };

	                var currentMarketObjectCoefficients = marketObjectCoefficients.TryGetValue(marketObject.Id, out var coefficients)
		                ? coefficients
                        : new List<CoefficientForObject>();

                    var currentUnitsCoefficients = unitsCoefficients.Where(x => units.Contains(x.Key)).SelectMany(x => x.Value).ToList();

                    currentMarketObjectCoefficients.AddRange(currentUnitsCoefficients);

	                modelToMarketObjectRelation.Coefficients = currentMarketObjectCoefficients.SerializeToXml();
	                modelToMarketObjectRelation.Save();

                    //сразу сохраняем объект в список, чтобы не выкачивать их при обработке и отправке запроса на сервер
                    if(isForTraining)
	                    MarketObjectsForTraining.Add(modelToMarketObjectRelation);

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

            var allAttributes = ModelingService.GetModelFactors(InputParameters.ModelId);
            RequestForService.AttributeNames.AddRange(allAttributes.Select(x => PreProcessAttributeName(x.AttributeName)));
            RequestForService.AttributeIds.AddRange(allAttributes.Select(x => x.AttributeId));

            MarketObjectsForTraining.ForEach(modelObject =>
            {
                var modelObjectAttributes = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
                if (modelObjectAttributes == null || modelObjectAttributes.Count == 0)
                    return;

                var coefficients = new List<decimal?>();
                allAttributes.ForEach(modelAttribute =>
                {
	                var currentAttribute = modelObjectAttributes.FirstOrDefault(x =>
		                x.AttributeId == modelAttribute.AttributeId && !string.IsNullOrWhiteSpace(x.Value));
                    coefficients.Add(currentAttribute?.Coefficient);
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

        //TODO CIPJSKO-526 добавить обработку по всем
        protected override void ProcessServiceResponse(GeneralResponse generalResponse)
        {
	        var trainingResult = JsonConvert.DeserializeObject<TrainingResponse>(generalResponse.Data.ToString());
	        PreprocessTrainingResult(trainingResult);

	        ResetPredictedPrice();
	        AddLog("Закончен сброс спрогнозированной цены.");

            CreateMarkCatalog(trainingResult.CoefficientsForAttributes);
            AddLog("Закончено создание меток.");

            var typifiedModels = GetTypifiedModels(JsonConvert.SerializeObject(trainingResult));
            typifiedModels.ForEach(typifiedModel =>
	        {
		        SaveCoefficients(trainingResult.CoefficientsForAttributes, typifiedModel);
		        AddLog($"Закончено сохранение коэффициентов для типизированной модели '{typifiedModel.AlgoritmType}' с ИД '{typifiedModel.Id}'.");
	        });
        }

        protected override void RollBackResult()
        {
	        var typifiedModels = ModelingService.GetTypifiedModelsByGeneralModelId(GeneralModel.Id, InputParameters.ModelType);
	        typifiedModels.ForEach(x =>
	        {
		        x.TrainingResult = null;
		        x.Save();
	        });
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
	                x.PricePerMeter
                })
				////TODO для тестирования
				//.SetPackageIndex(0)
				//.SetPackageSize(100)
                .Execute()
                .GroupBy(x => new
                {
                    x.CadastralNumber,
                    x.PricePerMeter
                })
                .Select(x => new MarketObjectPure
                {
                    Id = x.Max(y => y.Id),
                    CadastralNumber = x.Key.CadastralNumber,
                    PricePerMeter = x.Key.PricePerMeter.GetValueOrDefault()
                }).ToList();
        }

        private OMGroupToMarketSegmentRelation GetGroupToMarketSegmentRelation()
        {
            var relation = OMGroupToMarketSegmentRelation
                .Where(x => x.GroupId == GeneralModel.GroupId)
                .Select(x => x.MarketSegment_Code)
                .Select(x => x.TerritoryType_Code)
                .ExecuteFirstOrDefault();

            if(relation == null)
                throw new Exception($"Не найдено соотношение группы и сегмента. Id группы: '{GeneralModel.GroupId}'");

            return relation;
        }

        private List<MarketObjectToUnitsRelation> GetMarketObjectToUnitsRelation(List<MarketObjectPure> marketObjects, bool downloadUnits)
        {
	        var cadastralNumbers = marketObjects.Select(x => x.CadastralNumber).ToList();
	        if (cadastralNumbers.Count == 0)
		        return new List<MarketObjectToUnitsRelation>();

            var unitsDictionary = new Dictionary<string, List<long>>();
            if (downloadUnits)
	        {
		        var units = OMUnit.Where(x => cadastralNumbers.Contains(x.CadastralNumber) && x.TourId == Tour.Id)
			        .Select(x => new
			        {
				        x.CadastralNumber,
				        x.PropertyType_Code,
				        x.BuildingCadastralNumber
			        })
			        .Execute();

		        var placementUnits = units.Where(x => x.PropertyType_Code == PropertyTypes.Pllacement).ToList();
		        if (placementUnits.Count > 0)
		        {
			        ProcessPlacemenUnits(placementUnits, units);
		        }

		        unitsDictionary = units.GroupBy(x => x.CadastralNumber)
			        .ToDictionary(k => k.Key, v => v.Select(x => x.Id).ToList());
            }

            var marketObjectToUnitsRelation = new List<MarketObjectToUnitsRelation>();
            marketObjects.ForEach(x =>
            {
	            var marketObjectUnits = unitsDictionary.TryGetValue(x.CadastralNumber, out var u) ? u : new List<long>();

	            marketObjectToUnitsRelation.Add(new MarketObjectToUnitsRelation
	            {
                    MarketObject = x,
                    UnitIds = marketObjectUnits
	            });
            });

            return marketObjectToUnitsRelation;
        }

        private void ProcessPlacemenUnits(List<OMUnit> placementUnits, List<OMUnit> units)
        {
	        AddLog($"Найдено {placementUnits.Count} Единиц оценки с типом '{PropertyTypes.Pllacement.GetEnumDescription()}'");

	        var buildingCadastralNumbers = placementUnits
		        .Where(x => !string.IsNullOrWhiteSpace(x.BuildingCadastralNumber))
		        .Select(x => x.BuildingCadastralNumber).Distinct().ToList();
	        var buildingUnits = buildingCadastralNumbers.Count > 0
		        ? OMUnit.Where(x =>
				        buildingCadastralNumbers.Contains(x.CadastralNumber) &&
				        x.PropertyType_Code == PropertyTypes.Building && x.TourId == Tour.Id)
			        .Select(x => x.CadastralNumber)
			        .Execute()
		        : new List<OMUnit>();

	        placementUnits.ForEach(placementUnit =>
	        {
		        if (!string.IsNullOrWhiteSpace(placementUnit.BuildingCadastralNumber))
		        {
			        var currentBuildingUnits = buildingUnits.Where(x => x.CadastralNumber == placementUnit.BuildingCadastralNumber).ToList();
			        currentBuildingUnits.ForEach(x => x.CadastralNumber = placementUnit.CadastralNumber);
			        units.AddRange(buildingUnits);
			        AddLog($"Единица оценки заменена на аналогичную по Кадастровому номеру здания '{placementUnit.BuildingCadastralNumber}'");
		        }

		        units.Remove(placementUnit);
	        });
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
            var modelObjects = OMModelToMarketObjects.Where(x => x.ModelId == GeneralModel.Id && x.PriceFromModel != null)
                .SelectAll().Execute();

            modelObjects.ForEach(x =>
            {
                x.PriceFromModel = null;
                x.Save();
            });
        }

        private List<OMModelTypified> GetTypifiedModels(string trainingResult)
        {
	        var typifiedModels = ModelingService.GetTypifiedModelsByGeneralModelId(GeneralModel.Id, InputParameters.ModelType);
	        AddLog($"Найдено {typifiedModels.Count} типизированных моделей");
	        if (typifiedModels.Count == 0)
	        {
		        typifiedModels = CreateTypifiedModels(GeneralModel.Id, InputParameters.ModelType, trainingResult);
		        AddLog($"Создано {typifiedModels.Count} типизированных моделей");
	        }

            return typifiedModels;
        }

        private List<OMModelTypified> CreateTypifiedModels(long generalModelId, KoAlgoritmType type, string trainingResult)
        {
	        var types = new List<KoAlgoritmType>();
	        if (type == KoAlgoritmType.None)
	        {
		        types.Add(KoAlgoritmType.Line);
		        types.Add(KoAlgoritmType.Exp);
		        types.Add(KoAlgoritmType.Multi);

            }
	        else
	        {
		        types.Add(type);
	        }

	        var createdTypifiedModels = new List<OMModelTypified>();
            types.ForEach(x =>
            {
	            var model = new OMModelTypified
	            {
		            ModelId = generalModelId,
		            AlgoritmType_Code = x,
		            TrainingResult = trainingResult
	            };
	            model.Save();

	            createdTypifiedModels.Add(model);
            });

            return createdTypifiedModels;
        }

        private void SaveCoefficients(Dictionary<string, decimal> coefficients, OMModelTypified typifiedModelId)
        {
	        var modelFactors = OMModelFactor.Where(x => x.TypifiedModelId == typifiedModelId.Id).SelectAll().Execute();

	        foreach (var coefficient in coefficients)
	        {
		        var modelFactor = modelFactors.FirstOrDefault(x => x.FactorId == coefficient.Key.ParseToLong());
		        if (modelFactor == null)
			        continue;

		        modelFactor.Weight = coefficient.Value;
		        modelFactor.Save();
		        AddLog($"Сохранение коэффициента '{coefficient.Value}' для фактора '{coefficient.Key}' модели '{typifiedModelId.AlgoritmType}'");
	        }
        }

        private void CreateMarkCatalog(Dictionary<string, decimal> coefficients)
        {
	        foreach (var coefficient in coefficients)
	        {
		        var factorId = coefficient.Key.ParseToLong();
		        var existedMarks = OMMarkCatalog.Where(x => x.GeneralModelId == GeneralModel.Id && x.FactorId == factorId).Execute();
		        existedMarks.ForEach(x => x.Destroy());

		        MarketObjectsForTraining.ForEach(modelObject =>
		        {
			        var objectCoefficients = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
			        var objectCoefficient = objectCoefficients.FirstOrDefault(x => x.AttributeId == factorId && !string.IsNullOrWhiteSpace(x.Value));
			        if (objectCoefficient == null || !string.IsNullOrWhiteSpace(objectCoefficient.Message))
				        return;

			        var value = objectCoefficient.Value;
			        var metka = objectCoefficient.Coefficient;

			        new OMMarkCatalog
			        {
				        GroupId = GeneralModel.GroupId,
				        FactorId = factorId,
				        ValueFactor = value,
				        MetkaFactor = metka
			        }.Save();
		        });
                AddLog($"Сохранение меток для фактора '{factorId}'");
	        }
        }

        #endregion

        #region Entities

        private class MarketObjectToUnitsRelation
        {
	        public MarketObjectPure MarketObject { get; set; }
	        public List<long> UnitIds { get; set; }

	        public MarketObjectToUnitsRelation()
	        {
		        UnitIds = new List<long>();
	        }
        }

        #endregion
    }
}
