using System.Threading;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	public class UniformReportLongProcess : LongProcess
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<UniformReportLongProcess>();
		public UniformReportService UniformReportService { get; set; }
		public const string LongProcessName = nameof(UniformReportLongProcess);

		public UniformReportLongProcess()
		{
			UniformReportService = new UniformReportService();
		}

		public static long AddProcessToQueue()
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName);
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Log.Information("Старт фонового процесса: {Description}.", processType.Description);

			UniformReportService.CreteCacheTable();
			Log.Verbose("Создана таблица-кеш для данных отчета.");

			var taskIds = UniformReportService.GetLongPerformanceTasks();
			Log.Verbose("Количество задач с большим числом юнитов {count}.", taskIds.Count);
			
			taskIds.ForEach(taskId =>
			{
				Log.ForContext("TaskId", taskId).Verbose("Начата обработка задачи с Id {taskId}.", taskId);

				UniformReportService.FillCache(taskId);

				Log.Verbose("Закончена обработка задачи с Id {taskId}.", taskId);
			});
		}
	}
}
