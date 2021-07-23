using System;
using System.Threading;
using CommonSdks.RecycleBin;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess.RecycleBin
{
	public class RestoreObjectFromRecycleBinLongProcess : LongProcess
	{
		public const string LongProcessName = "RestoreObjectFromRecycleBinLongProcess";
		private static readonly ILogger _log = Log.ForContext<RestoreObjectFromRecycleBinLongProcess>();

		private RecycleBinService RecycleBinService { get; }

		public static long AddProcessToQueue(RestoreObjectFromRecycleBinLongProcessParams settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, settings.EventId, settings.SerializeToXml());
		}

		public static bool IsDuplicateProcessExists(long eventId)
		{
			return OMQueue.Where(x => x.ParentProcessType.ProcessName == LongProcessName
			                          && x.ObjectId == eventId
			                          && (x.Status_Code == Status.Added ||
			                              x.Status_Code == Status.PrepareToRun ||
			                              x.Status_Code == Status.Running))
				.ExecuteExists();
		}

		public RestoreObjectFromRecycleBinLongProcess()
		{
			RecycleBinService = new RecycleBinService();
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			try
			{
				_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description);
				WorkerCommon.SetProgress(processQueue, 0);

				var settings =
					processQueue.Parameters.DeserializeFromXml<RestoreObjectFromRecycleBinLongProcessParams>();
				_log.Information("{ProcessType}. Настройки: {Settings}", processType.Description,
					JsonConvert.SerializeObject(settings));

				RecycleBinService.RestoreObject(settings.EventId);

				WorkerCommon.SetProgress(processQueue, 100);
				_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);

				NotificationSender.SendNotification(processQueue, "Результат операции Восстановления сущности из корзины", $"Запись из корзины {settings.ObjectName} успешно восстановлена");
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Операция Восстановления сущности из корзины завершена с ошибкой");
				NotificationSender.SendNotification(processQueue, "Результат операции Восстановления сущности из корзины", $"Операция восстановления сущности из корзины была прервана: {ex.Message}");
				throw;
			}
		}
	}
}
