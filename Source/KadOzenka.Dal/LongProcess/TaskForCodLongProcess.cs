using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using KadOzenka.Dal.Tasks;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
    public class TaskForCodLongProcess : LongProcess
    {
	    private static readonly ILogger _log = Log.ForContext<ExportAttributeToKoProcess>();
	    private GbuObjectService GbuObjectService { get; }
	    private RosreestrRegisterService RosreestrRegisterService { get; }

	    public TaskForCodLongProcess()
	    {
		    RosreestrRegisterService = new RosreestrRegisterService();
			GbuObjectService = new GbuObjectService();
		}


		public static void AddProcessToQueue(long taskId)
        {
            LongProcessManager.AddTaskToQueue(nameof(TaskForCodLongProcess), objectId: taskId);
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            _log.ForContext("TaskId", processQueue.ObjectId).Debug("Запущен процесс для выгрузки заданий для ЦОД");

			var taskName = string.Empty;
            try
            {
	            var taskId = processQueue.ObjectId;
	            if (!taskId.HasValue)
	            {
		            var message = Common.Consts.MessageForProcessInterruptedBecauseOfNoObjectId;
		            WorkerCommon.SetMessage(processQueue, message);
		            WorkerCommon.SetProgress(processQueue, Common.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
		            SendMessage(processQueue, message, GetMessageSubject(taskName));
		            return;
	            }

	            taskName = new TaskService().GetTemplateForTaskName(taskId.Value);

				var units = GetUnits(taskId.Value);

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
			_log.Debug("Процесс для выгрузки заданий для ЦОД завершен");
		}


        #region Support Methods

        private List<OMUnit> GetUnits(long taskId)
        {
	        _log.Debug("Начата загрузка ЕО для Задания на оценку с ИД {TaskId}", taskId);

	        var units = OMUnit.Where(x => x.TaskId == taskId && x.ObjectId != null)
		        .Select(x => new
		        {
					x.ObjectId,
			        x.CadastralNumber
		        }).Execute();

	        _log.Debug($"Загружено {units.Count} ЕО");

	        return FilterUnits(units);
        }

        private List<OMUnit> FilterUnits(List<OMUnit> units)
        {
	        _log.Debug("Начата фильтрация ЕО по признакам для ЦОД из Росреестра");

	        var fs = RosreestrRegisterService.GetPFsAttribute();

	        var fsAttributes = GbuObjectService.GetAllAttributes(
			        units.Select(x => x.ObjectId.GetValueOrDefault()).ToList(),
			        new List<long> { RosreestrRegisterService.RegisterId },
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
		        reportService.AddValue("Изменение ФС", statusNumberColumn.Index, row);
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