using System.Threading;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Microsoft.Practices.EnterpriseLibrary.Data;
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
			
			CreteTable();
			Log.Verbose("Создана таблица-кеш для данных отчета.");

			var taskIds = UniformReportService.GetLongPerformanceTasks();
			Log.Verbose("Количество задач с большим числом юнитов {count}.", taskIds.Count);
			
			taskIds.ForEach(taskId =>
			{
				Log.ForContext("TaskId", taskId).Verbose("Начата обработка задачи с Id {taskId}.", taskId);

				var sql = UniformReportService.GetBasicSql(taskId);
				sql.Append($@"
					INSERT INTO {UniformReportService.TableName} 
					select {taskId}, cadastralNumber, array_remove(attributes, NULL) as attributes from data;");

				var command = DBMngr.Main.GetSqlStringCommand(sql.ToString());
				DBMngr.Main.ExecuteNonQuery(command);

				Log.Verbose("Закончена обработка задачи с Id {taskId}.", taskId);
			});
		}


		#region Support Methods

		private void CreteTable()
		{
			var sql = @"DROP TABLE IF EXISTS uniformreport;

				CREATE TABLE uniformreport (
				    task_id				bigint NOT NULL,
				    cadastral_number	varchar(20) NOT NULL,
				    attributes			text
				);

				CREATE INDEX ON uniformreport (task_id);";

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			DBMngr.Main.ExecuteNonQuery(command);
		}

		#endregion


		#region Entities

		
		#endregion
	}
}
