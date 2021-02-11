using System;
using System.Threading;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.Tasks;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess.RecycleBin
{
	public class MoveTaskToRecycleBinLongProcess : LongProcess
	{
		public const string LongProcessName = "MoveTaskToRecycleBinLongProcess";
		private static readonly ILogger _log = Log.ForContext<MoveTaskToRecycleBinLongProcess>();

		private TaskService TaskService { get; }

		public static long AddProcessToQueue(MoveTaskToRecycleBinLongProcessParams settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, settings.TaskId, settings.SerializeToXml());
		}

		public static bool IsDuplicateProcessExists(long taskId)
		{
			return OMQueue.Where(x => x.ParentProcessType.ProcessName == LongProcessName
			                          && x.ObjectId == taskId
									  && (x.Status_Code == Status.Added ||
			                              x.Status_Code == Status.PrepareToRun ||
			                              x.Status_Code == Status.Running))
				.ExecuteExists();
		}

		public MoveTaskToRecycleBinLongProcess()
		{
			TaskService = new TaskService();
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			try
			{
				_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description);
				WorkerCommon.SetProgress(processQueue, 0);

				var settings = processQueue.Parameters.DeserializeFromXml<MoveTaskToRecycleBinLongProcessParams>();
				_log.Information("{ProcessType}. Настройки: {Settings}", processType.Description,
					JsonConvert.SerializeObject(settings));

				TaskService.DeleteTask(settings.TaskId, settings.UserId);

				WorkerCommon.SetProgress(processQueue, 100);
				_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);

				NotificationSender.SendNotification(processQueue, "Результат операции Удаления задания на оценку", $"Задание на оценку '{settings.TaskName}' тура '{settings.TourYear}' успешно удалено");
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Операция Удаления задания на оценку завершена с ошибкой");
				NotificationSender.SendNotification(processQueue, "Результат операции Удаления задания на оценку", $"Операция удаления задания на оценку была прервана: {ex.Message}");
				throw;
			}

		}
	}
}
