using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.Common;
using KadOzenka.Dal.LongProcess.Modeling.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using MarketPlaceBusiness;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Ko;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Modeling
{
    public class ObjectFormationForModelingProcess : BaseObjectFormationForModelingProcess
    {
	    public static long ProcessId => 60;

        private OMModel Model { get; set; }
        private OMTour Tour { get; set; }
        private OMQueue Queue { get; set; }
        private List<OMModelToMarketObjects> ModelObjects { get; set; }
        private ILongProcessService LongProcessService { get; }
        protected MarketObjectsForModelingService MarketObjectsForModelingService { get; set; }
        private ObjectFormationInputParameters InputParameters { get; set; }
        private string MessageSubject => $"Сбор данных для Модели '{Model?.Name}'";
        private object _locker { get; set; }

        public ObjectFormationForModelingProcess() : base(Log.ForContext<ObjectFormationForModelingProcess>())
        {
	        _locker = new object();
            ModelObjects = new List<OMModelToMarketObjects>();
	        LongProcessService = new LongProcessService();
	        MarketObjectsForModelingService = new MarketObjectsForModelingService();
        }


        public static void AddProcessToQueue(ObjectFormationInputParameters inputParameters)
        {
	        LongProcessManager.AddTaskToQueue(nameof(ObjectFormationForModelingProcess),
		        objectId: inputParameters.ModelId, 
                registerId: OMModel.GetRegisterId(),
		        parameters: inputParameters.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			WorkerCommon.SetProgress(processQueue, 0);
			Queue = processQueue;

			InputParameters = processQueue.Parameters?.DeserializeFromXml<ObjectFormationInputParameters>();
            if (InputParameters == null)
			{
				WorkerCommon.SetMessage(processQueue, Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				NotificationSender.SendNotification(processQueue, "Моделирование", "Операция завершена с ошибкой, т.к. нет входных данных.");
				return;
			}
            Logger.ForContext("InputParameters", InputParameters, true).Debug("Старт фонового процесса Формирования массива данных для Моделирования");


            try
			{
				Model = ModelingService.GetModelEntityById(InputParameters.ModelId);
				Tour = ModelingService.GetModelTour(Model.GroupId);

				if (LongProcessService.HasOtherActiveProcessInQueue(Queue.Id, ProcessId, Model.Id))
					throw new Exception("Процесс сбора данных уже был запущен ранее");

				var modelAttributes = GetGeneralModelAttributes(Model.Id);
				AddLog(Queue, $"Найдено {modelAttributes.Count} атрибутов для модели.", logger: Logger);

                AddLog(processQueue, $"Начата сбор данных для модели '{Model.Name}'.", logger: Logger);
                var processedMarketObjectsCount = PrepareData(modelAttributes);
                AddLog(processQueue, $"Закончен сбор данных для модели '{Model.Name}'.", logger: Logger);

                CreateMarkCatalog(Model.GroupId, ModelObjects, modelAttributes, Queue);

                SaveStatistic(ModelObjects, modelAttributes, Model, Queue);

                NotificationSender.SendNotification(processQueue, MessageSubject, $"Операция успешно завершена.<br>Всего обработано объектов-аналогов: {processedMarketObjectsCount}.");
			}
            catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				NotificationSender.SendNotification(processQueue, MessageSubject, $"Операция завершена с ошибкой: {exception.Message}. Подробнее в журнале ({errorId})");
				Logger.Error(exception, "Ошибка в ходе сбора данных для моделирования");
			}

			WorkerCommon.SetProgress(processQueue, 100);
			Logger.Information("Закончен фоновый процесс Формирования массива данных для Моделирования");
		}


        #region Support Methods

        private int PrepareData(List<ModelAttributePure> modelAttributes)
        {
	        var segment = GetGroupToMarketSegmentRelation();
            var marketObjects = MarketObjectsForModelingService
                .GetObjectsForFormation(Model.IsOksObjectType.GetValueOrDefault(), (long) segment.MarketSegment_Code).Select(
		            x => new MarketObjectPure
		            {
			            Id = x.Id,
			            CadastralNumber = x.CadastralNumber,
			            PricePerMeter = x.PricePerMeter
		            }).ToList();
            AddLog(Queue, $"Найдено {marketObjects.Count} объекта-аналога.", logger: Logger);

            var numberOfDeletedModelObjects = ModelObjectsService.DestroyModelMarketObjects(Model);
            AddLog(Queue, $"Удалено {numberOfDeletedModelObjects} ранее найденных объектов модели.", logger: Logger);

            var marketObjectAttributes = modelAttributes.Where(x => x.RegisterId == MarketObjectsForModelingService.RegisterId).ToList();
            AddLog(Queue, $"Найдено {marketObjectAttributes.Count} атрибутов для модели из таблицы с Аналогами.", logger: Logger);
            var tourFactorsAttributes = modelAttributes.Where(x => x.RegisterId != MarketObjectsForModelingService.RegisterId).ToList();
            AddLog(Queue, $"Найдено {tourFactorsAttributes.Count} атрибутов для модели из таблицы с факторами тура.", logger: Logger);

            var dictionaries = GetDictionaries(modelAttributes);
            AddLog(Queue, $"Найдено {dictionaries?.Count} словарей для атрибутов  модели.", logger: Logger);

            List<MarketObjectToUnitRelation> marketObjectToUnitsRelation;
            using (Logger.TimeOperation("Получение Единиц оценки по Объектам аналогам"))
            {
	            marketObjectToUnitsRelation = GetMarketObjectToUnitRelation(marketObjects);
	            AddLog(Queue, $"Получено {marketObjectToUnitsRelation.Count(x => x.Unit?.Id != null)} Единиц оценки для всех объектов.", logger: Logger);
            }
            
            var counter = 0;
            AddLog(Queue, "Обработано объектов: ", logger: Logger);
            //берем 75% от всего числа объектов, из них первая половина для обучающей выборки, вторая - для контрольной 
            var objectsCount = marketObjectToUnitsRelation.Count * 75 / 100;
            var firstHalf = objectsCount / 2.0;

            var localCancelTokenSource = new CancellationTokenSource();
            var options = new ParallelOptions
            {
	            CancellationToken = localCancelTokenSource.Token,
	            MaxDegreeOfParallelism = 5
            };
            var packageSize = 1500;
            var numberOfPackages = marketObjectToUnitsRelation.Count / packageSize + 1;
            Parallel.For(0, numberOfPackages, options, (packageIndex, s) =>
            {
	            Logger.Debug("Начата работа с пакетом №{PackageIndex} из {MaxPackagesCount}", packageIndex, numberOfPackages);

                var marketObjectToUnitsPage = marketObjectToUnitsRelation.Skip(packageIndex * packageSize).Take(packageSize).ToList();
                if (marketObjectToUnitsPage.Count == 0)
                    return;

                var marketObjectIds = marketObjectToUnitsPage.Select(x => x.MarketObject.Id).ToList();
                var unitIds = marketObjectToUnitsPage.Where(x => x.Unit?.Id != null).Select(x => x.Unit.Id.Value).Distinct().ToList();

                var marketObjectCoefficients = GetCoefficientsFromMarketObject(marketObjectIds, dictionaries, marketObjectAttributes);
                var unitsCoefficients = GetCoefficientsFromTourFactors(unitIds, dictionaries, tourFactorsAttributes);

                marketObjectToUnitsPage.ForEach(marketObjectToUnitRelation =>
                {
                    var marketObject = marketObjectToUnitRelation.MarketObject;
                    var unit = marketObjectToUnitRelation.Unit;

                    var isForTraining = counter < firstHalf;
                    var isForControl = counter >= firstHalf && counter <= objectsCount;
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

                    var currentUnitCoefficients = unit != null && unitsCoefficients.TryGetValue(unit.Id.GetValueOrDefault(), out var unitCoefficients) 
	                    ? unitCoefficients 
	                    : new List<CoefficientForObject>();

                    currentMarketObjectCoefficients.AddRange(currentUnitCoefficients);

                    //если для объекта не найдены коэффициенты, заполняем их значением null
                    var resultCoefficients = modelAttributes.Select(x => new CoefficientForObject(x.AttributeId)).ToList();
                    currentMarketObjectCoefficients.ForEach(currentCoefficient =>
                    {
	                    var baseCoefficient = resultCoefficients.First(x => x.AttributeId == currentCoefficient.AttributeId);
	                    baseCoefficient.Coefficient = currentCoefficient.Coefficient;
	                    baseCoefficient.Value = currentCoefficient.Value;
                    });

                    modelToMarketObjectRelation.Coefficients = resultCoefficients.SerializeCoefficient();
                    modelToMarketObjectRelation.Save();

                    lock (_locker)
                    {
	                    //сохраняем в список, чтобы не выкачивать повторно
	                    ModelObjects.Add(modelToMarketObjectRelation);
                        counter++;
                        if (counter % packageSize == 0)
	                        AddLog(Queue, $"{counter}, ", false, logger: Logger);
                    }
                });

                Logger.Debug("Закончена работа с пакетом №{PackageIndex} из {MaxPackagesCount}.", packageIndex, numberOfPackages);
            });

            AddLog(Queue, $"{counter}.", false, logger: Logger);

            return counter;
        }

        private List<OMModelingDictionary> GetDictionaries(List<ModelAttributePure> modelAttributes)
        {
            var dictionaryIds = modelAttributes?.Where(x => x.DictionaryId != null).Select(x => x.DictionaryId.Value)
                .Distinct().ToList();

            return DictionaryService.GetDictionaries(dictionaryIds);
        }

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

        private List<MarketObjectToUnitRelation> GetMarketObjectToUnitRelation(List<MarketObjectPure> marketObjects)
        {
	        List<string> cadastralNumbers;
	        using (Logger.TimeOperation("Выборка КН из ОА"))
	        {
		        cadastralNumbers = marketObjects.Select(x => x.CadastralNumber).Distinct().ToList();
		        if (cadastralNumbers.Count == 0)
			        return new List<MarketObjectToUnitRelation>();

		        Logger.Debug("Найдено {CadastralNumbersCount} уникальных КН ОА", cadastralNumbers.Count);
            }

	        List<OMUnit> units;
	        using (Logger.TimeOperation("Поиск ЕО по КН объектов аналогов из тура с ИД {TourId}", Tour.Id))
	        {
		        units = OMUnit.Where(x => cadastralNumbers.Contains(x.CadastralNumber) && x.TourId == Tour.Id)
			        .Select(x => new
			        {
				        x.CadastralNumber,
				        x.PropertyType_Code,
				        x.BuildingCadastralNumber
			        })
			        .Execute();

		        Logger.Debug("Найдено {UnitsCount} ЕО", units.Count);
	        }

	        using (Logger.TimeOperation("Обработка помещений в найденных ЕО"))
	        {
		        var placementUnits = units.Where(x => x.PropertyType_Code == PropertyTypes.Pllacement).ToList();
		        if (placementUnits.Count > 0)
		        {
			        ProcessPlacemenUnits(placementUnits, units);
		        }
	        }

	        var unitsDictionary = units.GroupBy(x => x.CadastralNumber).ToDictionary(k => k.Key, v =>
	        {
		        var unit = v.First();

		        return new UnitPure
		        {
			        Id = unit.Id,
			        PropertyType = unit.PropertyType_Code
		        };
	        });

            var marketObjectToUnitRelation = new List<MarketObjectToUnitRelation>();
            using (Logger.TimeOperation("Формирование отношений ЕО к ОА"))
            {
	            marketObjects.ForEach(x =>
	            {
		            var resultUnit = unitsDictionary.TryGetValue(x.CadastralNumber, out var unit) ? unit : null;
		            if (resultUnit == null && InputParameters.IsExcludeMarketObjectsWithoutUnit)
			            return;

		            marketObjectToUnitRelation.Add(new MarketObjectToUnitRelation
		            {
			            MarketObject = x,
			            Unit = resultUnit
		            });
	            });

                Logger.Debug("Изанчально было {MarketObjectsCount} ОА, после фильтрации по непустой ЕО осталось {LeftCount}", marketObjects.Count, marketObjectToUnitRelation.Count);
            }

            return marketObjectToUnitRelation;
        }

        private void ProcessPlacemenUnits(List<OMUnit> placementUnits, List<OMUnit> units)
        {
            AddLog(Queue, $"Найдено {placementUnits.Count} Единиц оценки с типом '{PropertyTypes.Pllacement.GetEnumDescription()}'", logger: Logger);

            var buildingCadastralNumbers = placementUnits
                .Where(x => !string.IsNullOrWhiteSpace(x.BuildingCadastralNumber))
                .Select(x => x.BuildingCadastralNumber).Distinct().ToList();
            Logger.Debug("Найдено {BuildingCadastralNumbersCount} уникальных КН зданий", buildingCadastralNumbers.Count);

            List<OMUnit> buildingUnits;
            using (Logger.TimeOperation("Получение ЕО по ранее найденным КН зданий из тура"))
            {
	            buildingUnits = buildingCadastralNumbers.Count > 0
		            ? OMUnit.Where(x =>
				            buildingCadastralNumbers.Contains(x.CadastralNumber) &&
				            x.PropertyType_Code == PropertyTypes.Building && x.TourId == Tour.Id)
			            .Select(x => new
			            {
				            x.CadastralNumber,
                            x.PropertyType_Code
			            })
			            .Execute()
		            : new List<OMUnit>();
	            
	            Logger.Debug("Найдено {BuildingUnitsCount} ЕО с типом здание", buildingUnits.Count);
            }

            using (Logger.TimeOperation("Замена ЕД помещений на здания"))
            {
	            placementUnits.ForEach(placementUnit =>
	            {
		            if (!string.IsNullOrWhiteSpace(placementUnit.BuildingCadastralNumber))
		            {
			            var currentBuildingUnits = buildingUnits
				            .Where(x => x.CadastralNumber == placementUnit.BuildingCadastralNumber).ToList();
			            currentBuildingUnits.ForEach(x => x.CadastralNumber = placementUnit.CadastralNumber);
			            units.AddRange(buildingUnits);
			            AddLog(Queue, $"Единица оценки заменена на аналогичную по Кадастровому номеру здания '{placementUnit.BuildingCadastralNumber}'", logger: Logger);
		            }

		            units.Remove(placementUnit);
	            });
            }
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
