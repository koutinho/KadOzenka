using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.LongProcess;
using ObjectModel.Gbu.GroupingAlgoritm;

namespace KadOzenka.Dal.LongProcess
{
	public class SetPriorityGroupProcess : ILongProcess
	{
		public const string LongProcessName = "SetPriorityGroupProcess";

		public static void AddProcessToQueue(GroupingSettings settings)
		{
			LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			var cancelSource = new CancellationTokenSource();
			var cancelToken = cancelSource.Token;
			try
			{
				WorkerCommon.SetProgress(processQueue, 0);
				var settings = processQueue.Parameters.DeserializeFromXml<GroupingSettings>();
				var t = Task.Run(() => {
					while (true)
					{
						if (cancelToken.IsCancellationRequested)
						{
							break;
						}
						if (PriorityGrouping.MaxCount > 0 && PriorityGrouping.CurrentCount > 0)
						{
							var newProgress = (long)Math.Round(((double)PriorityGrouping.CurrentCount / PriorityGrouping.MaxCount) * 100);
							if (newProgress != processQueue.Progress)
							{
								WorkerCommon.SetProgress(processQueue, newProgress);
							}
						}
					}
				}, cancelToken);

				PriorityGrouping.SetPriorityGroup(settings);
				//Test1();
				cancelSource.Cancel();
				t.Wait(cancellationToken);
				cancelSource.Dispose();

				WorkerCommon.SetProgress(processQueue, 100);
				SendSuccessNotification(processQueue);
			}
			catch (Exception ex)
			{
				cancelSource.Cancel();
				SendFailureNotification(processQueue, ex.Message);
				throw;
			}
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			throw new NotImplementedException();
		}

		public bool Test()
		{
			return true;
		}

		private void SendSuccessNotification(OMQueue processQueue)
		{
			new MessageService().SendMessages(new MessageDto
			{
				UserIds = processQueue.UserId.HasValue ? new long[] { processQueue.UserId.Value } : new long[] { },
				Subject = $"Результат Операции группировки",
				Message = $"Операция группировки успешно завершена",
				IsUrgent = true,
				IsEmail = true
			});
		}

		private void SendFailureNotification(OMQueue processQueue, string message)
		{
			new MessageService().SendMessages(new MessageDto
			{
				UserIds = processQueue.UserId.HasValue ? new long[] { processQueue.UserId.Value } : new long[] { },
				Subject = $"Результат Операции группировки",
				Message = $"Операция группировки была прервана: {message}",
				IsUrgent = true,
				IsEmail = true
			});
		}

		private void Test1()
		{
			PriorityGrouping.MaxCount = 100;
			PriorityGrouping.CurrentCount = 0;
			for (int i = 0; i < 100; i++)
			{
				PriorityGrouping.CurrentCount++;
				Thread.Sleep(1000);
			}
		}
	}
}
