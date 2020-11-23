using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess.Consts
{
	public class LongProcessService
	{
		private static List<Status> _processRunningStatuses;

		private static List<Status> ProcessRunningStatuses =>
			_processRunningStatuses ?? (_processRunningStatuses = new List<Status> {Status.Running, Status.Added, Status.PrepareToRun});

		public static bool CheckProcessExistsInQueue(long processId, long? objectId)
		{
			return GetBaseQueryToActiveProcessInQueue(processId, objectId).ExecuteExists();
		}

		public static OMQueue GetQueue(long processId, long? objectId)
		{
			return GetBaseQueryToActiveProcessInQueue(processId, objectId).SelectAll().ExecuteFirstOrDefault();
		}

		
		#region Support Methods

		private static QSQuery<OMQueue> GetBaseQueryToActiveProcessInQueue(long processId, long? objectId)
		{
			return OMQueue.Where(x =>
				x.ProcessTypeId == processId && ProcessRunningStatuses.Contains(x.Status_Code) &&
				x.ObjectId == objectId);
		}

		#endregion
	}
}
