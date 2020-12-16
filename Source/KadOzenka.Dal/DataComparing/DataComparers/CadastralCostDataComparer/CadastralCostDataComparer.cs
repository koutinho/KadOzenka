using System;
using System.Linq;
using Core.Messages;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataComparing.Configs;
using KadOzenka.Dal.DataComparing.DataComparers.CadastralCostDataComparer.DataComparerStrategy;
using KadOzenka.Dal.DataComparing.Files;
using KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataComparing.DataComparers.CadastralCostDataComparer
{
	public class CadastralCostDataComparer : IDataComparer
	{
		private static readonly ILogger _log = Log.ForContext<CadastralCostDataComparer>();

		public void PerformProc(MessageAddressersDto messageAddresses)
		{
			var foldersToProcess = CadastralCostDataComparingStorageManager.GetTaskFoldersToProcess();
			_log.Debug($"Найдено {foldersToProcess.Count} папок для обработки");

			foreach (var taskFolder in foldersToProcess)
			{
				try
				{
					_log.Debug("Обработка директории {TaskFolder}", taskFolder);
					ProcessTaskFolder(taskFolder, messageAddresses);
				}
				catch (Exception ex)
				{
					_log.Error(ex, "Ошибка при обработке директории {TaskFolder}", taskFolder);
					NotificationSender.SendNotification("Сравнение протоколов кадастровой стоимости", 
						$"В ходе обработки директории {taskFolder} произошла ошибка: {ex.Message}", messageAddresses);
				}
			}

			_log.Debug("Обработка завершена");
		}

		private void ProcessTaskFolder(string taskFolder, MessageAddressersDto messageAddresses)
		{
			var task = CadastralCostDataComparingConfig.Current.GetTaskInfoFromFolderName(taskFolder);
			var compareStrategy  = GetCompareStrategy(taskFolder, task, messageAddresses);
			compareStrategy?.Compare();
		}

		private IDataComparerStrategy GetCompareStrategy(string taskFolder, OMTask task, MessageAddressersDto messageAddresses)
		{
			IDataComparerStrategy strategy = null;
			var rsmCostFiles = CadastralCostDataComparingStorageManager
				.GetTaskCostFiles(taskFolder, CadastralCostDataComparingConfig.Current.TaskRmsFolder)
				.Select(x => new CadastralCostKsFile(x, SystemType.Rsm)).ToList();
			var pkkoCostFiles = CadastralCostDataComparingStorageManager
				.GetTaskCostFiles(taskFolder, CadastralCostDataComparingConfig.Current.TaskPkkoFolder)
				.Select(x => new CadastralCostKsFile(x, SystemType.Pkko)).ToList();
			_log.ForContext("TaskFolder", taskFolder)
				.ForContext("RsmCostFiles", rsmCostFiles.Select(x => x.FullName).ToList())
				.ForContext("PkkoCostFiles", pkkoCostFiles.Select(x => x.FullName).ToList())
				.Debug("Получена информация о COST файлах");


			if (!rsmCostFiles.IsEmpty() && !pkkoCostFiles.IsEmpty())
			{
				strategy = new CostFilesDataComparerStrategy(rsmCostFiles, pkkoCostFiles, taskFolder, task, messageAddresses);
			}
			else
			{
				var rsmFdFiles = CadastralCostDataComparingStorageManager
					.GetTaskFDFiles(taskFolder, CadastralCostDataComparingConfig.Current.TaskRmsFolder)
					.Select(x => new CadastralCostFDFile(x, SystemType.Rsm)).ToList();
				var pkkoFdFiles = CadastralCostDataComparingStorageManager
					.GetTaskFDFiles(taskFolder, CadastralCostDataComparingConfig.Current.TaskPkkoFolder)
					.Select(x => new CadastralCostFDFile(x, SystemType.Pkko)).ToList();
				_log.ForContext("TaskFolder", taskFolder)
					.ForContext("RsmFDFiles", rsmFdFiles.Select(x => x.FullName).ToList())
					.ForContext("PkkoFDFiles", pkkoFdFiles.Select(x => x.FullName).ToList())
					.Debug("Получена информация о FD файлах");

				if (!rsmFdFiles.IsEmpty() && !pkkoFdFiles.IsEmpty() && task.CadastralCostComparingStatus_Code ==
					KoDataComparingStatus.ThereAreInconsistencies)
				{
					strategy = new FDFilesDataComparerStrategy(rsmFdFiles, pkkoFdFiles, taskFolder, task, messageAddresses);
				}
			}

			return strategy;
		}
	}

}
