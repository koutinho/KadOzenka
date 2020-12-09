﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataComparing.Configs;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;

namespace KadOzenka.Dal.DataComparing.Files
{
	public class TaskChangesFile : File
	{
		private const int KnColumnNumber = 0;
		private const int NewValueColumnNumber = 5;
		private const int ChangingColumnNumber = 6;

		public ExcelFile ExcelFile { get; private set; }

		public string FileNameWithoutSystemSuffix { get; }


		public TaskChangesFile(string fileName): base(fileName)
		{
			SystemType = FileNameWithoutExtension.EndsWith(TaskChangesDataComparingConfig.Current.HandleFileName(TaskChangesDataComparingConfig.Current.RsmSuffix))
				? SystemType.Rsm
				: FileNameWithoutExtension.EndsWith(TaskChangesDataComparingConfig.Current.HandleFileName(TaskChangesDataComparingConfig.Current.PkkoSuffix))
					? SystemType.Pkko
					: throw new Exception($"Не удалось определить тип системы по суффиксу имени файла: {FileNameWithoutExtension}");

			FileNameWithoutSystemSuffix = FileNameWithoutExtension.TrimEnd(SystemType == SystemType.Rsm
				? TaskChangesDataComparingConfig.Current.NameSeparator + TaskChangesDataComparingConfig.Current.HandleFileName(TaskChangesDataComparingConfig.Current.RsmSuffix)
				: TaskChangesDataComparingConfig.Current.NameSeparator + TaskChangesDataComparingConfig.Current.HandleFileName(TaskChangesDataComparingConfig.Current.PkkoSuffix));
		}

		public void LoadFileFromStorage()
		{
			var fileStream = TaskChangesDataComparingStorageManager.GetComparingDataFile(Name);
			ExcelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
			fileStream.Dispose();
		}

		public void Compare(TaskChangesFile comparableFile, ExcelFile resultExcelFile, string resultWorksheetName)
		{
			resultExcelFile.Worksheets.Add(resultWorksheetName);
			DataExportCommon.AddRow(resultExcelFile.Worksheets[resultWorksheetName], 0, new object[] { "КН", "Новое значение", "Изменение" });

			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 100
			};

			var targetLastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(ExcelFile.Worksheets[0]);
			var comparableLastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(comparableFile.ExcelFile.Worksheets[0]);

			var resultRowCounter = 1;
			for (var i = 1; i <= targetLastUsedRowIndex; i++)
			{
				var targetKn = ExcelFile.Worksheets[0].Rows[i].Cells[KnColumnNumber].Value?.ToString();
				var targetNewValue = ExcelFile.Worksheets[0].Rows[i].Cells[NewValueColumnNumber].Value?.ToString();
				var targetChanging = ExcelFile.Worksheets[0].Rows[i].Cells[ChangingColumnNumber].Value?.ToString();

				var isExists = false;
				Parallel.ForEach(comparableFile.ExcelFile.Worksheets[0].Rows, options, (row, state) =>
				{
					if (row.Index != 0 && row.Index <= comparableLastUsedRowIndex)
					{
						var comparableKn = row.Cells[KnColumnNumber].Value?.ToString();
						var comparableNewValue = row.Cells[NewValueColumnNumber].Value?.ToString();
						var comparableChanging = row.Cells[ChangingColumnNumber].Value?.ToString();

						if (targetKn == comparableKn && targetNewValue == comparableNewValue && targetChanging == comparableChanging)
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
						targetChanging,
						targetNewValue
					});
					resultRowCounter++;
				}
			}
		}
	}
}
