using System;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.KoObject;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public class TaskSetEstimatedGroup : LongProcess
	{
		public const string LongProcessName = "SetEstimatedGroup";

		public static void AddProcessToQueue(int registerId, long objectId, EstimatedGroupModel param)
		{
			LongProcessManager.AddTaskToQueue(LongProcessName, registerId, objectId, param.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			try
			{
                WorkerCommon.SetProgress(processQueue, 0);

                var param = processQueue.Parameters.DeserializeFromXml<EstimatedGroupModel>();
				KoObjectSetEstimatedGroup.Run(param);
				NotificationSender.SendNotification(processQueue, "Присвоение оценочной группы", "Присвоение оценочной группы успешно завершено");

                WorkerCommon.SetProgress(processQueue, 100);
            }
			catch (Exception e)
			{
				NotificationSender.SendNotification(processQueue, "Присвоение оценочной группы", $"Присвоение оценочной группы завершено с ошибкой. Подробнее в журнале ошибок");
				ErrorManager.LogError(e);
				throw;
			}
		}
	}
}
