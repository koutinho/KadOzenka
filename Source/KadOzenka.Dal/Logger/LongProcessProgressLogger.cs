using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.Logger
{
	public class LongProcessProgressLogger
	{
		private Task _taskLogProgress;
		private CancellationTokenSource _cancelSourceLogProcess;

		public void StartLogProgress(OMQueue processQueue, Func<int> getMaxCount, Func<int> getCurrentCount)
		{
			if (_taskLogProgress != null)
				StopLogProgress();

			_cancelSourceLogProcess = new CancellationTokenSource();
			var token = _cancelSourceLogProcess.Token;
			_taskLogProgress = Task.Run(() => {
				while (true)
				{
					if (token.IsCancellationRequested)
					{
						break;
					}

					LogProgress(getMaxCount(), getCurrentCount(), processQueue);
					Thread.Sleep(1000);
				}
			}, token);
		}

		public void StopLogProgress()
		{
			if (_taskLogProgress == null)
				return;

			_cancelSourceLogProcess.Cancel();

			try
			{
				_taskLogProgress.Wait();
			}
			finally
			{
				_cancelSourceLogProcess.Dispose();
			}

			_taskLogProgress = null;
		}

		private void LogProgress(int maxCount, int currentCount, OMQueue processQueue)
		{
			if (maxCount <= 0 || currentCount <= 0)
				return;

			var newProgress = (long)Math.Round(((double)currentCount / maxCount) * 100);
			if (newProgress != processQueue.Progress)
				WorkerCommon.SetProgress(processQueue, newProgress);
		}
	}
}
