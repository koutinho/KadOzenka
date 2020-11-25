using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Common;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
	public class ModelDictionaryImportFromExcelLongProcess : LongProcess
	{
		private DictionaryService DictionaryService { get; }
		public const string LongProcessName = nameof(ModelDictionaryImportFromExcelLongProcess);
		private readonly ILogger _log = Log.ForContext<ModelDictionaryImportFromExcelLongProcess>();

		public ModelDictionaryImportFromExcelLongProcess()
		{
			DictionaryService = new DictionaryService();
		}


		public static void AddProcessToQueue(Stream file, DictionaryImportFileFromExcelDto settings, OMImportDataLog import)
		{
			LongProcessManager.AddTaskToQueue(LongProcessName, OMModelingDictionary.GetRegisterId(), import.Id, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.Information("Старт фонового процесса для Импорта словаря Моделирования {InputParameters}", processQueue.Parameters);

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

				var settings = processQueue.Parameters.DeserializeFromXml<DictionaryImportFileFromExcelDto>();

				var task = Task.Run(() =>
				{
					while (true)
					{
						if (cancelToken.IsCancellationRequested)
						{
							break;
						}
						if (DictionaryService.RowsCount > 0 && DictionaryService.CurrentRow > 0)
						{
							var newProgress = (long)Math.Round(((double)DictionaryService.CurrentRow / DictionaryService.RowsCount) * 100);
							if (newProgress != processQueue.Progress)
							{
								WorkerCommon.SetProgress(processQueue, newProgress);
							}
						}
					}
				}, cancelToken);

				var fileStream = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated,
					import.DataFileName);

				_log.ForContext("IsNewDictionary", settings.IsNewDictionary).Verbose("Создание или обновление словаря.");
				if (settings.IsNewDictionary)
				{
					DictionaryService.CreateDictionaryFromExcel(fileStream, settings.FileInfo,
						settings.NewDictionaryName, import);
				}
				else
				{
					DictionaryService.UpdateDictionaryFromExcel(fileStream, settings.FileInfo, settings.DictionaryId,
						settings.DeleteOldValues, import);
				}

				cancelSource.Cancel();
				task.Wait(cancellationToken);
				cancelSource.Dispose();

				WorkerCommon.SetProgress(processQueue, 100);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			_log.Information("Окончание фонового процесса для Импорта словаря Моделирования");
		}

		public override void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			var import = OMImportDataLog.Where(x => x.Id == objectId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (import == null)
				return;

			import.Status_Code = ImportStatus.Faulted;
			import.DateFinished = DateTime.Now;
			import.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : string.Empty)}";
			import.Save();
		}
	}
}