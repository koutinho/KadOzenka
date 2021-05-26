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
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Logger;
using ObjectModel.Directory;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base
{
	public abstract class BaseImporter
	{
		private readonly ILogger _logger;
		private DataImporterGknLongProcessProgressLogger DataImporterGknLongProcessProgressLogger { get; }
		private GbuReportService GbuReportService { get; set; }
		public const int CadastralNumberColumnIndex = 0;
		public const int NotProcessedAttributesColumnIndex = 1;
		public const int TypeConvertingErrorColumnIndex = 2;
		public const int ErrorMessageColumnIndex = 3;


		protected BaseImporter(DataImporterGknLongProcessProgressLogger dataImporterGknLongProcessProgressLogger, ILogger logger)
		{
			DataImporterGknLongProcessProgressLogger = dataImporterGknLongProcessProgressLogger;
			_logger = logger;

			InitReport();
		}


		protected abstract void ImportGkn(DataImporterGkn dataImporterGkn, FileStream fileStream, string pathSchema,
			OMTask task, CancellationToken cancellationToken, object additionalParameters = null);


		public string Import(FileStream fileStream, OMTask task, OMImportDataLog dataLog,
			CancellationToken processCancellationToken, object additionalParameters = null)
		{
			_logger.Information("Начат импорт для задачи с Id {TaskId}", task.Id);

			var schemaPath = FileStorageManager.GetPathForStorage("SchemaPath");

			var dataImporterGkn = new DataImporterGkn(GbuReportService);
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
				_logger.Error(ex, "Импорт завершен с ошибкой");
				DataImporterGknLongProcessProgressLogger.StopLogProgress();
				throw;
			}

			_logger.Information("Импорт завершен");

			if (GbuReportService.IsReportEmpty) 
				return string.Empty;

			var reportId = GbuReportService.SaveReport();
			return GbuReportService.GetUrlToDownloadFile(reportId);
		}


		#region Support Methods

		private void ExportTaskChanges(OMTask task)
		{
			_logger.Information("Формирование протокола изменений по результатам загрузки для единицы оценки {TaskId}", task.Id);
			var path = TaskChangesDataComparingStorageManager.GetComparingDataRsmFileFullName(task);
			var unloadSettings = new KOUnloadSettings { TaskFilter = new List<long> { task.Id }, IsDataComparingUnload = true, FileName = path };
			DEKOChange.ExportUnitChangeToExcel(null, unloadSettings, null);
		}

		private void InitReport()
		{
			var headers = new List<GbuReportService.Column>
			{
				new()
				{
					Header = "Кадастровый номер",
					Index = CadastralNumberColumnIndex,
					Width = 4
				},
				new()
				{
					Header = "Несохраненные атрибуты (не соответствуют типу ОН)",
					Index = NotProcessedAttributesColumnIndex,
					Width = 8
				},
				new()
				{
					Header = "Несоответствия типа атрибута значению из файла",
					Index = TypeConvertingErrorColumnIndex,
					Width = 8
				},new()
				{
					Header = "Ошибка",
					Index = ErrorMessageColumnIndex,
					Width = 8
				}
			};

			GbuReportService = new GbuReportService("Ошибки в ходе загрузки");
			GbuReportService.AddHeaders(headers);
			GbuReportService.SetIndividualWidth(headers);
		}

		#endregion
	}
}
