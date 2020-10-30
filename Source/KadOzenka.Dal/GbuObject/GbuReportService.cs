using System;
using System.Collections.Generic;
using System.IO;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.Gbu;
using System.Text;
using Ionic.Zip;

namespace KadOzenka.Dal.GbuObject
{
	public class GbuReportService
	{
		public string UrlToDownload => $"/DataExport/DownloadExportResult?exportId={ReportId}";
		public CellStyle WarningCellStyle { get; private set; }
		public CellStyle ErrorCellStyle { get; private set; }
		private readonly Serilog.ILogger _log = Serilog.Log.ForContext<GbuReportService>();
		private const int MaxRowsCountInSheet = 1000000;

		private List<ExcelFile> _excelFiles;
		private Row CurrentRow { get; set; }
		private long ReportId { get; set; }
		private readonly List<Column> _columnsWidth;
		private List<string> _headers;


		public GbuReportService()
		{
			_excelFiles = new List<ExcelFile>();
			_headers = new List<string>();
			_columnsWidth = new List<Column>();

			WarningCellStyle = new CellStyle();
			WarningCellStyle.FillPattern.SetPattern(FillPatternStyle.Solid, SpreadsheetColor.FromName(ColorName.Yellow),
				SpreadsheetColor.FromName(ColorName.Black));

			ErrorCellStyle = new CellStyle();
			ErrorCellStyle.FillPattern.SetPattern(FillPatternStyle.Solid, SpreadsheetColor.FromName(ColorName.Red),
				SpreadsheetColor.FromName(ColorName.Black));

			CreateFile();
		}


		/// <summary>
		/// Отдает текущий номер строки и инкрементирует значение для следующего обращения 
		/// </summary>
		/// <returns></returns>
		public Row GetCurrentRow()
		{
			ValidateRowsCountInSheet();

			var tmpRow = CurrentRow.Copy();

			CurrentRow.Index++;

			return tmpRow;
		}

		/// <summary>
		/// Отдает запрошенное количество строк для заполнения и увеличивает значения для след запроса
		/// </summary>
		/// <param name="rangeRows"></param>
		/// <returns></returns>
		public List<Row> GetRangeRows(int rangeRows)
		{
			var res = new List<Row>();
			var tmpRangeRows = rangeRows;
			while (CurrentRow.Index + tmpRangeRows >= CurrentRow.Index)
			{
				res.Add(CurrentRow.Copy());
				CurrentRow.Index++;
				tmpRangeRows--;
			}

			return res;
		}

