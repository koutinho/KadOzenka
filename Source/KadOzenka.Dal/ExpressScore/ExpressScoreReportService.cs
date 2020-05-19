using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Enum;
using ObjectModel.Common;
using ObjectModel.Directory.ES;
using ObjectModel.ES;
using ObjectModel.Sud;

namespace KadOzenka.Dal.ExpressScore
{
	public class ExpressScoreReportService
	{

		public static string ExpressScoreReportStorage = "ExpressScoreReportStorage";

		/// <summary>
		/// Матрица для обязательных параметров
		/// </summary>
		string[,] RequiredMatrixValueStrings;

		/// <summary>
		/// Матрица для всех остальных параметров
		/// </summary>
		string[,] CostMatrixValueStrings;

		#region required matrix params

		public int CurrentRowRequiredParam { get; private set; }
		private int CurrentColumnRequiredParam { get; set; }

		#endregion

		#region cost matrix params

		private int ColumnNameIndex { get; set; }

		private int CurrentColumnCostMatrix { get; set; }

		private int CurrentRowCostMatrix { get; set; }

		private int ColumnTargetObject { get; } = 0;

		private int CurrentRowTargetObjectCostMatrix { get; set; }

		private int CurrentRowCharacteristicCostMatrix { get; set; }

		private int CostMatrixRows { get; set; }

		#endregion


		#region required matrix methods

		public void InitRequiredMatrix(int row, int column)
		{
			RequiredMatrixValueStrings = new string[column + 1, row];
			ColumnNameIndex = column;
			CurrentRowRequiredParam = 0;
			CurrentColumnRequiredParam = 0;
		}

		public void SetNextColumnRequiredParam()
		{
			CurrentColumnRequiredParam++;
			CurrentRowRequiredParam = 0;
		}

