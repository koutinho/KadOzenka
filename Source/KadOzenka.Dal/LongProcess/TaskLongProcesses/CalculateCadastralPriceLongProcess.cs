using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
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
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Entities;
using KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Exceptions;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Units;
using KadOzenka.Dal.Units.Repositories;
using Microsoft.Extensions.Configuration;
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
		
		private IRegisterCacheWrapper RegisterCacheWrapper { get; }
		private IUnitService UnitService { get; }
		private IModelingService ModelingService { get; }
		private IModelFactorsService ModelFactorsService { get; }
		private IGroupService GroupService { get; }
		private IUnitRepository UnitRepository { get; }


		public CalculateCadastralPriceLongProcess(IUnitRepository unitRepository = null,
			IUnitService unitService = null, IModelingService modelingService = null,
			IModelFactorsService modelFactorsService = null,
			IGroupService groupService = null,
			IRegisterCacheWrapper registerCacheWrapper = null)
		{
			UnitRepository = unitRepository ?? new UnitRepository();
			UnitService = unitService ?? new UnitService();
			RegisterCacheWrapper = registerCacheWrapper ?? new RegisterCacheWrapper();
			ModelingService = modelingService ?? new ModelingService();
			ModelFactorsService = modelFactorsService ?? new ModelFactorsService();
			GroupService = groupService ?? new GroupService();

			_locker = new object();
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

			var errorsDuringCalculation = new List<CalcErrorItem>();
			if (settings.IsAllGroups)
			{
				errorsDuringCalculation = CalculateByOldRealization(settings);
			}
			else
			{
				var groups = GroupService.GetGroupsByIds(settings.SelectedGroupIds);
				groups.ForEach(group =>
				{
					_log.Debug("Начата обработка группы '{GroupName}' (с ИД - {GroupId})", group.GroupName, group.Id);

					//todo отчет
					var activeGroupModel = ModelingService.GetActiveModelEntityByGroupId(group.Id);
					if (activeGroupModel == null)
						throw new Exception($"Не найдена активная модель для группы '{group.GroupName}' (с ИД - {group.Id})");

					_log.Debug("Активная модель - '{ModelName}' (с ИД - {ModelId})", activeGroupModel.Name, activeGroupModel.Id);
					if (activeGroupModel.Type_Code == KoModelType.Manual && activeGroupModel.AlgoritmType_Code == KoAlgoritmType.Multi)
					{
						errorsDuringCalculation = CalculateByNewRealization(settings, activeGroupModel, group.Id, cancellationToken);
					}
					else
					{
						settings.SelectedGroupIds = new List<long> {group.Id};
						errorsDuringCalculation = CalculateByOldRealization(settings);
					}
				});
			}

			return errorsDuringCalculation;
		}

		private List<CalcErrorItem> CalculateByNewRealization(CadastralPriceCalculationSettions settings, OMModel activeGroupModel, 
			long groupId, CancellationToken cancellationToken)
		{
			_log.Debug("Начат расчет через новую реализацию");

			var modelFactors = PrepareModelFactors(activeGroupModel);
			var formula = PrepareFormula(activeGroupModel, modelFactors);

			var marks = GetMarks(groupId, modelFactors);
			//если будут проблемы с производительностью вынески выгрузку ЕО в потоки
			var units = GetUnits(settings, groupId);

			var processedUnitsCount = 0;
			var processedPackageCount = 0;
			var errorsDuringCalculation = new List<CalcErrorItem>();
			var processConfiguration = GetProcessConfiguration(units.Count);
			Parallel.For(0, processConfiguration.NumberOfPackages, processConfiguration.ParallelOptions, (packageIndex, s) =>
			{
				CheckCancellationToken(cancellationToken, processConfiguration.CancellationTokenSource, processConfiguration.ParallelOptions);

				var unitsPackage = units.Skip(packageIndex * processConfiguration.PackageSize).Take(processConfiguration.PackageSize).ToList();
				_log.Debug("Начата работа с пакетом №{i} из {NumberOfPackages}. В нем {UnitsInPackageCount} ЕО", packageIndex, processConfiguration.NumberOfPackages, unitsPackage.Count);

				var unitsGroupedByTour = unitsPackage.GroupBy(x => x.TourId.GetValueOrDefault()).ToList();
				unitsGroupedByTour.ForEach(unitGroup =>
				{
					var unitsWithTheSameTour = unitGroup.ToList();

					var unitIds = unitsWithTheSameTour.Select(x => x.Id).ToList();
					var tourId = unitGroup.Key;
					var allUnitsFactors = UnitService.GetUnitsFactors(unitIds, tourId, settings.IsParcel,
						modelFactors.Select(x => x.FactorId).ToList());

					unitsWithTheSameTour.ForEach(currentUnit =>
					{
						try
						{
							CheckCancellationToken(cancellationToken, processConfiguration.CancellationTokenSource, processConfiguration.ParallelOptions);

							if (currentUnit.Square.GetValueOrDefault() == 0)
								throw new NoInfoForCalculationException("У ЕО не заполнена площадь");

							if (!allUnitsFactors.TryGetValue(currentUnit.Id, out var currentUnitFactors))
								throw new NoInfoForCalculationException("У ЕО нет факторов");

							var notEmptyUnitFactors = currentUnitFactors.Where(x => x.Value != null).ToList();
							if (notEmptyUnitFactors.Count != modelFactors.Count)
							{
								var emptyFactors = currentUnitFactors.Where(x => x.Value == null).ToList();
								throw new NoInfoForCalculationException(
									$"У ЕО не заполнены данные по атрибутам: {string.Join(',', emptyFactors.Select(x => x.AttributeData.Name))}");
							}

							var cost = CalculateCadastralCost(formula, modelFactors, notEmptyUnitFactors, marks);
							currentUnit.CadastralCost = (decimal?) cost;
							currentUnit.Upks = currentUnit.CadastralCost / currentUnit.Square.Value;
							UnitRepository.Save(currentUnit);

							Interlocked.Increment(ref processedUnitsCount);
						}
						catch (Exception e)
						{
							_log.Error(e, "Ошибка по время обработки ЕО с КН '{CadastralNumber}'", currentUnit.CadastralNumber);

							lock (_locker)
							{
								errorsDuringCalculation.Add(new CalcErrorItem
								{
									CadastralNumber = currentUnit.CadastralNumber,
									Error = e.Message,
									GroupId = groupId,
									PropertyType = currentUnit.PropertyType_Code.GetEnumDescription(),
									TaskId = currentUnit.TaskId
								});

								LongProcessProgressLogger.LogProgress(processConfiguration.MaxUnitsCount, processedUnitsCount, _queue);
							}
						}
					});
				});

				Interlocked.Increment(ref processedPackageCount);
				_log.Debug("Всего обработано пакетов {ProcessedPackageCount} из {NumberOfPackages}. И {ProcessedUnitsCount} ЕО из {UnitsCount}", processedPackageCount, processConfiguration.NumberOfPackages, processedUnitsCount, processConfiguration.MaxUnitsCount);
			});

			_log.Debug("Закончен расчет через новую реализацию");

			return errorsDuringCalculation;
		}

		private List<CalcErrorItem> CalculateByOldRealization(CadastralPriceCalculationSettions settings)
		{
			_log.Debug("Начат расчет по старой реализации");
			var errorsDuringCalculation = OMGroup.CalculateSelectGroup(settings);
			_log.ForContext("Result", errorsDuringCalculation, true).Debug("Закончен расчет. Возвращенное значение.");
			
			return errorsDuringCalculation;
		}

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

		private List<FactorInfo> PrepareModelFactors(OMModel activeGroupModel)
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
					MarkType = factor.MarkType_Code,
					AttributeName = attribute.Name,
					AttributeType = attribute.Type
				};
			}).ToList();

			return factors;
		}

		public string PrepareFormula(OMModel activeGroupModel, List<FactorInfo> factors)
		{
			var formula = ModelingService.GetFormula(activeGroupModel, activeGroupModel.AlgoritmType_Code);
			_log.Debug("Начальная формула: {Formula}", formula);

			//имена факторов в формуле записываются через кавычки
			formula = formula.Replace("\"", "");
			factors.ForEach(factor =>
			{
				var factorNameInFormula = factor.AttributeName;
				if (factor.MarkType == MarkType.Default)
				{
					factorNameInFormula = $"{Dal.Modeling.ModelingService.MarkTagInFormula}({factorNameInFormula})";
				}

				formula = formula.Replace(factorNameInFormula, $"{AttributePrefixInFormula}{factor.FactorId}");
			});

			_log.Debug("Обработанная формула (для постановки чисел): {Formula}", formula);

			return formula;
		}

		private Dictionary<Tuple<long, string>, decimal?> GetMarks(long groupId, List<FactorInfo> factors)
		{
			var factorsWithDefaultMarkIds = factors.Where(x => x.MarkType == MarkType.Default)
				.Select(x => (long?)x.FactorId).ToList();

			_log.ForContext("FactorsWithDefaultMarkIds", factorsWithDefaultMarkIds, true)
				.Debug("Начата выгрузка меток для {FactorsCount} факторов с меткой по умолчанию", factorsWithDefaultMarkIds.Count);

			var marks = ModelFactorsService.GetMarks(groupId, factorsWithDefaultMarkIds);
			_log.Debug("Выгружено {MarksCount} меток", marks.Count);

			var groupedMarks = marks
				.GroupBy(x => Tuple.Create(x.FactorId.GetValueOrDefault(), x.ValueFactor))
				.ToDictionary(x => x.Key, x =>
				{
					var marksInGroup = x.ToList();
					if (marksInGroup.Count > 1)
						_log.Warning("Найдено более одной метки для группы {GroupId}, фактора {FactorId}, значения {Value}", groupId, x.Key.Item1, x.Key.Item2);
					
					return x.FirstOrDefault()?.MetkaFactor;
				});
			_log.Debug("Сформированы словари меток с {KeysCount} ключами по фактору и значению", groupedMarks.Count);

			return groupedMarks;
		}

		public double CalculateCadastralCost(string formula, List<FactorInfo> factors, List<UnitFactor> unitsFactors, Dictionary<Tuple<long, string>, decimal?> marks)
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

		private decimal GetMetkaFromMarkCatalog(Dictionary<Tuple<long, string>, decimal?> marks, FactorInfo unitFactor, string value)
		{
			if (!marks.TryGetValue(Tuple.Create(unitFactor.FactorId, value), out var metka))
				throw new NoInfoForCalculationException($"Не найдена метка для фактора '{unitFactor.AttributeName}' (ИД {unitFactor.FactorId}) со значением '{value}'");

			if (metka == null)
				throw new NoInfoForCalculationException($"Метка для фактора '{unitFactor.AttributeName}' (ИД {unitFactor.FactorId}) пустая");

			return metka.Value;
		}

		private List<OMUnit> GetUnits(CadastralPriceCalculationSettions settings, long groupId)
		{
			_log.Debug("Начато скачивание ЕО");

			Expression<Func<OMUnit, bool>> baseExpression = x => settings.TaskIds.Contains((long)x.TaskId) && x.GroupId == groupId;
			
			Expression<Func<OMUnit, bool>> typeCondition = settings.IsParcel
				? x => x.PropertyType_Code == PropertyTypes.Stead
				: x => x.PropertyType_Code != PropertyTypes.Stead;

			var body = System.Linq.Expressions.Expression.AndAlso(baseExpression.Body, typeCondition.Body);
			var lambda = System.Linq.Expressions.Expression.Lambda<Func<OMUnit, bool>>(body, baseExpression.Parameters[0]);

			var units = UnitRepository.GetEntitiesByCondition(lambda, x => new
			{
				x.CadastralNumber,
				x.TourId,
				x.TaskId,
				x.PropertyType_Code,
				x.Square,
				x.CadastralCost,
				x.Upks
			});

			_log.Debug("Выгружено {UnitsCount} ЕО", units.Count);

			return units;
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
