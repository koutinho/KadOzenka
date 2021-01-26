using System.Threading;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess.RecycleBin
{
	public class FlushOldDataFromRecycleBinLongProcess : LongProcess
	{
		private static readonly ILogger _log = Log.ForContext<FlushOldDataFromRecycleBinLongProcess>();

		private RecycleBinService RecycleBinService { get; }

		public FlushOldDataFromRecycleBinLongProcess()
		{
			RecycleBinService = new RecycleBinService();
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description);
			WorkerCommon.SetProgress(processQueue, 0);

			var processParams = processType.Parameters.DeserializeFromXml<FlushOldDataFromRecycleBinLongProcessParams>() ?? new FlushOldDataFromRecycleBinLongProcessParams();
			RecycleBinService.FlushOldData(processParams.KeepDataForPastNDays);

			WorkerCommon.SetProgress(processQueue, 100);
			_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);
		}
	}
}