		public void AddValueRequiredParam(string value)
		{
			try
			{
				RequiredMatrixValueStrings[CurrentColumnRequiredParam, CurrentRowRequiredParam] = value;
				CurrentRowRequiredParam++;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public void AddNameCharacteristicRequiredParam(string name, int? row = null)
		{
			try
			{
				RequiredMatrixValueStrings[ColumnNameIndex, row ?? CurrentRowRequiredParam] = name;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

		}

		#endregion

		#region cost matrix method

		public void InitCostFactorMatrix(int row, int column)
		{
			if (CostMatrixValueStrings == null)
			{
				CostMatrixValueStrings = new string[column + 1, row];
				ColumnNameIndex = column;
				CurrentColumnCostMatrix = 1;
				CurrentRowCharacteristicCostMatrix = 0;
				CostMatrixRows = row;
			}
		}

		public void RemoveCostFactorMatrix()
		{
			CostMatrixValueStrings = null;
			ColumnNameIndex = 0;
			CurrentColumnCostMatrix = 0;
			CurrentRowCharacteristicCostMatrix = 0;
		}

		public void AddValueCostMatrix(string value)
		{
			try
			{
				CostMatrixValueStrings[CurrentColumnCostMatrix, CurrentRowCostMatrix] = value;
				CurrentRowCostMatrix++;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public void AddValueTargetObjectCostMatrix(string value)
		{
			try
			{
				CostMatrixValueStrings[ColumnTargetObject, CurrentRowTargetObjectCostMatrix] = value;
				CurrentRowTargetObjectCostMatrix++;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public void AddNameCharacteristicCostMatrix(string name, int? row = null)
		{
			try
			{
				CostMatrixValueStrings[ColumnNameIndex, row ?? CurrentRowCharacteristicCostMatrix] = name;
				CurrentRowCharacteristicCostMatrix++;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

		}

		public void SetNextColumnComplexMatrix()
		{
			CurrentColumnCostMatrix++;
			CurrentRowCostMatrix = 0;
		}

		#endregion

		public string KnTargetObject { get; set; }

		public long GenerateReport(decimal summaryCost, decimal squareCost, DealTypeShort dealType, ScenarioType scenario)
		{
			ExcelFile excelTemplate = new ExcelFile();
			var mainWorkSheet = excelTemplate.Worksheets.Add("Экспресс оценка");


			mainWorkSheet.Rows[0].Cells[0].SetValue("Характеристика");
			mainWorkSheet.Rows[0].Cells[0].Style.Font.Weight = 600;
			mainWorkSheet.Rows[0].Cells[0].Style.Font.Size = 250;
			mainWorkSheet.Rows[0].Cells[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[0].Cells[0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mainWorkSheet.Rows[0].Cells[0].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

			mainWorkSheet.Rows[0].Cells[1].SetValue("Целевой объект");
			mainWorkSheet.Rows[0].Cells[1].Style.Font.Weight = 600;
			mainWorkSheet.Rows[0].Cells[1].Style.Font.Size = 250;
			mainWorkSheet.Rows[0].Cells[1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[0].Cells[1].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mainWorkSheet.Rows[0].Cells[1].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			for (int k = 1; k < ColumnNameIndex; k++)
			{
				mainWorkSheet.Rows[0].Cells[k + 1].SetValue($"Аналог {k}");
				mainWorkSheet.Rows[0].Cells[k + 1].Style.Font.Weight = 600;
				mainWorkSheet.Rows[0].Cells[k + 1].Style.Font.Size = 250;
				mainWorkSheet.Rows[0].Cells[k + 1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
				mainWorkSheet.Rows[0].Cells[k + 1].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
				mainWorkSheet.Rows[0].Cells[k + 1].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			}


			List<List<string>> rows = new List<List<string>>();
			if (RequiredMatrixValueStrings != null)
			{
				for (int i = 0; i < CurrentRowRequiredParam; i++) // по строкам
				{
					List<string> row = new List<string>();
					row.Add(RequiredMatrixValueStrings[ColumnNameIndex, i]);
					for (int j = 0; j < ColumnNameIndex; j++) // по столбцам
					{
						row.Add(RequiredMatrixValueStrings[j, i]);
					}
					rows.Add(row);
				}
			}

			int numberRow = 1;
			foreach (var row in rows)
			{
				AddRow(mainWorkSheet, numberRow, row);
				numberRow++;
			}
			rows.Clear();

			AddAdditionalRows(mainWorkSheet, numberRow, dealType, scenario);
			numberRow += 4;

			if (CostMatrixValueStrings != null)
			{
				for (int i = 0; i < CostMatrixRows; i++) // по строкам
				{
					List<string> row = new List<string>();
					row.Add(CostMatrixValueStrings[ColumnNameIndex, i]);
					for (int j = 0; j < ColumnNameIndex; j++) // по столбцам
					{
						row.Add(CostMatrixValueStrings[j, i]);
					}
					rows.Add(row);
				}
			}
			foreach (var row in rows)
			{
				AddRow(mainWorkSheet, numberRow, row);
				numberRow++;
			}

			AddSummaryRows(mainWorkSheet, numberRow, squareCost, summaryCost);
			MemoryStream stream = new MemoryStream();
			excelTemplate.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			long reportId = SaveReportToExportTable(stream, $"Очет по объекту {KnTargetObject}");

			return reportId;
		}

		private void AddRow(ExcelWorksheet sheet, int row, List<string> values)
		{
			int col = 0;
			foreach (var val in values)
			{
				if (decimal.TryParse(val, out var d))
				{
					sheet.Rows[row].Cells[col].SetValue(val);
					sheet.Rows[row].Cells[col].Style.NumberFormat = "#,##0.00";
				}
				else
				if (DateTime.TryParse(val, out var dat))
				{
					sheet.Rows[row].Cells[col].SetValue(val);
					sheet.Rows[row].Cells[col].Style.NumberFormat = "mm/dd/yyyy";
			
				}
				else
				{
					sheet.Rows[row].Cells[col].SetValue(val);
					sheet.Columns[col].SetWidth(5.0, LengthUnit.Centimeter);
				}

				if (col == 0)
				{
					sheet.Columns[col].Style.Font.Size = 250;
					sheet.Columns[col].Style.Font.Weight = 600;
					sheet.Columns[col].SetWidth(8.0, LengthUnit.Centimeter);
				}

				if (col != 0)
				{
				}
				sheet.Rows[row].SetHeight(1.5, LengthUnit.Centimeter);
				sheet.Rows[row].Cells[col].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
				sheet.Rows[row].Cells[col].Style.WrapText = true;
				sheet.Rows[row].Cells[col].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
				sheet.Rows[row].Cells[col].Style.VerticalAlignment = VerticalAlignmentStyle.Center;

				col++;
			}

		}

		private void AddSummaryRows(ExcelWorksheet mainWorkSheet, int numberRow, decimal squareCost, decimal summaryCost)
		{
			mainWorkSheet.Rows[numberRow].Cells[0].SetValue("Арендная ставка объекта оценки, руб/кв. м/год");

			mainWorkSheet.Rows[numberRow].Cells[0].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mainWorkSheet.Rows[numberRow].Cells[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow].Cells[0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow].Cells[0].Style.WrapText = true;
			mainWorkSheet.Rows[numberRow].SetHeight(1.5, LengthUnit.Centimeter);

			var mrSq = mainWorkSheet.Cells.GetSubrangeAbsolute(numberRow, 1, numberRow, ColumnNameIndex);
			mrSq.Merged = true;
			mrSq.Value = squareCost;
			mrSq.Style.Font.Size = 250;
			mrSq.Style.Font.Weight = 600;
			mrSq.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mrSq.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mrSq.Style.VerticalAlignment = VerticalAlignmentStyle.Center;

			mainWorkSheet.Rows[numberRow + 1].Cells[0].SetValue("Арендная ставка объекта оценки, руб/кв. м/год");

			mainWorkSheet.Rows[numberRow + 1].Cells[0].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mainWorkSheet.Rows[numberRow + 1].Cells[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow + 1].Cells[0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow + 1].Cells[0].Style.WrapText = true;
			mainWorkSheet.Rows[numberRow + 1].SetHeight(1.5, LengthUnit.Centimeter);

			var mrSum = mainWorkSheet.Cells.GetSubrangeAbsolute(numberRow + 1, 1, numberRow + 1, ColumnNameIndex);
			mrSum.Merged = true;
			mrSum.Value = summaryCost;
			mrSum.Style.Font.Size = 250;
			mrSum.Style.Font.Weight = 600;
			mrSum.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mrSum.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mrSum.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		}

		private void AddAdditionalRows(ExcelWorksheet mainWorkSheet, int numberRow, DealTypeShort dealType, ScenarioType scenario)
		{

			var mrSce = mainWorkSheet.Cells.GetSubrangeAbsolute(numberRow, 0, numberRow + 1, 0);
			mrSce.Merged = true;
			mrSce.Style.Font.Size = 250;
			mrSce.Style.Font.Weight = 600;
			mrSce.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mrSce.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mrSce.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mrSce.Style.WrapText = true;
			mrSce.Value = "Вариант расчета объекта оценки (ЕОН/ без доли ЗУ)";

			var mrSceA = mainWorkSheet.Cells.GetSubrangeAbsolute(numberRow, 1, numberRow, ColumnNameIndex - 1);
			mrSceA.Merged = true;
			mrSceA.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mrSceA.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mrSceA.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mrSceA.Style.WrapText = true;
			mrSceA.Value = "Расчет единого объекта недвижимости";


			var mrSceB = mainWorkSheet.Cells.GetSubrangeAbsolute(numberRow + 1, 1, numberRow + 1, ColumnNameIndex - 1);
			mrSceB.Merged = true;
			mrSceB.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mrSceB.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mrSceB.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mrSceB.Style.WrapText = true;
			mrSceB.Value = "Расчет объекта капитального строительства без доли ЗУ";

			mainWorkSheet.Rows[numberRow].SetHeight(1.5, LengthUnit.Centimeter);
			mainWorkSheet.Rows[numberRow + 1].SetHeight(1.5, LengthUnit.Centimeter);

			mainWorkSheet.Rows[numberRow].Cells[ColumnNameIndex].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mainWorkSheet.Rows[numberRow].Cells[ColumnNameIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow].Cells[ColumnNameIndex].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow + 1].Cells[ColumnNameIndex].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mainWorkSheet.Rows[numberRow + 1].Cells[ColumnNameIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow + 1].Cells[ColumnNameIndex].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			if (scenario == ScenarioType.Eon)
			{
				mainWorkSheet.Rows[numberRow ].Cells[ColumnNameIndex].SetValue("V");
			}

			if (scenario == ScenarioType.Oks)
			{
				mainWorkSheet.Rows[numberRow + 1].Cells[ColumnNameIndex].SetValue("V");
			}



			var mr = mainWorkSheet.Cells.GetSubrangeAbsolute(numberRow + 2, 0, numberRow + 3, 0);
			mr.Merged = true;
			mr.Style.Font.Size = 250;
			mr.Style.Font.Weight = 600;
			mr.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mr.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mr.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mr.Value = "Тип сделки";

			var mrA = mainWorkSheet.Cells.GetSubrangeAbsolute(numberRow + 2, 1, numberRow + 2, ColumnNameIndex - 1);
			mrA.Merged = true;
			mrA.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mrA.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mrA.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mrA.Value = "Аренда";


			var mrB = mainWorkSheet.Cells.GetSubrangeAbsolute(numberRow + 3, 1, numberRow + 3, ColumnNameIndex - 1);
			mrB.Merged = true;
			mrB.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mrB.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mrB.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mrB.Value = "Продажа";

			mainWorkSheet.Rows[numberRow + 2].SetHeight(1.5, LengthUnit.Centimeter);
			mainWorkSheet.Rows[numberRow + 3].SetHeight(1.5, LengthUnit.Centimeter);

			mainWorkSheet.Rows[numberRow + 2].Cells[ColumnNameIndex].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mainWorkSheet.Rows[numberRow + 2].Cells[ColumnNameIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow + 2].Cells[ColumnNameIndex].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow + 3].Cells[ColumnNameIndex].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mainWorkSheet.Rows[numberRow + 3].Cells[ColumnNameIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow + 3].Cells[ColumnNameIndex].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			if (dealType == DealTypeShort.Rent)
			{
				mainWorkSheet.Rows[numberRow + 2].Cells[ColumnNameIndex].SetValue("V");
			}

			if (dealType == DealTypeShort.Sale)
			{
				mainWorkSheet.Rows[numberRow + 3].Cells[ColumnNameIndex].SetValue("V");
			}

		}
		private long SaveReportToExportTable(MemoryStream stream, string fileName)
		{
			var currentDate = DateTime.Now;

			try
			{
				var export = new OMExportByTemplates
				{
					UserId = SRDSession.GetCurrentUserId().Value,
					DateCreated = currentDate,
					Status = 0,
					TemplateFileName = fileName,
					MainRegisterId = OMExpressScore.GetRegisterId(),
					RegisterViewId = "EsEstimateObjectCard"
				};
				export.Save();

				FileStorageManager.Save(stream, ExpressScoreReportStorage, currentDate, export.Id.ToString());

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