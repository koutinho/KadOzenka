using System.IO;
using System.Threading;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base;
using KadOzenka.Dal.Logger;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Importers
{
	public class PetitionImporter : BaseImporter
	{
		public PetitionImporter(DataImporterGknLongProcessProgressLogger dataImporterGknLongProcessProgressLogger)
		: base(dataImporterGknLongProcessProgressLogger, Serilog.Log.ForContext<PetitionImporter>())
		{
		}


		protected override void ImportGkn(DataImporterGkn dataImporterGkn, FileStream fileStream, string pathSchema, OMTask task,
			CancellationToken cancellationToken, object columnsMappingObj = null)
		{
			var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
			dataImporterGkn.ImportGknPetitionFromExcel(excelFile, pathSchema, task, cancellationToken);
		}
	}
}
