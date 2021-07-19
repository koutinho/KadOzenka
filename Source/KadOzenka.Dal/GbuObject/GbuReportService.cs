using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Main.FileStorages;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using System.Text;
using Ionic.Zip;
using KadOzenka.Dal.GbuObject.Dto;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.GbuObject
{
	public class GbuReportService : IDisposable, IGbuReportService
	{
		public string DefaultExtension => "xlsx";
		public string FileStorageKey => "GbuOperationsReportsPath";
		public bool IsReportEmpty { get; protected set; } = true;
		public static readonly int MaxRowsCountInSheet = 1000000;

		private readonly Serilog.ILogger _log = Serilog.Log.ForContext<GbuReportService>();
		

		public CellStyle WarningCellStyle { get; }
		public CellStyle ErrorCellStyle { get; }

		protected ExcelFile _curretExcelFile;
		private int _fileCounter = 0;
		private string _fileName;
		private ZipFile _zipFile;
		protected Row CurrentRow { get; set; }
		private readonly List<Column> _columnsWidth;
		private List<string> _headers;
		private bool _applyStyle;


		public GbuReportService(string fileName, bool applyStyle = true)
		{
			_fileName = fileName;
			_applyStyle = applyStyle;
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

		public GbuReportService()
		{
			
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

		public void SetIndividualWidth(int column, int width, bool saveWidth = true)
		{
			_log.Verbose("Установка ширины {width} для столбца {column}", width, column);

			_curretExcelFile.Worksheets[0].Columns[column].SetWidth(width, LengthUnit.Centimeter);
			if (saveWidth)
			{
				_columnsWidth.Add(new Column { Index = column, Width = width });
			}
		}

		public void SetIndividualWidth(List<Column> columns)
		{
			_log.Verbose("Установка ширины для нескольких столбцов");

			columns.ForEach(x => { SetIndividualWidth(x.Index, x.Width); });
		}

		public void AddHeaders(List<Column> columns)
		{
			var headers = columns.Select(x => x.Header).ToList();
			AddHeaders(headers);
		}

		public void AddValue(string value, int column, Row row, CellStyle cellStyle = null)
		{
			try
			{
				var cell = row.File.Worksheets[0].Rows[row.Index].Cells[column];

				cell.SetValue(value);

				if (cellStyle != null)
					cell.Style = cellStyle;
				
				IsReportEmpty = false;

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
			IsReportEmpty = false;
		}

		public void AddRow(Row row, List<object> values)
		{
			CommonSdks.DataExportCommon.AddRow(row.File.Worksheets[0], row.Index, values.ToArray());
			IsReportEmpty = false;
		}

		public long SaveReport()
		{
			return _zipFile == null 
				? SaveReportXlsx() 
				: SaveReportZip();
		}

		public ReportFile GetReportFile()
		{
			ReportFile file = new ReportFile();
			if (_zipFile == null)
			{
				file.FileName = $"{_fileName}.{DefaultExtension}";
				file.FileStream = new MemoryStream();
				_curretExcelFile.Save(file.FileStream, SaveOptions.XlsxDefault);
				file.FileStream.Seek(0, SeekOrigin.Begin);
			}
			else
			{
				file.FileName = $"{_fileName}.zip";
				file.FileStream = CreateZipMemoryStream();
			}

			return file;
		}

		public void Dispose()
		{
			_zipFile?.Dispose();
		}

		public string GetUrlToDownloadFile(long reportId)
		{
			//TODO Dal не должен знать о контроллере
			return $"/GbuOperationsReports/Download?reportId={reportId}";
		}


		public ReportInfo GetFile(long reportId)
		{
			var report = GetFileInfo(reportId);

			var fileStream = FileStorageManager.GetFileStream(FileStorageKey, report.DateOnServer, report.FileNameOnServer);

			return new ReportInfo
			{
				Stream = fileStream,
				FullFileName = report.FileName
			};
		}

		public OMGbuOperationsReports GetFileInfo(long reportId)
		{
			var report = OMGbuOperationsReports.Where(x => x.Id == reportId).SelectAll().ExecuteFirstOrDefault();
			if (report == null)
				throw new Exception($"В журнале с сохраненными отчетами не найдена запись с ИД '{reportId}'");

			return report;
		}

		#region Support Methods

		private void AddCurrentFileIntoZip()
		{
			if(_zipFile == null)
			{
				_zipFile = new ZipFile
				{
					AlternateEncoding = Encoding.UTF8, AlternateEncodingUsage = ZipOption.AsNecessary
				};
			}

			if (_applyStyle)
				SetStyle();

			var currentFileName = $"{_fileName} {_fileCounter + 1}.xlsx";
			_log.Debug($"Начато добавление файла '{currentFileName}' в zip");

			var stream = new MemoryStream();
			_curretExcelFile.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			_zipFile.AddEntry(currentFileName, stream);
			_log.Debug($"Закончено добавление файла '{currentFileName}' в zip");

			_curretExcelFile = null;
			_fileCounter++;
		}

		private void SetStyle()
		{
			var sheet = _curretExcelFile.Worksheets[0];

			int countRows = sheet.Rows.Count;
			int countColumns = sheet.CalculateMaxUsedColumns();
			int errCount = 0;
			int successCount = 0;
			_log.Debug("Установка стилей в Excel. В таблице {countRows} x {countColumns}", countRows, countColumns);

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
		}

		protected virtual void CreateFile()
		{
			_log.Debug("Создание нового файла");

			_curretExcelFile = new ExcelFile();
			var sheet = _curretExcelFile.Worksheets.Add("Лист 1");
			sheet.Cells.Style.Font.Name = "Times New Roman";

			CurrentRow = new Row
			{
				File = _curretExcelFile
			};
		}

		private void ValidateRowsCountInSheet()
		{
			if (CurrentRow.Index > MaxRowsCountInSheet)
			{
				AddCurrentFileIntoZip();
				CreateFile();
				AddHeaders(_headers);
				_columnsWidth.ForEach(x => SetIndividualWidth(x.Index, x.Width, false));
			}
		}

		private long SaveReportXlsx()
		{
			_log.Debug("Начато сохранение отчета через xlsx");

			var stream = new MemoryStream();
			_curretExcelFile.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			var exportId = SaveReportGeneral(stream, _fileName, "xlsx");
			_log.Debug("Закончено сохранение отчета через xlsx");

			return exportId;
		}

		private long SaveReportZip()
		{
			_log.Debug("Сохранение отчета через zip");

			var zipStream = CreateZipMemoryStream();

			long exportId;
			using (_log.TimeOperation("Сохранение zip-файла"))
			{
				exportId = SaveReportGeneral(zipStream, _fileName, "zip");
			}
			_log.Debug("Закончено сохранение отчета через zip");

			return exportId;
		}

		private long SaveReportGeneral(Stream stream, string fileName, string fileExtension)
		{
			_log.Debug("Начато сохранение отчета");

			var export = new OMGbuOperationsReports
			{
				UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
				CreationDate = DateTime.Now,
				Status_Code = ExportStatus.Added,
				FileName = $"{fileName}.{fileExtension}"
			};
			try
			{
				export.Save();

				FileStorageManager.Save(stream, FileStorageKey, export.DateOnServer, export.FileNameOnServer);

				SaveExportResult(export, ExportStatus.Completed);

				_log.ForContext("ResultFileName", export.FileName)
					.ForContext("FileId", export.Id)
					.Debug("Закончено сохранение отчета {FileName}", _fileName);

				return export.Id;
			}
			catch (Exception ex)
			{
				SaveExportResult(export, ExportStatus.Faulted);
				_log.Error(ex, "Сохранение отчета завершилось с ошибкой");
				throw;
			}
		}

		private void SaveExportResult(OMGbuOperationsReports export, ExportStatus status)
		{
			export.Status_Code = status;
			export.FinishDate = DateTime.Now;
			export.Save();
		}

		private MemoryStream CreateZipMemoryStream()
		{
			if(_curretExcelFile != null)
				AddCurrentFileIntoZip();

			MemoryStream zipStream = new MemoryStream();
			_zipFile.Save(zipStream);
			zipStream.Seek(0, SeekOrigin.Begin);

			return zipStream;
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

		public class ReportFile
		{
			public MemoryStream FileStream { get; set; }
			public string FileName { get; set; }
		}

		#endregion
	}
}