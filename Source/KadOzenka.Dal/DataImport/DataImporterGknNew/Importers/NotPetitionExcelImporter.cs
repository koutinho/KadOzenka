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
	/// <summary>
	/// Класс для импорта .xml документа ЗнО любого типа кроме "Обращений"
	/// </summary>
	public class NotPetitionExcelImporter : BaseImporter, IDataImporterGkn
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<NotPetitionExcelImporter>();
		private DataImporterGknLongProcessProgressLogger DataImporterGknLongProcessProgressLogger { get; }

		public NotPetitionExcelImporter(DataImporterGknLongProcessProgressLogger dataImporterGknLongProcessProgressLogger)
		{
			DataImporterGknLongProcessProgressLogger = dataImporterGknLongProcessProgressLogger;
		}


		public void Import(FileStream fileStream, OMTask task, OMImportDataLog dataLog, CancellationToken processCancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
