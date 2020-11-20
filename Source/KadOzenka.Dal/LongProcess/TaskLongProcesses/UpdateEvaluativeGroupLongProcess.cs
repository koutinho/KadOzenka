using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Groups.Dto;
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
		private TaskService TaskService { get; }
		private GroupService GroupService { get; }
		private GbuReportService ReportService { get; }
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
			TaskService = new TaskService();
			ReportService = new GbuReportService();
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
				WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				SendMessage(processQueue, "Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов", GetMessageSubject(null));
				return;
			}

			var task = new TaskPure();
			try
			{
				_log.Debug("Начат перенос оценочной группы для задания на оценку с ИД {TaskId}", taskId);

				task = GetTask(taskId.Value);

				var evaluativeGroupAttribute = GetEvaluativeGroupAttribute();

				GenerateReportColumns(evaluativeGroupAttribute);

				PerformOperation(processQueue, cancellationToken, task, evaluativeGroupAttribute);

				ReportService.SetStyle();
				ReportService.SaveReport($"Отчет по переносу оценочной группы для задания '{task.Name}'", OMTask.GetRegisterId(), "KoTasks");

				var message = "Операция успешно завершена.<br>" + $@"<a href=""{ReportService.UrlToDownload}"">Скачать результат</a>";
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

		private void GenerateReportColumns(RegisterAttribute evaluativeGroupAttribute)
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

			ReportService.AddHeaders(columns);
			ReportService.SetIndividualWidth(columns);
		}

		private void PerformOperation(OMQueue processQueue, CancellationToken cancellationToken, TaskPure task,
			RegisterAttribute evaluativeGroupAttribute)
		{
			var cancelProgressCounterSource = new CancellationTokenSource();
			var cancelProgressCounterToken = cancelProgressCounterSource.Token;
			var progressCounterTask = Task.Run(() =>
			{
				while (true)
				{
					if (cancelProgressCounterToken.IsCancellationRequested)
					{
						break;
					}

					LogProgress(MaxCount, CurrentCount, processQueue);
				}
			}, cancelProgressCounterToken);

			Run(task, evaluativeGroupAttribute);

			cancelProgressCounterSource.Cancel();
			progressCounterTask.Wait(cancellationToken);
			cancelProgressCounterSource.Dispose();
		}

		private void Run(TaskPure task, RegisterAttribute evaluativeGroupAttribute)
		{
			var units = GetUnits(task.Id);
			MaxCount = units.Count;

			var gbuEvaluativeGroupValues = GbuObjectService.GetAllAttributes(
				units.Select(x => x.ObjectId.GetValueOrDefault()).ToList(), 
				new List<long> {evaluativeGroupAttribute.RegisterId},
				new List<long> {evaluativeGroupAttribute.Id}, 
				DateTime.Now.GetEndOfTheDay(), isLight: true);
			_log.Debug("Найдено {GbuEvaluativeGroupValuesCount} значений ГБУ-атрибутов", gbuEvaluativeGroupValues.Count);

			var tourGroupsInfo = GetGroups(task.TourId);

			_locked = new object();
			var cancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 20
			};

			Parallel.ForEach(units, options, unit =>
			{
				UpdateUnitGroup(gbuEvaluativeGroupValues, unit, tourGroupsInfo);
			});
		}

		private List<OMUnit> GetUnits(long taskId)
		{
			var possibleStatuses = new List<UnitUpdateStatus>
			{
				UnitUpdateStatus.GroupChange, UnitUpdateStatus.GroupAndFsChange, UnitUpdateStatus.GroupAndEgrnChange,
				UnitUpdateStatus.GroupAndFsAndEgrnChanges
			};

			var units = OMUnit.Where(x => x.TaskId == taskId && x.ObjectId != null && possibleStatuses.Contains(x.UpdateStatus_Code))
				.Select(x => new
				{
					x.CadastralNumber,
					x.ObjectId,
					x.GroupId,
					x.PropertyType_Code
				})
				.Execute();

			_log.Debug("Найдено {UnitsCount} единиц оценки со статусом 'Изменение группы'", units.Count);

			return units;
		}

		private TourGroupsInfo GetGroups(long tourId)
		{
			var allGroupsInTour = GroupService.GetGroupsTreeForTour(tourId);
			_log.Debug("Найдено {GroupsCount} групп для Тура {TourId}", allGroupsInTour.Count, tourId);

			var oksGroups = GetGroups(allGroupsInTour, KoGroupAlgoritm.MainOKS);
			var zuGroups = GetGroups(allGroupsInTour, KoGroupAlgoritm.MainParcel);

			return new TourGroupsInfo
			{
				OksGroups = oksGroups,
				OksSubGroups = oksGroups.SelectMany(x => x.Items).ToList(),
				ZuGroups = zuGroups,
				ZuSubGroups = zuGroups.SelectMany(x => x.Items).ToList()
			};
		}

		private List<GroupTreeDto> GetGroups(List<GroupTreeDto> allGroupsInTour, KoGroupAlgoritm type)
		{
			return allGroupsInTour.Where(x => x.Id == (int)type).SelectMany(x => x.Items).ToList();
		}

		private void UpdateUnitGroup(List<GbuObjectAttribute> gbuEvaluativeGroupValues, OMUnit unit, TourGroupsInfo allGroupsInTour)
		{
			lock (_locked)
			{
				CurrentCount++;
			}

			GroupTreeDto group = null;
			var groupNumberFromAttribute = string.Empty;
			try
			{
				groupNumberFromAttribute = gbuEvaluativeGroupValues.FirstOrDefault(x => x.ObjectId == unit.ObjectId)?.GetValueInString();
				
				if (!string.IsNullOrWhiteSpace(groupNumberFromAttribute))
				{
					group = unit.PropertyType_Code == PropertyTypes.Stead
						? GetGroup(groupNumberFromAttribute, allGroupsInTour.ZuGroups, allGroupsInTour.ZuSubGroups)
						: GetGroup(groupNumberFromAttribute, allGroupsInTour.OksGroups, allGroupsInTour.OksSubGroups);

					unit.GroupId = group?.Id;
					unit.Save();

					_log.ForContext("ObjectId", unit.ObjectId)
						.ForContext("UnitId", unit.Id)
						.ForContext("UnitCadastralNumber", unit.CadastralNumber)
						.ForContext("NewGroupId", unit.GroupId)
						.Debug("Обновление единицы оценки {UnitCadastralNumber}", unit.CadastralNumber);
				}

				lock (_locked)
				{
					AddRowToReport(unit.CadastralNumber, groupNumberFromAttribute, group);
				}
			}
			catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				lock (_locked)
				{
					AddRowToReport(unit.CadastralNumber, groupNumberFromAttribute, group, $"Ошибка, подробнее в журнале {errorId}");
				}

				_log.Error(exception, "Ошибка при обработке юнита во время переноса оценочной группы");
			}
		}

		private GroupTreeDto GetGroup(string number, List<GroupTreeDto> groups, List<GroupTreeDto> subgroups)
		{
			var resultGroup = groups.FirstOrDefault(x => x.CombinedNumber == number);
			if (resultGroup != null) 
				return resultGroup;

			var resultSubgroup = subgroups.FirstOrDefault(x => x.CombinedNumber == number);
			return resultSubgroup;
		}

		private void AddRowToReport(string cadastralNumber, string groupNumberFromAttribute, GroupTreeDto group, string errorMessage = null)
		{
			var row = ReportService.GetCurrentRow();

			var fullGroupName = group == null ? string.Empty : group.GroupName;

			ReportService.AddValue(cadastralNumber, CadastralNumberColumn.Index, row);
			ReportService.AddValue(groupNumberFromAttribute, AttributeValueColumn.Index, row);
			ReportService.AddValue(fullGroupName, GroupNameColumn.Index, row);

			if (!string.IsNullOrWhiteSpace(errorMessage))
			{
				ReportService.AddValue(errorMessage, ErrorColumn.Index, row, ReportService.ErrorCellStyle);
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

		private class TourGroupsInfo
		{
			public List<GroupTreeDto> OksGroups { get; set; }
			public List<GroupTreeDto> OksSubGroups { get; set; }
			public List<GroupTreeDto> ZuGroups { get; set; }
			public List<GroupTreeDto> ZuSubGroups { get; set; }

		}

		#endregion
	}
}
