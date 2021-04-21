using Core.Main.FileStorages;
using ObjectModel.Common;
using System;
using System.IO;
using System.Threading;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base;
using KadOzenka.Dal.Logger;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Importers
{
	public class PetitionImporter : BaseImporter, IDataImporterGkn
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<PetitionImporter>();
		private DataImporterGknLongProcessProgressLogger DataImporterGknLongProcessProgressLogger { get; }

		public PetitionImporter(DataImporterGknLongProcessProgressLogger dataImporterGknLongProcessProgressLogger)
		{
			DataImporterGknLongProcessProgressLogger = dataImporterGknLongProcessProgressLogger;
		}


		public void Import(FileStream fileStream, OMTask task, OMImportDataLog dataLog, CancellationToken processCancellationToken)
		{
			Log.Information("Начат импорт из xlsx для задачи с Id {TaskId}", task.Id);

			var schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");

			var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
			var dataImporterGkn = new DataImporterGkn();
			try
			{
				DataImporterGknLongProcessProgressLogger.StartLogProgress(dataLog, dataImporterGkn);
				dataImporterGkn.ImportGknPetitionFromExcel(excelFile, schemaPath, task, processCancellationToken);
				DataImporterGknLongProcessProgressLogger.StopLogProgress();

				if (!processCancellationToken.IsCancellationRequested && task.NoteType_Code != KoNoteType.Initial)
				{
					ExportTaskChanges(task);
				}
			}
			catch (Exception ex)
			{
				Log.Information(ex, "Импорт из xlsx завершен с ошибкой");
				DataImporterGknLongProcessProgressLogger.StopLogProgress();
				throw;
			}

			Log.Information("Импорт из xlsx завершен");
		}
	}
}
