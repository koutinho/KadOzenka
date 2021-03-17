using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.Logger
{
	public interface ILongProcessProgressLogger
	{
		void StartLogProgress(OMQueue processQueue, Func<int> getMaxCount, Func<int> getCurrentCount);
		void StopLogProgress();
		void LogProgress(int maxCount, int currentCount, OMQueue processQueue);
	}


	public class LongProcessProgressLogger : ILongProcessProgressLogger
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
				try
				{
					while (true)
					{
						
							if (token.IsCancellationRequested)
							{
								break;
							}

							LogProgress(getMaxCount(), getCurrentCount(), processQueue);
							Thread.Sleep(1000);
					}
				}
				catch (Exception ex)
				{
					Serilog.Log.Logger.Error(ex, "Ошибка во время логирования прогресса");
					throw;
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
			catch (Exception ex)
			{
				Serilog.Log.Logger.Error(ex, "Ошибка во время остановки процесса логирования прогресса");
				_taskLogProgress = null;
				throw;
			}
			finally
			{
				_cancelSourceLogProcess.Dispose();
			}

			_taskLogProgress = null;
		}

		public void LogProgress(int maxCount, int currentCount, OMQueue processQueue)
		{
			if (maxCount <= 0 || currentCount <= 0)
				return;

			var newProgress = (long)Math.Round(((double)currentCount / maxCount) * 100);
			//убираем 100, т.к. после обработки объектов процесс обычно выполняет еще какие-нибудь действия
			if (newProgress != processQueue.Progress && newProgress != 100)
			{
				Serilog.Log.Logger.Debug("Обрабатывается объект {CurrentCount} из {MaxCount}", currentCount, maxCount);
				WorkerCommon.SetProgress(processQueue, newProgress);
			}
		}
	}
}
