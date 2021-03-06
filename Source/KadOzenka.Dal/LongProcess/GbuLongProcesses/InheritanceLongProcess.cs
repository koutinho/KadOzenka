using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess.GbuLongProcesses
{
	public class InheritanceLongProcess : LongProcess
	{
		public const string LongProcessName = "InheritanceLongProcess";
		private static readonly ILogger _log = Log.ForContext<InheritanceLongProcess>();

		public static long AddProcessToQueue(GbuInheritanceAttributeSettings setting)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, setting.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			var cancelSource = new CancellationTokenSource();
			var cancelToken = cancelSource.Token;
			try
			{
				WorkerCommon.SetProgress(processQueue, 0);

				var settings = processQueue.Parameters.DeserializeFromXml<GbuInheritanceAttributeSettings>();
				var t = Task.Run(() =>
				{
					while (true)
					{
						if (cancelToken.IsCancellationRequested)
						{
							break;
						}

						if (GbuObjectInheritanceAttribute.MaxCount > 0 && GbuObjectInheritanceAttribute.CurrentCount > 0)
						{
							var newProgress =
								(long)Math.Round(((double)GbuObjectInheritanceAttribute.CurrentCount / GbuObjectInheritanceAttribute.MaxCount) * 100);
							if (newProgress != processQueue.Progress)
							{
								WorkerCommon.SetProgress(processQueue, newProgress);
							}
						}
					}
				}, cancelToken);


				var urlToDownload = new GbuObjectInheritanceAttribute(settings).Run();
				cancelSource.Cancel();
				t.Wait(cancellationToken);
				cancelSource.Dispose();

				WorkerCommon.SetProgress(processQueue, 100);


				string message = "Операция успешно завершена." +
				                 $@"<a href=""{urlToDownload}"">Скачать результат</a>";

				NotificationSender.SendNotification(processQueue, "Результат операции Наследования", message);
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Операция наследования завершена с ошибкой");

				cancelSource.Cancel();
				NotificationSender.SendNotification(processQueue, "Результат операции наследование", $"Операция была прервана: {ex.Message}");
				throw;
			}
		}
	}
}