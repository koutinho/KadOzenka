using System;
using System.Threading;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.ManagementDecisionSupport.Settings;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalReportsExport;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess.ManagementDecisionSupport
{
	public class StatisticsReportWidgetExportLongProcess : LongProcess
	{
		public const string LongProcessName = "StatisticsReportWidgetExportLongProcess";
		private readonly ILogger _log = Log.ForContext<StatisticsReportWidgetExportLongProcess>();
		private readonly StatisticsReportsWidgetExportService _statisticsReportsWidgetExportService;

		public StatisticsReportWidgetExportLongProcess()
		{
			_statisticsReportsWidgetExportService = new StatisticsReportsWidgetExportService(new StatisticsReportsWidgetService());
		}

		public static void AddProcessToQueue(StatisticsReportWidgetExportLongProcessSettings statisticsReportWidgetExportLongProcessSettings)
		{
			LongProcessManager.AddTaskToQueue(LongProcessName, null, null, statisticsReportWidgetExportLongProcessSettings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description);
			WorkerCommon.SetProgress(processQueue, 0);

			var settings = processQueue.Parameters.DeserializeFromXml<StatisticsReportWidgetExportLongProcessSettings>();
			_log.Information("{ProcessType}. Настройки: {Settings}", processType.Description,
			JsonConvert.SerializeObject(settings));
			try
			{
				StatisticsReportsExportResult result;
				switch (settings.StatisticsReportExportType)
				{
					case StatisticsReportExportType.ImportedObjects:
						result = _statisticsReportsWidgetExportService.ExportImportedObjects(settings.DataSourceRequest,
							settings.DateStart, settings.DateEnd, true);
						break;
					case StatisticsReportExportType.ExportedObjects:
						result = _statisticsReportsWidgetExportService.ExportExportedObjects(settings.DataSourceRequest,
							settings.DateStart, settings.DateEnd, true);
						break;
					case StatisticsReportExportType.ZoneStatistics:
						result = _statisticsReportsWidgetExportService.ExportZoneStatistics(settings.DataSourceRequest,
							settings.DateStart, settings.DateEnd, true);
						break;
					case StatisticsReportExportType.FactorStatistics:
						result = _statisticsReportsWidgetExportService.ExportFactorStatistics(settings.DataSourceRequest,
							settings.DateStart, settings.DateEnd, true);
						break;
					case StatisticsReportExportType.GroupStatistics:
						result = _statisticsReportsWidgetExportService.ExportGroupStatistics(settings.DataSourceRequest,
							settings.DateStart, settings.DateEnd, true);
						break;
					default:
						throw new Exception($"Неподдерживаемый тип выгрузки: {settings.StatisticsReportExportType}");
				}

				string message = "Операция успешно завершена." +
				                 $@"<a href=""/DataExport/DownloadExportResult?exportId={result.ReportId.Value}"">Скачать результат</a>";
				NotificationSender.SendNotification(processQueue, settings.StatisticsReportExportType.GetEnumDescription(), message);

				WorkerCommon.SetProgress(processQueue, 100);
				_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);
			}
			catch (Exception ex)
			{
				NotificationSender.SendNotification(processQueue, settings.StatisticsReportExportType.GetEnumDescription(), $"Операция была прервана: {ex.Message}");
				throw;
			}
		}
	}
}
