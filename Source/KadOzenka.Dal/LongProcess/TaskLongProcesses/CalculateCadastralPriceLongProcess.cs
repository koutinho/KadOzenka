using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Exceptions;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Units;
using KadOzenka.Dal.Units.Repositories;
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
		public const string LongProcessName = "CalculateCadastralPrice";
		private static readonly int GroupColumn = 0;
		private static readonly int TaskColumn = 1;
		private static readonly int PropertyTypeColumn = 2;
		private static readonly int KnColumn = 3;
		private static readonly int ErrorColumn = 4;
		private readonly ILogger _log = Log.ForContext<CalculateCadastralPriceLongProcess>();
		private IRegisterCacheWrapper RegisterCacheWrapper { get; }
		private IUnitService UnitService { get; }
		private IModelingService ModelingService { get; }
		private IModelFactorsService ModelFactorsService { get; }
		private IGroupService GroupService { get; }
		private IUnitRepository UnitRepository { get; }
		public static string AttributePrefixInFormula = "factor_";

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

				var settings = processQueue.Parameters.DeserializeFromXml<CadastralPriceCalculationSettions>();
				var urlToDownload = PerformProc(settings);

				WorkerCommon.SetProgress(processQueue, 100);
				string message = "Операция успешно завершена." +
				                 $@"<a href=""{urlToDownload}"">Скачать результат</a>";
				NotificationSender.SendNotification(processQueue, "Результат Операции Расчета кадастровой стоимости", message);
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

		public string PerformProc(CadastralPriceCalculationSettions settings)
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

					var activeGroupModel = ModelingService.GetActiveModelEntityByGroupId(group.Id);
					_log.Debug("Активная модель - '{ModelName}' (с ИД - {ModelId})", activeGroupModel?.Name, activeGroupModel?.Id);
					if (activeGroupModel == null)
						throw new Exception($"Не найдена активная модель для группы '{group.GroupName}' (с ИД - {group.Id})");

					if (activeGroupModel.Type_Code == KoModelType.Manual && activeGroupModel.AlgoritmType_Code == KoAlgoritmType.Multi)
					{
						//todo try/catch and report
						CalculateByNewRealization(settings, activeGroupModel, group);
					}
					else
					{
						settings.SelectedGroupIds = new List<long> {group.Id};
						errorsDuringCalculation = CalculateByOldRealization(settings);
					}
				});
			}

			_log.Debug("Начато формирование отчета");
			var urlToDownloadReport = FormReport(errorsDuringCalculation);
			_log.Debug("Закончено формирование отчета");

			CompareData(settings.TaskIds);

			return urlToDownloadReport;
		}

		private void CalculateByNewRealization(CadastralPriceCalculationSettions settings, OMModel activeGroupModel, OMGroup group)
		{
			_log.Debug("Начат расчет через новую реализацию");
			var modelInfo = PrepareModelingInfo(activeGroupModel);

			var factorsWithDefaultMark = modelInfo.Factors.Where(x => x.MarkType_Code == MarkType.Default).ToList();
			//todo to dictionary
			var marks = ModelFactorsService.GetMarks(group.Id, factorsWithDefaultMark.Select(x => x.FactorId).ToList());

			var units = GetUnits(settings, group.Id);

			//TODO parallel and packages
			units.ForEach(unit =>
			{
				if (unit.Square == null)
					throw new NoInfoForCalculationException("У ЕО не заполнена площадь");

				var allUnitFactors = UnitService.GetUnitModelFactors(unit);
				var notEmptyUnitFactors = allUnitFactors.Where(x => x.Value != null).ToList();
				
				if (notEmptyUnitFactors.Count != modelInfo.Factors.Count)
				{
					var emptyFactors = allUnitFactors.Where(x => x.Value == null).ToList();
					throw new NoInfoForCalculationException($"У ЕО не заполнены данные по атрибутам: {string.Join(',', emptyFactors.Select(x => x.AttributeData.Name))}");
				}


				var price = Calculate(modelInfo, notEmptyUnitFactors, marks);
				//todo обработать конвертацию
				unit.CadastralCost = (decimal?) price;
				unit.Upks = unit.CadastralCost / unit.Square.Value;
				UnitRepository.Save(unit);
			});
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

		public ModelingInfo PrepareModelingInfo(OMModel activeGroupModel)
		{
			var modelFactors = ModelFactorsService.GetFactors(activeGroupModel.Id, activeGroupModel.AlgoritmType_Code);
			if (modelFactors.Count == 0)
				throw new Exception($"У модели '{activeGroupModel.Name}' (С ИД - {activeGroupModel.Id}) нет факторов");
			_log.Debug("Загружено {FactorsCount} факторов модели", modelFactors.Count);

			var formula = ModelingService.GetFormula(activeGroupModel, activeGroupModel.AlgoritmType_Code);
			_log.Debug("Начальная формула: {Formula}", formula);

			//имена факторов в формуле записываются через кавычки
			formula = formula.Replace("\"", "");
			modelFactors.ForEach(factor =>
			{
				var attributeName = RegisterCacheWrapper.GetAttributeData(factor.FactorId.GetValueOrDefault()).Name;
				var factorNameInFormula = attributeName;
				if (factor.MarkType_Code == MarkType.Default)
				{
					//todo в паттерн
					factorNameInFormula = $"метка({factorNameInFormula})";
				}
				formula = formula.Replace(factorNameInFormula, $"{AttributePrefixInFormula}{factor.FactorId}");
			});
			_log.Debug("Обработанная формула: {Formula}", formula);

			return new ModelingInfo
			{
				Formula = formula,
				Factors = modelFactors
			};
		}

		public double Calculate(ModelingInfo modelInfo, List<UnitFactor> unitsFactors, List<OMMarkCatalog> marks)
		{
			var arguments = new PrimitiveElement[unitsFactors.Count];
			for (var i = 0; i < unitsFactors.Count; i++)
			{
				var currentUnitFactor = unitsFactors[i];
				var factorId = currentUnitFactor.AttributeId;
				var modelFactor = modelInfo.Factors.First(x => x.FactorId == factorId);
				//TODO вынести кеш и имя в формуле в ModelingInfo
				Argument argument;
				var attribute = RegisterCacheWrapper.GetAttributeData(factorId);
				var factorNameInFormula = $"{AttributePrefixInFormula}{factorId}";
				if (modelFactor.MarkType_Code == MarkType.Default)
				{
					var metka = GetMetkaFromMarkCatalog(marks, currentUnitFactor, attribute);
					argument = new Argument(factorNameInFormula, (double)metka);
				}
				else
				{
					switch (attribute.Type)
					{
						case RegisterAttributeType.INTEGER:
							argument = new Argument(factorNameInFormula, currentUnitFactor.LongValue.GetValueOrDefault());
							break;
						case RegisterAttributeType.DECIMAL:
							argument = new Argument(factorNameInFormula, (double)currentUnitFactor.DecimalValue.GetValueOrDefault());
							break;
						default:
							var metka = GetMetkaFromMarkCatalog(marks, currentUnitFactor, attribute);
							argument = new Argument(factorNameInFormula, (double)metka);
							break;
					}
				}
				
				arguments[i] = argument;
			}

			var expression = new Expression(modelInfo.Formula, arguments);
			var str = expression.getExpressionString();
			
			return expression.calculate();
		}

		private decimal GetMetkaFromMarkCatalog(List<OMMarkCatalog> marks, UnitFactor currentUnitFactor, RegisterAttribute attribute)
		{
			var value = currentUnitFactor.GetValueInString();
			
			var mark = marks.FirstOrDefault(x => x.FactorId == attribute.Id && x.ValueFactor == value);
			if (mark == null)
				throw new NoInfoForCalculationException($"Не найдена метка для фактора '{attribute.Name}' со значением '{value}'");
			
			return mark.MetkaFactor.GetValueOrDefault();
		}
		
		private List<OMUnit> GetUnits(CadastralPriceCalculationSettions settings, long groupId)
		{
			//TODO one condition

			List<OMUnit> units;
			if (settings.IsParcel)
			{
				units = UnitRepository.GetEntitiesByCondition(
					x => settings.TaskIds.Contains((long) x.TaskId) && x.GroupId == groupId &&
					     x.PropertyType_Code == PropertyTypes.Stead, null);
			}
			else
			{
				units = UnitRepository.GetEntitiesByCondition(
					x => settings.TaskIds.Contains((long) x.TaskId) && x.GroupId == groupId &&
					     x.PropertyType_Code != PropertyTypes.Stead, null);
			}

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

		
		#region Entities

		public class ModelingInfo
		{
			public string Formula { get; set; }
			public List<OMModelFactor> Factors { get; set; }
		}


		#endregion
	}
}
