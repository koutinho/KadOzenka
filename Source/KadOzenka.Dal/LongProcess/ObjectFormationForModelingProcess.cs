using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Dto;
using Kendo.Mvc.Extensions;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Ko;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using Serilog;
using Core.Register.LongProcessManagment;
using System.Threading;
using Core.ErrorManagment;

namespace KadOzenka.Dal.LongProcess
{
    public class ObjectFormationForModelingProcess : LongProcess
    {
	    private readonly ILogger _log = Log.ForContext<ObjectFormationForModelingProcess>();

        public DictionaryService DictionaryService { get; set; }
        protected ModelingService ModelingService { get; set; }
        protected ModelFactorsService ModelFactorsService { get; set; }
        private OMModel Model { get; set; }
        private OMTour Tour { get; set; }
        private OMQueue Queue { get; set; }
        private List<OMModelToMarketObjects> MarketObjectsForTraining { get; set; }
        private string MessageSubject => $"Сбор данных для Модели '{Model?.Name}'";

        public ObjectFormationForModelingProcess()
        {
	        ModelingService = new ModelingService();
	        ModelFactorsService = new ModelFactorsService();
	        DictionaryService = new DictionaryService();
	        MarketObjectsForTraining = new List<OMModelToMarketObjects>();
        }


        public static void AddProcessToQueue(long modelId)
        {
	        LongProcessManager.AddTaskToQueue(nameof(ObjectFormationForModelingProcess), objectId: modelId, registerId: OMModel.GetRegisterId());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			WorkerCommon.SetProgress(processQueue, 0);
			var modelId = processQueue.ObjectId;
			Queue = processQueue;

            _log.ForContext("ModelId", modelId).Information("Старт фонового процесса Формирования массива данных для Моделирования");

			if (!modelId.HasValue)
			{
				var message = Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId;
				WorkerCommon.SetMessage(processQueue, message);
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				SendMessage(processQueue, "Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов", MessageSubject);
				return;
			}

			try
			{
				Model = ModelingService.GetModelEntityById(modelId);
				Tour = ModelingService.GetModelTour(Model.GroupId);

				AddLog(processQueue, $"Начата сбор данных для модели '{Model.Name}'.", logger: _log);
                PrepareData();
                AddLog(processQueue, $"Закончен сбор данных для модели '{Model.Name}'.", logger: _log);

                AddLog(processQueue, $"Начато формирование каталога меток для модели '{Model.Name}'.", logger: _log);
                CreateMarkCatalog();
                AddLog(processQueue, $"Закончено формирование каталога меток для модели '{Model.Name}'.", logger: _log);

                SendMessage(processQueue, "Операция успешно завершена", MessageSubject);

            }
            catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				SendMessage(processQueue, $"Операция завершена с ошибкой: {exception.Message}. Подробнее в журнале ({errorId})", MessageSubject);
                _log.Error(exception, "Ошибка в ходе сбора нанных для моделирования");
			}

			WorkerCommon.SetProgress(processQueue, 100);
            _log.Information("Закончен фоновый процесс Формирования массива данных для Моделирования");
		}


        #region Support Methods

        private void PrepareData()
        {
	        var marketObjects = GetMarketObjects();
            AddLog(Queue, $"Найдено {marketObjects.Count} объекта-аналога.", logger: _log);

            ModelingService.DestroyModelMarketObjects(Model.Id);
            AddLog(Queue, "Удалены предыдущие данные.", logger: _log);

            var modelAttributes = ModelFactorsService.GetGeneralModelAttributes(Model.Id);
            AddLog(Queue, $"Найдено {modelAttributes.Count} атрибутов для модели.", logger: _log);

            var groupedModelAttributes = modelAttributes.GroupBy(x => x.RegisterId, (k, g) => new GroupedModelAttributes
            {
                RegisterId = (int)k,
                Attributes = g.ToList()
            }).ToList();
            var marketObjectAttributes = groupedModelAttributes.Where(x => x.RegisterId == OMCoreObject.GetRegisterId())
                .SelectMany(x => x.Attributes).ToList();
            AddLog(Queue, $"Найдено {marketObjectAttributes.Count} атрибутов для модели из таблицы с Аналогами.", logger: _log);
            var tourFactorsAttributes = groupedModelAttributes.Where(x => x.RegisterId != OMCoreObject.GetRegisterId()).ToList();
            AddLog(Queue, $"Найдено {tourFactorsAttributes.Count} атрибутов для модели из таблицы с факторами тура.", logger: _log);

            var dictionaries = GetDictionaries(modelAttributes);
            AddLog(Queue, $"Найдено {dictionaries?.Count} словарей для атрибутов  модели.", logger: _log);

            var marketObjectToUnitsRelation = GetMarketObjectToUnitsRelation(marketObjects, tourFactorsAttributes.Count != 0);
            AddLog(Queue, $"Получено {marketObjectToUnitsRelation.Sum(x => x.UnitIds?.Count)} Единиц оценки для всех объектов.", logger: _log);

            var i = 0;
            AddLog(Queue, "Обработано объектов: ", logger: _log);
            var packageSize = 500;
            var packageIndex = 0;
            for (var packageCounter = packageIndex * packageSize; packageCounter < (packageIndex + 1) * packageSize; packageCounter++)
            {
                var marketObjectToUnitsPage = marketObjectToUnitsRelation.Skip(packageIndex * packageSize).Take(packageSize).ToList();
                if (marketObjectToUnitsPage.Count == 0)
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
                        ModelId = Model.Id,
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

                    //сохраняем в список, чтобы не выкачивать повторно
                    if(isForTraining)
	                    MarketObjectsForTraining.Add(modelToMarketObjectRelation);

                    if (i % 100 == 0)
                        AddLog(Queue, $"{i}, ", false, logger: _log);
                });

