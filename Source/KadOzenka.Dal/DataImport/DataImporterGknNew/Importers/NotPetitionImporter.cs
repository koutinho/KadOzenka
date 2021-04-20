using Core.Main.FileStorages;
using ObjectModel.Common;
using System;
using System.IO;
using System.Threading;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base;
using KadOzenka.Dal.Logger;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Importers
{
	public class NotPetitionImporter : BaseImporter, IDataImporterGkn
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<NotPetitionImporter>();
		private DataImporterGknLongProcessProgressLogger DataImporterGknLongProcessProgressLogger { get; }

		public NotPetitionImporter(DataImporterGknLongProcessProgressLogger dataImporterGknLongProcessProgressLogger)
		{
			DataImporterGknLongProcessProgressLogger = dataImporterGknLongProcessProgressLogger;
		}


		public void Import(FileStream fileStream, OMTask task, OMImportDataLog dataLog, CancellationToken processCancellationToken)
		{
			Log.Information("Начат импорт из xml для задачи с Id {TaskId}", task.Id);

			var schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");
			try
			{
				var dataImporterGkn = new DataImporterGkn();
				DataImporterGknLongProcessProgressLogger.StartLogProgress(dataLog, dataImporterGkn);
				dataImporterGkn.ImportDataGknFromXml(fileStream, schemaPath, task, processCancellationToken);
				DataImporterGknLongProcessProgressLogger.StopLogProgress();

				if (!processCancellationToken.IsCancellationRequested && task.NoteType_Code != KoNoteType.Initial)
				{
					ExportTaskChanges(task);
				}
			}
			catch (Exception ex)
			{
				Log.Information(ex, "Импорт из xml завершен с ошибкой");
				DataImporterGknLongProcessProgressLogger.StopLogProgress();
				throw;
			}

			Log.Information("Импорт из xml завершен");
		}
	}
}
