﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Tasks;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public class UpdateTaskCadastralDataLongProcess : LongProcess
	{
		public const string LongProcessName = "UpdateTaskCadastralData";

		private readonly ILogger _log = Log.ForContext<HarmonizationProcess>();

		public static long AddProcessToQueue(long taskId)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, OMTask.GetRegisterId(), taskId);
		}

		private SystemAttributeSettingsService SystemAttributeSettingsService { get; }
		private GbuObjectService GbuObjectService { get; }
		private TaskService TaskService { get; }

		public UpdateTaskCadastralDataLongProcess()
		{
			SystemAttributeSettingsService = new SystemAttributeSettingsService();
			GbuObjectService = new GbuObjectService();
			TaskService = new TaskService();
		}

		private long? _cadastralQuarterAttrId;
		private long? _buildingCadastralNumberAttrId;
		private string _taskName;
		private object _locked;

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.Information("Старт фонового процесса: {ProcessType}", processType.Description);
			WorkerCommon.SetProgress(processQueue, 0);

			if (!processQueue.ObjectId.HasValue)
			{
				_log.Warning("В фоновый процесс {ProcessType} не передан ObjectId (ИД задания на оценку)", processType.Description);
				WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				NotificationSender.SendNotification(processQueue, "Актуализация кадастровых данных",
					"Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов");
				return;
			}

			try
			{
				_log.Information("Начато обновление кадастровых данных для задания на оценку {TaskId}", processQueue.ObjectId.Value);
				var reportId = UpdateTaskCadastralData(processQueue.ObjectId.Value);
				NotificationSender.SendNotification(processQueue, "Результат Операции Актуализации кадастровых данных", GetEmailMessage(reportId));
				WorkerCommon.SetProgress(processQueue, 100);
				_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);
			}
			catch(Exception ex)
			{
				var errorId = ErrorManager.LogError(ex);
				var message = $"Операция Актуализации кадастровых данных была прервана: {ex.Message} (Подробнее в журнале: {errorId})";
				NotificationSender.SendNotification(processQueue, "Актуализация кадастровых данных", message);
				throw;
			}
		}

		private long UpdateTaskCadastralData(long taskId)
		{
			SetupInitialSettings(taskId);
			var units = GetUnits(taskId);
			_log.ForContext("TaskId", taskId)
				.Debug("Загружено {UnitsCount} единиц оценки для обработки", units.Count);

			var reportService = new GbuReportService();
			reportService.AddHeaders(
				new List<string>
				{
					"Кадастровый номер", "Кадастровый квартал старый", "Кадастровый квартал новый",
					"Кадастровый номер здания старый", "Кадастровый номер здания новый"
				});

			_locked = new object();
			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 20
			};
			var currentDate = DateTime.Now;
			var gbuAttrIdList = new List<long>();
			if(_cadastralQuarterAttrId.HasValue)
				gbuAttrIdList.Add(_cadastralQuarterAttrId.Value);
			if(_buildingCadastralNumberAttrId.HasValue)
				gbuAttrIdList.Add(_buildingCadastralNumberAttrId.Value);

			_log.ForContext("BuildingCadastralNumberAttrId", _buildingCadastralNumberAttrId)
				.ForContext("CadastralQuarterAttrId", _cadastralQuarterAttrId)
				.Debug("Получение значений гбу атрибутов юнитов");
			var gbuAttrValues = GbuObjectService.GetAllAttributes(units.Select(x => x.ObjectId.Value).Distinct().ToList(), null,
				gbuAttrIdList, currentDate, isLight: true);

			var cadastralQuartalDictionary = new Dictionary<long, string>();
			if (_cadastralQuarterAttrId.HasValue)
			{
				cadastralQuartalDictionary = gbuAttrValues.Where(x => x.AttributeId == _cadastralQuarterAttrId.Value)
					.ToDictionary(x => x.ObjectId, x => x.GetValueInString());
				_log.ForContext("CadastralQuartalDictionaryCount", cadastralQuartalDictionary.Count)
					.Debug("Добавлены значения в словарь данных для кадастровых кварталов");
			}

			var buildingCadastralNumberDictionary = new Dictionary<long, string>();
			var buildingCadastralNumberDictionaryForPlacements = new Dictionary<long, string>();
			if (_buildingCadastralNumberAttrId.HasValue)
			{
				buildingCadastralNumberDictionary = gbuAttrValues.Where(x => x.AttributeId == _buildingCadastralNumberAttrId.Value)
					.ToDictionary(x => x.ObjectId, x => x.GetValueInString());
				_log.ForContext("BuildingCadastralNumberDictionaryCount", buildingCadastralNumberDictionary.Count)
					.Debug("Добавлены значения в словарь данных для кадастровых номеров зданий");

				var placementGbuObjIds = units.Where(x => x.PropertyType_Code == PropertyTypes.Pllacement)
					.Select(x => x.ObjectId.Value).Distinct().ToList();
				buildingCadastralNumberDictionaryForPlacements = GetBuildingCadastralNumberDictionaryForPlacements(placementGbuObjIds,
					gbuAttrValues, currentDate, buildingCadastralNumberDictionary);
				_log.ForContext("BuildingCadastralNumberDictionaryForPlacementCount", buildingCadastralNumberDictionaryForPlacements.Count)
					.Debug("Добавлены значения в словарь данных для кадастровых номеров зданий для помещений");
			}

			Parallel.ForEach(units, options, unit =>
			{
				var currentCadastralQuarter = unit.CadastralBlock;
				var currentBuildingCadastralNumber = unit.BuildingCadastralNumber;
				currentCadastralQuarter = cadastralQuartalDictionary.ContainsKey(unit.ObjectId.Value) && !string.IsNullOrEmpty(cadastralQuartalDictionary[unit.ObjectId.Value])
					? cadastralQuartalDictionary[unit.ObjectId.Value]
					: currentCadastralQuarter;
				if (unit.PropertyType_Code == PropertyTypes.Pllacement)
				{
					currentBuildingCadastralNumber =
						buildingCadastralNumberDictionaryForPlacements.ContainsKey(unit.ObjectId.Value) && !string.IsNullOrEmpty(buildingCadastralNumberDictionaryForPlacements[unit.ObjectId.Value])
							? buildingCadastralNumberDictionaryForPlacements[unit.ObjectId.Value]
							: currentBuildingCadastralNumber;
				}
				else
				{
					currentBuildingCadastralNumber = buildingCadastralNumberDictionary.ContainsKey(unit.ObjectId.Value) && !string.IsNullOrEmpty(buildingCadastralNumberDictionary[unit.ObjectId.Value])
						? buildingCadastralNumberDictionary[unit.ObjectId.Value]
						: currentBuildingCadastralNumber;
				}

				if (currentCadastralQuarter != unit.CadastralBlock || currentBuildingCadastralNumber != unit.BuildingCadastralNumber)
				{
					lock (_locked)
					{
						var row = reportService.GetCurrentRow();
						AddRowToReport(row,
							unit.CadastralNumber,
							unit.CadastralBlock,
							currentCadastralQuarter,
							unit.BuildingCadastralNumber,
							currentBuildingCadastralNumber,
							reportService);
					}

					_log.ForContext("ObjectId", unit.ObjectId.Value)
						.ForContext("UnitId", unit.Id)
						.ForContext("PrevCadastralQuarter", unit.CadastralBlock)
						.ForContext("CurrentCadastralQuarter", currentCadastralQuarter)
						.ForContext("PrevBuildingCadastralNumber", unit.BuildingCadastralNumber)
						.ForContext("CurrentBuildingCadastralNumber", currentBuildingCadastralNumber)
						.Verbose("Выполнение обновления единицы оценки {UnitCadastralNumber}",
							unit.CadastralNumber);

					unit.CadastralBlock = currentCadastralQuarter;
					unit.BuildingCadastralNumber = currentBuildingCadastralNumber;
					unit.Save();
				}
			});

			_log.Debug("Получение информации о наличии предыдущих единиц оценки у обновляемых юнитов");
			var unitWithHistoryIds = GetUnitIdWithHistory(taskId);
			_log.Debug("Найдена информация о наличии предыдущих единиц оценки у {UnitsWithHistoryCount} из {UnitsCount} обновляемых юнитов", unitWithHistoryIds.Count, units.Count);
			_log.Debug("Сохранение наследуемых атрибутов");
			var unitsWithHistory = units.Where(x => unitWithHistoryIds.Contains(x.Id)).ToList();
			Parallel.ForEach(unitsWithHistory, options, unit =>
			{
				unit.InheritedKOFactors(_log);
			});

			_log.Debug("Настройка стилей отчета");
			reportService.SetStyle();
			reportService.SetIndividualWidth(0, 4);
			reportService.SetIndividualWidth(1, 4);
			reportService.SetIndividualWidth(2, 4);
			reportService.SetIndividualWidth(3, 4);
			reportService.SetIndividualWidth(4, 4);

			_log.Debug("Сохранение отчета");
			var reportId = reportService.SaveReport("Отчет актуализация кадастровых данных", OMTask.GetRegisterId(), "KoTasks");

			return reportId;
		}

		private Dictionary<long, string> GetBuildingCadastralNumberDictionaryForPlacements(List<long> placementGbuObjIds, List<GbuObjectAttribute> gbuAttrValues,
			DateTime currentDate, Dictionary<long, string> buildingCadastralNumberDictionary)
		{
			var result = new Dictionary<long, string>();

			var placementBuildingCadastralNumbers = gbuAttrValues.Where(x =>
				placementGbuObjIds.Contains(x.ObjectId) && x.AttributeId == _buildingCadastralNumberAttrId.Value).Select(x => x.GetValueInString()).Distinct().ToList();
			if (!placementBuildingCadastralNumbers.IsEmpty())
			{
				//По кадастровому номеру здания из гбу части ищем Объекты недвижимости
				var buildingGbuObjects = OMMainObject
					.Where(x => placementBuildingCadastralNumbers.Contains(x.CadastralNumber))
					.Select(x => new {x.Id, x.CadastralNumber})
					.Execute();
				_log.Debug("Найдено {PlacementBuildingGbuObjectsCount} гбу объектов по кадастровому номеру здания для помещений", buildingGbuObjects.Count);
				//Для найденных объектов недвижимости ищем значения гбу атрибута Кадастровый номер здания
				var builgingsBuildingCadastralNumberGbuAttr = GbuObjectService.GetAllAttributes(
					buildingGbuObjects.Select(x => x.Id).Distinct().ToList(), null,
					new List<long> {_buildingCadastralNumberAttrId.Value}, currentDate, isLight: true);
				_log.Debug("Найдено {PlacementBuildingCadastralNumberGbuAttrCount} гбу атрибутов кадастровых номеров зданий для зданий помещений", builgingsBuildingCadastralNumberGbuAttr.Count);
				foreach (var placementsObjId in placementGbuObjIds)
				{
					if (buildingCadastralNumberDictionary.ContainsKey(placementsObjId))
					{
						var buildingCadastralNumber = buildingCadastralNumberDictionary[placementsObjId];
						var buildingGbuId = buildingGbuObjects.FirstOrDefault(x =>
							x.CadastralNumber == buildingCadastralNumber)?.Id;
						if (buildingGbuId.HasValue)
						{
							var attrValue = builgingsBuildingCadastralNumberGbuAttr
								.FirstOrDefault(x => x.Id == buildingGbuId.Value)?.GetValueInString();
							if (!string.IsNullOrEmpty(attrValue))
							{
								result.Add(placementsObjId, attrValue);
							}
						}
					}
				}
			}

			return result;
		}

		private void SetupInitialSettings(long taskId)
		{
			var cadastralQuarterAttrId = SystemAttributeSettingsService.GetCadastralDataCadastralQuarterAttributeId();
			_cadastralQuarterAttrId = cadastralQuarterAttrId;
			var buildingCadastralNumberAttrId = SystemAttributeSettingsService.GetCadastralDataBuildingCadastralNumberAttributeId();
			_buildingCadastralNumberAttrId = buildingCadastralNumberAttrId;
			_taskName = TaskService.GetTemplateForTaskName(taskId);

			_log.ForContext("CadastralQuarterAttrId", _cadastralQuarterAttrId)
				.ForContext("BuildingCadastralNumberAttrId", _buildingCadastralNumberAttrId)
				.ForContext("TaskName", _taskName)
				.Debug("Установлены начальные настройки для выполнения процесса");
		}

		private List<OMUnit> GetUnits(long taskId)
		{
			return OMUnit.Where(x => x.TaskId == taskId)
				.Select(x => new
				{
					x.Id,
					x.ObjectId,
					x.CadastralNumber,
					x.CadastralBlock,
					x.BuildingCadastralNumber,
					x.TourId,
					x.PropertyType,
					x.PropertyType_Code
				})
				.Execute();
		}

		private HashSet<long> GetUnitIdWithHistory(long taskId)
		{
			var query = OMUnit.Where(x => x.TaskId == taskId);

			var subQuery = new QSQuery(OMUnit.GetRegisterId())
			{
				Columns = new List<QSColumn>
				{
					new QSColumnConstant(1)
				},
				Condition = new QSConditionGroup(QSConditionGroupType.And)
				{
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(
							OMUnit.GetColumn(x => x.TourId),
							QSConditionType.Equal,
							OMUnit.GetColumn(x => x.TourId)){
							RightOperandLevel = 1
						},
						new QSConditionSimple(
							OMUnit.GetColumn(x => x.CadastralNumber),
							QSConditionType.Equal,
							OMUnit.GetColumn(x => x.CadastralNumber)){
							RightOperandLevel = 1
						},
						new QSConditionSimple(
							OMUnit.GetColumn(x => x.Id),
							QSConditionType.NotEqual,
							OMUnit.GetColumn(x => x.Id)){
							RightOperandLevel = 1
						}
					}
				}
			};
			var historyUnitsCondition = new QSConditionSimple(new QSColumnQuery(subQuery, "historyUnits"), QSConditionType.Exists);
			query = query.And(historyUnitsCondition);
			var unitIds = query.Select(x => x.Id).Execute().Select(x => x.Id);

			return new HashSet<long>(unitIds);
		}

		private void AddRowToReport(GbuReportService.Row rowNumber, string kn, string cadastralQuarterOld, string cadastralQuarterNew, string buildingKnOld, string buildingKnNew, GbuReportService reportService)
		{
			reportService.AddValue(kn, 0, rowNumber);
			reportService.AddValue(cadastralQuarterOld, 1, rowNumber);
			reportService.AddValue(cadastralQuarterNew, 2, rowNumber);
			reportService.AddValue(buildingKnOld, 3, rowNumber);
			reportService.AddValue(buildingKnNew, 4, rowNumber);
		}

		private string GetEmailMessage(long reportId)
		{
			return $"Операция Актуализации кадастровых данных для задания на оценку '{_taskName}' успешно завершена. " +
			       $@"<a href=""/DataExport/DownloadExportResult?exportId={reportId}"">Скачать результат</a>";
		}
	}
}
