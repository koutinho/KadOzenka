using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using KadOzenka.Dal.Tasks;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public class UpdateEvaluativeGroupLongProcess : LongProcess
	{
		private readonly ILogger _log = Log.ForContext<UpdateEvaluativeGroupLongProcess>();

		private SystemAttributeSettingsService SystemAttributeSettingsService { get; }
		private GbuObjectService GbuObjectService { get; }
		private RosreestrRegisterService RosreestrRegisterService { get; }
		private TaskService TaskService { get; }
		private GroupService GroupService { get; }
		private GbuReportService.Column CadastralNumberColumn { get; set; }
		private GbuReportService.Column AttributeValueColumn { get; set; }
		private GbuReportService.Column GroupNameColumn { get; set; }
		private GbuReportService.Column ErrorColumn { get; set; }
		private object _locked;
		public int MaxCount;
		public int CurrentCount;

		public UpdateEvaluativeGroupLongProcess()
		{
			SystemAttributeSettingsService = new SystemAttributeSettingsService();
			GbuObjectService = new GbuObjectService();
			RosreestrRegisterService = new RosreestrRegisterService();
			TaskService = new TaskService();
			GroupService = new GroupService();
		}



		public static long AddProcessToQueue(long taskId)
		{
			var longProcessName = "UpdateEvaluativeGroupLongProcess";

			return LongProcessManager.AddTaskToQueue(longProcessName, OMTask.GetRegisterId(), taskId);
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.Debug("Старт фонового процесса: {ProcessType}", processType.Description);
			WorkerCommon.SetProgress(processQueue, 0);

			var taskId = processQueue.ObjectId;
			if (!taskId.HasValue)
			{
				WorkerCommon.SetMessage(processQueue, Common.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Common.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				SendMessage(processQueue, "Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов", GetMessageSubject(null));
				return;
			}

			var task = new TaskPure();
			try
			{
				_log.Debug("Начат перенос оценочной группы для задания на оценку с ИД {TaskId}", taskId);

				task = GetTask(taskId.Value);

				var evaluativeGroupAttribute = GetEvaluativeGroupAttribute();
				using var reportService = new GbuReportService($"Отчет по переносу оценочной группы для задания '{task.Name}'");
				GenerateReportColumns(evaluativeGroupAttribute, reportService);

				Run(task, evaluativeGroupAttribute, reportService);

				reportService.SaveReport( OMTask.GetRegisterId(), "KoTasks");

				var message = "Операция успешно завершена.<br>" + $@"<a href=""{reportService.UrlToDownload}"">Скачать результат</a>";
				SendMessage(processQueue, message, GetMessageSubject(task.Name));
				WorkerCommon.SetProgress(processQueue, 100);

				_log.Debug("Закончен перенос оценочной группы для задания на оценку {TaskId}", taskId);
			}
			catch(Exception ex)
			{
				var errorId = ErrorManager.LogError(ex);
				SendMessage(processQueue, $"Операция завершена с ошибкой: {ex.Message}.<br>(Подробнее в журнале: {errorId})", GetMessageSubject(task.Name));
				_log.Error(ex, "Ошибка при переносе оценочной группы");
				
				throw;
			}
		}


		#region Support Methods

		private TaskPure GetTask(long taskId)
		{
			var task = OMTask.Where(x => x.Id == taskId).Select(x => new
			{
				x.TourId,
				x.DocumentId,
				x.NoteType,
				x.EstimationDate
			}).ExecuteFirstOrDefault();
			if (task == null)
				throw new Exception($"Не найдено Задание на оценку с ИД '{taskId}'");
			if (task.TourId == null)
				throw new Exception($"У Задания на оценку с ИД '{taskId}' не проставлен Тур");

			var document = OMInstance.Where(x => x.Id == task.DocumentId).Select(x => new
			{
				x.CreateDate,
				x.RegNumber
			}).ExecuteFirstOrDefault();
			
			var taskName = TaskService.GetTemplateForTaskName(task.EstimationDate, document?.CreateDate,
				document?.RegNumber, task.NoteType);

			return new TaskPure
			{
				Id = task.Id,
				Name = taskName, 
				TourId = task.TourId.Value
			};
		}

		private RegisterAttribute GetEvaluativeGroupAttribute()
		{
			var evaluativeGroupAttributeId = SystemAttributeSettingsService.GetEvaluativeGroupAttributeId();
			if (evaluativeGroupAttributeId == null)
				throw new Exception("Не установлен атрибут для оценочной группы");

			var evaluativeGroupAttribute = RegisterCache.GetAttributeData((int)evaluativeGroupAttributeId);
			var evaluativeGroupRegister = RegisterCache.GetRegisterData(evaluativeGroupAttribute.RegisterId);
			evaluativeGroupAttribute.RegisterName = evaluativeGroupRegister.Description;

			_log.ForContext("EvaluativeGroupAttributeId", evaluativeGroupAttribute.Id)
				.ForContext("EvaluativeGroupAttributeName", evaluativeGroupAttribute.Name)
				.ForContext("EvaluativeGroupRegisterId", evaluativeGroupAttribute.RegisterId)
				.ForContext("EvaluativeGroupRegisterName", evaluativeGroupAttribute.RegisterName)
				.Debug("Найден ГБУ-атрибут с оценочной группой");

			return evaluativeGroupAttribute;
		}

		private void GenerateReportColumns(RegisterAttribute evaluativeGroupAttribute, GbuReportService reportService)
		{
			CadastralNumberColumn = new GbuReportService.Column
			{
				Index = 0,
				Header = "Кадастровый номер",
				Width = 4
			};
			AttributeValueColumn = new GbuReportService.Column
			{
				Index = 1,
				Header = $"Значение атрибута '{evaluativeGroupAttribute.Name} ({evaluativeGroupAttribute.RegisterName})'",
				Width = 4
			};
			GroupNameColumn = new GbuReportService.Column
			{
				Index = 2,
				Header = "Проставленная группа",
				Width = 8
			};
			ErrorColumn = new GbuReportService.Column
			{
				Index = 3,
				Header = "Ошибка",
				Width = 8
			};

			var columns = new List<GbuReportService.Column> { CadastralNumberColumn, AttributeValueColumn, GroupNameColumn, ErrorColumn };

			reportService.AddHeaders(columns);
			reportService.SetIndividualWidth(columns);
		}

		//private void PerformOperation(OMQueue processQueue, CancellationToken cancellationToken, TaskPure task,
		//	RegisterAttribute evaluativeGroupAttribute)
		//{
		//	var cancelProgressCounterSource = new CancellationTokenSource();
		//	var cancelProgressCounterToken = cancelProgressCounterSource.Token;
		//	var progressCounterTask = Task.Run(() =>
		//	{
		//		while (true)
		//		{
		//			if (cancelProgressCounterToken.IsCancellationRequested)
		//			{
		//				break;
		//			}

		//			LogProgress(MaxCount, CurrentCount, processQueue);
		//		}
		//	}, cancelProgressCounterToken);

		//	Run(task, evaluativeGroupAttribute);

		//	cancelProgressCounterSource.Cancel();
		//	progressCounterTask.Wait(cancellationToken);
		//	cancelProgressCounterSource.Dispose();
		//}

		private void Run(TaskPure task, RegisterAttribute evaluativeGroupAttribute, GbuReportService reportService)
		{
			var units = GetUnits(task.Id);
			MaxCount = units.Count;

			var gbuEvaluativeGroupValues = GbuObjectService.GetAllAttributes(
				units.Select(x => x.ObjectId.GetValueOrDefault()).ToList(), 
				new List<long> {evaluativeGroupAttribute.RegisterId},
				new List<long> {evaluativeGroupAttribute.Id}, 
				DateTime.Now.GetEndOfTheDay(), isLight: true);
			_log.Debug("Найдено {GbuEvaluativeGroupValuesCount} значений ГБУ-атрибутов", gbuEvaluativeGroupValues.Count);

			var tourGroupsInfo = GroupService.GetTourGroupsInfo(task.TourId, ObjectTypeExtended.Both);
			_log.Debug("Найдены группы для Тура");

			_locked = new object();
			var cancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 20
			};

			Parallel.ForEach(units, options, unit =>
			{
				UpdateUnitGroup(gbuEvaluativeGroupValues, unit, tourGroupsInfo, reportService);
			});
		}

		private List<OMUnit> GetUnits(long taskId)
		{
			_log.Debug("Начата загрузка ЕО для Задания на оценку с ИД {TaskId}", taskId);

			var units = OMUnit.Where(x => x.TaskId == taskId && x.ObjectId != null)
				.Select(x => new
				{
					x.CadastralNumber,
					x.ObjectId,
					x.GroupId,
					x.PropertyType_Code
				})
				.Execute();

			_log.Debug($"Загружено {units.Count} ЕО");

			return FilterUnits(units);
		}

		private List<OMUnit> FilterUnits(List<OMUnit> units)
		{
			_log.Debug("Начата фильтрация ЕО по признакам для ЦОД из Росреестра (Изменение группы)");

			var group = RosreestrRegisterService.GetPGroupAttribute();

			var groupAttributes = GbuObjectService.GetAllAttributes(
				units.Select(x => x.ObjectId.GetValueOrDefault()).ToList(),
				new List<long> { RosreestrRegisterService.RegisterId },
				new List<long> { group.Id },
				DateTime.Now.GetEndOfTheDay(),
				isLight: true);
			_log.Debug($"Найдено {groupAttributes.Count} ОН со значениями из Росреестра");

			var resultObjectIds = new List<long>();
			foreach (var groupAttribute in groupAttributes)
			{
				if (groupAttribute.GetValueInString() == "1")
				{
					resultObjectIds.Add(groupAttribute.ObjectId);
				}
			}
			_log.Debug($"После фильтрации ЕО по признакам для ЦОД из Росреестра осталось {resultObjectIds.Count} объектов");

			return units.Where(x => resultObjectIds.Contains(x.ObjectId.GetValueOrDefault())).ToList();
		}

		private void UpdateUnitGroup(List<GbuObjectAttribute> gbuEvaluativeGroupValues, OMUnit unit, TourGroupsInfo allGroupsInTour, GbuReportService reportService)
		{
			lock (_locked)
			{
				CurrentCount++;
			}

			GroupTreeDto group = null;
			var groupFromGbu = string.Empty;
			try
			{
				groupFromGbu = gbuEvaluativeGroupValues.FirstOrDefault(x => x.ObjectId == unit.ObjectId)?.GetValueInString();
				
				if (!string.IsNullOrWhiteSpace(groupFromGbu))
				{
					group = unit.PropertyType_Code == PropertyTypes.Stead
						? GetGroup(groupFromGbu, allGroupsInTour.ZuGroups, allGroupsInTour.ZuSubGroups)
						: GetGroup(groupFromGbu, allGroupsInTour.OksGroups, allGroupsInTour.OksSubGroups);

					if (group != null && unit.GroupId != group.Id)
					{
						unit.GroupId = group.Id;
						unit.Save();
					}
					
					_log.ForContext("ObjectId", unit.ObjectId)
						.ForContext("UnitId", unit.Id)
						.ForContext("NewGroupId", unit.GroupId)
						.ForContext("NewGroupName", group?.GroupName)
						.ForContext("GroupFromGbu", groupFromGbu)
						.Debug("Обновление группы у ЕО '{UnitCadastralNumber}'", unit.CadastralNumber);
				}

				lock (_locked)
				{
					AddRowToReport(reportService, unit.CadastralNumber, groupFromGbu, group);
				}
			}
			catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				lock (_locked)
				{
					AddRowToReport(reportService, unit.CadastralNumber, groupFromGbu, group, $"Ошибка, подробнее в журнале {errorId}");
				}

				_log.Error(exception, "Ошибка при обработке юнита во время переноса оценочной группы");
			}
		}

		private GroupTreeDto GetGroup(string groupFromGbu, List<GroupTreeDto> groups, List<GroupTreeDto> subgroups)
		{
			//в большинстве случаев в groupFromGbu будет только номер,
			//но решили обработать кейс, если там будет номер + имя

			var groupNumberPattern = new Regex(@"^\d+\.\s*\d*");
			var match = groupNumberPattern.Match(groupFromGbu);
			if (!match.Success)
				return null;

			var number = match.Value.TrimEnd(' ', '.');

			var resultGroup = groups.FirstOrDefault(x => x.CombinedNumber == number);
			if (resultGroup != null) 
				return resultGroup;

			var resultSubgroup = subgroups.FirstOrDefault(x => x.CombinedNumber == number);
			return resultSubgroup;
		}

		private void AddRowToReport(GbuReportService reportService, string cadastralNumber, string groupNumberFromAttribute, GroupTreeDto group, string errorMessage = null)
		{
			var row = reportService.GetCurrentRow();

			var fullGroupName = group == null ? string.Empty : group.GroupName;

			reportService.AddValue(cadastralNumber, CadastralNumberColumn.Index, row);
			reportService.AddValue(groupNumberFromAttribute, AttributeValueColumn.Index, row);
			reportService.AddValue(fullGroupName, GroupNameColumn.Index, row);

			if (!string.IsNullOrWhiteSpace(errorMessage))
			{
				reportService.AddValue(errorMessage, ErrorColumn.Index, row, reportService.ErrorCellStyle);
			}
		}

		private string GetMessageSubject(string taskName)
		{
			var baseMessage = "Результат переноса оценочной группы";

			return string.IsNullOrWhiteSpace(taskName) ? baseMessage : $"{baseMessage} для задания на оценку '{taskName}'";
		}

		#endregion


		#region Entities

		private class TaskPure
		{
			public long Id { get; set; }
			public string Name { get; set; }
			public long TourId { get; set; }
		}

		#endregion
	}
}
