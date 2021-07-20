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
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Dictionaries.Entities;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Common;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
	public class ModelDictionaryImportFromExcelLongProcess : LongProcess
	{
		private readonly ILogger _log = Log.ForContext<ModelDictionaryImportFromExcelLongProcess>();
		private string MessageSubject => "Загрузка справочника для моделирования";
		private IModelDictionaryService ModelDictionaryService { get; }
		private IImportDataLogRepository ImportDataLogRepository { get; }
		private IWorkerCommonWrapper Worker { get; }
		private IFileStorageManagerWrapper FileStorageManagerWrapper { get; }


		/// <summary>
		/// Конструктор для юнит-тестов
		/// </summary>
		public ModelDictionaryImportFromExcelLongProcess(IModelDictionaryService modelDictionaryService,
			IImportDataLogRepository importDataLogRepository, INotificationSender notificationSender, 
			IWorkerCommonWrapper worker, IFileStorageManagerWrapper fileStorageManagerWrapper,
			ILongProcessProgressLogger logger)
			: base(notificationSender, logger)
		{
			ModelDictionaryService = modelDictionaryService;
			ImportDataLogRepository = importDataLogRepository;
			Worker = worker;
			FileStorageManagerWrapper = fileStorageManagerWrapper;
		}

		public ModelDictionaryImportFromExcelLongProcess()
		{
			ModelDictionaryService = new ModelDictionaryService();
			ImportDataLogRepository = new ImportDataLogRepository();
			Worker = new WorkerCommonWrapper();
			FileStorageManagerWrapper = new FileStorageManagerWrapper();
		}


		public static void AddProcessToQueue(DictionaryImportFileFromExcelDto settings, OMImportDataLog import)
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

				var settings = processQueue.Parameters.DeserializeFromXml<DictionaryImportFileFromExcelDto>();
				LongProcessProgressLogger.StartLogProgress(processQueue, () => ModelDictionaryService.RowsCount, () => ModelDictionaryService.CurrentRow);

				ModelDictionaryService.UpdateDictionaryFromExcel(import, settings.FileInfo, settings.DictionaryId,
					settings.DeleteOldValues);
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