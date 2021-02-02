using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Units.Repositories;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
    public class TaskForCodLongProcess : LongProcess
    {
	    private static readonly ILogger _log = Log.ForContext<ExportAttributeToKoProcess>();
	    private ITaskService TaskService { get; }
	    private IRosreestrRegisterService RosreestrRegisterService { get; }
	    private IGbuObjectService GbuObjectService { get; }
		private IGbuReportService GbuReportService { get; }
		private IUnitRepository UnitRepository { get; }
		private IWorkerCommonWrapper Worker { get; }


		public TaskForCodLongProcess(ITaskService taskService = null,
			IRosreestrRegisterService rosreestrRegisterService = null, IGbuObjectService gbuObjectService = null,
			IGbuReportService gbuReportService = null, IUnitRepository unitRepository = null,
			IWorkerCommonWrapper worker = null, INotificationSender notificationSender = null,
			ILongProcessProgressLogger logger = null)
			: base(notificationSender, logger)
		{
			TaskService = taskService ?? new TaskService();
			RosreestrRegisterService = rosreestrRegisterService ?? new RosreestrRegisterService();
			GbuObjectService = gbuObjectService ?? new GbuObjectService();
			GbuReportService = gbuReportService ?? new GbuReportService("Задания для ЦОД");
			Worker = worker ?? new WorkerCommonWrapper();
			UnitRepository = unitRepository ?? new UnitRepository();
		}


		public static void AddProcessToQueue(long taskId)
        {
            LongProcessManager.AddTaskToQueue(nameof(TaskForCodLongProcess), objectId: taskId);
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
	        Worker.SetProgress(processQueue, 0);

			_log.ForContext("TaskId", processQueue.ObjectId).Debug("Запущен процесс для выгрузки заданий для ЦОД");

			var taskName = string.Empty;
            try
            {
	            var taskId = processQueue.ObjectId;
	            if (!taskId.HasValue)
	            {
		            var message = Common.Consts.MessageForProcessInterruptedBecauseOfNoObjectId;
		            Worker.SetMessage(processQueue, message);
		            Worker.SetProgress(processQueue, Common.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
		            NotificationSender.SendNotification(processQueue, GetMessageSubject(taskName), message);
		            return;
	            }

	            taskName = TaskService.GetTemplateForTaskName(taskId.Value);

				var units = GetUnits(taskId.Value);

				var urlToDownloadReport = GenerateReport(units);

				var successMessage = "Формирование задания для ЦОД успешно завершено.<br>" +
				              $@"<a href=""{urlToDownloadReport}"">Скачать результат</a>";
				NotificationSender.SendNotification(processQueue, GetMessageSubject(taskName), successMessage);
			}
            catch (Exception exception)
            {
	            var errorId = ErrorManager.LogError(exception);
	            
	            var message = $"Операция завершилась с ошибкой. Подробности в журнале (ИД {errorId}).\n{exception.Message}";
	            NotificationSender.SendNotification(processQueue, GetMessageSubject(taskName), message);

				_log.Error(exception, "Во время работы процесса выброшена ошибка");
			}

            Worker.SetProgress(processQueue, 100);
			_log.Debug("Процесс для выгрузки заданий для ЦОД завершен");
		}


        #region Support Methods

        private List<OMUnit> GetUnits(long taskId)
        {
	        _log.Debug("Начата загрузка ЕО для Задания на оценку с ИД {TaskId}", taskId);

	        var units = UnitRepository.GetEntitiesByCondition(x => x.TaskId == taskId && x.ObjectId != null, x => new
	        {
		        x.ObjectId,
		        x.CadastralNumber
	        });

	        _log.Debug("Загружено unitsCount ЕО", units.Count);

	        return FilterUnits(units);
        }

        private List<OMUnit> FilterUnits(List<OMUnit> units)
        {
	        _log.Debug("Начата фильтрация ЕО по признакам для ЦОД из Росреестра (Изменение ФС)");

	        var fs = RosreestrRegisterService.GetPFsAttribute();

	        var fsAttributes = GbuObjectService.GetAllAttributes(
			        units.Select(x => x.ObjectId.GetValueOrDefault()).ToList(),
			        new List<long> { RosreestrRegisterService.RosreestrRegisterId },
			        new List<long> { fs.Id },
			        DateTime.Now.GetEndOfTheDay(),
			        isLight: true);
	        _log.Debug($"Найдено {fsAttributes.Count} ОН со значениями из Росреестра");

	        var resultObjectIds = new List<long>();
			foreach (var fsAttribute in fsAttributes)
	        {
		        if (fsAttribute.GetValueInString() == "1")
		        {
			        resultObjectIds.Add(fsAttribute.ObjectId);
				}
	        }
			_log.Debug($"После фильтрации ЕО по признакам для ЦОД из Росреестра осталось {resultObjectIds.Count} объектов");

			return units.Where(x => resultObjectIds.Contains(x.ObjectId.GetValueOrDefault())).ToList();
		}

		private string GenerateReport(List<OMUnit> units)
        {
	        _log.Information("Запущена генерация отчета");

			var cadastralNumberColumn = new GbuReportService.Column
	        {
		        Index = 0,
		        Header = "Кадастровый номер",
		        Width = 4
	        };
	        var statusNumberColumn = new GbuReportService.Column
	        {
		        Index = 1,
		        Header = "Статус после обновления",
		        Width = 8
	        };
	        GbuReportService.AddHeaders(new List<string> { cadastralNumberColumn.Header, statusNumberColumn.Header });
	        GbuReportService.SetIndividualWidth(cadastralNumberColumn.Index, cadastralNumberColumn.Width);
	        GbuReportService.SetIndividualWidth(statusNumberColumn.Index, statusNumberColumn.Width);

	        units.ForEach(x =>
	        {
		        var row = GbuReportService.GetCurrentRow();
		        GbuReportService.AddValue(x.CadastralNumber, cadastralNumberColumn.Index, row);
		        GbuReportService.AddValue("Изменение ФС", statusNumberColumn.Index, row);
	        });

	        GbuReportService.SaveReport();

	        _log.Information("Закончена генерация отчета");

			return GbuReportService.UrlToDownload;
        }

        private string GetMessageSubject(string taskName)
        {
	        return string.IsNullOrWhiteSpace(taskName)
		        ? "Результат формирования задания для ЦОД"
		        : $"Результат формирования задания ЦОД для задачи '{taskName}'";
        }

        #endregion
	}
}