using System;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.KoObject;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public class TaskSetEstimatedGroup : LongProcess
	{
		public const string LongProcessName = "SetEstimatedGroup";
		private readonly ILogger _log = Log.ForContext<HarmonizationProcess>();

		public static long AddProcessToQueue(int registerId, long objectId, EstimatedGroupModel param)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, registerId, objectId, param.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			try
			{
				_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description);
				WorkerCommon.SetProgress(processQueue, 0);

                var param = processQueue.Parameters.DeserializeFromXml<EstimatedGroupModel>();
                _log.Information("{ProcessType}. Настройки: {Settings}", processType.Description, JsonConvert.SerializeObject(param));

				var koObjectSetEstimatedGroup = new KoObjectSetEstimatedGroup();
                LongProcessProgressLogger.StartLogProgress(processQueue, () => koObjectSetEstimatedGroup.CountAllUnits, () => koObjectSetEstimatedGroup.CurrentCount);

				var urlToDownload = koObjectSetEstimatedGroup.Run(param);
				LongProcessProgressLogger.StopLogProgress();

				string message = "Присвоение оценочной группы успешно завершено." +
				                 $@"<a href=""{urlToDownload}"">Скачать результат</a>";
				NotificationSender.SendNotification(processQueue, "Присвоение оценочной группы", message);

                WorkerCommon.SetProgress(processQueue, 100);
                _log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);
			}
			catch (Exception e)
			{
				_log.Error(e, "Операция присвоения оценочной группы завершена с ошибкой");
				LongProcessProgressLogger.StopLogProgress();
				NotificationSender.SendNotification(processQueue, "Присвоение оценочной группы", $"Присвоение оценочной группы завершено с ошибкой. Подробнее в журнале ошибок");
				ErrorManager.LogError(e);
				throw;
			}
		}
	}
}
