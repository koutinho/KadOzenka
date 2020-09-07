using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KadOzenka.BlFrontEnd.DataExport
{
    public class DataExportConsole
    {
		public static void ExportData()
		{
			string filePath = "D:\\Genix\\test.xlsx";

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
				//new DataExportColumn
				//{
				//	 AttributrId = 10004300,
				//	 ColumnName = "Test 3",
				//	 IsKey = false
				//},
				//new DataExportColumn
				//{
				//	 AttributrId = 10005300,
				//	 ColumnName = "Test 4",
				//	 IsKey = false
				//}
			};

			//var stream = DataExporter.ExportDataToExcel(100, excelFile, settings);


			FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			
			new DataExporterByTemplate().AddExportToQueue(100, "1", filePath, fs, settings);

			// Сохранение результата в result xlsx
			//stream.Save("D:\\Genix\\result.xlsx");

			//stream.Seek(0, SeekOrigin.Begin);
			//using (FileStream file = new FileStream("D:\\Genix\\result.xlsx", FileMode.Create, FileAccess.Write))
			//{
			//	byte[] bytes = new byte[stream.Length];
			//	stream.Read(bytes);
			//	file.Write(bytes);
				
			//	stream.Close();
			//}
		}
    }
}
