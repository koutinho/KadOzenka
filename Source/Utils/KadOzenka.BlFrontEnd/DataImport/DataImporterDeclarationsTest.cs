using System.Collections.Generic;
using System.IO;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.DataImport.Dto;

namespace KadOzenka.BlFrontEnd.DataImport
{
	public class DataImporterDeclarationsTest
	{
		public void ImportData()
		{
			string filePath = "C:\\Genix\\data.xlsx";

			ExcelFile excelFile = ExcelFile.Load(filePath, new XlsxLoadOptions());
			var settings = new List<DataExportColumn>()
			{
				new DataExportColumn
				{
					AttributrId = 50101000,
					ColumnName = "Статус декларации",
					IsKey = false
				},
				new DataExportColumn
				{
					AttributrId = 50101100,
					ColumnName = "Кадастровый номер объекта",
					IsKey = false
				},
				new DataExportColumn
				{
					AttributrId = 50101300,
					ColumnName = "Дата выдачи исполнителю",
					IsKey = false
				},
				new DataExportColumn
				{
					AttributrId = 50100300,
					ColumnName = "Идентификатор заявителя (он же собственник)",
					IsKey = false
				},
				new DataExportColumn
				{
					AttributrId = 50100200,
					ColumnName = "Идентификатор книги",
					IsKey = false
				},
				new DataExportColumn
				{
					AttributrId = 50100100,
					ColumnName = "Идентификатор",
					IsKey = true
				}
			};

			Stream stream = DataImporterDeclarations.ImportDataFromExcel(excelFile, settings);
			stream.Seek(0, SeekOrigin.Begin);
			using (FileStream file = new FileStream("C:\\Genix\\result_030220.xlsx", FileMode.Create, FileAccess.Write))
			{
				byte[] bytes = new byte[stream.Length];
				stream.Read(bytes);
				file.Write(bytes);

				stream.Close();
			}
		}
	}
}
