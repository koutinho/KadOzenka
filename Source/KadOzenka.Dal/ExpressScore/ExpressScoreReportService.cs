using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Misc;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.Enum;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.Directory.ES;
using ObjectModel.ES;
using ObjectModel.Sud;
using Serilog;

namespace KadOzenka.Dal.ExpressScore
{
	public class ExpressScoreReportService
	{
        private static readonly ILogger _log = Log.ForContext<ExpressScoreReportService>();
		/// <summary>
		/// Матрица для обязательных параметров
		/// </summary>
		string[,] RequiredMatrixValueStrings;

		/// <summary>
		/// Матрица для всех остальных параметров
		/// </summary>
		string[,] CostMatrixValueStrings;

		public bool HasException { get; set; } = false;

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
			try
			{
				RequiredMatrixValueStrings = new string[column + 1, row];
				ColumnNameIndex = column;
				CurrentRowRequiredParam = 0;
				CurrentColumnRequiredParam = 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				HasException = true;
			}
		}

		public void SetNextColumnRequiredParam()
		{
			try
			{
				CurrentColumnRequiredParam++;
				CurrentRowRequiredParam = 0;

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				HasException = true;
			}
		
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
				_log.Error("Ошибка при добавлении обязательного параметра в матрицу", e);
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				HasException = true;
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
                _log.Error("Ошибка при добавлении характеристики", e);
                Console.WriteLine(e);
				ErrorManager.LogError(e);
				HasException = true;
			}
        }

		#endregion

		#region cost matrix method

		public void InitCostFactorMatrix(int row, int column)
		{
			try
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
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				HasException = true;
			}

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
				ErrorManager.LogError(e);
				HasException = true;
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
				ErrorManager.LogError(e);
				HasException = true;
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
				_log.Error("Ошибка при добавление характеристики", e);
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				HasException = true;
			}
        }

		public void SetNextColumnComplexMatrix()
		{
			try
			{
				CurrentColumnCostMatrix++;
				CurrentRowCostMatrix = 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				HasException = true;
			}
		}

		#endregion

		public string KnTargetObject { get; set; }

		/// <summary>
		/// Генерация отчета
		/// </summary>
		/// <param name="summaryCost"></param>
		/// <param name="squareCost"></param>
		/// <param name="dealType"></param>
		/// <param name="scenario"></param>
		/// <returns> Возвращаем ид отчета, если в результате построения отчета были выброшены исключения то возвращается -1</returns>
		public long GenerateReport(decimal summaryCost, decimal squareCost, DealTypeShort dealType, ScenarioType scenario)
		{
			if (HasException) return -1;

            try
			{
				ExcelFile excelTemplate = new ExcelFile();
				var mainWorkSheet = excelTemplate.Worksheets.Add("Экспресс оценка");
				mainWorkSheet.Cells.Style.Font.Name = "Times New Roman";

				//Создаем счетсчик строки и дальше увеличиваем по необходимости
				int numberRow = 0;
				AddHeader(mainWorkSheet, numberRow);
				numberRow += 2;

				AddMergeElement(mainWorkSheet, numberRow, numberRow, 0, ColumnNameIndex, "Характеристики объектов-аналогов", true);
				numberRow++;

				List<List<string>> rows = new List<List<string>>();
				if (RequiredMatrixValueStrings != null)
				{
					for (int i = 0; i < CurrentRowRequiredParam - 3; i++) // по строкам
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


				foreach (var row in rows)
				{
					AddRow(mainWorkSheet, numberRow, row);
					numberRow++;
				}
				rows.Clear();

				AddAdditionalRows(mainWorkSheet, numberRow, dealType, scenario);
				numberRow += 4;

				AddMergeElement(mainWorkSheet, numberRow, numberRow, 0, ColumnNameIndex, "Определение кадастровой стоимости", true);
				numberRow++;

				if (RequiredMatrixValueStrings != null)
				{
					for (int i = 4; i < CurrentRowRequiredParam; i++) // по строкам
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

				string textSquare = "";
				string textSummary = "";

				if (dealType == DealTypeShort.Sale)
				{
					textSquare = "Стоимость объекта оценки, руб/кв.м";
					textSummary = "Стоимость объекта оценки, руб";
				}

				if (dealType == DealTypeShort.Rent)
				{
					textSquare = "Арендная ставка объекта оценки, руб/кв. м/год";
					textSummary = "Арендная ставка объекта оценки, руб/год";
					squareCost *= 12;
					summaryCost *= 12;
				}

				AddSummaryRows(mainWorkSheet, numberRow, squareCost, summaryCost, textSquare, textSummary);
				MemoryStream stream = new MemoryStream();
				excelTemplate.Save(stream, SaveOptions.XlsxDefault);
				stream.Seek(0, SeekOrigin.Begin);

				long reportId = SaveReportToExportTable(stream, $"Отчет по объекту {KnTargetObject}");

				return reportId;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				return -1;
			}
		}

		private void AddBoldValue(ExcelWorksheet mainWorkSheet, int numberRow, int numberColumn, string text)
		{
			mainWorkSheet.Rows[numberRow].Cells[numberColumn].SetValue(text);
			mainWorkSheet.Rows[numberRow].Cells[numberColumn].Style.Font.Weight = 600;
			mainWorkSheet.Rows[numberRow].Cells[numberColumn].Style.Font.Size = 250;
			mainWorkSheet.Rows[numberRow].Cells[numberColumn].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow].Cells[numberColumn].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow].Cells[numberColumn].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
		}

		private void AddHeader(ExcelWorksheet mainWorkSheet, int numberRow)
		{
			AddMergeElement(mainWorkSheet, numberRow, numberRow, 0, ColumnNameIndex, "Справка об определении стоимости");
			
			AddBoldValue(mainWorkSheet, numberRow + 1, 0, "Характеристики");
			AddBoldValue(mainWorkSheet, numberRow + 1, 1, "Объект оценки");

			for (int k = 1; k < ColumnNameIndex; k++)
			{
				AddBoldValue(mainWorkSheet, numberRow + 1, k + 1, $"Объект-аналог {k}");
			}
        }

		private void AddRow(ExcelWorksheet sheet, int row, List<string> values)
		{
            _log.ForContext("current_row", row)
                .ForContext("current_Sheet_is_not_null", !sheet.IsNullOrDbNull())
                .ForContext("all_values_for_row", values)
                .Verbose("Добавление строки в отчет");

			bool isGreen = false;
			int col = 0;
            try
            {
                foreach (var val in values)
                {
                    _log.Debug("======> row={row}, column={col}, value={val}", row, col, val);
                    if (decimal.TryParse(val, out var d))
                    {
                        sheet.Rows[row].Cells[col].SetValue(val.Replace(",", "."));
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
                        if (val.Contains("Корректировка"))
                        {
                            isGreen = true;
                        }
                    }

                    //sheet.Rows[row].SetHeight(1.5, LengthUnit.Centimeter);
                    sheet.Rows[row].Cells[col].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                    sheet.Rows[row].Cells[col].Style.WrapText = true;
                    sheet.Rows[row].Cells[col].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                    sheet.Rows[row].Cells[col].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
                    if (isGreen)
                    {
                        sheet.Rows[row].Cells[col].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch,
                            SpreadsheetColor.FromArgb(225, 232, 225), SpreadsheetColor.FromArgb(225, 232, 225));
                    }

                    col++;
                }
			}
            catch (Exception e)
            {
                HasException = true;
                ErrorManager.LogError(e);
                Console.WriteLine(e);
            }

        }

		private void AddSummaryRows(ExcelWorksheet mainWorkSheet, int numberRow, decimal squareCost, decimal summaryCost, string textSquare, string textSummary)
		{
			mainWorkSheet.Rows[numberRow].Cells[0].SetValue(textSquare);

			mainWorkSheet.Rows[numberRow].Cells[0].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mainWorkSheet.Rows[numberRow].Cells[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow].Cells[0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow].Cells[0].Style.WrapText = true;
			mainWorkSheet.Rows[numberRow].SetHeight(1.5, LengthUnit.Centimeter);

			var mrSq = mainWorkSheet.Cells.GetSubrangeAbsolute(numberRow, 1, numberRow, ColumnNameIndex);
			mrSq.Merged = true;
			mrSq.Value = squareCost.ToString("N").Replace(",", ".");
			mrSq.Style.Font.Size = 250;
			mrSq.Style.Font.Weight = 600;
			mrSq.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mrSq.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mrSq.Style.VerticalAlignment = VerticalAlignmentStyle.Center;

			mainWorkSheet.Rows[numberRow + 1].Cells[0].SetValue(textSummary);

			mainWorkSheet.Rows[numberRow + 1].Cells[0].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mainWorkSheet.Rows[numberRow + 1].Cells[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow + 1].Cells[0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mainWorkSheet.Rows[numberRow + 1].Cells[0].Style.WrapText = true;
			mainWorkSheet.Rows[numberRow + 1].SetHeight(1.5, LengthUnit.Centimeter);

			var mrSum = mainWorkSheet.Cells.GetSubrangeAbsolute(numberRow + 1, 1, numberRow + 1, ColumnNameIndex);
			mrSum.Merged = true;
			mrSum.Value = summaryCost.ToString("N").Replace(",", ".");
			mrSum.Style.Font.Size = 250;
			mrSum.Style.Font.Weight = 600;
			mrSum.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mrSum.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mrSum.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		}

		private void AddAdditionalRows(ExcelWorksheet mainWorkSheet, int numberRow, DealTypeShort dealType, ScenarioType scenario)
		{
            // Вариант расчета
			AddMergeElement(mainWorkSheet, numberRow, numberRow + 1, 0, 0, "Вариант расчета объекта оценки (ЕОН/ без доли ЗУ)", true);

			AddMergeElement(mainWorkSheet, numberRow, numberRow, 1, ColumnNameIndex - 1, "Расчет единого объекта недвижимости", true);

			AddMergeElement(mainWorkSheet, numberRow + 1, numberRow + 1, 1, ColumnNameIndex - 1, "Расчет объекта капитального строительства без доли ЗУ", true);

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
				//mainWorkSheet.Pictures.Add("wwwroot/images/galochka.png",
				//	new AnchorCell(mainWorkSheet.Columns[ColumnNameIndex], mainWorkSheet.Rows[numberRow], 100, 100),
				//	new AnchorCell(mainWorkSheet.Columns[ColumnNameIndex], mainWorkSheet.Rows[numberRow], 100, 100)).Position.Mode = PositioningMode.Move;
				mainWorkSheet.Rows[numberRow ].Cells[ColumnNameIndex].SetValue("V");
			}

			if (scenario == ScenarioType.Oks)
			{
				mainWorkSheet.Rows[numberRow + 1].Cells[ColumnNameIndex].SetValue("V");
			}

			// Тип сделки
            AddMergeElement(mainWorkSheet, numberRow + 2, numberRow + 3, 0, 0, "Тип сделки", true);

			AddMergeElement(mainWorkSheet, numberRow + 2, numberRow + 2, 1, ColumnNameIndex - 1, "Аренда", true);

            AddMergeElement(mainWorkSheet, numberRow + 3, numberRow + 3, 1, ColumnNameIndex - 1, "Продажа", true);

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

		private void AddMergeElement(ExcelWorksheet mainWorkSheet, int startRow, int endRow, int startColumn, int endColumn, string text, bool isBorder = false)
		{
            var mergeRow = mainWorkSheet.Cells.GetSubrangeAbsolute(startRow, startColumn, endRow, endColumn);
			mergeRow.Merged = true;
			mergeRow.Style.Font.Size = 250;
			mergeRow.Style.Font.Weight = 600;
			if(isBorder) mergeRow.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mergeRow.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mergeRow.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mergeRow.Style.WrapText = true;
			mergeRow.Value = text;
        }
		private long SaveReportToExportTable(MemoryStream stream, string fileName)
		{
            if (HasException)
            {
                return -1;
            }
			var currentDate = DateTime.Now;

            if (!SRDSession.GetCurrentUserId().HasValue)
            {
                _log.ForContext("UserId", SRDSession.GetCurrentUserId())
                    .Warning("Сохранение отчета");
			}

			try
			{
				var export = new OMExportByTemplates
				{
					UserId = SRDSession.GetCurrentUserId().Value,
					DateCreated = currentDate,
					Status = 0,
					FileResultTitle = fileName,
					FileExtension = "xlsx",
					MainRegisterId = OMExpressScore.GetRegisterId(),
					RegisterViewId = "EsEstimateObjectCard"
				};
				export.Save();

				export.DateFinished = DateTime.Now;
				export.ResultFileName = DataExporterCommon.GetStorageResultFileName(export.Id);
				export.Status = (long)ImportStatus.Completed;
				FileStorageManager.Save(stream, DataExporterCommon.FileStorageName, export.DateFinished.Value, export.ResultFileName);
				export.Save();

				return export.Id;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				return -1;
			}
		}
	}
}