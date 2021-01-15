using System;
using System.Linq;
using System.Threading;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Entities;
using Serilog;
using SerilogTimings.Extensions;

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

		
		
		public JobInfo GetJobsInfo()
		{
			using (Logger.TimeOperation("Получение информации о невыполненных заданиях"))
			{
				var sql = @$"select min(job_number) as {nameof(JobInfo.Min)}, max(job_number) as {nameof(JobInfo.Max)}
							from {TmpTableName} where COALESCE(is_done, 0) = 0";

				var jobsInfo = QSQuery.ExecuteSql<JobInfo>(sql).FirstOrDefault();
				Logger.Debug($"Минимальный номер невыполненной работы - {jobsInfo?.Min}, максимальный - {jobsInfo?.Max}");

				return jobsInfo;
			}
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
