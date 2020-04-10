using System;
using System.Threading;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess
{
	public abstract class LongProcess : ILongProcess
	{
        protected const int PercentageInterval = 10;
        protected NotificationSender NotificationSender { get; set; }

		protected LongProcess()
		{
			NotificationSender = new NotificationSender();
		}

		public abstract void StartProcess(OMProcessType processType, OMQueue processQueue,
			CancellationToken cancellationToken);

		public virtual void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			throw new NotImplementedException();
		}

		public bool Test()
		{
			return true;
		}

        protected static void LogProgress(OMQueue processQueue, int i, int numberOfObjects, ref int nextPercent)
        {
            var currentPercent = (i * 100) / numberOfObjects;
            if (currentPercent < nextPercent)
                return;

            WorkerCommon.SetProgress(processQueue, currentPercent);
            nextPercent = currentPercent + PercentageInterval;
        }
    }
}
