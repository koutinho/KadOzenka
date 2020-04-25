using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KadOzenka.BlFrontEnd.DataExport
{
	public class DataImportConsole
	{
		public static void ImportData()
		{
			string filePath = "D:\\Genix\\result1_05111.xlsx";

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

			//FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			//DataImporter.AddImportToQueue(100, "1", filePath, fs, settings);

			bool success;

			Stream stream = DataImporterCommon.ImportDataFromExcel(100, excelFile, settings, out success);
			stream.Seek(0, SeekOrigin.Begin);
			using (FileStream file = new FileStream("D:\\Genix\\result123.xlsx", FileMode.Create, FileAccess.Write))
			{
				byte[] bytes = new byte[stream.Length];
				stream.Read(bytes);
				file.Write(bytes);

				stream.Close();
			}
		}
	}
}
