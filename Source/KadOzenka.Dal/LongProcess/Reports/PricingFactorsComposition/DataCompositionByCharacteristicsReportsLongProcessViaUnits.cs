using System.Threading;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	public class DataCompositionByCharacteristicsReportsLongProcessViaUnits : LongProcess
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<DataCompositionByCharacteristicsReportsLongProcessViaUnits>();
		public DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; set; }
		public const string LongProcessName = "DataCompositionByCharacteristicsReportsLongProcess";

		public DataCompositionByCharacteristicsReportsLongProcessViaUnits()
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
			Log.Verbose("Количество задач с большим числом юнитов {Сount}.", taskIds.Count);

			taskIds.ForEach(taskId =>
			{
				Log.Verbose("Начата обработка задачи с Id {TaskId}.", taskId);

				DataCompositionByCharacteristicsService.FillCache(taskId);

				Log.Verbose("Закончена обработка задачи с Id {TaskId}.", taskId);
			});
		}
	}
}
