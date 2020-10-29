using System;
using System.Collections.Generic;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Tasks;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
    public class TaskForCodLongProcess : LongProcess
    {
	    private static readonly ILogger _log = Log.ForContext<ExportAttributeToKoProcess>();

		public static void AddProcessToQueue(long taskId)
        {
            LongProcessManager.AddTaskToQueue(nameof(TaskForCodLongProcess), objectId: taskId);
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            _log.ForContext("TaskId", processQueue.ObjectId).Information("Запущен процесс для выгрузки заданий для ЦОД");

			var taskName = string.Empty;
            try
            {
	            var taskId = processQueue.ObjectId;
	            if (!taskId.HasValue)
	            {
		            var message = Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId;
		            WorkerCommon.SetMessage(processQueue, message);
		            WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
		            SendMessage(processQueue, message, GetMessageSubject(taskName));
		            return;
	            }

	            taskName = new TaskService().GetTemplateForTaskName(taskId.Value);

				var units = GetUnits(taskId.Value);
				_log.Information($"Найдено {units.Count} Единиц оценки");

				var urlToDownloadReport = GenerateReport(units);

				var successMessage = "Формирование задания для ЦОД успешно завершено.<br>" +
				              $@"<a href=""{urlToDownloadReport}"">Скачать результат</a>";
				SendMessage(processQueue, successMessage, GetMessageSubject(taskName));
			}
            catch (Exception exception)
            {
	            var errorId = ErrorManager.LogError(exception);
	            
	            var message = $"Операция завершилась с ошибкой. Подробности в журнале (ИД {errorId}).\n{exception.Message}";
				SendMessage(processQueue, message, GetMessageSubject(taskName));

				_log.Error(exception, "Во время работы процесса выброшена ошибка");
			}
            
			WorkerCommon.SetProgress(processQueue, 100);
			_log.Information("Процесс для выгрузки заданий для ЦОД завершен");
		}


        #region Support Methods

        private List<OMUnit> GetUnits(long taskId)
        {
	        var unitUpdateStatuses = new List<UnitUpdateStatus> { UnitUpdateStatus.New, UnitUpdateStatus.FsChange };

	        var units = OMUnit.Where(x => x.TaskId == taskId && unitUpdateStatuses.Contains(x.UpdateStatus_Code))
		        .Select(x => new
		        {
			        x.CadastralNumber,
			        x.UpdateStatus_Code
		        }).Execute();

	        return units;
        }

        private string GenerateReport(List<OMUnit> units)
        {
	        _log.Information("Запущена генерация отчета");

			var reportService = new GbuReportService();

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
	        reportService.AddHeaders(new List<string> { cadastralNumberColumn.Header, statusNumberColumn.Header });
	        reportService.SetIndividualWidth(cadastralNumberColumn.Index, cadastralNumberColumn.Width);
	        reportService.SetIndividualWidth(statusNumberColumn.Index, statusNumberColumn.Width);

	        units.ForEach(x =>
	        {
		        var row = reportService.GetCurrentRow();
		        reportService.AddValue(x.CadastralNumber, cadastralNumberColumn.Index, row);
		        reportService.AddValue(x.UpdateStatus_Code.GetEnumDescription(), statusNumberColumn.Index, row);
	        });

	        reportService.SetStyle();
	        reportService.SaveReport("Задания для ЦОД");

	        _log.Information("Закончена генерация отчета");

			return reportService.UrlToDownload;
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