using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.BlFrontEnd.DataExport
{
	public class DataImportConsole
	{
		public static void ImportData()
		{
			string filePath = "D:\\Genix\\result1.xlsx";

			ExcelFile excelFile = ExcelFile.Load(filePath, new XlsxLoadOptions());
			List<DataExportColumn> settings = new List<DataExportColumn>()
			{
				new DataExportColumn
				{
					 AttributrId = 10005400,
					 ColumnName = "Test 1",
					 IsKey = true
				},
				new DataExportColumn
				{
					 AttributrId = 10007100,
					 ColumnName = "Test 2",
					 IsKey = false
				},
				new DataExportColumn
				{
					 AttributrId = 10004300,
					 ColumnName = "Test 3",
					 IsKey = false
				},
				new DataExportColumn
				{
					 AttributrId = 10005300,
					 ColumnName = "Test 4",
					 IsKey = false
				}
			};

			DataImporter.ImportDataFromExcel(100, excelFile, settings);
		}
	}
}
