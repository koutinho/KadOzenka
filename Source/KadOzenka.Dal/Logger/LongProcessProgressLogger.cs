using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using Serilog;

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
		private readonly ILogger _log = Log.ForContext<LongProcessProgressLogger>();

		public void StartLogProgress(OMQueue processQueue, Func<int> getMaxCount, Func<int> getCurrentCount)
		{
			if (_taskLogProgress != null)
				StopLogProgress();

			_cancelSourceLogProcess = new CancellationTokenSource();
			var token = _cancelSourceLogProcess.Token;
			_taskLogProgress = Task.Run(() => {
				try
				{
					_log.Information("Запуск процесса логирования в потоке {Thread}", Thread.CurrentThread.ManagedThreadId);
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
					_log.Error(ex, "Ошибка во время логирования прогресса в потоке {Thread}", Thread.CurrentThread.ManagedThreadId);
					throw;
				}
			}, token);
		}

		public void StopLogProgress()
		{
			_log.Information("Остановка процесса (вход в метод) логирования в потоке {Thread}", Thread.CurrentThread.ManagedThreadId);
			if (_taskLogProgress == null)
				return;

			_log.Information("Остановка процесса (отмена токена) логирования в потоке {Thread}", Thread.CurrentThread.ManagedThreadId);
			if (!_cancelSourceLogProcess.IsCancellationRequested)
				_cancelSourceLogProcess.Cancel();

			_log.Information("Остановка процесса (Wait) логирования в потоке {Thread}", Thread.CurrentThread.ManagedThreadId);
			try
			{
				_taskLogProgress.Wait();
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException is TaskCanceledException)
				{
					_log.ForContext("CancelTokenSource", _cancelSourceLogProcess, true)
						.Warning(ex, "Попытка остановить логирование прогресса при активном токене отмены");
				}
				else throw;
			}
			catch (TaskCanceledException ex)
			{
				_log.ForContext("CancelTokenSource", _cancelSourceLogProcess,true)
					.Warning(ex, "Попытка остановить логирование прогресса при активном токене отмены");
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Ошибка во время остановки процесса логирования прогресса");
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
				_log.Debug("Обрабатывается объект {CurrentCount} из {MaxCount}", currentCount, maxCount);
				WorkerCommon.SetProgress(processQueue, newProgress);
			}
		}
	}
}
