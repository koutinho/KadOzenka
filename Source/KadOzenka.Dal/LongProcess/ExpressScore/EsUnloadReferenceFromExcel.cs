using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Common;
using ObjectModel.ES;

namespace KadOzenka.Dal.LongProcess.ExpressScore
{
	public class EsUnloadReferenceFromExcel: LongProcess
	{
		public const string LongProcessName = "EsUnloadReferenceFromExcel";

		public static void AddProcessToQueue(Stream file, ImportFileFromExcelDto settings)
		{
			var import = ExpressScoreReferenceService.CreateDataFileImport(file, settings.FileInfo);
			LongProcessManager.AddTaskToQueue(LongProcessName, OMEsReference.GetRegisterId(), import.Id, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			try
			{
				var cancelSource = new CancellationTokenSource();
				var cancelToken = cancelSource.Token;


				WorkerCommon.SetProgress(processQueue, 0);

				var import = OMImportDataLog.Where(x => x.Id == processQueue.ObjectId).SelectAll().ExecuteFirstOrDefault();

				if (import == null)
				{
					WorkerCommon.SetMessage(processQueue, "Процесс не выполнен, так как отсутствует исходный файл.");
					return;
				}

				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
				import.DateStarted = DateTime.Now;
				import.Save();

				var settings = processQueue.Parameters.DeserializeFromXml<ImportFileFromExcelDto>();

				var refService = new ExpressScoreReferenceService();
				var t = Task.Run(() => {
					while (true)
					{
						if (cancelToken.IsCancellationRequested)
						{
							break;
						}
						if (refService.AllRows > 0 && refService.CurrentRow > 0)
						{
							var newProgress = (long)Math.Round(((double)refService.CurrentRow / refService.AllRows) * 100);
							if (newProgress != processQueue.Progress)
							{
								WorkerCommon.SetProgress(processQueue, newProgress);
							}
						}
					}
				}, cancelToken);

				refService.CreateOrUpdateReferenceThroughLongProcess(import, settings);

				import.Status_Code = ImportStatus.Completed;
				import.Save();

				cancelSource.Cancel();
				t.Wait(cancellationToken);
				cancelSource.Dispose();

				WorkerCommon.SetProgress(processQueue, 100);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
		}

		public override void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == objectId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (import == null)
			{
				return;
			}

			import.Status_Code = ImportStatus.Faulted;
			import.DateFinished = DateTime.Now;
			import.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : string.Empty)}";
			import.Save();
		}
	}
}