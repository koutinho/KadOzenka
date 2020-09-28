using System.Threading;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	public class DataCompositionByCharacteristicsReportsLongProcess : LongProcess
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<DataCompositionByCharacteristicsReportsLongProcess>();
		public DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; set; }
		public const string LongProcessName = nameof(DataCompositionByCharacteristicsReportsLongProcess);

		public DataCompositionByCharacteristicsReportsLongProcess()
		{
			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
		}

		public static long AddProcessToQueue()
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName);
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Log.Information("Старт фонового процесса: {Description}.", processType.Description);

			DataCompositionByCharacteristicsService.CreteCacheTable();
			Log.Verbose("Создана таблица-кеш для данных отчета.");

			var taskIds = DataCompositionByCharacteristicsService.GetLongPerformanceTasks();
			Log.Verbose("Количество задач с большим числом юнитов {count}.", taskIds.Count);
			
			taskIds.ForEach(taskId =>
			{
				Log.ForContext("TaskId", taskId).Verbose("Начата обработка задачи с Id {taskId}.", taskId);

				DataCompositionByCharacteristicsService.FillCache(taskId);

				Log.Verbose("Закончена обработка задачи с Id {taskId}.", taskId);
			});
		}
	}
}
