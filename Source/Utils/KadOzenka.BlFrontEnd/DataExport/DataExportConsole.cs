using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.BlFrontEnd.DataExport
{
    public class DataExportConsole
    {
		public static void ExportData()
		{
			string filePath = "test.xlsx";

			ExcelFile excelFile = ExcelFile.Load(filePath, new XlsxLoadOptions());


			var stream = DataExporter.ExportDataToExcel(100, excelFile, new List<DataExportColumn>());

			// Сохранение результата в result xlsx

		}
    }
}
