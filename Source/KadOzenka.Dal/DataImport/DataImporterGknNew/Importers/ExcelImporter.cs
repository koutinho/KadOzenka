using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base;
using KadOzenka.Dal.Logger;
using ObjectModel.KO;
using Serilog;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.DataImport.Validation;
using Newtonsoft.Json;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Importers
{
	/// <summary>
	/// Класс для импорта Excel документа ЗнО любого типа кроме "Обращений"
	/// </summary>
	public class ExcelImporter : BaseImporter
	{
		public ExcelImporter(DataImporterGknLongProcessProgressLogger dataImporterGknLongProcessProgressLogger)
		: base(dataImporterGknLongProcessProgressLogger, Log.ForContext<ExcelImporter>())
		{
		}


		protected override void ImportGkn(DataImporterGkn dataImporterGkn, FileStream fileStream, string pathSchema, OMTask task,
			CancellationToken cancellationToken, object columnsMappingObj = null)
		{
			var columnsMappingStr = columnsMappingObj?.ToString();
			if (string.IsNullOrWhiteSpace(columnsMappingStr))
				throw new Exception("Не передано соответствие колонок Excel и заполняемых атрибутов");

			var columnsMapping = JsonConvert.DeserializeObject<List<ColumnToAttributeMapping>>(columnsMappingStr);
			DataImporterGknValidator.ValidateExcelColumnsForNotPetition(columnsMapping.Select(x => x.AttributeId));

			var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
			dataImporterGkn.ImportGknFromExcel(excelFile, pathSchema, task, columnsMapping, cancellationToken);
		}
	}
}
