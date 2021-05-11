using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using MarketPlaceBusiness.Common;
using ObjectModel.Common;
using ObjectModel.Directory;
using ObjectModel.Directory.Common;
using ObjectModel.Market;
using Serilog;

namespace KadOzenka.Dal.OutliersChecking
{
	public class OutliersCheckingReport
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<OutliersCheckingReport>();

		private const int IdColumnNumber = 0;
		private const int KnColumnNumber = 1;
		private const int LocationNameColumnNumber = 2;
		private const int PricePerMeterColumnNumber = 3;
		private const int PreviousStatusColumnNumber = 4;
		private const int LowerMedianColumnNumber = 5;
		private const int UpperMedianColumnNumber = 6;
		private const int MinDeltaCoefColumnNumber = 7;
		private const int MaxDeltaCoefColumnNumber = 8;
		private const int LowerLimitColumnNumber = 9;
		private const int UpperLimitColumnNumber = 10;
		private const int IsExcludedColumnNumber = 11;

		private const int MaxWorksheetNameLength = 31;

		private ExcelFile _excelFile;
		private ExcelWorksheet _currentWorksheet;
		private int _currentRow;
		private List<string> _headersList = new List<string>
		{
			"ИД",
			"КН",
			"Зона-округ",
			"Цена за кв. М",
			"Статус до обработки",
			"Нижняя медиана",
			"Верхняя медиана",
			"COEFmin",
			"COEFmax",
			"LIMITmin",
			"LIMITmax",
			"Признак исключения"
		};

		public OutliersCheckingReport()
		{
			_excelFile = new ExcelFile();
		}

		public void AddNewWorksheetForSegment(MarketSegment segment, DealType dealType, ObjectPropertyTypeDivision propertyTypeDivision)
		{
			Log.Debug("Добавление новой страницы отчета для '{PropertyTypeDivision}' типа сделки '{DealType}' сегмента '{MarketSegment}'",
				propertyTypeDivision.GetEnumDescription(),
				dealType.GetEnumDescription(), segment.GetEnumDescription());

			_currentRow = 0;
			_currentWorksheet = _excelFile.Worksheets.Add(GetWorksheetName($"{segment.GetEnumDescription()}"));
			_currentWorksheet.Cells.Style.Font.Name = "Times New Roman";
			AddTitle(segment, dealType, propertyTypeDivision);
			AddHeaders();
			SetWidth();
		}

		private string GetWorksheetName(string name)
		{
			var worksheetName = $"{_excelFile.Worksheets.Count + 1} {name}";
			return worksheetName.Length > MaxWorksheetNameLength
				? $"{worksheetName.Substring(0, MaxWorksheetNameLength - 4)}..."
				: worksheetName;
		}

		public void AddRow(OutliersCheckingReportRow row)
		{
			_currentWorksheet.Rows[_currentRow].Cells[IdColumnNumber].SetValue(row.MarketObjectId);
			_currentWorksheet.Rows[_currentRow].Cells[KnColumnNumber].SetValue(row.Kn);
			_currentWorksheet.Rows[_currentRow].Cells[LocationNameColumnNumber].SetValue(row.LocationName);
			_currentWorksheet.Rows[_currentRow].Cells[PricePerMeterColumnNumber].SetValue(Convert.ToDouble(row.PricePerMeter));
			_currentWorksheet.Rows[_currentRow].Cells[PreviousStatusColumnNumber].SetValue(row.PreviousStatus.GetEnumDescription());
			_currentWorksheet.Rows[_currentRow].Cells[LowerMedianColumnNumber].SetValue(Convert.ToDouble(row.LowerMedian));
			_currentWorksheet.Rows[_currentRow].Cells[UpperMedianColumnNumber].SetValue(Convert.ToDouble(row.UpperMedian));
			if(row.MinDeltaCoef.HasValue)
				_currentWorksheet.Rows[_currentRow].Cells[MinDeltaCoefColumnNumber].SetValue(Convert.ToDouble(row.MinDeltaCoef));
			if(row.MaxDeltaCoef.HasValue)
				_currentWorksheet.Rows[_currentRow].Cells[MaxDeltaCoefColumnNumber].SetValue(Convert.ToDouble(row.MaxDeltaCoef));
			_currentWorksheet.Rows[_currentRow].Cells[LowerLimitColumnNumber].SetValue(Convert.ToDouble(row.LowerLimit));
			_currentWorksheet.Rows[_currentRow].Cells[UpperLimitColumnNumber].SetValue(Convert.ToDouble(row.UpperLimit));
			if(row.IsExcluded)
				_currentWorksheet.Rows[_currentRow].Cells[IsExcludedColumnNumber].SetValue("Исключен");

			_currentRow++;
		}

