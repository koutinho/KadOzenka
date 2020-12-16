using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Messages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataComparing.Configs;
using KadOzenka.Dal.DataComparing.Files;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.LongProcess;
using ObjectModel.Directory;
using Serilog;
using TaskChangesFile = KadOzenka.Dal.DataComparing.Files.TaskChangesFile;

namespace KadOzenka.Dal.DataComparing.DataComparers
{
	public class TaskChangesDataComparer : IDataComparer
	{
		private static readonly ILogger _log = Log.ForContext<TaskChangesDataComparer>();

		public void PerformProc(MessageAddressersDto messageAddresses)
		{
			var filePairs = FindFilesPairs();
			_log.Debug($"Найдено {filePairs.Count} пар файлов");
			foreach (var filePair in filePairs)
			{
				try
				{
					_log.Debug("Выполняется сравнение файлов '{RsmFileName}' и '{PkkoFileName}'", filePair.Item1.Name, filePair.Item2.Name);
					Compare(filePair.Item1, filePair.Item2, messageAddresses);
				}
				catch (Exception ex)
				{
					_log.Error(ex, "Ошибка при сравнении файлов '{RsmFileName}' и '{PkkoFileName}'", filePair.Item1.Name, filePair.Item2.Name);
					NotificationSender.SendNotification("Сравнение протоколов загрузки",
						$"В ходе сравнения файлов '{filePair.Item1.Name}' и '{filePair.Item2.Name}' произошла ошибка: {ex.Message}", messageAddresses);
				}
			}
			_log.Debug("Обработка завершена");
		}

		private List<Tuple<TaskChangesFile, TaskChangesFile>> FindFilesPairs()
		{
			var allFiles = TaskChangesDataComparingStorageManager.GetAllComparingDataFileNames().Select(x => new TaskChangesFile(x)).ToList();
			var rsmFiles = allFiles.Where(x => x.SystemType == SystemType.Rsm).ToList();
			var pkkoFiles = allFiles.Where(x => x.SystemType == SystemType.Pkko).ToList();
			
			var filePairs = new List<Tuple<TaskChangesFile, TaskChangesFile>>();
			foreach (var rsmFile in rsmFiles.DistinctBy(x => x.FileNameWithoutSystemSuffix))
			{
				var pkkoFile = pkkoFiles.FirstOrDefault(x =>
					x.FileNameWithoutSystemSuffix == rsmFile.FileNameWithoutSystemSuffix);
				if (pkkoFile != null)
					filePairs.Add(new Tuple<TaskChangesFile, TaskChangesFile>(rsmFile, pkkoFile));
			}

			return filePairs;
		}

		private void Compare(TaskChangesFile rsmFileInfo, TaskChangesFile pkkoFileInfo, MessageAddressersDto messageAddresses)
		{
			_log.ForContext("RsmFileName", rsmFileInfo.Name)
				.ForContext("PkkoFileName", pkkoFileInfo.Name)
				.Debug("Загрузка данных из файлов протоколов загрузки");
			rsmFileInfo.LoadFileFromStorage();
			pkkoFileInfo.LoadFileFromStorage();

			_log.ForContext("RsmFileName", rsmFileInfo.Name)
				.ForContext("PkkoFileName", pkkoFileInfo.Name)
				.Debug("Формирование отчета");
			var resultExcelFile = new ExcelFile();
			rsmFileInfo.Compare(pkkoFileInfo, resultExcelFile, "Данные, отсутствующие в ПККО");
			pkkoFileInfo.Compare(rsmFileInfo, resultExcelFile, "Данные, отсутствующие в РСМ");

			var areProtocolsEqual = DataExportCommon.GetLastUsedRowIndex(resultExcelFile.Worksheets["Данные, отсутствующие в ПККО"]) == 0
			                            && DataExportCommon.GetLastUsedRowIndex(resultExcelFile.Worksheets["Данные, отсутствующие в РСМ"]) == 0;

			_log.ForContext("RsmFileName", rsmFileInfo.Name)
				.ForContext("PkkoFileName", pkkoFileInfo.Name)
				.Debug("Поиск и сохранение изменений для единицы оценки");
			var task = TaskChangesDataComparingConfig.Current.GetTaskInfoFromFileName(rsmFileInfo);
			task.DataChangesComparingStatus_Code = areProtocolsEqual
				? KoDataComparingStatus.DataAreMatch
				: KoDataComparingStatus.ThereAreInconsistencies;
			task.Save();

			_log.ForContext("RsmFileName", rsmFileInfo.Name)
				.ForContext("PkkoFileName", pkkoFileInfo.Name)
				.Debug("Сохранение отчета и перенос данных в результирующую папку");
			var ms = new MemoryStream();
			resultExcelFile.Save(ms, SaveOptions.XlsxDefault);
			ms.Seek(0, SeekOrigin.Begin);
			TaskChangesDataComparingStorageManager.SaveResultFile(ms, TaskChangesDataComparingConfig.Current.GetResultFileName(task));

			TaskChangesDataComparingStorageManager.MoveComparingDataFileToResultFolder(rsmFileInfo.Name);
			TaskChangesDataComparingStorageManager.MoveComparingDataFileToResultFolder(pkkoFileInfo.Name);

			NotificationSender.SendNotification("Сравнение протоколов загрузки",
				TaskChangesDataComparingConfig.Current.GetEmailMessageForTask(task),
				messageAddresses);
		}

	}

}
