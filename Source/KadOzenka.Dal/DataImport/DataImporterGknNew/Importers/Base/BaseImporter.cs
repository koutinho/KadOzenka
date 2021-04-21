using System.Collections.Generic;
using System.Threading;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;
using ObjectModel.KO;
using Serilog;
using Core.Main.FileStorages;
using ObjectModel.Common;
using System;
using System.IO;
using KadOzenka.Dal.Logger;
using ObjectModel.Directory;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base
{
	public abstract class BaseImporter
	{
		private readonly ILogger _logger;
		private DataImporterGknLongProcessProgressLogger DataImporterGknLongProcessProgressLogger { get; }


		protected BaseImporter(DataImporterGknLongProcessProgressLogger dataImporterGknLongProcessProgressLogger, ILogger logger)
		{
			DataImporterGknLongProcessProgressLogger = dataImporterGknLongProcessProgressLogger;
			_logger = logger;
		}


		protected abstract void ImportGkn(DataImporterGkn dataImporterGkn, FileStream fileStream, string pathSchema,
			OMTask task, CancellationToken cancellationToken, object additionalParameters = null);


		public void Import(FileStream fileStream, OMTask task, OMImportDataLog dataLog,
			CancellationToken processCancellationToken, object additionalParameters = null)
		{
			_logger.Information("Начат импорт для задачи с Id {TaskId}", task.Id);

			var schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");

			var dataImporterGkn = new DataImporterGkn();
			try
			{
				DataImporterGknLongProcessProgressLogger.StartLogProgress(dataLog, dataImporterGkn);
				ImportGkn(dataImporterGkn, fileStream, schemaPath, task, processCancellationToken, additionalParameters);
				DataImporterGknLongProcessProgressLogger.StopLogProgress();

				if (!processCancellationToken.IsCancellationRequested && task.NoteType_Code != KoNoteType.Initial)
				{
					ExportTaskChanges(task);
				}
			}
			catch (Exception ex)
			{
				_logger.Information(ex, "Импорт завершен с ошибкой");
				DataImporterGknLongProcessProgressLogger.StopLogProgress();
				throw;
			}

			_logger.Information("Импорт завершен");
		}

		protected void ExportTaskChanges(OMTask task)
		{
			_logger.Information("Формирование протокола изменений по результатам загрузки для единицы оценки {TaskId}", task.Id);
			var path = TaskChangesDataComparingStorageManager.GetComparingDataRsmFileFullName(task);
			var unloadSettings = new KOUnloadSettings { TaskFilter = new List<long> { task.Id }, IsDataComparingUnload = true, FileName = path };
			DEKOChange.ExportUnitChangeToExcel(null, unloadSettings, null);
		}
	}
}
