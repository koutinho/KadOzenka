using System;
using System.Threading;
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
