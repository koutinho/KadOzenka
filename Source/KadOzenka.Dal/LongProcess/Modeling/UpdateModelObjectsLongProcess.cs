﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.LongProcess.Common;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Modeling;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Modeling
{
	public class UpdateModelObjectsLongProcess : LongProcess
	{
		private readonly ILogger _log = Log.ForContext<UpdateModelObjectsLongProcess>();
		private string MessageSubject => "Обновление объектов моделирования";
		public IModelingService ModelingService { get; set; }



		public UpdateModelObjectsLongProcess()
		{
			ModelingService = new ModelingService();
		}



		public static long AddToQueue(Stream file, string fileName, List<ColumnToAttributeMapping> columnsMapping)
		{
			var import = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
				DataFileTitle = DataImporterCommon.GetDataFileTitle(fileName),
				FileExtension = DataImporterCommon.GetFileExtension(fileName),
				ColumnsMapping = JsonConvert.SerializeObject(columnsMapping),
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
			//	ObjectId = import.Id
			//}, new CancellationToken());

			LongProcessManager.AddTaskToQueue(nameof(UpdateModelObjectsLongProcess), OMImportDataLog.GetRegisterId(), import.Id);

			return import.Id;
		}

		//TODO добавить прогресс-бар
		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.Debug("Старт фонового процесса {InputParameters}", processQueue.Parameters);

			if (processQueue.ObjectId.GetValueOrDefault() == 0)
			{
				WorkerCommon.SetMessage(processQueue, Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Common.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				NotificationSender.SendNotification(processQueue, MessageSubject, "Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов");
				return;
			}

			try
			{
				WorkerCommon.SetProgress(processQueue, 0);

				var import = OMImportDataLog.Where(x => x.Id == processQueue.ObjectId).SelectAll().ExecuteFirstOrDefault();
				if (import == null)
				{
					NotificationSender.SendNotification(processQueue, MessageSubject, "Процесс не выполнен, так как отсутствует исходный файл.");
					return;
				}

				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
				import.DateStarted = DateTime.Now;
				import.Save();

				ExcelFile excelFile;
				List<ColumnToAttributeMapping> columnsMapping;
				using (_log.TimeOperation("Подготовка данных для запуска обновления"))
				{
					var fileStream = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
					excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
					columnsMapping = JsonConvert.DeserializeObject<List<ColumnToAttributeMapping>>(import.ColumnsMapping);
				}

				Stream updatingResult;
				using (_log.TimeOperation("Обновление объектов"))
				{
					updatingResult = ModelingService.UpdateModelObjects(excelFile, columnsMapping);
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
				WorkerCommon.SetProgress(processQueue, 100);
			}
			catch (Exception e)
			{
				_log.Error(e, "Ошибка");
				NotificationSender.SendNotification(processQueue, MessageSubject,
					$"Операция завершена с ошибкой. Подробнее в журнале (ИД {ErrorManager.LogError(e)})");
			}

			_log.Debug("Финиш фонового процесса");
		}


		#region Support Methods

		private void SendSuccessfulMessage(OMQueue processQueue, long importId)
		{
			var message = $@"Операция успешно завершена
				<a href=""/DataImport/DownloadImportResultFile?importId={importId}"">Скачать результат</a>";

			NotificationSender.SendNotification(processQueue, MessageSubject, message);
		}

		#endregion
	}
}
