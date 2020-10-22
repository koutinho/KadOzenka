using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.SRD;
using Ionic.Zip;
using KadOzenka.Dal.DataExport;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;
using Serilog;
using Serilog.Context;

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
			LogContext.PushProperty("QueueId", processQueue.Id);
			_log.Information("Старт фонового процесса: {ProcessType}", processType.Description);

			if (!processQueue.ObjectId.HasValue)
			{
				WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				return;
			}
			
			OMUnloadResultQueue unloadResultQueue = OMUnloadResultQueue
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

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
				var settings = processQueue.Parameters.DeserializeFromXml<KOUnloadSettings>();
				_log.Information("{ProcessType}. Настройки: {Settings}", processType.Description, JsonConvert.SerializeObject(settings));

				WorkerCommon.SetProgress(processQueue, 10);
				var res = KOUnloadResult.Unload(processQueue, unloadResultQueue, settings);

				_log.ForContext("SendResultToReon", settings.SendResultToReon)
					.Debug("{ProcessType}: Найдено объектов {ResCount}.", processType.Description, res.Count);

				var correctRes = res.Where(x => !x.NoResult).ToList();
				string msg = GetMessage("Результат операции:", correctRes, true);

				_log.ForContext("SendResultToReon", settings.SendResultToReon)
					.Information("{ProcessType}: Объектов с результатом {CorrectResCount} из {ResCount}.", processType.Description, correctRes.Count, res.Count);

			 
				if (settings.SendResultToReon)
				{
					_log.ForContext("SendResultToReon", settings.SendResultToReon)
						.Information("{ProcessType}: Старт загрузки результатов в РЕОН. По {CorrectResCount} объектам.", processType.Description, correctRes.Count);

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
					var filesCount = correctRes.Where(x => x.FileId != 0).ToList()?.Count;
					_log.ForContext("SendResultToReon", settings.SendResultToReon).ForContext("CorrectResCount", correctRes.Count)
						.Information("{ProcessType}: Загрузка результатов в РЕОН завершена по {FilesIdCount} объектам.", processType.Description, filesCount);

				}
				WorkerCommon.SetProgress(processQueue, 90);

				unloadResultQueue.DateFinished = DateTime.Now;
				unloadResultQueue.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
				unloadResultQueue.Save();

				NotificationSender.SendNotification(processQueue, "Выгрузка результатов оценки", msg);
				
				var stateDescription = "Выгрузка результатов оценки успешно завершена";
				WorkerCommon.SetProgress(processQueue, 100);
				WorkerCommon.SetCompleteState(processQueue, stateDescription, stateDescription);

				_log.ForContext("ResCount", res.Count)
					.ForContext("CorrectResCount", correctRes.Count)
					.Information("Завершение фонового процесса: {ProcessType}", processType.Description);
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
				msgResult += GetFinalZipLink(result.Where(x => x.FileId != 0).ToList());
				msgResult += "<br>";
				msgResult += GetFaultReportMessage(result.Where(x => x.FileId == 0).ToList());
			}

			return msgResult;
		}

		public string GetFinalZipLink(List<ResultKoUnloadSettings> result)
		{
			long fileId;
			using (ZipFile zipFile = new ZipFile())
			{
				zipFile.AlternateEncoding = Encoding.UTF8;
				zipFile.AlternateEncodingUsage = ZipOption.AsNecessary;

				foreach (var entry in result)
				{
					var entryStream = DataExporterCommon.GetExportResultFileStream(entry.FileId);
					zipFile.AddEntry(DataExporterCommon.GetDownloadResultFileName(entry.FileId), entryStream);
				}

				MemoryStream stream = new MemoryStream();
				zipFile.Save(stream);
				stream.Seek(0, SeekOrigin.Begin);
				var fileName = "Результаты оценки";
				fileId = SaveReportDownload.SaveReport(fileName, stream, OMUnit.GetRegisterId(), reportExtension: "zip");
			}

			return $"<a href='/DataExport/DownloadExportResult?exportId={fileId}'>Результаты оценки</a>" + "<br>";
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