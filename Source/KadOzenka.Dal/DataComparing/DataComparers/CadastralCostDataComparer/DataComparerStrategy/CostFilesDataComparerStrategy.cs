using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Messages;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataComparing.Configs;
using KadOzenka.Dal.DataComparing.Files;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataComparing.DataComparers.CadastralCostDataComparer.DataComparerStrategy
{
	public class CostFilesDataComparerStrategy : DataComparerStrategy
	{
		private static readonly ILogger _log = Log.ForContext<CostFilesDataComparerStrategy>();

		private List<CadastralCostKsFile> _rsmCostFiles;
		private List<CadastralCostKsFile> _pkkoCostFiles;
		private string _taskFolder;
		private OMTask _task;
		private MessageAddressersDto _messageAddresses;

		public CostFilesDataComparerStrategy(List<CadastralCostKsFile> rsmCostFiles, List<CadastralCostKsFile> pkkoCostFiles, string taskFolder, OMTask task, MessageAddressersDto messageAddresses)
		{
			_rsmCostFiles = rsmCostFiles;
			_pkkoCostFiles = pkkoCostFiles;
			_taskFolder = taskFolder;
			_task = task;
			_messageAddresses = messageAddresses;

		}

		public override void Compare()
		{
			_log.ForContext("TaskFolder", _taskFolder)
					.Debug("Загрузка данных из COST файлов");
			var rsmUnitCadastralCostDictionary = GetUnitCadastralCostDictionary(_rsmCostFiles);
			var pkkoUnitCadastralCostDictionary = GetUnitCadastralCostDictionary(_pkkoCostFiles);

			var areUnitSetsInconsistencies =
				!AreUnitSetsEqual(rsmUnitCadastralCostDictionary, pkkoUnitCadastralCostDictionary);
			var areUnitCostsInconsistencies = false;
			var resultExcelFile = new ExcelFile();
			if (areUnitSetsInconsistencies)
			{
				_log.ForContext("TaskFolder", _taskFolder)
					.Debug("Найдены несоответствия в наборах данных");

				FillUnitSetsInconsistenciesReport(rsmUnitCadastralCostDictionary, pkkoUnitCadastralCostDictionary, resultExcelFile,
					"Данные, отсутствующие в ПККО");
				FillUnitSetsInconsistenciesReport(pkkoUnitCadastralCostDictionary, rsmUnitCadastralCostDictionary, resultExcelFile,
					"Данные, отсутствующие в РСМ");
			}
			else
			{
				areUnitCostsInconsistencies =
					!AreUnitCostsEqual(rsmUnitCadastralCostDictionary, pkkoUnitCadastralCostDictionary);
				if (areUnitCostsInconsistencies)
				{
					_log.ForContext("TaskFolder", _taskFolder)
						.Debug("Найдены несоответствия в кадастровой стоимости");
					FillUnitCostsInconsistenciesReport(rsmUnitCadastralCostDictionary, pkkoUnitCadastralCostDictionary, resultExcelFile);
				}
				else
				{
					_log.ForContext("TaskFolder", _taskFolder)
						.Debug("Данные совпадают");
					resultExcelFile.Worksheets.Add("Данные совпадают");
				}
			}

			_task.CadastralCostComparingStatus_Code = !areUnitSetsInconsistencies && !areUnitCostsInconsistencies
				? KoDataComparingStatus.DataAreMatch
				: KoDataComparingStatus.ThereAreInconsistencies;
			_task.Save();

			_log.ForContext("TaskFolder", _taskFolder)
				.Debug("Сохранение отчета и перенос данных в результирующую папку");
			var ms = new MemoryStream();
			resultExcelFile.Save(ms, SaveOptions.XlsxDefault);
			ms.Seek(0, SeekOrigin.Begin);
			CadastralCostDataComparingStorageManager.SaveResultFile(_taskFolder, ms);
			CadastralCostDataComparingStorageManager.RemovePreviousCostFilesFromResult(_taskFolder);
			_rsmCostFiles.ForEach(x => CadastralCostDataComparingStorageManager.MoveFileToResultFolder(x, _taskFolder));
			_pkkoCostFiles.ForEach(x => CadastralCostDataComparingStorageManager.MoveFileToResultFolder(x, _taskFolder));

			NotificationSender.SendNotification("Сравнение протоколов кадастровой стоимости",
				CadastralCostDataComparingConfig.Current.GetCostEmailMessageForTask(_task, areUnitSetsInconsistencies,
					areUnitCostsInconsistencies),
				_messageAddresses);
		}

		private bool AreUnitSetsEqual(Dictionary<string, decimal?> x, Dictionary<string, decimal?> y)
		{
			if (x.Count != y.Count)
				return false;
			if (x.Keys.Except(y.Keys).Any())
				return false;
			if (y.Keys.Except(x.Keys).Any())
				return false;

			return true;
		}

		private bool AreUnitCostsEqual(Dictionary<string, decimal?> x, Dictionary<string, decimal?> y)
		{
			foreach (var pair in x)
				if (pair.Value != y[pair.Key])
					return false;
			return true;
		}

		private void FillUnitSetsInconsistenciesReport(Dictionary<string, decimal?> targetDictionary, Dictionary<string, decimal?> comparableDictionary, ExcelFile resultExcelFile, string resultWorksheetName)
		{
			resultExcelFile.Worksheets.Add(resultWorksheetName);
			DataExportCommon.AddRow(resultExcelFile.Worksheets[resultWorksheetName], 0, new object[] { "КН", "Кадастровая стоимость" });

			var missingUnitCadastralNumbers = targetDictionary.Keys.Except(comparableDictionary.Keys);
			var row = 1;
			foreach (var missingUnitCadastralNumber in missingUnitCadastralNumbers)
			{
				var value = targetDictionary[missingUnitCadastralNumber];
				DataExportCommon.AddRow(resultExcelFile.Worksheets[resultWorksheetName], row, new object[] { missingUnitCadastralNumber, GetDecimalString(value) });
				row++;
			}
		}

		private void FillUnitCostsInconsistenciesReport(Dictionary<string, decimal?> targetDictionary, Dictionary<string, decimal?> comparableDictionary, ExcelFile resultExcelFile)
		{
			resultExcelFile.Worksheets.Add("Различия в КС");
			DataExportCommon.AddRow(resultExcelFile.Worksheets["Различия в КС"], 0, new object[] { "КН", "Кадастровая стоимость в РСМ", "Кадастровая стоимость в ПККО" });
			var row = 1;
			foreach (var pair in targetDictionary)
			{
				if (pair.Value != comparableDictionary[pair.Key])
				{
					DataExportCommon.AddRow(resultExcelFile.Worksheets["Различия в КС"], row, new object[] { pair.Key, GetDecimalString(pair.Value), GetDecimalString(comparableDictionary[pair.Key]) });
					row++;
				}
			}
		}
	}
}
