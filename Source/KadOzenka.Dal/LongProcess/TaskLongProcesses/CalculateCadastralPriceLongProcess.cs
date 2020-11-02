using System;
using System.Collections.Generic;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public class CalculateCadastralPriceLongProcess : LongProcess
	{
		public const string LongProcessName = "CalculateCadastralPrice";
		private static readonly int KnColumn = 0;
		private static readonly int ErrorColumn = 1;

		public static long AddProcessToQueue(KOCalcSettings settings)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, null, null, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			try
			{
				WorkerCommon.SetProgress(processQueue, 0);

				var settings = processQueue.Parameters.DeserializeFromXml<KOCalcSettings>();
				var reportId = PerformProc(settings);

				WorkerCommon.SetProgress(processQueue, 100);
				string message = "Операция успешно завершена." +
				                 $@"<a href=""/DataExport/DownloadExportResult?exportId={reportId}"">Скачать результат</a>";
				NotificationSender.SendNotification(processQueue, "Результат Операции Расчета кадастровой стоимости", message);
			}
			catch (Exception ex)
			{
				var errorId = ErrorManager.LogError(ex);
				NotificationSender.SendNotification(processQueue, "Результат Операции Расчета кадастровой стоимости", $"Операция была прервана: {ex.Message} (Подробнее в журнале: {errorId})");
				throw;
			}
		}

		private long PerformProc(KOCalcSettings settings)
		{
			var reportService = new GbuReportService();
			reportService.AddHeaders(new List<string> {"КН", "Ошибка"});

			var result = OMGroup.CalculateSelectGroup(settings);

			foreach (var errorItem in result)
			{
				var row = reportService.GetCurrentRow();
				reportService.AddValue(errorItem.CadastralNumber, KnColumn, row);
				reportService.AddValue(errorItem.Error, ErrorColumn, row);
			}

			reportService.SetStyle();
			reportService.SetIndividualWidth(KnColumn, 4);
			reportService.SetIndividualWidth(ErrorColumn, 5);
			var reportId = reportService.SaveReport("Отчет по итогам расчета кадастровой стоимости");

			return reportId;
		}
	}
}
