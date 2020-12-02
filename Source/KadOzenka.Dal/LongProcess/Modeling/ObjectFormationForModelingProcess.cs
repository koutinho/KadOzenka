using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Ko;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Modeling
{
    public class ObjectFormationForModelingProcess : BaseObjectFormationForModelingProcess
    {
	    public static long ProcessId => 60;

        private OMModel Model { get; set; }
        private OMTour Tour { get; set; }
        private OMQueue Queue { get; set; }
        private List<OMModelToMarketObjects> ModelObjects { get; }
        private string MessageSubject => $"Сбор данных для Модели '{Model?.Name}'";

        public ObjectFormationForModelingProcess() : base(Log.ForContext<ObjectFormationForModelingProcess>())
        {
	        ModelObjects = new List<OMModelToMarketObjects>();
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

            Log.ForContext("ModelId", modelId).Information("Старт фонового процесса Формирования массива данных для Моделирования");

			if (!modelId.HasValue)
			{
				var message = Common.Consts.MessageForProcessInterruptedBecauseOfNoObjectId;
				WorkerCommon.SetMessage(processQueue, message);
				WorkerCommon.SetProgress(processQueue, Common.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				SendMessage(processQueue, "Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов", MessageSubject);
				return;
			}

			try
			{
				Model = ModelingService.GetModelEntityById(modelId);
				Tour = ModelingService.GetModelTour(Model.GroupId);

				var modelAttributes = GetGeneralModelAttributes(Model.Id);
				AddLog(Queue, $"Найдено {modelAttributes.Count} атрибутов для модели.", logger: Logger);

                AddLog(processQueue, $"Начата сбор данных для модели '{Model.Name}'.", logger: Logger);
                var processedMarketObjectsCount = PrepareData(modelAttributes);
                AddLog(processQueue, $"Закончен сбор данных для модели '{Model.Name}'.", logger: Logger);

                CreateMarkCatalog(Model.GroupId, ModelObjects, modelAttributes, Queue);

                SaveStatistic(ModelObjects, modelAttributes, Model, Queue);

                SendMessage(processQueue, $"Операция успешно завершена.<br>Всего обработано объектов-аналогов: {processedMarketObjectsCount}.", MessageSubject);
			}
            catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				SendMessage(processQueue, $"Операция завершена с ошибкой: {exception.Message}. Подробнее в журнале ({errorId})", MessageSubject);
                Log.Error(exception, "Ошибка в ходе сбора данных для моделирования");
			}

			WorkerCommon.SetProgress(processQueue, 100);
            Log.Information("Закончен фоновый процесс Формирования массива данных для Моделирования");
		}


        #region Support Methods

        private int PrepareData(List<ModelAttributePure> modelAttributes)
        {
	        var marketObjects = GetMarketObjects();
            AddLog(Queue, $"Найдено {marketObjects.Count} объекта-аналога.", logger: Logger);

            var numberOfDeletedModelObjects = ModelingService.DestroyModelMarketObjects(Model);
            AddLog(Queue, $"Удалено {numberOfDeletedModelObjects} ранее найденных объектов модели.", logger: Logger);

            var marketObjectAttributes = modelAttributes.Where(x => x.RegisterId == OMCoreObject.GetRegisterId()).ToList();
            AddLog(Queue, $"Найдено {marketObjectAttributes.Count} атрибутов для модели из таблицы с Аналогами.", logger: Logger);
            var tourFactorsAttributes = modelAttributes.Where(x => x.RegisterId != OMCoreObject.GetRegisterId()).ToList();
            AddLog(Queue, $"Найдено {tourFactorsAttributes.Count} атрибутов для модели из таблицы с факторами тура.", logger: Logger);

            var dictionaries = GetDictionaries(modelAttributes);
            AddLog(Queue, $"Найдено {dictionaries?.Count} словарей для атрибутов  модели.", logger: Logger);

            var marketObjectToUnitsRelation = GetMarketObjectToUnitRelation(marketObjects, tourFactorsAttributes.Count != 0);
            AddLog(Queue, $"Получено {marketObjectToUnitsRelation.Count(x => x.Unit?.Id != null)} Единиц оценки для всех объектов.", logger: Logger);

            var i = 0;
            AddLog(Queue, "Обработано объектов: ", logger: Logger);
            //берем 75% от всего числа объектов, из них первая половина для обучающей выборки, вторая - для контрольной 
            var objectsCount = marketObjects.Count * 75 / 100;
            var firstHalf = objectsCount / 2.0;
            var packageSize = 1000;
            var packageIndex = 0;
            for (var packageCounter = packageIndex * packageSize; packageCounter < (packageIndex + 1) * packageSize; packageCounter++)
            {
                var marketObjectToUnitsPage = marketObjectToUnitsRelation.Skip(packageIndex * packageSize).Take(packageSize).ToList();
                if (marketObjectToUnitsPage.Count == 0)
                    break;

                var marketObjectIds = marketObjectToUnitsPage.Select(x => x.MarketObject.Id).ToList();
                var unitIds = marketObjectToUnitsPage.Where(x => x.Unit?.Id != null).Select(x => x.Unit.Id.Value).Distinct().ToList();

                var marketObjectCoefficients = GetCoefficientsFromMarketObject(marketObjectIds, dictionaries, marketObjectAttributes);
                var unitsCoefficients = GetCoefficientsFromTourFactors(unitIds, dictionaries, tourFactorsAttributes);

                marketObjectToUnitsPage.ForEach(marketObjectToUnitRelation =>
                {
                    var marketObject = marketObjectToUnitRelation.MarketObject;
                    var unit = marketObjectToUnitRelation.Unit;

                    var isForTraining = i < firstHalf;
                    var isForControl = i >= firstHalf && i <= objectsCount;
                    i++;
                    var modelToMarketObjectRelation = new OMModelToMarketObjects
                    {
                        ModelId = Model.Id,
                        MarketObjectId = marketObject.Id,
                        UnitId = unit?.Id,
                        UnitPropertyType_Code = unit?.PropertyType ?? PropertyTypes.None,
                        CadastralNumber = marketObject.CadastralNumber,
                        Price = marketObject.PricePerMeter,
                        IsForTraining = isForTraining,
                        IsForControl = isForControl
                    };

                    var currentMarketObjectCoefficients = marketObjectCoefficients.TryGetValue(marketObject.Id, out var coefficients)
                        ? coefficients
                        : new List<CoefficientForObject>();

                    var currentUnitCoefficients = unitsCoefficients.Where(x => x.Key == unit?.Id).SelectMany(x => x.Value).ToList();

                    currentMarketObjectCoefficients.AddRange(currentUnitCoefficients);

                    modelToMarketObjectRelation.Coefficients = currentMarketObjectCoefficients.SerializeToXml();
                    modelToMarketObjectRelation.Save();

                    //сохраняем в список, чтобы не выкачивать повторно
                    ModelObjects.Add(modelToMarketObjectRelation);

                    if (i % 100 == 0)
                        AddLog(Queue, $"{i}, ", false, logger: Logger);
                });

                packageIndex++;
            }

            AddLog(Queue, $"{i}.", false, logger: Logger);

            return i;
        }

        private List<OMModelingDictionary> GetDictionaries(List<ModelAttributePure> modelAttributes)
        {
            var dictionaryIds = modelAttributes?.Where(x => x.DictionaryId != null).Select(x => x.DictionaryId.Value)
                .Distinct().ToList();

            return DictionaryService.GetDictionaries(dictionaryIds);
        }

        private List<MarketObjectPure> GetMarketObjects()
        {
            var groupToMarketSegmentRelation = GetGroupToMarketSegmentRelation();
            AddLog(Queue, $"Найден тип: {groupToMarketSegmentRelation.MarketSegment_Code.GetEnumDescription()}", logger: Logger);

            //TODO ждем выполнения CIPJSKO-307
            //var territoryCondition = ModelingService.GetConditionForTerritoryType(groupToMarketSegmentRelation.TerritoryType_Code);

            var baseQuery = OMCoreObject.Where(x =>
	            x.PropertyMarketSegment_Code == groupToMarketSegmentRelation.MarketSegment_Code &&
	            x.CadastralNumber != null &&
	            x.ProcessType_Code != ProcessStep.Excluded);

            var type = Model.IsOksObjectType.GetValueOrDefault() ? QSConditionType.NotEqual : QSConditionType.Equal;
            baseQuery.And(new QSConditionSimple(OMCoreObject.GetColumn(x => x.PropertyTypesCIPJS_Code),
	            type, (int) PropertyTypesCIPJS.LandArea));

			baseQuery.Select(x => new
			{
				x.CadastralNumber,
				x.PricePerMeter
			});

			////TODO для тестирования
			//baseQuery.SetPackageIndex(0).SetPackageSize(100);

			return baseQuery
                //TODO для тестирования расчета МС и Процента
                //return OMCoreObject.Where(x => x.CadastralNumber == "77:06:0004004:9714")
                //TODO ждем выполнения CIPJSKO-307
                //.And(territoryCondition)
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
                .Select(x => new
                {
	                x.MarketSegment_Code,
	                x.TerritoryType_Code
                })
                .ExecuteFirstOrDefault();

            if (relation == null)
                throw new Exception($"Не найдено соотношение группы и сегмента. Id группы: '{Model.GroupId}'");

            return relation;
        }

        private List<MarketObjectToUnitRelation> GetMarketObjectToUnitRelation(List<MarketObjectPure> marketObjects, bool downloadUnits)
        {
            var cadastralNumbers = marketObjects.Select(x => x.CadastralNumber).Distinct().ToList();
            if (cadastralNumbers.Count == 0)
                return new List<MarketObjectToUnitRelation>();

            var unitsDictionary = new Dictionary<string, UnitPure>();
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

                unitsDictionary = units.GroupBy(x => x.CadastralNumber).ToDictionary(k => k.Key, v =>
                {
	                var unit = v.First();

	                return new UnitPure
	                {
		                Id = unit.Id,
                        PropertyType = unit.PropertyType_Code
	                };
                });
            }

            var marketObjectToUnitRelation = new List<MarketObjectToUnitRelation>();
            marketObjects.ForEach(x =>
            {
                var resultUnit = unitsDictionary.TryGetValue(x.CadastralNumber, out var unit) ? unit : null;

                marketObjectToUnitRelation.Add(new MarketObjectToUnitRelation
                {
                    MarketObject = x,
                    Unit = resultUnit
                });
            });

            return marketObjectToUnitRelation;
        }

        private void ProcessPlacemenUnits(List<OMUnit> placementUnits, List<OMUnit> units)
        {
            AddLog(Queue, $"Найдено {placementUnits.Count} Единиц оценки с типом '{PropertyTypes.Pllacement.GetEnumDescription()}'", logger: Logger);

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
                    AddLog(Queue, $"Единица оценки заменена на аналогичную по Кадастровому номеру здания '{placementUnit.BuildingCadastralNumber}'", logger: Logger);
                }

                units.Remove(placementUnit);
            });
        }

        #endregion


        #region Entities

        private class MarketObjectToUnitRelation
        {
	        public MarketObjectPure MarketObject { get; set; }
	        public UnitPure Unit { get; set; }
        }

        private class UnitPure
		{
			public long? Id { get; set; }
			public PropertyTypes? PropertyType { get; set; }
		}

        #endregion
    }
}
