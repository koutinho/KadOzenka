using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using Kendo.Mvc.Extensions;
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
	    public DictionaryService DictionaryService { get; set; }
        private TrainingRequest RequestForService { get; set; }
        protected GeneralModelingInputParameters InputParameters { get; set; }
        private OMModel GeneralModel { get; }
        private OMTour Tour { get; }
        private List<OMModelToMarketObjects> MarketObjectsForTraining { get; }
        private List<ModelAttributeRelationDto> ModelAttributes { get; set; }
        protected override string SubjectForMessageInNotification => $"Процесс обучения модели '{GeneralModel.Name}'";

        public Training(string inputParametersXml, OMQueue processQueue, ILogger logger)
            : base(processQueue, logger)
        {
            InputParameters = inputParametersXml.DeserializeFromXml<GeneralModelingInputParameters>();
            GeneralModel = ModelingService.GetModelEntityById(InputParameters.ModelId);
            Tour = ModelingService.GetModelTour(GeneralModel.GroupId);
            MarketObjectsForTraining = new List<OMModelToMarketObjects>();
            ModelAttributes = new List<ModelAttributeRelationDto>();
            DictionaryService = new DictionaryService();
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

            ModelAttributes = ModelFactorsService.GetGeneralModelAttributes(GeneralModel.Id);
            AddLog($"Найдено {ModelAttributes?.Count} атрибутов для модели.");
            var groupedModelAttributes = ModelAttributes.GroupBy(x => x.RegisterId, (k, g) => new GroupedModelAttributes
            {
	            RegisterId = (int)k,
	            Attributes = g.ToList()
            }).ToList();
            var marketObjectAttributes = groupedModelAttributes.Where(x => x.RegisterId == OMCoreObject.GetRegisterId())
	            .SelectMany(x => x.Attributes).ToList();
            AddLog($"Найдено {marketObjectAttributes.Count} атрибутов для модели из таблицы с Аналогами.");
            var tourFactorsAttributes = groupedModelAttributes.Where(x => x.RegisterId != OMCoreObject.GetRegisterId()).ToList();
            AddLog($"Найдено {tourFactorsAttributes.Count} атрибутов для модели из таблицы с факторами тура.");

            var dictionaries = GetDictionaries(ModelAttributes);
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

                var marketObjectCoefficients = GetCoefficientsFromMarketObject(marketObjectIds, dictionaries,
		                marketObjectAttributes);
                var unitsCoefficients = GetCoefficientsFromTourFactors(unitIds, dictionaries, tourFactorsAttributes);

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

            RequestForService.AttributeNames.AddRange(ModelAttributes.Select(x => PreProcessAttributeName(x.AttributeName)));
            RequestForService.AttributeIds.AddRange(ModelAttributes.Select(x => x.AttributeId));

            MarketObjectsForTraining.ForEach(modelObject =>
            {
                var modelObjectAttributes = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
                if (modelObjectAttributes == null || modelObjectAttributes.Count == 0)
                    return;

                var coefficients = new List<decimal?>();
                ModelAttributes.ForEach(modelAttribute =>
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

        protected override void ProcessServiceResponse(GeneralResponse generalResponse)
        {
	        var trainingResults = new List<TrainingResponse>();

            if (InputParameters.ModelType == KoAlgoritmType.None)
	        {
		        trainingResults = JsonConvert.DeserializeObject<List<TrainingResponse>>(generalResponse.Data.ToString());
            }
	        else
	        {
		        var trainingResult = JsonConvert.DeserializeObject<TrainingResponse>(generalResponse.Data.ToString());
		        trainingResults.Add(trainingResult);
            }

            ResetPredictedPrice();
            AddLog("Закончен сброс спрогнозированной цены.");

            CreateMarkCatalog();
            AddLog("Закончено создание меток.");

            trainingResults.ForEach(trainingResult =>
            {
	            if (trainingResult == null)
		            throw new Exception("Сервис моделирования не вернул результат обучения");

	            PreprocessTrainingResult(trainingResult);

	            var trainingType = GetTrainingType(trainingResult.Type);
	            
	            SaveCoefficients(trainingResult.CoefficientsForAttributes, trainingType);
	            SaveTrainingResult(trainingType, JsonConvert.SerializeObject(trainingResult));

	            AddLog($"Закончено сохранение коэффициентов для типизированной модели '{trainingType.GetEnumDescription()}'.");
            });
        }

        protected override void RollBackResult()
        {
            ModelingService.ResetTrainingResults(GeneralModel, InputParameters.ModelType);

            var factors = ModelFactorsService.GetFactors(GeneralModel.Id, InputParameters.ModelType);
            factors.ForEach(x =>
            {
	            x.Weight = 0;
	            x.Save();
            });
        }


        #region Support Methods

        public List<OMModelingDictionary> GetDictionaries(List<ModelAttributeRelationDto> modelAttributes)
        {
	        var dictionaryIds = modelAttributes?.Where(x => x.DictionaryId != null).Select(x => x.DictionaryId.Value)
		        .Distinct().ToList();

	        return DictionaryService.GetDictionaries(dictionaryIds);
        }

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

        private Expression<Func<OMCoreObject, bool>> GetConditionForTerritoryType(TerritoryType territoryType)
        {
            switch (territoryType)
            {
                case TerritoryType.Main:
                    Expression<Func<OMCoreObject, bool>> mainTerritoryCondition = x => x.Address == "Main";
                    return mainTerritoryCondition;
                case TerritoryType.Additional:
                    Expression<Func<OMCoreObject, bool>> additionalTerritoryCondition = x => x.Address == "Additional";
                    return additionalTerritoryCondition;
                case TerritoryType.MainAndAdditional:
                    Expression<Func<OMCoreObject, bool>> bothTerritoryCondition = x => x.Address == "MainAndAdditional";
                    return bothTerritoryCondition;
                default:
                    Expression<Func<OMCoreObject, bool>> unknownTerritoryCondition = x => x.Address == "default";
                    return unknownTerritoryCondition;
            }
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

        private Dictionary<long, List<CoefficientForObject>> GetCoefficientsFromMarketObject(List<long> objectIds, List<OMModelingDictionary> dictionaries,
            List<ModelAttributeRelationDto> modelAttributes)
        {
            if (modelAttributes == null || modelAttributes.Count == 0 || objectIds == null || objectIds.Count == 0)
                return new Dictionary<long, List<CoefficientForObject>>();

            var query = new QSQuery
            {
                MainRegisterID = OMCoreObject.GetRegisterId(),
                Condition = new QSConditionSimple
                {
                    ConditionType = QSConditionType.In,
                    LeftOperand = OMCoreObject.GetColumn(x => x.Id),
                    RightOperand = new QSColumnConstant(objectIds)
                }
            };

            return GetCoefficients(query, dictionaries, modelAttributes);
        }

        private Dictionary<long, List<CoefficientForObject>> GetCoefficientsFromTourFactors(List<long> unitIds, List<OMModelingDictionary> dictionaries,
            List<GroupedModelAttributes> modelAttributes)
        {
            if (modelAttributes == null || modelAttributes.Count == 0 || unitIds == null || unitIds.Count == 0)
                return new Dictionary<long, List<CoefficientForObject>>();

            var coefficients = new Dictionary<long, List<CoefficientForObject>>();

            modelAttributes.ForEach(modelAttribute =>
            {
                var idAttribute = RegisterCache.RegisterAttributes.Values
                    .FirstOrDefault(x => x.RegisterId == modelAttribute.RegisterId && x.IsPrimaryKey)?.Id;

                var query = new QSQuery
                {
                    MainRegisterID = modelAttribute.RegisterId,
                    Condition = new QSConditionSimple
                    {
                        ConditionType = QSConditionType.In,
                        LeftOperand = new QSColumnSimple((int)idAttribute),
                        RightOperand = new QSColumnConstant(unitIds)
                    }
                };

                var currentCoefficients = GetCoefficients(query, dictionaries, modelAttribute.Attributes);
                coefficients.AddRange(currentCoefficients);
            });

            return coefficients;
        }

        private Dictionary<long, List<CoefficientForObject>> GetCoefficients(QSQuery query, List<OMModelingDictionary> dictionaries, List<ModelAttributeRelationDto> attributes)
        {
            attributes.ForEach(attribute =>
            {
                query.AddColumn(attribute.AttributeId, attribute.AttributeId.ToString());
            });

            var sql = query.GetSql();

            var coefficients = new Dictionary<long, List<CoefficientForObject>>();
            var table = query.ExecuteQuery();
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var id = row["id"].ParseToLong();
                var currentCoefficients = new List<CoefficientForObject>();
                attributes.ForEach(attribute =>
                {
                    var value = row[attribute.AttributeId.ToString()].ParseToStringNullable();

                    CoefficientForObject coefficient;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        coefficient = new CoefficientForObject(attribute.AttributeId)
                        {
                            Message = "Не найдено значение."
                        };
                    }
                    else
                    {
                        var dictionary = attribute.DictionaryId == null
                            ? null
                            : dictionaries.FirstOrDefault(x => x.Id == attribute.DictionaryId);

                        coefficient = CalculateCoefficientViaDictionary(value, attribute, dictionary);
                    }
                    currentCoefficients.Add(coefficient);
                });

                coefficients[id] = currentCoefficients;
            }

            return coefficients;
        }

        private CoefficientForObject CalculateCoefficientViaDictionary(object value, ModelAttributeRelationDto modelAttribute, OMModelingDictionary dictionary)
        {
            var coefficient = new CoefficientForObject(modelAttribute.AttributeId);

            switch (modelAttribute.AttributeTypeCode)
            {
                case RegisterAttributeType.STRING:
                    {
                        if (dictionary == null)
                        {
                            coefficient.Message = GetErrorMessage("строка");
                        }
                        else
                        {
                            var stringValue = value?.ParseToString();
                            coefficient.Value = stringValue;
                            coefficient.Coefficient = DictionaryService.GetCoefficientFromStringFactor(stringValue, dictionary);
                        }

                        break;
                    }
                case RegisterAttributeType.DATE:
                    {
                        if (dictionary == null)
                        {
                            coefficient.Message = GetErrorMessage("дата");
                        }
                        else
                        {
                            var dateValue = value?.ParseToDateTimeNullable();
                            coefficient.Value = dateValue?.ToShortDateString();
                            coefficient.Coefficient = DictionaryService.GetCoefficientFromDateFactor(dateValue, dictionary);
                        }

                        break;
                    }
                //число
                case RegisterAttributeType.INTEGER:
                case RegisterAttributeType.DECIMAL:
                    {
                        var numberValue = value?.ParseToDecimalNullable();

                        var number = DictionaryService.GetCoefficientFromNumberFactor(numberValue, dictionary);

                        coefficient.Value = number.ToString();
                        coefficient.Coefficient = number;
                        break;
                    }
                default:
                    {
                        coefficient.Message = "Ошибка: атрибут относится к типу 'неизвестный тип'.";
                        break;
                    }
            }

            return coefficient;
        }

        private string GetErrorMessage(string type)
        {
            return $"Ошибка: нет справочника. Атрибут относится к типу '{type}', но к нему не выбран справочник.";
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

        private void SaveCoefficients(Dictionary<string, decimal> coefficients, KoAlgoritmType type)
        {
	        var factors = ModelFactorsService.GetFactors(GeneralModel.Id, type);

	        foreach (var coefficient in coefficients)
            {
	            var attributeId = coefficient.Key.ParseToLong();
	            var factor = factors.FirstOrDefault(x => x.FactorId == attributeId);
	            if (factor == null)
		            throw new Exception($"Не найден фактор с ИД {attributeId}");

	            if (factor.Weight != coefficient.Value)
	            {
		            factor.Weight = coefficient.Value;
		            factor.Save();
	            }

                AddLog($"Сохранение коэффициента '{coefficient.Value}' для фактора '{attributeId}' модели '{type.GetEnumDescription()}'");
	        }
        }

        private void CreateMarkCatalog()
        {
	        foreach (var attribute in ModelAttributes)
	        {
		        ModelFactorsService.DeleteMarks(GeneralModel.GroupId, attribute.AttributeId);
		        AddLog("Удалены предыдущие метки");

                MarketObjectsForTraining.ForEach(modelObject =>
		        {
			        var objectCoefficients = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
			        var objectCoefficient = objectCoefficients.FirstOrDefault(x => x.AttributeId == attribute.AttributeId && !string.IsNullOrWhiteSpace(x.Value));
			        if (objectCoefficient == null || !string.IsNullOrWhiteSpace(objectCoefficient.Message))
				        return;

			        var value = objectCoefficient.Value;
			        var metka = objectCoefficient.Coefficient;

			        ModelFactorsService.CreateMark(value, metka, attribute.AttributeId, GeneralModel.GroupId);
		        });

                AddLog($"Сохранение меток для фактора '{attribute.AttributeName}', ИД ({attribute.AttributeId})");
	        }
        }

        private KoAlgoritmType GetTrainingType(string type)
        {
	        switch (type)
	        {
		        case "line":
			        return KoAlgoritmType.Line;
		        case "exponential":
			        return KoAlgoritmType.Exp;
		        case "multiplicative":
			        return KoAlgoritmType.Multi;
		        default:
			        throw new Exception("Невозможно конвертировать тип модели, присланной из сервиса");
	        }
        }

        private void SaveTrainingResult(KoAlgoritmType type, string trainingResult)
		{
			switch (type)
			{
				case KoAlgoritmType.Exp:
					GeneralModel.ExponentialTrainingResult = trainingResult;
					break;
				case KoAlgoritmType.Line:
					GeneralModel.LinearTrainingResult = trainingResult;
                    break;
				case KoAlgoritmType.Multi:
					GeneralModel.MultiplicativeTrainingResult = trainingResult;
                    break;
                case KoAlgoritmType.None:
	                throw new Exception("Невозможно обновить результаты обучения модели, т.к. не указан её тип");
			}

			GeneralModel.Save();
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

        public class GroupedModelAttributes
        {
	        public int RegisterId { get; set; }
	        public List<ModelAttributeRelationDto> Attributes { get; set; }

	        public GroupedModelAttributes()
	        {
		        Attributes = new List<ModelAttributeRelationDto>();
	        }
        }

        #endregion
    }
}
