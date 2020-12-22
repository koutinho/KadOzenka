using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Tasks;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public class CalculateCadastralPriceLongProcess : LongProcess
	{
		public const string LongProcessName = "CalculateCadastralPrice";
		private static readonly int GroupColumn = 0;
		private static readonly int TaskColumn = 1;
		private static readonly int PropertyTypeColumn = 2;
		private static readonly int KnColumn = 3;
		private static readonly int ErrorColumn = 4;

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
			var result = OMGroup.CalculateSelectGroup(settings);
			var reportId = FormReport(result);

			var taskIds = settings.TaskFilter;
			if (taskIds.Count > 0)
			{
				var tasks = OMTask.Where(x => taskIds.Contains(x.Id)).SelectAll().Execute();
				foreach (var task in tasks)
				{
					var path = CadastralCostDataComparingStorageManager.GetTaskRsmFolderFullPath(task);
					var unloadSettings = new KOUnloadSettings { TaskFilter = new List<long>{ task.Id }, IsDataComparingUnload = true, DirectoryName = path };
					DEKOUnit.ExportToXml(null, unloadSettings, null);
				}
			}
			

			return reportId;
		}

		private static long FormReport(List<CalcErrorItem> result)
		{
			using var reportService = new GbuReportService("Отчет по итогам расчета кадастровой стоимости");
			reportService.AddHeaders(new List<string> { "Оценочная группа", "Задание на оценку", "Тип объекта", "КН", "Ошибка"});
			reportService.SetIndividualWidth(GroupColumn, 3);
			reportService.SetIndividualWidth(TaskColumn, 4);
			reportService.SetIndividualWidth(PropertyTypeColumn, 5);
			reportService.SetIndividualWidth(KnColumn, 4);
			reportService.SetIndividualWidth(ErrorColumn, 5);

			var groupData = new Dictionary<long, string>();
			var taskData = new Dictionary<long, string>();
			var groupIds = result.Where(x => x.GroupId.HasValue).Select(x => x.GroupId.Value).ToList();
			if (!groupIds.IsEmpty())
			{
				groupData = OMGroup.Where(x => groupIds.Contains(x.Id)).Select(x => x.Number).Execute()
					.ToDictionary(x => x.Id, x => x.Number);
			}
			var taskIds = result.Where(x => x.TaskId.HasValue).Select(x => x.TaskId.Value).ToList();
			if (!taskIds.IsEmpty())
			{
				taskData = new TaskService().GetTemplatesForTaskName(taskIds);
			}
			
			foreach (var errorItem in result)
			{
				var row = reportService.GetCurrentRow();
				if (groupData.ContainsKey(errorItem.GroupId.GetValueOrDefault()))
					reportService.AddValue(groupData[errorItem.GroupId.GetValueOrDefault()], GroupColumn, row);

				if (taskData.ContainsKey(errorItem.TaskId.GetValueOrDefault()))
					reportService.AddValue(taskData[errorItem.TaskId.GetValueOrDefault()], TaskColumn, row);

				reportService.AddValue(errorItem.PropertyType, PropertyTypeColumn, row);
				reportService.AddValue(errorItem.CadastralNumber, KnColumn, row);
				reportService.AddValue(errorItem.Error, ErrorColumn, row);
			}

			var reportId = reportService.SaveReport();
			return reportId;
		}
	}
}
