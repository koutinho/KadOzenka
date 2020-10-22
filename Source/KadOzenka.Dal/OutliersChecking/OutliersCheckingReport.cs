using System;
using System.Collections.Generic;
using System.IO;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
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

		public void AddNewWorksheetForSegment(MarketSegment segment)
		{
			Log.Debug("Добавление новой страницы отчета для сегмента {MarketSegment}", segment.GetEnumDescription());

			_currentRow = 0;
			_currentWorksheet = _excelFile.Worksheets.Add(segment.GetEnumDescription());
			_currentWorksheet.Cells.Style.Font.Name = "Times New Roman";
			AddHeaders();
		}

		public void AddRow(OutliersCheckingReportRow row)
		{
			_currentWorksheet.Rows[_currentRow].Cells[0].SetValue(row.MarketObjectId);
			_currentWorksheet.Rows[_currentRow].Cells[1].SetValue(row.Kn);
			_currentWorksheet.Rows[_currentRow].Cells[3].SetValue(row.LocationName);
			_currentWorksheet.Rows[_currentRow].Cells[4].SetValue(Convert.ToDouble(row.PricePerMeter));
			_currentWorksheet.Rows[_currentRow].Cells[5].SetValue(row.PreviousStatus.GetEnumDescription());
			_currentWorksheet.Rows[_currentRow].Cells[6].SetValue(Convert.ToDouble(row.LowerMedian));
			_currentWorksheet.Rows[_currentRow].Cells[7].SetValue(Convert.ToDouble(row.UpperMedian));
			if(row.MinDeltaCoef.HasValue)
				_currentWorksheet.Rows[_currentRow].Cells[8].SetValue(Convert.ToDouble(row.MinDeltaCoef));
			if(row.MaxDeltaCoef.HasValue)
				_currentWorksheet.Rows[_currentRow].Cells[9].SetValue(Convert.ToDouble(row.MaxDeltaCoef));
			_currentWorksheet.Rows[_currentRow].Cells[10].SetValue(Convert.ToDouble(row.LowerLimit));
			_currentWorksheet.Rows[_currentRow].Cells[11].SetValue(Convert.ToDouble(row.UpperLimit));
			if(row.IsExcluded)
				_currentWorksheet.Rows[_currentRow].Cells[12].SetValue("Исключен");

			_currentRow++;
		}

		public void SetStyle()
		{
			int worksheetCount = _excelFile.Worksheets.Count;
			int countRows = _currentWorksheet.Rows.Count;
			int countColumns = _headersList.Count;
			int errCount = 0;
			int successCount = 0;
			for (var w = 0; w < worksheetCount; w++)
			{
				Log.ForContext("MarketSegment", _excelFile.Worksheets[w].Name)
					.Debug("Установка стилей в Excel таблице {CountRows} x {CountColumns}", countRows, countColumns);
				for (int i = 0; i < countRows; i++)
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
					MainRegisterId = OMCoreObject.GetRegisterId(),
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

		private void AddHeaders()
		{
			int col = 0;
			foreach (string value in _headersList)
			{
				_currentWorksheet.Rows[0].Cells[col].SetValue(value);
				col++;
			}

			_currentRow++;
		}

		private bool IsDecimalCell(int row, int col)
		{
			return row > 0 && (col == 3 || col >= 5 && col <= 10);
		}
	}
}
