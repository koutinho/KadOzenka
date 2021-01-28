using System;
using System.Collections.Generic;
using System.Threading;
using Core.Register.LongProcessManagment;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using Serilog;
using Serilog.Context;

namespace KadOzenka.Dal.LongProcess.BackgroundExporter
{
	public class BackgroundExporterLongProcess: ILongProcess
	{
		private readonly ILogger _log = Log.ForContext<BackgroundExporterLongProcess>();

		public static string ProcessType = nameof(BackgroundExporterLongProcess); 

		public static long AddProcessToQueue(List<long> backgroundIds)
		{
			string param = JsonConvert.SerializeObject(backgroundIds);
			return LongProcessManager.AddTaskToQueue(ProcessType, null, null, param);
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			LogContext.PushProperty("UserName", processQueue?.SpecialTdUserFullname);
			LogContext.PushProperty("QueueId", processQueue?.Id);
			LogContext.PushProperty("TypeId", processType?.Id);
			_log.ForContext("CreateDate", processQueue?.CreateDate)
				.ForContext("StartDate", processQueue?.StartDate)
				.Information("Старт фонового процесса: {Description}", processType?.Description);

			WorkerCommon.SetProgress(processQueue, 0);

			var backgroundIds = JsonConvert.DeserializeObject<List<long>>(processQueue?.Parameters);

			_log.ForContext("BackgroundIds", backgroundIds, true).Debug("Найденные ид для выполенения в фоновом процессе");

			List<OMBackgroundExport> backgroundExports =
				OMBackgroundExport.Where(x => backgroundIds.Contains(x.Id)).SelectAll().Execute();

			new Platform.Web.Services.BackgroundExporterScheduler.BackgroundExporterLongProcess().Export(backgroundExports, processType, processQueue, cancellationToken);
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
		}

		public bool Test()
		{
			return true;
		}
	}
}