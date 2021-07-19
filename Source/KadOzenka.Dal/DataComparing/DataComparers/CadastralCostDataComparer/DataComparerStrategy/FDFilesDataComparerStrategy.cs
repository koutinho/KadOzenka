using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommonSdks;
using Core.Messages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataComparing.Configs;
using KadOzenka.Dal.DataComparing.Files;
using KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.LongProcess;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataComparing.DataComparers.CadastralCostDataComparer.DataComparerStrategy
{
	public class FDFilesDataComparerStrategy : DataComparerStrategy
	{
		private static readonly ILogger _log = Log.ForContext<FDFilesDataComparerStrategy>();

		private List<CadastralCostFDFile> _rsmFdFiles;
		private List<CadastralCostFDFile> _pkkoFdFiles;
		private string _taskFolder;
		private OMTask _task;
		private MessageAddressersDto _messageAddresses;

		public FDFilesDataComparerStrategy(List<CadastralCostFDFile> rsmFdFiles, List<CadastralCostFDFile> pkkoFdFiles, string taskFolder, OMTask task, MessageAddressersDto messageAddresses)
		{
			_rsmFdFiles = rsmFdFiles;
			_pkkoFdFiles = pkkoFdFiles;
			_taskFolder = taskFolder;
			_task = task;
			_messageAddresses = messageAddresses;
		}

		public override void Compare()
		{
			var rsmCostFilesFromResult = CadastralCostDataComparingStorageManager
						.GetTaskCostFilesFromResult(_taskFolder, SystemType.Rsm)
						.Select(x => new CadastralCostKsFile(x, SystemType.Rsm)).ToList();
			var pkkoCostFilesFromResult = CadastralCostDataComparingStorageManager
				.GetTaskCostFilesFromResult(_taskFolder, SystemType.Pkko)
				.Select(x => new CadastralCostKsFile(x, SystemType.Pkko)).ToList();

			if(rsmCostFilesFromResult.IsEmpty() && pkkoCostFilesFromResult.IsEmpty())
				throw new Exception("Не удалось получить COST файлы из директории результата сравнения для проведения дополнительного анализа FD файлов");

			_log.ForContext("TaskFolder", _taskFolder)
				.ForContext("RsmCostFilesFromResult", rsmCostFilesFromResult.Select(x => x.FullName).ToList())
				.ForContext("PkkoCostFilesFromResult", pkkoCostFilesFromResult.Select(x => x.FullName).ToList())
				.Debug("Получена информация о COST файлах из результата сравнения для дополнительного анализа");

			var rsmUnitCadastralCostDictionary = GetUnitCadastralCostDictionary(rsmCostFilesFromResult);
			var pkkoUnitCadastralCostDictionary = GetUnitCadastralCostDictionary(pkkoCostFilesFromResult);

			var resultFdExcelFile = new ExcelFile();
			FillInconsistenciesReport(rsmUnitCadastralCostDictionary, pkkoUnitCadastralCostDictionary, resultFdExcelFile);

			_log.ForContext("TaskFolder", _taskFolder)
				.Debug("Сохранение отчета и перенос данных в результирующую папку");
			var fdMs = new MemoryStream();
			resultFdExcelFile.Save(fdMs, SaveOptions.XlsxDefault);
			fdMs.Seek(0, SeekOrigin.Begin);
			CadastralCostDataComparingStorageManager.SaveResultFile(_taskFolder, fdMs, true);
			CadastralCostDataComparingStorageManager.RemovePreviousFDFilesFromResult(_taskFolder);
			_rsmFdFiles.ForEach(x => CadastralCostDataComparingStorageManager.MoveFileToResultFolder(x, _taskFolder));
			_pkkoFdFiles.ForEach(x => CadastralCostDataComparingStorageManager.MoveFileToResultFolder(x, _taskFolder));

			new NotificationSender().SendNotification("Сравнение протоколов кадастровой стоимости (FD файлы)",
				CadastralCostDataComparingConfig.Current.GetFDEmailMessageForTask(_task),
				_messageAddresses);
		}

		private void FillInconsistenciesReport(Dictionary<string, decimal?> rsmUnitCadastralCostDictionary,
			Dictionary<string, decimal?> pkkoUnitCadastralCostDictionary, ExcelFile resultExcelFile)
		{
			var worksheet = resultExcelFile.Worksheets.Add("Расхождения в КС");

			_log.ForContext("TaskFolder", _taskFolder).Debug("Загрузка данных из FD РСМ файлов");
			var rsmUnitInfoDictionary = GetUnitInfoDictionary(_rsmFdFiles);

			_log.ForContext("TaskFolder", _taskFolder).Debug("Загрузка данных из FD ПККО файлов");
			var pkkoUnitInfoDictionary = GetUnitInfoDictionary(_pkkoFdFiles);

			_log.ForContext("TaskFolder", _taskFolder).Debug("Формирование отчета");
			var intersectedUnitCadastralNumbers =
				rsmUnitCadastralCostDictionary.Keys.Intersect(pkkoUnitCadastralCostDictionary.Keys);
			var row = 0;
			foreach (var unitCadastralNumber in intersectedUnitCadastralNumbers)
			{
				if (rsmUnitCadastralCostDictionary[unitCadastralNumber] != pkkoUnitCadastralCostDictionary[unitCadastralNumber])
				{
					var mergeRow =
						worksheet.Cells.GetSubrangeAbsolute(row, 0, row, 3);
					mergeRow.Merged = true;
					mergeRow.Style.Font.Weight = 600;
					mergeRow.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
					mergeRow.Style.Borders.SetBorders(MultipleBorders.Top, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
					mergeRow.Value = unitCadastralNumber;
					row++;
					worksheet.Rows[row].Cells[0].SetValue("КС РСМ");
					worksheet.Rows[row].Cells[0].Style.Font.Weight = 600;
					worksheet.Rows[row].Cells[1].SetValue(GetDecimalString(rsmUnitCadastralCostDictionary[unitCadastralNumber]));
					worksheet.Rows[row].Cells[2].SetValue("КС ПККО");
					worksheet.Rows[row].Cells[2].Style.Font.Weight = 600;
					worksheet.Rows[row].Cells[3].SetValue(GetDecimalString(pkkoUnitCadastralCostDictionary[unitCadastralNumber]));
					row++;

					var rmsRow = row;
					var pkkoRow = row;
					rsmUnitInfoDictionary.TryGetValue(unitCadastralNumber, out var rmsUnit);
					if (rmsUnit != null)
						rmsRow = rmsUnit.FillExcelFile(worksheet, row, SystemType.Rsm);
					pkkoUnitInfoDictionary.TryGetValue(unitCadastralNumber, out var pkkoUnit);
					if (pkkoUnit != null)
						pkkoRow = pkkoUnit.FillExcelFile(worksheet, row, SystemType.Pkko);

					row = Math.Max(rmsRow, pkkoRow);
				}
			}

			var maxColIndex = CommonSdks.DataExportCommon.GetLastUsedColumnIndex(worksheet);
			for (int i = 0; i <= maxColIndex; i++)
			{
				worksheet.Columns[i].SetWidth(6, LengthUnit.Centimeter);
			}
		}

		private Dictionary<string, Files.CadastralCostFDFile.UnitInfo.UnitInfo> GetUnitInfoDictionary(List<CadastralCostFDFile> fdFiles)
		{
			var dictionary = new Dictionary<string, Files.CadastralCostFDFile.UnitInfo.UnitInfo>();
			foreach (var fdFile in fdFiles)
			{
				fdFile.LoadFileFromStorage();
				fdFile.FillUnitFactorsDictionary(dictionary);
			}

			return dictionary;
		}
	}
}
