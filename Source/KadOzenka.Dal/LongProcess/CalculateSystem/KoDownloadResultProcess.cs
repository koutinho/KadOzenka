using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.DataExport;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess.CalculateSystem
{
	public class KoDownloadResultProcess: LongProcess
	{
		public const string LongProcessName = "KoDownloadResult";
		private readonly ILogger _log = Log.ForContext<KoDownloadResultProcess>();

		public static void AddImportToQueue(KOUnloadSettings setting)
		{
			var koUnloadResults = KOUnloadResult.GetKoUnloadResultTypes(setting);
			var unloadResultQueue = new OMUnloadResultQueue
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
				UnloadTypesMapping = JsonConvert.SerializeObject(koUnloadResults),
				UnloadCurrentCount = 0,
				UnloadTotalCount = koUnloadResults.Count
			};
			unloadResultQueue.Save();

			LongProcessManager.AddTaskToQueue(LongProcessName, OMTour.GetRegisterId(), unloadResultQueue.Id, setting.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			if (!processQueue.ObjectId.HasValue)
			{
				WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				return;
			}
			_log.Debug("StartProcess {processType} {QueueId}", processType.Description, processQueue.Id);
			OMUnloadResultQueue unloadResultQueue = OMUnloadResultQueue
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();
			_log.Debug("StartProcess {processType} {QueueId} {ResultQueueId}", processType.Description, processQueue.Id, unloadResultQueue.Id);
			if (unloadResultQueue == null)
			{
				WorkerCommon.SetMessage(processQueue, Consts.Consts.GetMessageForProcessInterruptedBecauseOfNoUnloadResultQueue(processQueue.ObjectId.Value));
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoUnloadResultQueue);
				return;
			}

			try
			{
				WorkerCommon.SetProgress(processQueue, 0);
				unloadResultQueue.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
				unloadResultQueue.DateStarted = DateTime.Now;
				unloadResultQueue.Save();
				WorkerCommon.SetProgress(processQueue, 5);
				_log.Debug("StartProcess {processType} {QueueId} {ResultQueueId}", processType.Description, processQueue.Id, unloadResultQueue.Id);
				var settings = processQueue.Parameters.DeserializeFromXml<KOUnloadSettings>();
			
				WorkerCommon.SetProgress(processQueue, 10);
				_log.Debug("StartProcess {processType} {QueueId} {UnloadXML1} {UnloadXML2}", processType.Description, processQueue.Id, settings.UnloadXML1, settings.UnloadXML2);
				var res = KOUnloadResult.Unload(processQueue, unloadResultQueue, settings);
				_log.Debug("StartProcess {processType} {QueueId} {ResCount} {FileName}", processType.Description, processQueue.Id, res.Count, res[0].FileName);
				WorkerCommon.SetProgress(processQueue, 50);

				var correctRes = res.Where(x => !x.NoResult).ToList();
				string msg = GetMessage("Результат операции:", correctRes, true);
				WorkerCommon.SetProgress(processQueue, 60);
				_log.Debug("StartProcess {processType} {QueueId} {CorrectRes.Count} {FileName} {SendResultToReon}", processType.Description, processQueue.Id, correctRes.Count, correctRes[0].TaskId, settings.SendResultToReon);

			 
				if (settings.SendResultToReon)
				{
					foreach (var item in correctRes.Where(x => x.FileId != 0).ToList())
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
				_log.Debug("StartProcess {processType} {QueueId} {CorrectRes.Count} {FileName}", processType.Description, processQueue.Id, correctRes.Count, correctRes[0].TaskId);
				WorkerCommon.SetProgress(processQueue, 90);

				unloadResultQueue.DateFinished = DateTime.Now;
				unloadResultQueue.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
				unloadResultQueue.Save();

				NotificationSender.SendNotification(processQueue, "Выгрузка результатов оценки", msg);
				WorkerCommon.SetProgress(processQueue, 100);
			}
			catch (Exception e)
			{
				int numberError = ErrorManager.LogError(e);
				WorkerCommon.SetMessage(processQueue, $"В результате выполнения возникли ошибки. Подробнее в Журнале ошибок. {numberError}");
				WorkerCommon.SetProgress(processQueue, 100);
				string msg = GetMessage($"Выгрузка была завершена с ошибкой {e.Message}. Подробности в журнале ошибок {numberError}.");

				NotificationSender.SendNotification(processQueue, "Выгрузка результатов оценки", msg);

				unloadResultQueue.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
				unloadResultQueue.DateFinished = DateTime.Now;
				unloadResultQueue.ErrorMessage = $"{e.Message} (журнал № {numberError})";
				unloadResultQueue.Save();

				throw;
			}
		}

		public string GetMessage(string message, List<ResultKoUnloadSettings> result = null, bool withLink = false)
		{
			string msgResult = message;

			if (withLink && result != null)
			{
				msgResult += "<br>";
				msgResult += GetLinks(result.Where(x => x.FileId != 0).ToList());
				msgResult += "<br>";
				msgResult += GetFaultReportMessage(result.Where(x => x.FileId == 0).ToList());
			}

			return msgResult;
		}

		public string GetLinks(List<ResultKoUnloadSettings> result)
		{
			string msg = "";
			foreach (var item in result)
			{
				msg += $"<a href='/DataExport/DownloadExportResult?exportId={item.FileId}'>{item.FileName}</a>" + "<br>";
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