		public void AddHeaders(List<string> values)
		{
			var rowIndex = 0;
			int columnIndex = 0;
			var sheet = CurrentRow.File.Worksheets[0];
			foreach (string value in values)
			{
				sheet.Rows[rowIndex].Cells[columnIndex].SetValue(value);
				sheet.Rows[rowIndex].Cells[columnIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
				sheet.Rows[rowIndex].Cells[columnIndex].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
				sheet.Rows[rowIndex].Cells[columnIndex].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
				sheet.Rows[rowIndex].Cells[columnIndex].Style.WrapText = true;
				columnIndex++;
			}

			_headers = values;
			CurrentRow.Index++;
		}

		public void AddValue(string value, int column, Row row, CellStyle cellStyle = null)
		{
			try
			{
				var cell = row.File.Worksheets[0].Rows[row.Index].Cells[column];

				cell.SetValue(value);

				if (cellStyle != null)
					cell.Style = cellStyle;

				if (new Random().Next(0, 10000) > 9950)
					Serilog.Log.ForContext<ExcelFile>().Verbose("Запись значения в Excel. Строка {Row}, столбец {Column}, значение {Value}", row.Index, column, value);
			}
			catch (Exception ex)
			{
				if (new Random().Next(0, 100) > 80)
					Serilog.Log.ForContext<ExcelFile>().Warning(ex, "Ошибка записи значения в Excel. Строка {Row}, столбец {Column}, значение {Value}", row.Index, column, value);
			}
		}

		public void AddRow(Row row, List<string> values)
		{
			for (var i = 0; i < values.Count; i++)
			{
				row.File.Worksheets[0].Rows[row.Index].Cells[i].SetValue(values[i]);
			}
		}

		public void SetStyle()
		{
			for (var fileCounter = 0; fileCounter < _excelFiles.Count; fileCounter++)
			{
				var sheet = _excelFiles[fileCounter].Worksheets[0];

				int countRows = sheet.Rows.Count;
				int countColumns = sheet.CalculateMaxUsedColumns();
				int errCount = 0;
				int successCount = 0;
				_log.Debug("Установка стилей в Excel для файла №{FileCounter}. В таблице {countRows} x {countColumns}", fileCounter, countRows, countColumns);
				
				for (int i = 0; i < countRows; i++)
				{
					for (int j = 0; j < countColumns; j++)
					{
						if (sheet.Rows[i] != null && sheet.Rows[i].Cells[j] != null)
						{
							try
							{
								sheet.Rows[i].Cells[j].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
								sheet.Rows[i].Cells[j].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
								sheet.Rows[i].Cells[j].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
								sheet.Rows[i].Cells[j].Style.WrapText = true;

								if (successCount < 5)
									_log.Verbose("Применение стилей в Excel {mainWorkSheetRow} {mainWorkSheetCell}", i, j);
								successCount++;
							}
							catch (Exception ex)
							{
								if (errCount < 5)
									_log.Warning(ex, "Ошибка применения стилей в Excel {mainWorkSheetRow} {mainWorkSheetCell}", i, j);
								errCount++;
							}
						}
					}
				}

				_log.Debug("Применение стилей в Excel для файла №{FileCounter} завершено. Успешно {successCount}, с ошибкой {errCount}", fileCounter, successCount, errCount);
			}
		}

		public void SetIndividualWidth(int column, int width, bool saveWidth = true)
		{
			_log.Verbose("Установка ширины {width} для столбца {column}", width, column);

			_excelFiles.ForEach(x =>
			{
				x.Worksheets[0].Columns[column].SetWidth(width, LengthUnit.Centimeter);
			});

			if (saveWidth)
			{
				_columnsWidth.Add(new Column { Index = column, Width = width });
			}
		}

		public long SaveReport(string fileName, long? mainRegisterId = null, string registerViewId = null)
		{
			return _excelFiles.Count == 1 
				? SaveReportXlsx(fileName, mainRegisterId, registerViewId) 
				: SaveReportZip(fileName, mainRegisterId, registerViewId);
		}


		#region Support Methods

		private void CreateFile()
		{
			_log.Debug("Создание нового файла");

			var newFile = new ExcelFile();
			
			var sheet = newFile.Worksheets.Add("Лист 1");
			sheet.Cells.Style.Font.Name = "Times New Roman";
			_excelFiles.Add(newFile);

			CurrentRow = new Row
			{
				File = newFile
			};
		}

		private void ValidateRowsCountInSheet()
		{
			if (CurrentRow.Index > MaxRowsCountInSheet)
			{
				CreateFile();

				AddHeaders(_headers);

				_columnsWidth.ForEach(x => SetIndividualWidth(x.Index, x.Width, false));
			}
		}

		private long SaveReportXlsx(string fileName, long? mainRegisterId = null, string registerViewId = null)
		{
			_log.Debug("Начато сохранение отчета через xlsx");

			try
			{
				MemoryStream stream = new MemoryStream();
				_excelFiles[0].Save(stream, SaveOptions.XlsxDefault);
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

				_log.ForContext("ResultFileName", export.ResultFileName)
					.ForContext("FileId", export.Id)
					.Debug("Закончено сохранение отчета {FileName}", fileName);

				ReportId = export.Id;

				return ReportId;
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Сохранение отчета завершилось исключением");
				ErrorManager.LogError(ex);
				throw;
			}
		}

		private long SaveReportZip(string fileName, long? mainRegisterId = null, string registerViewId = null)
		{
			_log.Debug("Сохранение отчета через zip");

			try
			{
				using (var zipFile = new ZipFile())
				{
					zipFile.AlternateEncoding = Encoding.UTF8;
					zipFile.AlternateEncodingUsage = ZipOption.AsNecessary;

					for (int i = 0; i < _excelFiles.Count; i++)
					{
						var currentFileName = $"{fileName} {i + 1}.xlsx";

						_log.Debug($"Начато добавление файла '{currentFileName}' в zip");

						var stream = new MemoryStream();
						_excelFiles[i].Save(stream, SaveOptions.XlsxDefault);
						stream.Seek(0, SeekOrigin.Begin);

						zipFile.AddEntry(currentFileName, stream);

						_log.Debug($"Закончено добавление файла '{currentFileName}' в zip");
					}

					var zipStream = new MemoryStream();
					zipFile.Save(zipStream);
					zipStream.Seek(0, SeekOrigin.Begin);
					var zipFileName = "Результаты переноса атрибутов";
					var registerId = mainRegisterId.HasValue ? mainRegisterId.Value : OMMainObject.GetRegisterId();
					var resultRegisterViewId = !string.IsNullOrEmpty(registerViewId) ? registerViewId : "GbuObjects";

					_log.Debug($"Начато сохранение zip-файла '{zipFileName}'");
					ReportId = SaveReportDownload.SaveReport(zipFileName, zipStream, registerId, resultRegisterViewId, "zip");
					_log.Debug($"Закончено сохранение zip-файла '{zipFileName}'");
				}

				return ReportId;
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Сохранение отчета завершилось исключением");
				ErrorManager.LogError(ex);
				throw;
			}
		}

		#endregion


		#region Entities

		public class Column
		{
			public int Index { get; set; }
			public string Header { get; set; }
			public int Width { get; set; }
		}

		public class Row
		{
			public int Index;
			public ExcelFile File;

			public Row Copy()
			{
				return new Row
				{
					Index = Index,
					File = File
				};
			}
		}

		#endregion
	}
}