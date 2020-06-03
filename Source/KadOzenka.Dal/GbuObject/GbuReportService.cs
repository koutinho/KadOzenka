using System;
using System.Collections.Generic;
using System.IO;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.SRD;
using GemBox.Spreadsheet;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.ES;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.GbuObject
{
	public class GbuReportService
	{
		private ExcelFile _excelTemplate;
		private ExcelWorksheet _mainWorkSheet;
		public static string ReportGbuStorage = "SaveReportPath";

		private int _currentRow { get; set; }

		public GbuReportService()
		{
			_excelTemplate = new ExcelFile();
			_mainWorkSheet = _excelTemplate.Worksheets.Add("Отчет");
			_mainWorkSheet.Cells.Style.Font.Name = "Times New Roman";
		}

		/// <summary>
		/// Отдает текущий номер строки и инкрементирует значение для следующего обращения 
		/// </summary>
		/// <returns></returns>
		public int GetCurrentRow()
		{
			int tmp = _currentRow;
			_currentRow++;
			return tmp;
		}

		public void AddHeaders(int rowNumber, List<string> values)
		{
			int col = 0;
			foreach (string value in values)
			{
				_mainWorkSheet.Rows[rowNumber].Cells[col].SetValue(value);
				_mainWorkSheet.Rows[rowNumber].Cells[col].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
				_mainWorkSheet.Rows[rowNumber].Cells[col].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
				_mainWorkSheet.Rows[rowNumber].Cells[col].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
				_mainWorkSheet.Rows[rowNumber].Cells[col].Style.WrapText = true;
				//mainWorkSheet.Columns[col].SetWidth(5, LengthUnit.Centimeter);
				col++;
			}

			_currentRow++;

		}

		public void AddValue(string value, int column, int row)
		{
				_mainWorkSheet.Rows[row].Cells[column].SetValue(value);
				//mainWorkSheet.Columns[col].SetWidth(5, LengthUnit.Centimeter);
		}

		public void SetStyle()
		{
			int countRows = _mainWorkSheet.Rows.Count;
			int countColumns = _mainWorkSheet.CalculateMaxUsedColumns();

			for (int i = 0; i < countRows; i++)
			{
				for (int j = 0; j < countColumns; j++)
				{
					_mainWorkSheet.Rows[i].Cells[j].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
					_mainWorkSheet.Rows[i].Cells[j].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
					_mainWorkSheet.Rows[i].Cells[j].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
					_mainWorkSheet.Rows[i].Cells[j].Style.WrapText = true;
				}
			}
		}

		public void SetIndividualWidth(int column, int width)
		{
			_mainWorkSheet.Columns[column].SetWidth(width, LengthUnit.Centimeter);
		}

		public long SaveReport(string fileName)
		{
			MemoryStream stream = new MemoryStream();
			_excelTemplate.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			var currentDate = DateTime.Now;
			
			try
			{
				var export = new OMExportByTemplates
				{
					UserId = SRDSession.GetCurrentUserId().Value,
					DateCreated = currentDate,
					Status = (int)ImportStatus.Added,
					TemplateFileName = fileName,
					MainRegisterId = OMMainObject.GetRegisterId(),
					RegisterViewId = "GbuObjects"
				};
				export.Save();

				FileStorageManager.Save(stream, ReportGbuStorage, currentDate, export.Id.ToString());

				return export.Id;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				throw;
			}
		}
	}
}