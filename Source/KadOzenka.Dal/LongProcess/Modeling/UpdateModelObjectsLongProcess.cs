using System;
using System.IO;
using System.Threading;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Modeling.Entities;
using KadOzenka.Dal.Modeling.Objects.Entities;
using KadOzenka.Dal.Modeling.Objects.Import;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.Modeling;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Modeling
{
	public class UpdateModelObjectsLongProcess : LongProcess
	{
		private readonly ILogger _log = Log.ForContext<UpdateModelObjectsLongProcess>();
		private string MessageSubject => "Загрузка файла с объектами моделирования";
		private ModelObjectsImporter ModelObjectsImporter { get; }



		public UpdateModelObjectsLongProcess()
		{
			ModelObjectsImporter = new ModelObjectsImporter();
		}



		public static long AddToQueue(Stream file, string fileName, ModelObjectsConstructor modelObjectsConstructor)
		{
			var import = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
				DataFileTitle = DataImporterCommon.GetDataFileTitle(fileName),
				FileExtension = DataImporterCommon.GetFileExtension(fileName),
				ColumnsMapping = JsonConvert.SerializeObject(modelObjectsConstructor.ColumnsMapping),
				MainRegisterId = OMModelToMarketObjects.GetRegisterId(),
				RegisterViewId = "KoModels"
			};
			import.Save();

			import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
			FileStorageManager.Save(file, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			import.Save();

			////TODO код для отладки
			//new UpdateModelObjectsLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	ObjectId = import.Id,
			//	Parameters = modelObjectsConstructor.SerializeToXml()
			//}, new CancellationToken());

			LongProcessManager.AddTaskToQueue(nameof(UpdateModelObjectsLongProcess), OMImportDataLog.GetRegisterId(),
				import.Id, modelObjectsConstructor.SerializeToXml());

			return import.Id;
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.Debug("Старт фонового процесса {InputParameters}", processQueue.Parameters);

			try
			{
				var import = GetImport(processQueue);
				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
				import.DateStarted = DateTime.Now;
				import.Save();

				var constructor = processQueue.Parameters.DeserializeFromXml<ModelObjectsConstructor>();
				var fileStream = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
				var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);

				LongProcessProgressLogger.StartLogProgress(processQueue, () => ModelObjectsImporter.MaxRowsCount, () => ModelObjectsImporter.CurrentRowCount);
				Stream updatingResult;
				using (_log.TimeOperation("Обновление объектов"))
				{
					updatingResult = ModelObjectsImporter.ChangeObjects(excelFile, constructor);
				}

				using (_log.TimeOperation("Сохранение файла с результатом обновления"))
				{
					import.DateFinished = DateTime.Now;
					import.ResultFileTitle = DataImporterCommon.GetFileResultTitleFromDataTitle(import);
					import.ResultFileName = DataImporterCommon.GetStorageResultFileName(import.Id);
					FileStorageManager.Save(updatingResult, DataImporterCommon.FileStorageName, import.DateFinished.Value, import.ResultFileName);
				}

				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
				import.Save();

				SendSuccessfulMessage(processQueue, import.Id);
			}
			catch (Exception e)
			{
				_log.Error(e, "Ошибка");
				NotificationSender.SendNotification(processQueue, MessageSubject,
					$"Операция завершена с ошибкой: {e.Message}. Подробнее в журнале (ИД {ErrorManager.LogError(e)})");
			}

			LongProcessProgressLogger.StopLogProgress();
			WorkerCommon.SetProgress(processQueue, 100);
			_log.Debug("Финиш фонового процесса");
		}


		#region Support Methods

		private OMImportDataLog GetImport(OMQueue processQueue)
		{
			if (processQueue.ObjectId.GetValueOrDefault() == 0)
				throw new Exception("Операция завершена с ошибкой, т.к. нет данных о расположении загруженного файла.");

			if (string.IsNullOrWhiteSpace(processQueue.Parameters))
				throw new Exception("Операция завершена с ошибкой, т.к. нет входных параметров.");

			var import = OMImportDataLog.Where(x => x.Id == processQueue.ObjectId).SelectAll().ExecuteFirstOrDefault();
			if (import == null)
			{
				throw new Exception("Процесс не выполнен, так как отсутствует исходный файл.");
			}

			return import;
		}

		private void SendSuccessfulMessage(OMQueue processQueue, long importId)
		{
			var message = $@"Операция успешно завершена
				<a href=""/DataImport/DownloadImportResultFile?importId={importId}"">Скачать результат</a>";

			NotificationSender.SendNotification(processQueue, MessageSubject, message);
		}

		#endregion
	}
}
