using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataExport;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess.CalculateSystem
{
	public class KoDownloadResultProcess: LongProcess
	{
		public const string LongProcessName = "KoDownloadResult";


		public static void AddImportToQueue(long tourId, KOUnloadSettings setting)
		{

			LongProcessManager.AddTaskToQueue(LongProcessName, OMTour.GetRegisterId(), tourId, setting.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			try
			{
				var data = processQueue.Parameters.DeserializeFromXml<KOUnloadSettings>();
				WorkerCommon.SetProgress(processQueue, 0);
				var res = KOUnloadResult.Unload(data);
				WorkerCommon.SetProgress(processQueue, 90);

				var correctRes = res.Where(x => !x.NoResult).ToList();

				string msg = GetMessage("Результат операции:", correctRes, true);


				if (data.SendResultToReon)
				{
					foreach (var item in correctRes)
					{
						new OMKoResultSendJournal
						{
							Guid = Guid.NewGuid().ToString(),
							TaskId = item.TaskId,
							CreateDate = DateTime.Now,
							ResultExportId = item.FileId

						}.Save();
					}
				}

				NotificationSender.SendNotification(processQueue, "Выгрузка результатов оценки", msg);
				WorkerCommon.SetProgress(processQueue, 100);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				int numberError = ErrorManager.LogError(e);
				WorkerCommon.SetMessage(processQueue, $"В результате выполнения возникли ошибки. Подробнее в Журнале ошибок. {numberError}");
				WorkerCommon.SetProgress(processQueue, 100);
				string msg = GetMessage($"Выгрузка была завершена с ошибкой {e.Message}. Подробности в журнале ошибок {numberError}.");

				NotificationSender.SendNotification(processQueue, "Выгрузка результатов оценки", msg);
				throw;
			}

		}

		public string GetMessage(string message, List<ResultKoUnloadSettings> result = null, bool withLink = false)
		{
			string msgResult = message;

			if (withLink && result != null)
			{
				msgResult += "<br>";
				msgResult += GetLinks(result);
				msgResult += "<br>";
				msgResult += GetFaultReportMessage(result.Where(x => x.FileId == 0).ToList());
			}

			return msgResult;
		}

		public string GetLinks(List<ResultKoUnloadSettings> result)
		{
			string msg = "";
			foreach (var item in result.Where(x => x.FileId != 0))
			{
				msg += $"<a href='/DataExport/DownloadKoExportResult?reportId={item.FileId}&isXml={item.IsXml}'>{item.FileName}</a>" + "<br>";
			}

			return msg;
		}

		public string GetFaultReportMessage(List<ResultKoUnloadSettings> faultResult)
		{
			string msg = "";
			foreach (var item in faultResult)
			{
				msg += $@"Не удалось создать отчет для ""{item.FileName}"" подробности в журнале ошибок." + "<br/>";
			}
			return msg;
		}

	}
}