using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Support;
using KadOzenka.Dal.WorkerCheckerDataBase.Interface;
using Newtonsoft.Json;
using Serilog;

namespace KadOzenka.Dal.WorkerCheckerDataBase.ReportTableUpdateCheckerDb
{
	public class ReportTableUpdaterChecker: IWorkerChecker
	{
		private ILogger logger = Log.ForContext<ReportTableUpdaterChecker>();
		public void Check()
		{
			if (!ReportTableUpdater.IsNotDoneJobExists(logger))
			{
				return;
			}

			var jobInfo = ReportTableUpdater.GetJobsInfo(logger);

			if(jobInfo == null) return;

			if (jobInfo.Max > jobInfo.Min)
			{
				var param = JsonConvert.SerializeObject(jobInfo);
				ABaseReportTableLongProcess.AddProcessToQueue(ReportTableUpdater.ProcessName, param);
			}
		}
	}
}