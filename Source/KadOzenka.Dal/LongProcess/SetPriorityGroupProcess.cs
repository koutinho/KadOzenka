using System;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
	public class SetPriorityGroupProcess : LongProcess
	{
		public const string LongProcessName = "SetPriorityGroupProcess";
		private readonly ILogger _log = Log.ForContext<SetPriorityGroupProcess>();
		private readonly ICodDictionaryService _dictionaryService;

		public SetPriorityGroupProcess(ICodDictionaryService dictionaryService)
		{
			_dictionaryService = dictionaryService;
		}

		public SetPriorityGroupProcess()
		{

			_dictionaryService = new CodDictionaryService();
		}

		public static long AddProcessToQueue(GroupingSettings settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			try
			{
				_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description ?? "debug");
				WorkerCommon.SetProgress(processQueue, 0);

				var settings = processQueue.Parameters.DeserializeFromXml<GroupingSettings>();
				LongProcessProgressLogger.StartLogProgress(processQueue, () => PriorityGrouping.MaxCount, () => PriorityGrouping.CurrentCount);

				var url = new PriorityGrouping(_dictionaryService).SetPriorityGroup(settings, cancellationToken);
				//TestLongRunningProcess(settings);

				LongProcessProgressLogger.StopLogProgress();

				WorkerCommon.SetProgress(processQueue, 100);

				string message = "Операция успешно завершена. " +
				                 $@"<a href=""{url}"">Скачать результат</a>";


				NotificationSender.SendNotification(processQueue, "Результат Операции группировки", message);

				_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Операция нормализации завершена с ошибкой");
				LongProcessProgressLogger.StopLogProgress();
				NotificationSender.SendNotification(processQueue, "Результат Операции группировки", $"Операция была прервана: {ex.Message}");
				ErrorManager.LogError(ex);
				throw;
			}
		}

		protected void TestLongRunningProcess(GroupingSettings setting)
		{
			PriorityGrouping.MaxCount = 200;
			PriorityGrouping.CurrentCount = 0;
			for (int i = 0; i < 200; i++)
			{
				PriorityGrouping.CurrentCount++;
				Thread.Sleep(1000);
			}
		}
	}
}
