using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base;
using KadOzenka.Dal.Logger;
using ObjectModel.KO;
using Serilog;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport.Validation;
using Newtonsoft.Json;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Importers
{
	/// <summary>
	/// Класс для импорта .xml документа ЗнО любого типа кроме "Обращений"
	/// </summary>
	public class NotPetitionExcelImporter : BaseImporter
	{
		public NotPetitionExcelImporter(DataImporterGknLongProcessProgressLogger dataImporterGknLongProcessProgressLogger)
		: base(dataImporterGknLongProcessProgressLogger, Log.ForContext<NotPetitionExcelImporter>())
		{
		}


		protected override void ImportGkn(DataImporterGkn dataImporterGkn, FileStream fileStream, string pathSchema, OMTask task,
			CancellationToken cancellationToken, object columnsMappingObj = null)
		{
			var columnsMappingStr = columnsMappingObj?.ToString();
			if (string.IsNullOrWhiteSpace(columnsMappingStr))
				throw new Exception("Не передано соответствие колонок Excel и заполняемых атрибутов");

			var columnsMapping = JsonConvert.DeserializeObject<List<DataExportColumn>>(columnsMappingStr);
			DataImporterGknValidator.ValidateExcelColumnsForNotPetition(columnsMapping.Select(x => x.AttributrId));

			//var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
			//dataImporterGkn.ImportGknPetitionFromExcel(excelFile, pathSchema, task, cancellationToken);
			throw new NotImplementedException();
		}
	}
}
