using System;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public class CalculateCadastralPriceLongProcess : LongProcess
	{
		public const string LongProcessName = "CalculateCadastralPrice";

		public static long AddProcessToQueue(KOCalcSettings settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			try
			{
				WorkerCommon.SetProgress(processQueue, 0);

				var settings = processQueue.Parameters.DeserializeFromXml<KOCalcSettings>();
				OMGroup.CalculateSelectGroup(settings);

				WorkerCommon.SetProgress(processQueue, 100);
				NotificationSender.SendNotification(processQueue, "Результат Операции Расчета кадастровой стоимости", "Операция успешно завершена.");
			}
			catch (Exception ex)
			{
				var errorId = ErrorManager.LogError(ex);
				NotificationSender.SendNotification(processQueue, "Результат Операции Расчета кадастровой стоимости", $"Операция была прервана: {ex.Message} (Подробнее в журнале: {errorId})");
				throw;
			}
		}
	}
}
