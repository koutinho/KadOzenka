using System;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
	public class SetPriorityGroupFinalProcess : LongProcess
	{
		public const string LongProcessName = "SetPriorityGroupFinalProcess";
		private readonly ILogger _log = Log.ForContext<SetPriorityGroupFinalProcess>();

		public static long AddProcessToQueue(GroupingSettingsFinal settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue,
			CancellationToken cancellationToken)
		{
			try
			{
				_log.Information("Начато выполнение фонового процесса: {ProcessType}",
					processType.Description);
				WorkerCommon.SetProgress(processQueue, 0);

				var settings = processQueue.Parameters.DeserializeFromXml<GroupingSettingsFinal>();

				var process = new PriorityGroupingFinal();

				LongProcessProgressLogger.StartLogProgress(processQueue, () => process.MaxCount,
					() => process.CurrentCount);

				var url = process.SetPriorityGroup(settings, cancellationToken);

				_log.Information("URL результата финальной группировки: {Url}", url);

				LongProcessProgressLogger.StopLogProgress();

				WorkerCommon.SetProgress(processQueue, 100);

				string message = "Операция успешно завершена. " +
				                 $@"<a href=""{url}"">Скачать результат</a>";


				NotificationSender.SendNotification(processQueue, "Результат Операции группировки", message);

				_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Операция нормализации завершена с ошибкой");
				LongProcessProgressLogger.StopLogProgress();
				NotificationSender.SendNotification(processQueue, "Результат Операции группировки",
					$"Операция была прервана: {ex.Message}");
				ErrorManager.LogError(ex);
				throw;
			}
		}
	}
}
