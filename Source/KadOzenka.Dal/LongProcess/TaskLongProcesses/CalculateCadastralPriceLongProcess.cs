using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.LongProcess._Common;
using KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Entities;
using KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Exceptions;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Units;
using KadOzenka.Dal.Units.Repositories;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Factors;
using ModelingBusiness.Model;
using ModelingBusiness.Model.Formulas;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;
using org.mariuszgromada.math.mxparser;
using Serilog;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public class CalculateCadastralPriceLongProcess : LongProcess
	{
		public static string AttributePrefixInFormula = "factor_";
		public const string LongProcessName = "CalculateCadastralPrice";
		private static readonly int GroupColumn = 0;
		private static readonly int TaskColumn = 1;
		private static readonly int PropertyTypeColumn = 2;
		private static readonly int KnColumn = 3;
		private static readonly int ErrorColumn = 4;
		private readonly object _locker;
		private OMQueue _queue;
		private readonly ILogger _log = Log.ForContext<CalculateCadastralPriceLongProcess>();
		private List<CalcErrorItem> _errorsDuringCalculation;

		private IRegisterCacheWrapper RegisterCacheWrapper { get; }
		private IUnitService UnitService { get; }
		private IModelService ModelService { get; }
		private IModelFactorsService ModelFactorsService { get; }
		private IModelDictionaryService ModelDictionaryService { get; }
		private IGroupService GroupService { get; }
		private IUnitRepository UnitRepository { get; }


		public CalculateCadastralPriceLongProcess(IUnitRepository unitRepository = null,
			IUnitService unitService = null, IModelService modelService = null,
			IModelFactorsService modelFactorsService = null,
			IModelDictionaryService modelDictionaryService = null,
			IGroupService groupService = null,
			IRegisterCacheWrapper registerCacheWrapper = null)
		{
			UnitRepository = unitRepository ?? new UnitRepository();
			UnitService = unitService ?? new UnitService();
			RegisterCacheWrapper = registerCacheWrapper ?? new RegisterCacheWrapper();
			ModelService = modelService ?? new ModelService();
			ModelFactorsService = modelFactorsService ?? new ModelFactorsService();
			ModelDictionaryService = modelDictionaryService ?? new ModelDictionaryService();
			GroupService = groupService ?? new GroupService();

			_locker = new object();
			_errorsDuringCalculation = new List<CalcErrorItem>();
		}

		//служба длительных процессов основана на конструкторе без параметров
		public CalculateCadastralPriceLongProcess()
		{
			UnitRepository = new UnitRepository();
			UnitService = new UnitService();
			RegisterCacheWrapper = new RegisterCacheWrapper();
			ModelService = new ModelService();
			ModelFactorsService = new ModelFactorsService();
			ModelDictionaryService = new ModelDictionaryService();
			GroupService = new GroupService();

			_locker = new object();
			_errorsDuringCalculation = new List<CalcErrorItem>();
		}



		public static long AddProcessToQueue(CadastralPriceCalculationSettions settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.ForContext("InputParameters", processQueue.Parameters).Debug("Старт процесса расчета. Входные параметры");

			try
			{
				WorkerCommon.SetProgress(processQueue, 0);
				_queue = processQueue;

				var settings = processQueue.Parameters.DeserializeFromXml<CadastralPriceCalculationSettions>();
				var errorsDuringCalculation = DoCalculation(settings, cancellationToken);
				
				string linkToReport = null;
				if (errorsDuringCalculation.Count > 0)
				{
					_log.Debug("Начато формирование отчета");
					var urlToDownloadReport = FormReport(errorsDuringCalculation);
					linkToReport = string.IsNullOrWhiteSpace(urlToDownloadReport) ? string.Empty : $@"<a href=""{urlToDownloadReport}"">Скачать результат</a>";
					_log.Debug("Закончено формирование отчета");
				}
				CompareData(settings.TaskIds);

				WorkerCommon.SetProgress(processQueue, 100);

				var message = "Операция успешно завершена." + linkToReport;
				NotificationSender.SendNotification(processQueue, "Результат Операции Расчета кадастровой стоимости", message);
			}
			catch (OperationCanceledException ex)
			{
				_log.Error(ex, "Операция остановлена пользователем");
				NotificationSender.SendNotification(processQueue, "Результат Операции Расчета кадастровой стоимости", "Операция была остановлена пользователем");
			}
			catch (Exception ex)
			{
				var errorId = ErrorManager.LogError(ex);
				NotificationSender.SendNotification(processQueue, "Результат Операции Расчета кадастровой стоимости", $"Операция была прервана: {ex.Message} (Подробнее в журнале: {errorId})");
				throw;
			}

			_log.Debug("Финиш процесса расчета");
		}



		#region Support Methods

		public List<CalcErrorItem> DoCalculation(CadastralPriceCalculationSettions settings, CancellationToken cancellationToken)
		{
			//группы с активной ручной-мультипликативной моделью считаем через новую реализацию. остальные - через старую

			if (settings.IsAllGroups)
			{
				throw new Exception("Расчет не поддерживается");
			}
			else
			{
				var groups = GroupService.GetGroupsByIds(settings.SelectedGroupIds).ToList();
				if (groups.Count == 0)
					throw new Exception($"Не найдено групп с ИД: {string.Join(',', settings.SelectedGroupIds)}");

				var processedUnitsCount = 0;
				var maxUnitsCount = GetMaxUnitsCount(settings);
				groups.ForEach(group =>
				{
					_log.Debug("Начата обработка группы '{GroupName}' (с ИД - {GroupId})", group.GroupName, group.Id);

					var activeGroupModel = ModelService.GetActiveModelEntityByGroupId(group.Id);
					if (activeGroupModel == null)
					{
						_log.Error($"Не найдена активная модель для группы '{group.GroupName}' (с ИД - {group.Id}). ЕО добавляются в отчет.");
						
						var units = GetUnits(settings, group.Id);
						units.ForEach(x => AddError(x, group.Id, Messages.NoActiveModelInCadasralPriceCalculation));
						processedUnitsCount += units.Count;
						
						return;
					}

					_log.Debug("Активная модель - '{ModelName}' (с ИД - {ModelId})", activeGroupModel.Name, activeGroupModel.Id);
					if (activeGroupModel.Type_Code == KoModelType.Manual)
					{
						CalculateByNewRealization(settings, activeGroupModel, group.Id, cancellationToken,
							maxUnitsCount, processedUnitsCount);
					}
					else
					{
						throw new Exception($"Расчет для модели '{activeGroupModel.Name}' не поддерживается");
						//settings.SelectedGroupIds = new List<long> {group.Id};
						//var errors = CalculateByOldRealization(settings);
						//_errorsDuringCalculation.AddRange(errors);
					}
				});
			}

			return _errorsDuringCalculation;
		}

		private void CalculateByNewRealization(CadastralPriceCalculationSettions settings, OMModel activeGroupModel, 
			long groupId, CancellationToken cancellationToken, int maxUnitsCount, int processedUnitsCount)
		{
			_log.Debug("Начат расчет через новую реализацию");

			var modelFactors = GetModelFactors(activeGroupModel);
			var formula = PrepareFormula(activeGroupModel, modelFactors);

			var marks = GetMarks(modelFactors);
			//если будут проблемы с производительностью вынески выгрузку ЕО в потоки
			var units = GetUnits(settings, groupId);
			if (units.Count == 0)
				return;

			var processedPackageCount = 0;
			var processConfiguration = GetProcessConfiguration(units.Count);
			Parallel.For(0, processConfiguration.NumberOfPackages, processConfiguration.ParallelOptions, (packageIndex, s) =>
			{
				CheckCancellationToken(cancellationToken, processConfiguration.CancellationTokenSource, processConfiguration.ParallelOptions);

				var unitsPackage = units.Skip(packageIndex * processConfiguration.PackageSize).Take(processConfiguration.PackageSize).ToList();
				_log.Debug("Начата работа с пакетом №{i} из {NumberOfPackages}. В нем {UnitsInPackageCount} ЕО", packageIndex, processConfiguration.NumberOfPackages, unitsPackage.Count);

				var unitIds = unitsPackage.Select(x => x.Id).ToList();
				var modelFactorIds = modelFactors.Select(x => x.FactorId).ToList();
				var unitsPackageFactors = UnitService.GetUnitsFactors(unitIds, settings.TourId, settings.IsParcel, modelFactorIds);
				_log.Debug("Загружено {UnitFactorsCount} факторов ЕО", unitsPackageFactors.Count);

				unitsPackage.ForEach(currentUnit =>
				{
					decimal upks = 0;
					try
					{
						CheckCancellationToken(cancellationToken, processConfiguration.CancellationTokenSource,
							processConfiguration.ParallelOptions);

						unitsPackageFactors.TryGetValue(currentUnit.Id, out var currentUnitFactors);
						ValidateUnitBeforeCalculation(currentUnit, currentUnitFactors, modelFactors);

						upks = (decimal) CalculateUpks(formula, modelFactors, currentUnitFactors, marks);
						currentUnit.Upks = upks;
						currentUnit.CadastralCost = upks * currentUnit.Square.Value;
						UnitRepository.Save(currentUnit);

						Interlocked.Increment(ref processedUnitsCount);
						lock (_locker)
						{
							LongProcessProgressLogger.LogProgress(maxUnitsCount, processedUnitsCount, _queue);
						}
					}
					catch (OverflowException e)
					{
						_log.Error(e, "Ошибка по время обработки ЕО с КН '{CadastralNumber}'. Переполнение decimal. Рассчитанная УПКС равна '{CalculatedCost}'", currentUnit.CadastralNumber, upks);

						AddError(currentUnit, groupId, $"Рассчитанная УПКС невалидна (слишком большое, либо слишком маленькое число). {upks}");
					}
					catch (Exception e)
					{
						_log.Error(e, "Ошибка по время обработки ЕО с КН '{CadastralNumber}'. Рассчитанная УПКС равна '{CalculatedCost}'", currentUnit.CadastralNumber, upks);

						AddError(currentUnit, groupId, e.Message);
					}
				});

				Interlocked.Increment(ref processedPackageCount);
				_log.Debug("Всего обработано пакетов {ProcessedPackageCount} из {NumberOfPackages}. И {ProcessedUnitsCount} ЕО из {UnitsCount}", processedPackageCount, processConfiguration.NumberOfPackages, processedUnitsCount, processConfiguration.MaxUnitsCount);
			});

			_log.Debug("Закончен расчет через новую реализацию");
		}

		private void ValidateUnitBeforeCalculation(OMUnit currentUnit, List<UnitFactor> unitFactors, List<FactorInfo> modelFactors)
		{
			if (currentUnit.Square.GetValueOrDefault() == 0)
				throw new NoInfoForCalculationException("У ЕО не заполнена площадь");

			if (unitFactors.IsEmpty())
				throw new NoInfoForCalculationException(Messages.UnitDoesNotHaveFactorsToCalculateCandastralPrice);

			var notEmptyUnitFactors = unitFactors.Where(x => x.Value != null).ToList();
			if (notEmptyUnitFactors.Count != modelFactors.Count)
			{
				var emptyFactors = unitFactors.Where(x => x.Value == null).ToList();
				throw new NoInfoForCalculationException($"{Messages.NotAllUnitFactorsAreFullToCalculateCadastralPrice}: {string.Join(',', emptyFactors.Select(x => x.AttributeData.Name))}");
			}
		}

		//private List<CalcErrorItem> CalculateByOldRealization(CadastralPriceCalculationSettions settings)
		//{
		//	_log.Debug("Начат расчет по старой реализации");
		//	var errorsDuringCalculation = OMGroup.CalculateSelectGroup(settings);
		//	_log.ForContext("Result", errorsDuringCalculation, true).Debug("Закончен расчет. Возвращенное значение.");
			
		//	return errorsDuringCalculation;
		//}

		private void CompareData(List<long> taskIds)
		{
			try
			{
				_log.Debug("Начато сравнение данных ПККО и РСМ");
				if (taskIds.Count > 0)
				{
					var tasks = OMTask.Where(x => taskIds.Contains(x.Id)).SelectAll().Execute();
					foreach (var task in tasks)
					{
						var path = CadastralCostDataComparingStorageManager.GetTaskRsmFolderFullPath(task);
						var unloadSettings = new KOUnloadSettings
							{TaskFilter = new List<long> {task.Id}, IsDataComparingUnload = true, DirectoryName = path};
						DEKOUnit.ExportToXml(null, unloadSettings, null);
					}
				}

				_log.Debug("Закончено сравнение данных ПККО и РСМ");
			}
			catch (Exception e)
			{
				_log.Error("Ошибка во время сравнения данных ПККО и РСМ", e);
			}
		}

		private ProcessConfiguration GetProcessConfiguration(int unitsCount)
		{
			var defaultPackageSize = 1000;
			var defaultThreadsCount = 5;

			var settingsFromConfig = GetParallelThreadsConfig("CadastralPriceCalculation", defaultPackageSize, defaultThreadsCount);

			return new ProcessConfiguration(settingsFromConfig.PackageSize, settingsFromConfig.ThreadsCount, unitsCount);
		}

		private List<FactorInfo> GetModelFactors(OMModel activeGroupModel)
		{
			var modelFactors = ModelFactorsService.GetFactors(activeGroupModel.Id, activeGroupModel.AlgoritmType_Code);
			if (modelFactors.Count == 0)
				throw new Exception($"У модели '{activeGroupModel.Name}' (С ИД - {activeGroupModel.Id}) нет факторов");
			
			_log.Debug("Загружено {FactorsCount} факторов модели", modelFactors.Count);

			var factors = modelFactors.Select(factor =>
			{
				var attribute = RegisterCacheWrapper.GetAttributeData(factor.FactorId.GetValueOrDefault());

				return new FactorInfo
				{
					FactorId = factor.FactorId.GetValueOrDefault(),
					DictionaryId = factor.DictionaryId,
					MarkType = factor.MarkType_Code,
					AttributeName = attribute.Name,
					AttributeType = attribute.Type
				};
			}).ToList();

			return factors;
		}

		public string PrepareFormula(OMModel activeGroupModel, List<FactorInfo> factors)
		{
			var formula = ModelService.GetFormula(activeGroupModel, activeGroupModel.AlgoritmType_Code);
			_log.Debug("Начальная формула: {Formula}", formula);

			//имена факторов в формуле записываются через кавычки
			formula = formula.Replace("\"", "");
			factors.ForEach(factor =>
			{
				var factorNameInFormula = factor.AttributeName;
				if (factor.MarkType == MarkType.Default)
				{
					factorNameInFormula = $"{BaseFormula.MarkTagInFormula}({factorNameInFormula})";
				}

				formula = formula.Replace(factorNameInFormula, $"{AttributePrefixInFormula}{factor.FactorId}");
			});

			_log.Debug("Обработанная формула (для постановки чисел): {Formula}", formula);

			return formula;
		}

		private Dictionary<string, decimal?> GetMarks(List<FactorInfo> factors)
		{
			var factorsWithMarks = factors.Where(x => x.MarkType == MarkType.Default || x.DictionaryId != null)
				.Select(x => x.DictionaryId).ToList();

			_log.ForContext("FactorsWithDefaultMarkIds", factorsWithMarks, true)
				.Debug("Начата выгрузка меток для {FactorsCount} факторов с меткой по умолчанию", factorsWithMarks.Count);

			var marks = ModelDictionaryService.GetMarks(factorsWithMarks);
			_log.Debug("Выгружено {MarksCount} меток", marks.Count);

			var groupedMarks = marks
				.GroupBy(x => x.Value)
				.ToDictionary(x => x.Key, x =>
				{
					var marksInGroup = x.ToList();
					var firstMark = marksInGroup.FirstOrDefault();
					if (marksInGroup.Count > 1)
						_log.Warning("Найдено более одной метки для словаря {DictionaryId}, значения {Value}", firstMark?.DictionaryId, x.Key);
					
					return firstMark?.CalculationValue;
				});
			_log.Debug("Сформированы словари меток с {KeysCount} ключами по фактору и значению", groupedMarks.Count);

			return groupedMarks;
		}

		public double CalculateUpks(string formula, List<FactorInfo> factors, List<UnitFactor> unitsFactors, Dictionary<string, decimal?> marks)
		{
			var arguments = new PrimitiveElement[unitsFactors.Count];
			for (var i = 0; i < unitsFactors.Count; i++)
			{
				var currentUnitFactor = unitsFactors[i];
				var factorId = currentUnitFactor.AttributeId;
				var unitFactorValue = currentUnitFactor.GetValueInString();
				var modelFactor = factors.First(x => x.FactorId == factorId);
				
				Argument argument;
				var factorNameInFormula = $"{AttributePrefixInFormula}{factorId}";
				if (modelFactor.MarkType == MarkType.Default)
				{
					var metka = GetMetkaFromMarkCatalog(marks, modelFactor, unitFactorValue);
					argument = new Argument(factorNameInFormula, (double)metka);
				}
				else
				{
					switch (modelFactor.AttributeType)
					{
						case RegisterAttributeType.INTEGER:
							argument = new Argument(factorNameInFormula, currentUnitFactor.LongValue.GetValueOrDefault());
							break;
						case RegisterAttributeType.DECIMAL:
							argument = new Argument(factorNameInFormula, (double)currentUnitFactor.DecimalValue.GetValueOrDefault());
							break;
						default:
							var metka = GetMetkaFromMarkCatalog(marks, modelFactor, unitFactorValue);
							argument = new Argument(factorNameInFormula, (double)metka);
							break;
					}
				}
				
				arguments[i] = argument;
			}

			var expression = new org.mariuszgromada.math.mxparser.Expression(formula, arguments);

			double price;
			try
			{
				price = expression.calculate();
			}
			catch (Exception e)
			{
				_log.Error(e, "Ошибка при расчете формулы через стороннюю библиотеку");
				throw new CalculationException(expression, factors);
			}

			if (double.IsNaN(price) || double.IsInfinity(price))
			{
				_log.Error($"Сторонняя библиотека не смогла посчитать значение цены. Возвращенное значение '{price}'");
				throw new CalculationException(expression, factors);
			}

			if (price == 0)
				throw new ZeroPriceException(expression, factors);

			return price;
		}

		private decimal GetMetkaFromMarkCatalog(Dictionary<string, decimal?> marks, FactorInfo unitFactor, string value)
		{
			if (!marks.TryGetValue(value, out var metka))
				throw new NoInfoForCalculationException($"Не найдена метка для фактора '{unitFactor.AttributeName}' (ИД {unitFactor.FactorId}) со значением '{value}'");

			if (metka == null)
				throw new NoInfoForCalculationException($"Метка для фактора '{unitFactor.AttributeName}' (ИД {unitFactor.FactorId}) пустая");

			return metka.Value;
		}

		private List<OMUnit> GetUnits(CadastralPriceCalculationSettions settings, long groupId)
		{
			_log.Debug("Начато скачивание ЕО");

			var lambda = BuildExpressionForUnits(settings, new List<long> {groupId});

			var units = UnitRepository.GetEntitiesByCondition(lambda, x => new
			{
				x.CadastralNumber,
				x.TaskId,
				x.PropertyType_Code,
				x.Square,
				x.CadastralCost,
				x.Upks
			});

			_log.Debug("Выгружено {UnitsCount} ЕО", units.Count);

			return units;
		}

		private int GetMaxUnitsCount(CadastralPriceCalculationSettions settings)
		{
			_log.Debug("Начат расчет общего количества ЕО по всем группам");

			var lambda = BuildExpressionForUnits(settings, settings.SelectedGroupIds);
			var maxUnitsCount = UnitRepository.ExecuteCount(lambda);

			_log.Debug("Общего число ЕО по всем группам - {MaxUnitsCount}", maxUnitsCount);

			return maxUnitsCount;
		}

		private Expression<Func<OMUnit, bool>> BuildExpressionForUnits(CadastralPriceCalculationSettions settings, List<long> groupIds)
		{
			Expression<Func<OMUnit, bool>> baseExpression = x =>
				settings.TaskIds.Contains((long) x.TaskId) && groupIds.Contains((long) x.GroupId);

			Expression<Func<OMUnit, bool>> typeCondition = settings.IsParcel
				? x => x.PropertyType_Code == PropertyTypes.Stead
				: x => x.PropertyType_Code != PropertyTypes.Stead;

			var body = System.Linq.Expressions.Expression.AndAlso(baseExpression.Body, typeCondition.Body);
			var lambda = System.Linq.Expressions.Expression.Lambda<Func<OMUnit, bool>>(body, baseExpression.Parameters[0]);
			
			return lambda;
		}

		private void AddError(OMUnit currentUnit, long groupId, string message)
		{
			lock (_locker)
			{
				_errorsDuringCalculation.Add(new CalcErrorItem
				{
					CadastralNumber = currentUnit.CadastralNumber,
					Error = message,
					GroupId = groupId,
					PropertyType = currentUnit.PropertyType_Code.GetEnumDescription(),
					TaskId = currentUnit.TaskId
				});
			}
		}

		private static string FormReport(List<CalcErrorItem> result)
		{
			using var reportService = new GbuReportService("Отчет по итогам расчета кадастровой стоимости");
			reportService.AddHeaders(new List<string> { "Оценочная группа", "Задание на оценку", "Тип объекта", "КН", "Ошибка"});
			reportService.SetIndividualWidth(GroupColumn, 3);
			reportService.SetIndividualWidth(TaskColumn, 4);
			reportService.SetIndividualWidth(PropertyTypeColumn, 5);
			reportService.SetIndividualWidth(KnColumn, 4);
			reportService.SetIndividualWidth(ErrorColumn, 5);

			var groupData = new Dictionary<long, string>();
			var taskData = new Dictionary<long, string>();
			var groupIds = result.Where(x => x.GroupId.HasValue).Select(x => x.GroupId.Value).ToList();
			if (!groupIds.IsEmpty())
			{
				groupData = OMGroup.Where(x => groupIds.Contains(x.Id)).Select(x => x.Number).Execute()
					.ToDictionary(x => x.Id, x => x.Number);
			}
			var taskIds = result.Where(x => x.TaskId.HasValue).Select(x => x.TaskId.Value).ToList();
			if (!taskIds.IsEmpty())
			{
				taskData = new TaskService().GetTemplatesForTaskName(taskIds);
			}
			
			foreach (var errorItem in result)
			{
				var row = reportService.GetCurrentRow();
				if (groupData.ContainsKey(errorItem.GroupId.GetValueOrDefault()))
					reportService.AddValue(groupData[errorItem.GroupId.GetValueOrDefault()], GroupColumn, row);

				if (taskData.ContainsKey(errorItem.TaskId.GetValueOrDefault()))
					reportService.AddValue(taskData[errorItem.TaskId.GetValueOrDefault()], TaskColumn, row);

				reportService.AddValue(errorItem.PropertyType, PropertyTypeColumn, row);
				reportService.AddValue(errorItem.CadastralNumber, KnColumn, row);
				reportService.AddValue(errorItem.Error, ErrorColumn, row);
			}

			var reportId = reportService.SaveReport();

			return reportService.GetUrlToDownloadFile(reportId);
		}

		#endregion
	}
}
