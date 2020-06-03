using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.ES;

namespace KadOzenka.Dal.LongProcess.ExpressScore
{
	public class EsUnloadReferenceFromExcel: LongProcess
	{
		public const string LongProcessName = "EsUnloadReferenceFromExcel";

		public static void AddProcessToQueue(Stream file, ImportFileFromExcelDto settings)
		{
			var fileSavedName = $"{settings.FileInfo.FileName} ({DateTime.Now.GetString().Replace(":", "_")})";

			long fileId = ExpressScoreReferenceService.SaveFileToStorage(file, fileSavedName);

			LongProcessManager.AddTaskToQueue(LongProcessName, OMEsReference.GetRegisterId(), fileId, settings.SerializeToXml());
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

				var settings = processQueue.Parameters.DeserializeFromXml<ImportFileFromExcelDto>();

				var file = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated,
					import.Id.ToString());


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

				refService.CreateOrUpdateReferenceThroughLongProcess(file, settings, import.DataFileName, import.Id);
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
	}
}