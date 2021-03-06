using System;
using System.IO;
using System.Threading;
using CommonSdks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.CommonFunctions.Repositories;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.Tours.Dto;
using KadOzenka.Dal.Tours;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Common;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
	public class GroupingDictionaryImportFromExcelLongProcess : LongProcess
	{
		private readonly ILogger _log = Log.ForContext<GroupingDictionaryImportFromExcelLongProcess>();
		private string MessageSubject => "Загрузка справочника для моделирования";
		private IGroupingDictionaryService DictionaryService { get; }
		private IImportDataLogRepository ImportDataLogRepository { get; }
		private IWorkerCommonWrapper Worker { get; }
		private IFileStorageManagerWrapper FileStorageManagerWrapper { get; }


		/// <summary>
		/// Конструктор для юнит-тестов
		/// </summary>
		public GroupingDictionaryImportFromExcelLongProcess(IGroupingDictionaryService dictionaryService,
			IImportDataLogRepository importDataLogRepository, INotificationSender notificationSender, 
			IWorkerCommonWrapper worker, IFileStorageManagerWrapper fileStorageManagerWrapper,
			ILongProcessProgressLogger logger)
			: base(notificationSender, logger)
		{
			DictionaryService = dictionaryService;
			ImportDataLogRepository = importDataLogRepository;
			Worker = worker;
			FileStorageManagerWrapper = fileStorageManagerWrapper;
		}

		public GroupingDictionaryImportFromExcelLongProcess()
		{
			DictionaryService = new GroupingDictionaryService();
			ImportDataLogRepository = new ImportDataLogRepository();
			Worker = new WorkerCommonWrapper();
			FileStorageManagerWrapper = new FileStorageManagerWrapper();
		}


		public static void AddProcessToQueue(Stream file, GroupingDictionaryImportFileFromExcelDto settings, OMImportDataLog import)
		{
			LongProcessManager.AddTaskToQueue(nameof(ModelDictionaryImportFromExcelLongProcess), OMModelingDictionary.GetRegisterId(), import.Id, settings.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.Information("Старт фонового процесса для Импорта словаря Моделирования {InputParameters}", processQueue.Parameters);

			try
			{
				Worker.SetProgress(processQueue, 0);

				var import = ImportDataLogRepository.GetById(processQueue.ObjectId.GetValueOrDefault(), null);
				if (import == null)
				{
					NotificationSender.SendNotification(processQueue, MessageSubject, "Процесс не выполнен, так как отсутствует исходный файл.");
					return;
				}

				var settings = processQueue.Parameters.DeserializeFromXml<GroupingDictionaryImportFileFromExcelDto>();
				LongProcessProgressLogger.StartLogProgress(processQueue, () => DictionaryService.RowsCount, () => DictionaryService.CurrentRow);

				var fileStream = FileStorageManagerWrapper.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated,
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
			}
			catch (Exception e)
			{
				var errorId = ErrorManager.LogError(e);
				_log.Error(e, "Загрузка справочников моделирования завершена с ошибкой {ErrorId}", errorId);
				NotificationSender.SendNotification(processQueue, MessageSubject, $"Операция завершена с ошибкой. Подробнее в журнале (ИД {errorId})");
			}

			LongProcessProgressLogger.StopLogProgress();
			Worker.SetProgress(processQueue, 100);
			_log.Information("Окончание фонового процесса для Импорта словаря Моделирования");
		}

		public override void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			base.LogError(objectId, ex, errorId);
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