using GemBox.Spreadsheet;

namespace KadOzenka.Dal.DataImport.DataImportKoFactory.ImportKoFactoryCommon
{
	public static class ImportKoCommon
	{
		public static void AddSuccessHeaderColumn(ExcelWorksheet mainWorkSheet, int column)
		{
			mainWorkSheet.Rows[0].Cells[column].SetValue($"Результат сохранения");
			mainWorkSheet.Rows[0].Cells[column].Style.Borders.SetBorders(MultipleBorders.All,
				SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
		}

		public static void AddSuccessCell(ExcelWorksheet mainWorkSheet, int row, int column, string message)
		{
			mainWorkSheet.Rows[row].Cells[column].SetValue(message);
			mainWorkSheet.Rows[row].Cells[column].Style.FillPattern
				.SetSolid(SpreadsheetColor.FromName(ColorName.LightGreen));
			mainWorkSheet.Rows[row].Cells[column].Style.Borders
				.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
					LineStyle.Thin);
		}

		public static void AddWarningCell(ExcelWorksheet mainWorkSheet, int row, int column, string message)
		{
			mainWorkSheet.Rows[row].Cells[column].SetValue(message);
			mainWorkSheet.Rows[row].Cells[column].Style.FillPattern
				.SetSolid(SpreadsheetColor.FromName(ColorName.Yellow));
			mainWorkSheet.Rows[row].Cells[column].Style.Borders
				.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
					LineStyle.Thin);
		}

		public static void AddErrorCell(ExcelWorksheet mainWorkSheet, int row, int column, string message)
		{
			mainWorkSheet.Rows[row].Cells[column].SetValue(message);
			mainWorkSheet.Rows[row].Cells[column].Style.FillPattern
				.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
			mainWorkSheet.Rows[row].Cells[column].Style.Borders
				.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
					LineStyle.Thin);
		}
	}
}