﻿using System;
using System.Collections.Generic;
using System.IO;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.SRD;
using DevExpress.CodeParser.Diagnostics;
using DevExpress.DataAccess.DataFederation;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
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
		private readonly Serilog.ILogger _log = Serilog.Log.ForContext<GbuReportService>();

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

		/// <summary>
		/// Отдает запрошенное количество строк для заполнения и увеличивает значения для след запроса
		/// </summary>
		/// <param name="rangeRows"></param>
		/// <returns></returns>
		public List<int> GetRangeRows(int rangeRows)
		{
			List<int> res = new List<int>();
			int tmpRangeRows = rangeRows;
			int counter = 0;
			while (_currentRow + tmpRangeRows >= _currentRow)
			{
				res.Add(_currentRow + counter);
				counter++;
				tmpRangeRows--;
			}

			_currentRow += rangeRows +1;// перводим указатель на следующую пустую строку
			return res;
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
				col++;
			}

			_currentRow++;

		}

		public void AddValue(string value, int column, int row)
		{
			try {
				_mainWorkSheet.Rows[row].Cells[column].SetValue(value);

				if (new Random().Next(0, 10000) > 9950)
					Serilog.Log.ForContext<ExcelFile>().Debug("Запись значения в Excel {row} {column} {value}", row, column, value);
			}
			catch (Exception ex) {
				if (new Random().Next(0, 100) > 80)
					Serilog.Log.ForContext<ExcelFile>().Error(ex, "Ошибка записи значения в Excel {row} {column} {value}", row, column, value);
            }
		}

        public void AddRow(List<string> values)
        {
            for (var i = 0; i < values.Count; i++)
            {
                _mainWorkSheet.Rows[_currentRow].Cells[i].SetValue(values[i]);
            }

            _currentRow++;
        }

        public void SetStyle()
		{
			int countRows = _mainWorkSheet.Rows.Count;
			int countColumns = _mainWorkSheet.CalculateMaxUsedColumns();
			int errCount = 0;
			int successCount = 0;
			_log.Debug("Установка стилей в Excel таблице {countRows} x {countColumns}", countRows, countColumns);
			for (int i = 0; i < countRows; i++)
            {
                for (int j = 0; j < countColumns; j++)
                {
                    if (_mainWorkSheet.Rows[i] != null && _mainWorkSheet.Rows[i].Cells[j] != null)
                    {
                        try
                        {
                            _mainWorkSheet.Rows[i].Cells[j].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                            _mainWorkSheet.Rows[i].Cells[j].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
                            _mainWorkSheet.Rows[i].Cells[j].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
                            _mainWorkSheet.Rows[i].Cells[j].Style.WrapText = true;
							
							if (successCount < 5)
								_log.Debug("Применение стилей в Excel {mainWorkSheetRow} {mainWorkSheetCell}", i, j);
							successCount++;
						}
                        catch (Exception ex)
                        {
							if (errCount < 5)
								_log.Error(ex, "Ошибка применения стилей в Excel {mainWorkSheetRow} {mainWorkSheetCell}", i, j);
							errCount++;
						}
                    }
                }
            }
			_log.Debug("Применение стилей в Excel завершено {successCount} {errCount}", successCount, errCount);
		}

		public void SetIndividualWidth(int column, int width)
		{
			_log.Debug("Установка ширины {width} для столбца {column}", width, column);
			_mainWorkSheet.Columns[column].SetWidth(width, LengthUnit.Centimeter);
		}

		public long SaveReport(string fileName, long? mainRegisterId = null, string registerViewId = null)
		{
			try
			{
				_log.Debug("Сохранение отчета {fileName}", fileName);
				MemoryStream stream = new MemoryStream();
				_excelTemplate.Save(stream, SaveOptions.XlsxDefault);
				stream.Seek(0, SeekOrigin.Begin);

				var currentDate = DateTime.Now;
				var export = new OMExportByTemplates
				{
					UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
					DateCreated = currentDate,
					DateStarted = currentDate,
					Status = (int)ImportStatus.Added,
					FileResultTitle = fileName,
					FileExtension = "xlsx",
					MainRegisterId = mainRegisterId.HasValue ? mainRegisterId.Value : OMMainObject.GetRegisterId(),
					RegisterViewId = !string.IsNullOrEmpty(registerViewId) ? registerViewId : "GbuObjects"
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
				_log.Error(e, "Сохранение отчета завершилось исключением");
				//Console.WriteLine(e);
				ErrorManager.LogError(e);
				throw;
			}
		}
	}
}