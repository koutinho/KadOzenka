using System;
using System.Threading;
using Core.Register.LongProcessManagment;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Support
{
	public abstract class ABaseReportTableLongProcess : LongProcess
	{
		public static string TmpTableName => "data_composition_by_characteristics_tmp";

		protected ILogger Logger { get; }

		protected ABaseReportTableLongProcess(ILogger logger)
		{
			Logger = logger;
		}

		public static void AddProcessToQueue(string processType, string param)
		{
			LongProcessManager.AddTaskToQueue(processType, null, null, param);
		}

		protected void CheckCancellationToken(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
				return;

			var message = "Обновление кеш-таблицы было отменено пользователем";
			Logger.Error(message);

			throw new Exception(message);
		}
	}
}
