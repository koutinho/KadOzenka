using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using ObjectModel.Directory;

namespace KadOzenka.Dal.DataComparing
{
	public enum SystemType { Unsupported, Rsm, Pkko}

	public class TaskChangesDataComparer : IDataComparer
	{
		private static readonly ILogger _log = Log.ForContext<TaskChangesDataComparer>();
		private const int KnColumnNumber = 0;
		private const int AttributeColumnNumber = 1;
		private const int NewValueColumnNumber = 3;

		public List<Tuple<FileInfo, FileInfo>> FindFilesPairs()
		{
			var allFiles = DataComparingStorageManager.GetAllFileNamesTaskChangesComparingDataFolder().Select(x => new FileInfo(x)).ToList();
			var rsmFiles = allFiles.Where(x => x.SystemType == SystemType.Rsm).ToList();
			var pkkoFiles = allFiles.Where(x => x.SystemType == SystemType.Pkko).ToList();
			
			var filePairs = new List<Tuple<FileInfo, FileInfo>>();
			foreach (var rsmFile in rsmFiles.DistinctBy(x => x.FileNameWithoutSystemSuffix))
			{
				var pkkoFile = pkkoFiles.FirstOrDefault(x =>
					x.FileNameWithoutSystemSuffix == rsmFile.FileNameWithoutSystemSuffix);
				if (pkkoFile != null)
					filePairs.Add(new Tuple<FileInfo, FileInfo>(rsmFile, pkkoFile));
			}

			return filePairs;
		}

		public void Compare(FileInfo rsmFileInfo, FileInfo pkkoFileInfo)
		{
			var rsmFile = DataComparingStorageManager.GetTaskChangesComparingData(rsmFileInfo.FileName);
			var pkkoFile = DataComparingStorageManager.GetTaskChangesComparingData(pkkoFileInfo.FileName);

			ExcelFile rsmExcel = ExcelFile.Load(rsmFile, LoadOptions.XlsxDefault);
			ExcelFile pkkoExcel = ExcelFile.Load(pkkoFile, LoadOptions.XlsxDefault);

			var resultExcelFile = new ExcelFile();
			Compare(resultExcelFile, "Данные, отсутствующие в ПККО", rsmExcel,pkkoExcel);
			Compare(resultExcelFile, "Данные, отсутствующие в РСМ", pkkoExcel, rsmExcel);

			var areProtocolsEqual = DataExportCommon.GetLastUsedRowIndex(resultExcelFile.Worksheets["Данные, отсутствующие в ПККО"]) == 0
			                            && DataExportCommon.GetLastUsedRowIndex(resultExcelFile.Worksheets["Данные, отсутствующие в РСМ"]) == 0;

			var task= rsmFileInfo.GetTaskInfoFromFileName();
			task.DataChangesComparingStatus_Code = areProtocolsEqual
				? KoDataComparingStatus.DataAreMatch
				: KoDataComparingStatus.ThereAreInconsistencies;
			task.Save();

			var ms = new MemoryStream();
			resultExcelFile.Save(ms, SaveOptions.XlsxDefault);
			ms.Seek(0, SeekOrigin.Begin);
			DataComparingStorageManager.SaveTaskChangesResult(ms, rsmFileInfo.FileNameWithoutSystemSuffix + DataComparingConfig.Current.NameSeparator + "Результат.xlsx");

			rsmFile.Dispose();
			pkkoFile.Dispose();
			DataComparingStorageManager.MoveComparingDataToResult(rsmFileInfo.FileName);
			DataComparingStorageManager.MoveComparingDataToResult(pkkoFileInfo.FileName);
		}

		private void Compare(ExcelFile resultExcelFile, string resultWorksheetName, ExcelFile targetFile, ExcelFile comparableFile)
		{
			resultExcelFile.Worksheets.Add(resultWorksheetName);
			DataExportCommon.AddRow(resultExcelFile.Worksheets[resultWorksheetName], 0, new object[] { "КН", "Атрибут", "Новое значение" });

			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 100
			};

			var targetLastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(targetFile.Worksheets[0]);
			var comparableLastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(comparableFile.Worksheets[0]);

			var resultRowCounter = 1;
			for (var i = 1; i <= targetLastUsedRowIndex; i++)
			{
				var targetKn = targetFile.Worksheets[0].Rows[i].Cells[KnColumnNumber].Value?.ToString();
				var targetAttribute = targetFile.Worksheets[0].Rows[i].Cells[AttributeColumnNumber].Value?.ToString();
				var targetNewValue = targetFile.Worksheets[0].Rows[i].Cells[NewValueColumnNumber].Value?.ToString();

				var isExists = false;
				Parallel.ForEach(comparableFile.Worksheets[0].Rows, options, (row, state) =>
				{
					if (row.Index != 0 && row.Index <= comparableLastUsedRowIndex)
					{
						var comparableKn = row.Cells[KnColumnNumber].Value?.ToString();
						var comparableAttribute = row.Cells[AttributeColumnNumber].Value?.ToString();
						var comparableNewValue = row.Cells[NewValueColumnNumber].Value?.ToString();

						if (targetKn == comparableKn && targetAttribute == comparableAttribute && targetNewValue == comparableNewValue)
						{
							isExists = true;
							state.Break();
						}
					}
				});

				if (!isExists)
				{
					DataExportCommon.AddRow(resultExcelFile.Worksheets[resultWorksheetName], resultRowCounter, new object[]
					{
						targetKn,
						targetAttribute,
						targetNewValue
					});
					resultRowCounter++;
				}
			}
		}
	}

}