		public void SetStyleAndSorting()
		{
			int worksheetCount = _excelFile.Worksheets.Count;
			int countColumns = _headersList.Count;
			int errCount = 0;
			int successCount = 0;
			for (var w = 0; w < worksheetCount; w++)
			{
				int countRows = _excelFile.Worksheets[w].Rows.Count;
				Log.ForContext("MarketSegment", _excelFile.Worksheets[w].Name)
					.Debug("Установка стилей в Excel таблице {CountRows} x {CountColumns}", countRows, countColumns);
				for (int i = 1; i < countRows; i++)
				{
					for (int j = 0; j < countColumns; j++)
					{
						if (_excelFile.Worksheets[w].Rows[i] != null && _excelFile.Worksheets[w].Rows[i].Cells[j] != null)
						{
							try
							{
								_excelFile.Worksheets[w].Rows[i].Cells[j].Style.HorizontalAlignment =
									HorizontalAlignmentStyle.Center;
								_excelFile.Worksheets[w].Rows[i].Cells[j].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
								_excelFile.Worksheets[w].Rows[i].Cells[j].Style.Borders.SetBorders(MultipleBorders.All,
									SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
								_excelFile.Worksheets[w].Rows[i].Cells[j].Style.WrapText = true;
								if (IsDecimalCell(i, j))
									_excelFile.Worksheets[w].Rows[i].Cells[j].Style.NumberFormat = "#,##0.00";
								if (IsDoubleCell(i, j))
									_excelFile.Worksheets[w].Rows[i].Cells[j].Style.NumberFormat = "#,##0.0000";

								if (successCount < 5)
									Log.ForContext("MarketSegment", _excelFile.Worksheets[w].Name)
										.Verbose("Применение стилей в Excel {WorkSheetRow} {WorkSheetCell}", i, j);
								successCount++;
							}
							catch (Exception ex)
							{
								if (errCount < 5)
									Log.ForContext("MarketSegment", _excelFile.Worksheets[w].Name)
										.Warning(ex,
											"Ошибка применения стилей в Excel {WorkSheetRow} {WorkSheetCell}", i, j);
								errCount++;
							}
						}
					}
				}

				_excelFile.Worksheets[w].Cells.GetSubrangeAbsolute(2, 0, countRows, countColumns).Sort(false).By(LocationNameColumnNumber).Apply();
			}
		}

		public long SaveReport()
		{
			try
			{
				MemoryStream stream = new MemoryStream();
				_excelFile.Save(stream, SaveOptions.XlsxDefault);
				stream.Seek(0, SeekOrigin.Begin);

				var currentDate = DateTime.Now;
				var export = new OMExportByTemplates
				{
					UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
					DateCreated = currentDate,
					DateStarted = currentDate,
					Status = (int)ImportStatus.Added,
					FileResultTitle = "Отчет по итогам проверки на вылеты",
					FileExtension = "xlsx",
					MainRegisterId = Consts.RegisterId,
					RegisterViewId = "MarketObjects"
				};
				export.Save();

				export.DateFinished = DateTime.Now;
				export.ResultFileName = DataExporterCommon.GetStorageResultFileName(export.Id);
				export.Status = (long)ImportStatus.Completed;
				FileStorageManager.Save(stream, DataExporterCommon.FileStorageName, export.DateFinished.Value, export.ResultFileName);
				export.Save();

				Log.ForContext("ResultFileName", export.ResultFileName)
					.ForContext("FileId", export.Id)
					.Debug("Сохранение отчета {FileName}", export.FileResultTitle);

				return export.Id;
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Сохранение отчета завершилось исключением");
				ErrorManager.LogError(ex);
				throw;
			}
		}

		private void AddTitle(MarketSegment segment, DealType dealType, ObjectPropertyTypeDivision propertyTypeDivision)
		{
			var mergeRow = _currentWorksheet.Cells.GetSubrangeAbsolute(_currentRow, 0, _currentRow, _headersList.Count - 1);
			mergeRow.Merged = true;
			mergeRow.Style.Font.Size = 250;
			mergeRow.Style.Font.Weight = 600;
			mergeRow.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			mergeRow.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
			mergeRow.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
			mergeRow.Style.WrapText = true;
			mergeRow.Value = $"Результат проверки на вылеты для '{propertyTypeDivision.GetEnumDescription()}' типа сделки '{dealType.GetEnumDescription()}' сегмента '{segment.GetEnumDescription()}'";

			_currentRow++;
		}

		private void AddHeaders()
		{
			int col = 0;
			foreach (string value in _headersList)
			{
				_currentWorksheet.Rows[_currentRow].Cells[col].SetValue(value);
				col++;
			}

			_currentRow++;
		}

		private void SetWidth()
		{
			_currentWorksheet.Columns[KnColumnNumber].SetWidth(4, LengthUnit.Centimeter);
			_currentWorksheet.Columns[LocationNameColumnNumber].SetWidth(7, LengthUnit.Centimeter);
			_currentWorksheet.Columns[PricePerMeterColumnNumber].SetWidth(3, LengthUnit.Centimeter);
			_currentWorksheet.Columns[PreviousStatusColumnNumber].SetWidth(4, LengthUnit.Centimeter);
			_currentWorksheet.Columns[LowerMedianColumnNumber].SetWidth(3, LengthUnit.Centimeter);
			_currentWorksheet.Columns[UpperMedianColumnNumber].SetWidth(3, LengthUnit.Centimeter);
			_currentWorksheet.Columns[MinDeltaCoefColumnNumber].SetWidth(2, LengthUnit.Centimeter);
			_currentWorksheet.Columns[MaxDeltaCoefColumnNumber].SetWidth(2, LengthUnit.Centimeter);
			_currentWorksheet.Columns[LowerLimitColumnNumber].SetWidth(3, LengthUnit.Centimeter);
			_currentWorksheet.Columns[UpperLimitColumnNumber].SetWidth(3, LengthUnit.Centimeter);
			_currentWorksheet.Columns[IsExcludedColumnNumber].SetWidth(3, LengthUnit.Centimeter);
		}

		private bool IsDecimalCell(int row, int col)
		{
			return row > 0 && (col == PricePerMeterColumnNumber 
			                   || col == LowerMedianColumnNumber 
			                   || col == UpperMedianColumnNumber 
			                   || col == LowerLimitColumnNumber 
			                   || col == UpperLimitColumnNumber);
		}

		private bool IsDoubleCell(int row, int col)
		{
			return row > 0 && (col == MinDeltaCoefColumnNumber || col == MaxDeltaCoefColumnNumber);
		}
	}
}