                packageIndex++;
            }

            AddLog(Queue, $"{i}.", false, logger: _log);
        }

        private List<OMModelingDictionary> GetDictionaries(List<ModelAttributeRelationDto> modelAttributes)
        {
            var dictionaryIds = modelAttributes?.Where(x => x.DictionaryId != null).Select(x => x.DictionaryId.Value)
                .Distinct().ToList();

            return DictionaryService.GetDictionaries(dictionaryIds);
        }

        private List<MarketObjectPure> GetMarketObjects()
        {
            var groupToMarketSegmentRelation = GetGroupToMarketSegmentRelation();
            AddLog(Queue, $"Найден тип: {groupToMarketSegmentRelation.MarketSegment_Code.GetEnumDescription()}", logger: _log);

            //TODO ждем выполнения CIPJSKO-307
            //var territoryCondition = ModelingService.GetConditionForTerritoryType(groupToMarketSegmentRelation.TerritoryType_Code);

			return OMCoreObject.Where(x =>
					x.PropertyMarketSegment_Code == groupToMarketSegmentRelation.MarketSegment_Code &&
					x.CadastralNumber != null &&
					x.ProcessType_Code != ProcessStep.Excluded
				)
                //TODO для тестирования расчета МС и Процента
				//return OMCoreObject.Where(x => x.CadastralNumber == "77:06:0004004:9714")
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

        //private Expression<Func<OMCoreObject, bool>> GetConditionForTerritoryType(TerritoryType territoryType)
        //{
        //    switch (territoryType)
        //    {
        //        case TerritoryType.Main:
        //            Expression<Func<OMCoreObject, bool>> mainTerritoryCondition = x => x.Address == "Main";
        //            return mainTerritoryCondition;
        //        case TerritoryType.Additional:
        //            Expression<Func<OMCoreObject, bool>> additionalTerritoryCondition = x => x.Address == "Additional";
        //            return additionalTerritoryCondition;
        //        case TerritoryType.MainAndAdditional:
        //            Expression<Func<OMCoreObject, bool>> bothTerritoryCondition = x => x.Address == "MainAndAdditional";
        //            return bothTerritoryCondition;
        //        default:
        //            Expression<Func<OMCoreObject, bool>> unknownTerritoryCondition = x => x.Address == "default";
        //            return unknownTerritoryCondition;
        //    }
        //}

        private OMGroupToMarketSegmentRelation GetGroupToMarketSegmentRelation()
        {
            var relation = OMGroupToMarketSegmentRelation
                .Where(x => x.GroupId == Model.GroupId)
                .Select(x => x.MarketSegment_Code)
                .Select(x => x.TerritoryType_Code)
                .ExecuteFirstOrDefault();

            if (relation == null)
                throw new Exception($"Не найдено соотношение группы и сегмента. Id группы: '{Model.GroupId}'");

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
            AddLog(Queue, $"Найдено {placementUnits.Count} Единиц оценки с типом '{PropertyTypes.Pllacement.GetEnumDescription()}'", logger: _log);

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
                    AddLog(Queue, $"Единица оценки заменена на аналогичную по Кадастровому номеру здания '{placementUnit.BuildingCadastralNumber}'", logger: _log);
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

        private void CreateMarkCatalog()
        {
	        var factors = ModelFactorsService.GetFactors(Model.Id, KoAlgoritmType.Exp);

	        factors.ForEach(attribute =>
	        {
		        ModelFactorsService.DeleteMarks(Model.GroupId, attribute.FactorId);
		        AddLog(Queue, $"Удалены предыдущие метки для фактора {attribute.FactorId}", logger: _log);
	        });

            for (var i = 0; i < MarketObjectsForTraining.Count; i++)
	        {
		        var modelObject = MarketObjectsForTraining[i];

		        decimal modelingPrice = 0;
                foreach (var factor in factors)
		        {
			        var objectCoefficients = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
			        var objectCoefficient = objectCoefficients.FirstOrDefault(x => x.AttributeId == factor.FactorId && !string.IsNullOrWhiteSpace(x.Value));
			        if (objectCoefficient == null || !string.IsNullOrWhiteSpace(objectCoefficient.Message))
				        return;

			        var value = objectCoefficient.Value;
			        var metka = objectCoefficient.Coefficient;

			        if (metka != null)
			        {
				        ModelFactorsService.CreateMark(value, metka, factor.FactorId, Model.GroupId);
				        modelingPrice = modelingPrice + (metka * factor.PreviousWeight ?? 1);
			        }
		        }

                var resultModelingPrice = (decimal?) Math.Exp((double) (Model.A0.GetValueOrDefault() + modelingPrice));
                modelObject.ModelingPrice = Math.Round(resultModelingPrice.GetValueOrDefault(), 2);
                if (modelObject.Price != 1)
                {
	                modelObject.Percent = (modelObject.ModelingPrice / modelObject.Price - 1) * 100;
                }
                modelObject.Save();
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
