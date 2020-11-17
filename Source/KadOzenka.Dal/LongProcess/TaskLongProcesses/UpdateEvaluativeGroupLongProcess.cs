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
using KadOzenka.Dal.Tasks;
using ObjectModel.Core.LongProcess;
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
		private GbuReportService ReportService { get; }
		private GbuReportService.Column CadastralNumberColumn { get; set; }
		private GbuReportService.Column AttributeValueColumn { get; set; }
		private GbuReportService.Column GroupNameColumn { get; set; }
		private GbuReportService.Column ErrorColumn { get; set; }
		private object _locked;

		public UpdateEvaluativeGroupLongProcess()
		{
			SystemAttributeSettingsService = new SystemAttributeSettingsService();
			GbuObjectService = new GbuObjectService();
			TaskService = new TaskService();
			ReportService = new GbuReportService();
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

			var taskName = string.Empty;
			try
			{
				_log.Debug("Начат перенос оценочной группы для задания на оценку {TaskId}", taskId);

				taskName = TaskService.GetTemplateForTaskName(taskId.Value);

				var evaluativeGroupAttribute = GetEvaluativeGroupAttribute();

				GenerateReportColumns(evaluativeGroupAttribute);

				UpdateUnitGroup(taskId.Value, evaluativeGroupAttribute);

				ReportService.SetStyle();
				ReportService.SaveReport($"Отчет по переносу оценочной группы для задания '{taskName}'", OMTask.GetRegisterId(), "KoTasks");

				var message = "Операция успешно завершена.<br>" + $@"<a href=""{ReportService.UrlToDownload}"">Скачать результат</a>";
				SendMessage(processQueue, message, GetMessageSubject(taskName));
				WorkerCommon.SetProgress(processQueue, 100);

				_log.Debug("Закончен перенос оценочной группы для задания на оценку {TaskId}", taskId);
			}
			catch(Exception ex)
			{
				var errorId = ErrorManager.LogError(ex);
				SendMessage(processQueue, $"Операция завершена с ошибкой: {ex.Message}.<br>(Подробнее в журнале: {errorId})", GetMessageSubject(taskName));
				_log.Error(ex, "Ошибка при переносе оценочной группы");
				
				throw;
			}
		}


		#region Support Methods

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

		private void UpdateUnitGroup(long taskId, RegisterAttribute evaluativeGroupAttribute)
		{
			var units = GetUnits(taskId);
			_log.Debug("Найдено {UnitsCount} единиц оценки с непроставленной группой", units.Count);

			var gbuEvaluativeGroupValues = GbuObjectService.GetAllAttributes(
				units.Select(x => x.ObjectId.GetValueOrDefault()).ToList(), null,
				new List<long> {evaluativeGroupAttribute.Id}, 
				DateTime.Now.GetEndOfTheDay(), isLight: true);
			_log.Debug("Найдено {GbuEvaluativeGroupValuesCount} значений ГБУ-атрибутов", gbuEvaluativeGroupValues.Count);

			var groups = GetGroups(gbuEvaluativeGroupValues);
			_log.Debug("Найдено {GroupsCount} групп по номерам из ГБУ", groups.Count);

			_locked = new object();
			var cancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 20
			};

			Parallel.ForEach(units, options, unit =>
			{
				OMGroup group = null;
				var groupNumberFromAttribute = string.Empty;
				try
				{
					groupNumberFromAttribute = gbuEvaluativeGroupValues.FirstOrDefault(x => x.ObjectId == unit.ObjectId)?.GetValueInString();
					if (!string.IsNullOrWhiteSpace(groupNumberFromAttribute))
					{
						group = groups.FirstOrDefault(x => x.Number == groupNumberFromAttribute);
						unit.GroupId = group?.Id;
						unit.Save();

						_log.ForContext("ObjectId", unit.ObjectId)
							.ForContext("UnitId", unit.Id)
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
			});
		}


		private List<OMUnit> GetUnits(long taskId)
		{
			return OMUnit.Where(x => x.TaskId == taskId && (x.GroupId == null || x.GroupId == -1) && x.ObjectId != null)
				.Select(x => new
				{
					x.CadastralNumber,
					x.ObjectId,
					x.GroupId
				})
				.Execute();
		}

		private List<OMGroup> GetGroups(List<GbuObjectAttribute> gbuEvaluativeGroupValues)
		{
			var allGroupNumbers = gbuEvaluativeGroupValues.Select(x => x.GetValueInString()).Distinct()
				.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

			_log.ForContext("GroupNumbers", allGroupNumbers, destructureObjects: true)
				.Debug("Найдено {AllGroupNumbersCount} уникальных номеров групп из ГБУ", allGroupNumbers.Count);

			if (allGroupNumbers.Count > 0)
			{
				return OMGroup.Where(x => allGroupNumbers.Contains(x.Number)).Select(x => new
				{
					x.GroupName,
					x.Number
				}).Execute();
			}

			return new List<OMGroup>();
		}

		private void AddRowToReport(string cadastralNumber, string groupNumberFromAttribute, OMGroup group, string errorMessage = null)
		{
			var row = ReportService.GetCurrentRow();

			var fullGroupName = group == null ? string.Empty : $"{group.Number}. {group.GroupName}";

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
	}
}
