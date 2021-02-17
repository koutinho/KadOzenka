using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess.Common
{
	public interface ILongProcessService
	{
		bool HasActiveProcessInQueue(long processId, long? objectId);
		OMQueue GetProcessActiveQueue(long processId, long? objectId);
		OMQueue GetLastSuccessfulCompletedQueue(long processId);
	}


	public class LongProcessService : ILongProcessService
	{
		private static List<Status> _processRunningStatuses;

		private static List<Status> ProcessRunningStatuses =>
			_processRunningStatuses ?? (_processRunningStatuses = new List<Status> {Status.Running, Status.Added, Status.PrepareToRun});

		public bool HasActiveProcessInQueue(long processId, long? objectId)
		{
			return GetBaseQueryToActiveProcessInQueue(processId, objectId).ExecuteExists();
		}

		public OMQueue GetProcessActiveQueue(long processId, long? objectId)
		{
			return GetBaseQueryToActiveProcessInQueue(processId, objectId).SelectAll().ExecuteFirstOrDefault();
		}

		public OMQueue GetLastSuccessfulCompletedQueue(long processId)
		{
			return OMQueue.Where(x => x.ProcessTypeId == processId && x.Status_Code == Status.Completed)
				.OrderByDescending(x => x.EndDate)
				.SelectAll()
				.ExecuteFirstOrDefault();
		}

		
		#region Support Methods

		private QSQuery<OMQueue> GetBaseQueryToActiveProcessInQueue(long processId, long? objectId)
		{
			return OMQueue.Where(x =>
				x.ProcessTypeId == processId && ProcessRunningStatuses.Contains(x.Status_Code) &&
				x.ObjectId == objectId);
		}

		#endregion
	}
}
